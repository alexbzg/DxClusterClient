﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using AsyncConnectionNS;
using System.IO;
using System.Text.RegularExpressions;
using System.Xml.Serialization;
using System.Diagnostics;
using System.Globalization;
using DxData;


namespace DxClusterClient
{
    public partial class FMain : Form
    {

        

        internal class ModeDictElement : Mode
        {
            public Mode parent;
            public new List<ModeDictElement> subItems;
        };



        public class SettingsListEntry
        {
            public string name;
            public bool status;
        }

        public class AppSettings
        {
            public string cs;
            public string host;
            public int port;
            public string adifFP;
            public List<int> dgvDXColumnWidth = new List<int>();
            public int dxccSelect;
            public List<bool> dxccConfirm;
            public List<SettingsListEntry> dxccBand;
            public List<SettingsListEntry> dxccModes;
            public bool dgvDxFilterNoCfm;
            public bool dgvDxFilterBandsAll;
            public List<SettingsListEntry> dgvDxFilterBands;
            public Point formLocation;
            public Size formSize;
        }


        private Dictionary<string, string>[] prefixes =  new Dictionary<string, string>[2] { new Dictionary<string, string>(), new Dictionary<string, string>() };
        private Dictionary<string, string> countryCodes = new Dictionary<string, string>();
        private Regex rgxDX = new Regex(@"DX de (\S+):\s+(\d+\.\d+)\s+(\S+)\s+(.+)\s(\d\d\d\dZ)");
        private List<Diap> modes = new List<Diap>();
        private BindingList<DxItem> blDxData = new BindingList<DxItem>();
        private BindingSource bsDxData;
        private AppSettings settings = new AppSettings();
        private AsyncConnection clusterCn;
        private ADIFData adifData;
        private bool loaded = false;
        private volatile bool closed = false;
        private Object dxDataLock = new Object();
        private ToolStripButton tsbDxDataBandFilterAll;
        private bool dxDataBandFilterAllProcessing = false;
        private Dictionary<string,ToolStripMenuItem> bandsMenuItems = new Dictionary<string,ToolStripMenuItem>();
        private Dictionary<string,ToolStripMenuItem> confirmMenuItems = new Dictionary<string, ToolStripMenuItem>();
        private Dictionary<string,ToolStripMenuItem> modesMenuItems = new Dictionary<string, ToolStripMenuItem>();
        private Dictionary<string, ModeDictElement> modesDict = new Dictionary<string, ModeDictElement>();
        private Dictionary<string, ToolStripButton> dxDataBandFilterButtons = new Dictionary<string, ToolStripButton>();
        private Dictionary<string, bool> dxDataBandFilterButtonsPrevStatus = new Dictionary<string, bool>();
        private HashSet<string> lotw1 = new HashSet<string>();

        public FMain()
        {
            InitializeComponent();
            bsDxData = new BindingSource(blDxData, null);

            dgvDxData.AutoGenerateColumns = false;
            dgvDxData.DataSource = bsDxData;

            miBands.DropDown.MouseEnter += miDropDownMouseEnter;
            miBands.DropDown.MouseLeave += miDropdownMouseLeave;

            miConfirm.DropDown.MouseEnter += miDropDownMouseEnter;
            miConfirm.DropDown.MouseLeave += miDropdownMouseLeave;

            miModes.DropDown.MouseEnter += miDropDownMouseEnter;
            miModes.DropDown.MouseLeave += miDropdownMouseLeave;

            readConfig();
            if (settings.dgvDxFilterBands == null)
                settings.dgvDxFilterBands = new List<SettingsListEntry>();
            if (settings.dxccModes == null)
                settings.dxccModes = new List<SettingsListEntry>();
            tsbNoCfm.Checked = settings.dgvDxFilterNoCfm;


            if (settings.dgvDXColumnWidth != null && settings.dgvDXColumnWidth.Count == dgvDxData.Columns.Count)
                for (int c = 0; c < dgvDxData.Columns.Count; c++)
                    dgvDxData.Columns[c].Width = settings.dgvDXColumnWidth[c];
            else
            {
                settings.dgvDXColumnWidth = new List<int>();
                for (int c = 0; c < dgvDxData.Columns.Count; c++)
                    settings.dgvDXColumnWidth.Add( dgvDxData.Columns[c].Width );
                writeConfig();
            }


            Trace.Listeners.Add(new TextWriterTraceListener("DxClusterClient.log"));
            Trace.AutoFlush = true;
            Trace.Indent();
            Trace.WriteLine("Initialising FMain");

            using (StreamReader srC = new StreamReader(Application.StartupPath + "\\cty.dat"))
            {
                string country = "";
                Regex rgxCountry = new Regex(@"\s(\S+):$");
                Regex rgxPfx = new Regex(@"(\(.*\))?(\[.*\])?");
                do
                {
                    string line = srC.ReadLine();
                    Match mtchCountry = rgxCountry.Match(line);
                    if (mtchCountry.Success)
                        country = mtchCountry.Groups[1].Value;
                    else {
                        string[] pfxs = line.TrimStart(' ').TrimEnd(';', ',').Split(',');
                        foreach (string pfx in pfxs)
                        {
                            int pfxType = 0;
                            string pfx0 = rgxPfx.Replace(pfx, "");
                            if (pfx0.StartsWith("="))
                            {
                                pfx0 = pfx0.TrimStart('=');
                                pfxType = 1;
                            }
                            if (prefixes[pfxType].ContainsKey(pfx0))
                            {
                                prefixes[pfxType][pfx0] += "; " + country;
                                System.Diagnostics.Debug.WriteLine("Type " + pfxType.ToString() + " " + pfx0 + ": " + prefixes[pfxType][pfx0]);
                            }
                            else
                                prefixes[pfxType].Add(pfx0, country);
                        }
                    }
                } while (srC.Peek() >= 0);
            }

            Trace.WriteLine("Finished reading cty.dat");

            try
            {
                using (StreamReader srM = new StreamReader(Application.StartupPath + "\\bandMap.txt"))
                {
                    Regex rgxMd = new Regex(@"^(\d+\.?\d*)\s*-?(\d+\.?\d*)\s+(\S+)$");
                    do
                    {
                        string line = srM.ReadLine();
                        Match mtchMd = rgxMd.Match(line);
                        if (mtchMd.Success)
                            modes.Add(new Diap
                            {
                                l = Convert.ToDouble(mtchMd.Groups[1].Value.Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator)),
                                h = Convert.ToDouble(mtchMd.Groups[2].Value.Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator)),
                                name = mtchMd.Groups[3].Value
                            });
                    } while (srM.Peek() >= 0);

                }
            } catch ( Exception e )
            {
                Trace.WriteLine(e.ToString());
            }


            countryCodes = ADIFData.buildCountryCodes(Application.StartupPath + "\\CountryCode.txt");

            try
            {
                using (StreamReader sr = new StreamReader(Application.StartupPath + "\\lotw1.txt"))
                {
                    do
                    {
                        string line = sr.ReadLine();
                        lotw1.Add(line);
                    } while (sr.Peek() >= 0);

                }
            }
            catch (Exception e)
            {
                Trace.WriteLine(e.ToString());
            }

            if (settings.adifFP != "" && File.Exists(settings.adifFP))
                loadADIF(settings.adifFP);

            int co = 0;
            foreach ( Diap band in DxConsts.Bands )
            {
            }
            if ( settings.dxccBand == null || settings.dxccBand.Count < bandsMenuItems.Count)
            {
                settings.dxccBand = new List<SettingsListEntry>();
                foreach (ToolStripMenuItem mi in bandsMenuItems.Values)
                    settings.dxccBand.Add(new SettingsListEntry { name = mi.Text, status = mi.Checked });
            }

            co = 0;
            foreach (string ct in DxConsts.ConfirmationTypes)
            {
                ToolStripMenuItem mi = new ToolStripMenuItem();
                mi.Text = ct;
                mi.Checked = settings.dxccConfirm != null && settings.dxccConfirm.Count > co ? settings.dxccConfirm[co] : true;
                mi.CheckOnClick = true;
                mi.CheckedChanged += miFilterCheckedChanged;
                miConfirm.DropDownItems.Add(mi);
                confirmMenuItems[ct] = mi;
                co++;
            }
            if (settings.dxccConfirm == null || settings.dxccConfirm.Count < confirmMenuItems.Count)
            {
                settings.dxccConfirm = new List<bool>();
                foreach (ToolStripMenuItem mi in confirmMenuItems.Values)
                    settings.dxccConfirm.Add(mi.Checked);
            }

            co = 0;
            foreach (ToolStripMenuItem mi in new ToolStripMenuItem[] { miSelectPrefix, miSelectBand, miSelectMode })
                mi.Checked = settings.dxccSelect == co++;

            foreach (Mode mode in DxConsts.Modes)
                createModeMenuItem(mode, null);

            foreach (Diap band in DxConsts.Bands)
                createBandControls(band);

            tsbDxDataBandFilterAll = new ToolStripButton();
            tsbDxDataBandFilterAll.CheckOnClick = true;
            tsbDxDataBandFilterAll.Text = "ALL";
            tsbDxDataBandFilterAll.DisplayStyle = ToolStripItemDisplayStyle.Text;
            tsbDxDataBandFilterAll.CheckedChanged += tsbDxDataBandFilterAllChanged;
            tsFilter.Items.Add(tsbDxDataBandFilterAll);
           


        }

        private void loadADIF( string adifFP )
        {
            try
            {
                adifData = new ADIFData(adifFP, countryCodes);
            } catch (Exception e)
            {
                Trace.WriteLine("Error loading ADIF data from " + adifFP);
                Trace.WriteLine(e.ToString());
                return;
            }
            miOpenDXCC.Enabled = true;
            settings.adifFP = adifFP;
            writeConfig();
        }

        private void tsbDxDataBandFilterAllChanged(object sender, EventArgs e)
        {
            if (dxDataBandFilterAllProcessing)
                return;
            dxDataBandFilterAllProcessing = true;
            if ( tsbDxDataBandFilterAll.Checked)
            {
                dxDataBandFilterButtonsPrevStatus.Clear();
                foreach (KeyValuePair<string, ToolStripButton> kv in dxDataBandFilterButtons) {
                    if (kv.Value.Text != kv.Key)
                        continue;
                        if (kv.Value.Visible)
                        {
                            dxDataBandFilterButtonsPrevStatus[kv.Key] = kv.Value.Checked;
                            kv.Value.Checked = true;
                        }
                }
            }
            else
                foreach (KeyValuePair<string, bool> kv in dxDataBandFilterButtonsPrevStatus)
                    dxDataBandFilterButtons[kv.Key].Checked = kv.Value;
            settings.dgvDxFilterBandsAll = tsbDxDataBandFilterAll.Checked;
            dxDataBandFilterAllProcessing = false;
            dgvDxDataUpdate();
            writeConfig();
        }

        private void createBandControls(Diap band)
        {
            string name;
            if (band.group != null)
            {
                name = band.group;
                if (bandsMenuItems.ContainsKey(name))
                {
                    bandsMenuItems[band.name] = bandsMenuItems[name];
                    dxDataBandFilterButtons[band.name] = dxDataBandFilterButtons[name];
                    return;
                }
            } else
                name = band.name;

            ToolStripMenuItem mi = new ToolStripMenuItem();
            mi.Text = name;
            if (settings.dxccBand != null && settings.dxccBand.Exists(x => x.name == name))
                mi.Checked = settings.dxccBand.FirstOrDefault(x => x.name == name).status;
            else
                mi.Checked = true;
            mi.CheckOnClick = true;
            mi.CheckedChanged += miFilterCheckedChanged;
            miBands.DropDownItems.Add(mi);
            bandsMenuItems[name] = mi;

            ToolStripButton tsb = new ToolStripButton();
            if ( settings.dgvDxFilterBands != null && settings.dgvDxFilterBands.Exists(x => x.name == name))
                tsb.Checked = settings.dgvDxFilterBands.FirstOrDefault(x => x.name == name).status;
            else
                tsb.Checked = true;
            tsb.CheckOnClick = true;
            tsb.Visible = bandsMenuItems[name].Checked;
            tsb.Text = name;
            tsb.DisplayStyle = ToolStripItemDisplayStyle.Text;
            tsb.CheckedChanged += dgvDxDataFiltersChanged;
            dxDataBandFilterButtons[band.name] = tsb;
            tsFilter.Items.Add(tsb);

            if ( band.group != null )
            {
                bandsMenuItems[band.group] = mi;
                dxDataBandFilterButtons[band.group] = tsb;
            }
        }


        private void createModeMenuItem( Mode mode, Mode parent)
        {
            ToolStripMenuItem mi = new ToolStripMenuItem();
            mi.Text = mode.name.EndsWith( "_ANY" ) ? "ANY" : mode.name;
            mi.Checked = true;
            mi.Enabled = parent == null || modesMenuItems[parent.name].Checked;
            if ( settings.dxccModes.Exists( x => x.name == mode.name ) )
                mi.Checked = settings.dxccModes.FirstOrDefault( x => x.name == mode.name ).status;
            mi.CheckOnClick = true;
            mi.CheckedChanged += miFilterCheckedChanged;
            ToolStripMenuItem parentMI = parent == null ? miModes : modesMenuItems[parent.name];
            parentMI.DropDownItems.Add(mi);
            modesMenuItems[mode.name] = mi;
            if ( !mode.name.EndsWith( "_ANY" ) )
            {
                ModeDictElement mde = new ModeDictElement { name = mode.name, dxGridViewName = mode.dxGridViewName, aliases = mode.aliases };
                if (mode.subItems != null)
                    mde.subItems = new List<ModeDictElement>();
                if (parent != null)
                {
                    modesDict[parent.name].subItems.Add(mde);
                    mde.parent = modesDict[parent.name];
                }
                modesDict[mode.name] = mde;
                if ( mode.aliases != null)
                    foreach (string alias in mode.aliases)
                        modesDict[alias] = mde;
            }
            if (mode.aliases != null)
                foreach (string alias in mode.aliases)
                    modesMenuItems[alias] = mi;
            if (mode.subItems != null)
            {
                createModeMenuItem(new Mode { name = mode.name + "_ANY" }, mode);
                foreach (Mode subItem in mode.subItems)
                    createModeMenuItem(subItem, mode);
                mi.DropDown.MouseEnter += miDropDownMouseEnter;
                mi.DropDown.MouseLeave += miDropdownMouseLeave;
                mi.DropDownOpening += miDropDownOpening;
            }
        }

        private void miDropDownOpening(object sender, EventArgs e)
        {
            ToolStripMenuItem miSender = (ToolStripMenuItem)sender;
            ToolStripMenuItem parentMI = (ToolStripMenuItem)miSender.OwnerItem;
            foreach (ToolStripMenuItem mi in parentMI.DropDownItems)
                if (mi != miSender && mi.DropDown.Visible)
                    mi.DropDown.Close();
        }




        private string configFilePath()
        {
            string directory = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            return Path.Combine(directory, "R7AB", "DxClusterClient", "config.xml");
        }

        private bool readConfig()
        {
            bool result = false;
            string fp = configFilePath();
            if (File.Exists(fp) )
            {
                XmlSerializer ser = new XmlSerializer(typeof(AppSettings));
                using (FileStream fs = File.OpenRead(fp))
                {
                    try
                    {
                        settings = (AppSettings)ser.Deserialize(fs);
                    }
                    catch (Exception e)
                    {
                        System.Diagnostics.Debug.WriteLine(e.ToString());
                    }
                }
            }
            return result;
        }

        public void writeConfig()
        {
            string adDir = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            if ( !Directory.Exists( Path.Combine( adDir, "R7AB", "DxClusterClient" ) ) )
                Directory.CreateDirectory(Path.Combine(adDir, "R7AB"));
            if (!Directory.Exists(Path.Combine(adDir, "R7AB", "DxClusterClient")))
                Directory.CreateDirectory(Path.Combine(adDir, "R7AB", "DxClusterClient"));
            string fp = configFilePath();
            using (StreamWriter sw = new StreamWriter(fp))
            {
                XmlSerializer ser = new XmlSerializer(typeof(AppSettings));
                ser.Serialize(sw, settings);
            }

        }

        private void login()
        {
            using (FLogin lf = new FLogin(Application.StartupPath + "\\clusters.txt"))
            {
                lf.tbCallsign.Text = settings.cs;
                lf.tbHost.Text = settings.host;
                if (settings.port != 0)
                    lf.tbPort.Text = settings.port.ToString();
                if (lf.ShowDialog() == DialogResult.OK)
                {

                    string host = lf.tbHost.Text;
                    int port = Convert.ToInt32(lf.tbPort.Text);
                    string callsign = lf.tbCallsign.Text;
                    if (!callsign.Equals(settings.cs))
                        settings.cs = callsign;
                    if (!host.Equals(settings.host))
                        settings.host = host;
                    if (port != settings.port)
                        settings.port = port;
                    writeConfig();

                    if (host == "test")
                    {
                        using (StreamReader sr = new StreamReader(Application.StartupPath + "\\test.txt"))
                            do
                            {
                                LineReceivedEventArgs e = new LineReceivedEventArgs();
                                e.line = sr.ReadLine();
                                lineReceived(this, e);
                            } while (sr.Peek() >= 0);
                    }
                    else
                    {

                        clusterCn = new AsyncConnection();
                        clusterCn.lineReceived += lineReceived;
                        if (clusterCn.connect(host, port))
                        {
                            clusterCn.sendCommand(callsign);
                            this.Text += " connected to " + host + ":" + port.ToString();
                        }
                    }

                } 
            }

        }

        private bool _testMode( string text, string mode )
        {
            return ( text.Contains(" " + mode + " ") || text.EndsWith(" " + mode) || text.StartsWith(mode + " ") || text == mode ) ||
                ( text.Contains( " " + mode ) && Char.IsDigit( text[text.IndexOf( " " + mode ) + mode.Length + 1 ] ) ) || 
                ( text.StartsWith( mode ) && Char.IsDigit( text[ mode.Length ] ) );
        }

        private string testMode( string text, Mode mode )
        {
            if ( _testMode( text, mode.name ) )
                return mode.name;
            if (mode.aliases != null)
                foreach (string alias in mode.aliases)
                    if (_testMode(text, alias))
                        return mode.name;
            if ( mode.subItems != null )
                foreach ( Mode subItem in mode.subItems )
                {
                    string r = testMode(text, subItem);
                    if (r != "")
                        return r;
                }
            return "";
        }

        private void lineReceived( object obj, LineReceivedEventArgs ea )
        {
            string line = ea.line.TrimEnd('\r', '\n');
            this.Invoke((MethodInvoker)delegate {
                tbCluster.AppendText( line + Environment.NewLine );
            });
            string country = "";
            Match mtchDX = rgxDX.Match(line);
            if ( mtchDX.Success )
            {
                string cs = mtchDX.Groups[3].Value;
                if (prefixes[1].ContainsKey(cs))
                    country = prefixes[1][cs];
                else
                    for (int c = 1; c <= cs.Length; c++)
                        if (prefixes[0].ContainsKey(cs.Substring(0, c)))
                            country = prefixes[0][cs.Substring(0, c)];
                double freq = Convert.ToDouble(mtchDX.Groups[2].Value.Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator));
                string mode = "";
                string textU = mtchDX.Groups[4].Value.ToUpper();
                foreach ( Mode modeI in DxConsts.Modes)
                {
                    if (!modeI.isCategory)
                    {
                        mode = testMode(textU, modeI);
                        if (mode != "")
                            break;
                    }
                }
                if ( mode == "" )
                    mode = getDiap( modes, freq );
                if (mode != "" && modesDict.ContainsKey(mode) && modesDict[mode].dxGridViewName != null)
                    mode = modesDict[mode].dxGridViewName;
                string band = getDiap(DxConsts.Bands, freq);
                try
                {
                    if (!closed)
                    Invoke((MethodInvoker)delegate
                        {
                            if (!closed)
                            {
                                lock (dxDataLock)
                                {
                                    int pos = dgvDxData.FirstDisplayedScrollingRowIndex;
                                    DxItem prev = blDxData.FirstOrDefault(x => x.cs == cs && (freq - x.nFreq < 0.3 && x.nFreq - freq < 0.3));
                                    if (prev != null)
                                    {
                                        int idx = blDxData.IndexOf(prev);
                                        blDxData.RemoveAt(idx);
                                        dgvDxData.ClearSelection();
                                        dgvDxData.CurrentCell = null;
                                        for (int c = idx; c >= 0; c--)
                                            if ( c < dgvDxData.RowCount )
                                                dgvDxDrawRow(c);
                                    }
                                    blDxData.Insert(0, new DxItem
                                    {
                                        cs = cs,
                                        l = lotw1.Contains(cs) ? "+" : "",
                                        prefix = country,
                                        de = mtchDX.Groups[1].Value,
                                        freq = mtchDX.Groups[2].Value,
                                        mode = mode,
                                        band = band,
                                        text = mtchDX.Groups[4].Value,
                                        time = mtchDX.Groups[5].Value,
                                        dt = DateTime.Now,
                                        nFreq = freq
                                    });
                                    List<DxItem> old = blDxData.Where(x => x.dt < DateTime.Now.Subtract(new TimeSpan(0, 30, 0))).ToList();
                                    if (old.Count > 0)
                                        old.ForEach(x => blDxData.Remove(x));
                                    dgvDxData.ClearSelection();
                                    dgvDxData.CurrentCell = null;
                                    dgvDxDrawRow(0);
                                    int nPos = pos;
                                    if (nPos > dgvDxData.RowCount)
                                        nPos--;
                                    while (nPos > -1 && !dgvDxData.Rows[nPos].Visible)
                                        nPos--;
                                    if (nPos > -1)
                                    {
                                        dgvDxData.FirstDisplayedScrollingRowIndex = nPos;
                                        Trace.WriteLine("Scroll from " + pos.ToString() + " to " + nPos.ToString());
                                    }
                                    else
                                    {
                                        nPos = pos;
                                        if (nPos < 0)
                                            nPos = 0;
                                        while (nPos < dgvDxData.RowCount && !dgvDxData.Rows[nPos].Visible)
                                            nPos++;
                                        if (nPos < dgvDxData.RowCount)
                                        {
                                            dgvDxData.FirstDisplayedScrollingRowIndex = nPos;
                                            Trace.WriteLine("Scroll from " + pos.ToString() + " to " + nPos.ToString());
                                        }
                                        else
                                        {
                                            Trace.WriteLine("scroll canceled");
                                        }
                                    }
                                }
                            }
                        });
                } catch (Exception e )
                {
                    dxDataLock = false;
                    Debug.WriteLine(e.ToString());
                    Debug.WriteLine( "line recived: " + line );
                }

            }
        }

        public static string getDiap( List<Diap> diaps, double freq )
        {
            
            string r = "";
            foreach (Diap m in diaps)
                if (freq >= m.l && freq <= m.h)
                {
                    r = m.name;
                    if (freq < m.h)
                        break;
                }
            return r;
        }

        private void FMain_Load(object sender, EventArgs e)
        {
            if (!settings.formSize.IsEmpty)
            {
                this.DesktopBounds =
                    new Rectangle(settings.formLocation, settings.formSize);
            }
            loaded = true;
            Trace.WriteLine("FMain loaded");
            login();
            tsbDxDataBandFilterAll.Checked = settings.dgvDxFilterBandsAll;
        }

        private void bSendCmd_Click(object sender, EventArgs e)
        {
            sendCmd();
        }

        private void sendCmd()
        {
            if (!tbCmd.Text.Equals(string.Empty))
            {
                sendCmd(tbCmd.Text);
                tbCmd.Text = "";
            }
        }

        private void sendCmd( string cmd )
        {
            clusterCn.sendCommand(cmd);
        }

        private void tbCmd_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                sendCmd();
        }

        private void miOpenDXCC_Click(object sender, EventArgs e)
        {
            new FDXCC(adifData).Show();
        }

        private void miLoadADIF_Click(object sender, EventArgs e)
        {
            if (ofDialog.ShowDialog() == DialogResult.OK)
                loadADIF(ofDialog.FileName);
        }

        private void miSelectPrefix_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem miSender = (ToolStripMenuItem)sender;
            if (!miSender.Checked)
            {
                int co = 0;
                foreach (ToolStripMenuItem mi in new ToolStripMenuItem[] { miSelectPrefix, miSelectBand, miSelectMode })
                {
                    mi.Checked = mi.Equals(miSender);
                    if ( mi.Checked )
                        settings.dxccSelect = co;
                    co++;
                }
                writeConfig();
                dgvDxData.Refresh();
            }
        }

        private void miFilterCheckedChanged(object sender, EventArgs e)
        {
            ToolStripMenuItem miSender = (ToolStripMenuItem)sender;
            if ( miSender.OwnerItem == miConfirm )
            {
                string ct = confirmMenuItems.FirstOrDefault(x => x.Value == miSender).Key;
                int idx = DxConsts.ConfirmationTypes.FindIndex( x => x == ct );
                settings.dxccConfirm[idx] = miSender.Checked;
            }
            else if ( miSender.OwnerItem == miBands )
            {
                settings.dxccBand.RemoveAll(x => x.name == miSender.Text);
                settings.dxccBand.Add(new SettingsListEntry { name = miSender.Text, status = miSender.Checked });
                ToolStripButton tsb = dxDataBandFilterButtons[miSender.Text];
                tsb.Visible = miSender.Checked;
                if (!tsb.Visible)
                    tsb.Checked = false;
            }
            else if ( modesMenuItems.ContainsValue( miSender ) )
            {
                string modeName = miSender.Text;
                if (modeName == "ANY")
                    modeName = miSender.OwnerItem.Text + "_ANY";
                if ( settings.dxccModes.Exists( x => x.name == modeName ) )
                    settings.dxccModes.RemoveAll( x => x.name == modeName );
                settings.dxccModes.Add(new SettingsListEntry { name = modeName, status = miSender.Checked });
                foreach (ToolStripMenuItem mi in miSender.DropDownItems)
                    mi.Enabled = miSender.Checked;
            }
            writeConfig();
            dgvDxDataUpdate(); 
        }

        private bool[] confirmContact( DxItem dx )
        {
            bool[] r = { false, false };


            Predicate<ADIFHeader> adifFilter = new Predicate<ADIFHeader>(delegate (ADIFHeader x) { return false; });
            if (miSelectPrefix.Checked)
                adifFilter = new Predicate<ADIFHeader>(delegate (ADIFHeader x)
                {
                    return x.prefix == dx.prefix;
                });
            else if (miSelectBand.Checked)
                adifFilter = new Predicate<ADIFHeader>(delegate (ADIFHeader x)
                {
                    return x.prefix == dx.prefix && x.band == dx.band;
                });
            else
            {
                bool any = false;
                if (modesMenuItems.ContainsKey(dx.mode) && modesMenuItems[dx.mode].OwnerItem != miModes)
                {
                    ToolStripMenuItem miOwner = (ToolStripMenuItem)modesMenuItems[dx.mode].OwnerItem;
                    any = modesMenuItems[miOwner.Text + "_ANY"].Checked;
                    if (any)
                    {
                        List<string> modes = new List<string>();
                        foreach (ToolStripMenuItem mi in miOwner.DropDownItems)
                            if (!mi.Text.Contains("_ANY") && mi.Checked)
                                modes.Add(mi.Text);
                        adifFilter = new Predicate<ADIFHeader>(delegate (ADIFHeader x)
                        {
                            return x.prefix == dx.prefix && x.band == dx.band && modes.Contains(x.mode);
                        });

                    }
                }
                if (!any)
                    adifFilter = new Predicate<ADIFHeader>(delegate (ADIFHeader x)
                    {
                        return x.prefix == dx.prefix && x.band == dx.band && x.mode == dx.mode;
                    });
            }
            foreach (KeyValuePair<ADIFHeader, ADIFState> hs in adifData.Where(x => adifFilter(x.Key)))
            {
                if (!r[0])
                    r[0] = true;
                int co = 0;
                foreach (string ct in DxConsts.ConfirmationTypes)
                    if (hs.Value.confirmation[co++] && confirmMenuItems[ct].Checked)
                    {
                        r[1]= true;
                        break;
                    }
                if (r[1])
                    break;
            }

            return r;
        }

       
        private void dgvDxData_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        {
            if (loaded)
            {
                settings.dgvDXColumnWidth[e.Column.Index] = e.Column.Width;
                writeConfig();
            }
        }

        private void FMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            closed = true;
            if ( clusterCn != null && clusterCn.connected )
            {
                clusterCn.lineReceived -= lineReceived;
                sendCmd("b");
                clusterCn.disconnect();
            }
            
        }

        private void miDropdownMouseLeave(object sender, EventArgs e)
        {
            ToolStripDropDown ddown = (ToolStripDropDown)sender;
            ddown.AutoClose = true;
            bool close = true;
            foreach ( ToolStripDropDownItem mi in ddown.Items)
                if ( mi.DropDown.Visible )
                {
                    close = false;
                    break;
                }
            if (close)
                ddown.Close();
        }

        private void miDropDownMouseEnter(object sender, EventArgs e )
        {
            ((ToolStripDropDown)sender).AutoClose = false;
        }

        private void miConfirm_DropDownOpening(object sender, EventArgs e)
        {
            if ( miBands.DropDown.Visible)
                miBands.DropDown.Close();
        }

        private void miBands_DropDownOpening(object sender, EventArgs e)
        {
            if (miConfirm.DropDown.Visible)
                miConfirm.DropDown.Close();
            if (miModes.DropDown.Visible)
                miModes.DropDown.Close();
        }

        private void miModes_DropDownOpening(object sender, EventArgs e)
        {
            if (miBands.DropDown.Visible)
                miBands.DropDown.Close();
        }

        private void dgvDxDataFiltersChanged( object sender, EventArgs e )
        {
            try
            {
                ToolStripButton tsbSender = (ToolStripButton)sender;
                if (tsbSender == tsbNoCfm)
                    settings.dgvDxFilterNoCfm = tsbNoCfm.Checked;
                else
                {
                    if (dxDataBandFilterAllProcessing)
                        return;
                    if (!tsbSender.Checked && tsbDxDataBandFilterAll.Checked)
                    {
                        dxDataBandFilterAllProcessing = true;
                        settings.dgvDxFilterBands.Clear();
                        tsbDxDataBandFilterAll.Checked = false;
                        settings.dgvDxFilterBandsAll = false;
                        foreach (KeyValuePair<string, ToolStripButton> kv in dxDataBandFilterButtons)
                        {
                            kv.Value.Checked = kv.Value == tsbSender;
                            settings.dgvDxFilterBands.Add(new SettingsListEntry { name = kv.Key, status = kv.Value.Checked });
                        }
                        dxDataBandFilterAllProcessing = false;
                    }
                    else
                    {
                        dxDataBandFilterButtonsPrevStatus[tsbSender.Text] = tsbSender.Checked;
                        settings.dgvDxFilterBands.RemoveAll(x => x.name == tsbSender.Text);
                        settings.dgvDxFilterBands.Add(new SettingsListEntry { name = tsbSender.Text, status = tsbSender.Checked });
                    }
                }
                writeConfig();
                dgvDxDataUpdate();
            } catch ( Exception ex )
            {
                Trace.WriteLine(ex.ToString());
            }
        }

        private void dgvDxDataUpdate()
        {
            lock (dxDataLock)
            {
                dgvDxData.ClearSelection();
                dgvDxData.CurrentCell = null;
                for (int c = dgvDxData.RowCount - 1; c >= 0; c--)
                    dgvDxDrawRow(c);
            }
        }


        private void dgvDxDrawRow( int c )
        {
            DxItem dx = blDxData[c];
            DataGridViewRow r = dgvDxData.Rows[c];
            bool[] cc = { true, true };
            if (adifData != null && dx.prefix != "" &&
                bandsMenuItems.ContainsKey(dx.band) && bandsMenuItems[dx.band].Checked &&
                modesMenuItems.ContainsKey(dx.mode) && modesMenuItems[dx.mode].Enabled && modesMenuItems[dx.mode].Checked)
                cc = confirmContact(dx);

            if ( dxDataBandFilterButtons.ContainsKey(dx.band) )
            {
                r.Visible = dxDataBandFilterButtons[dx.band].Checked;
                if (!r.Visible)
                    return;
            }
            if (tsbNoCfm.Checked)
            {
                r.Visible = !dx.isBeacon() && !cc[1];
                if (!r.Visible)
                    return;
            }
            else
                r.Visible = true;

            int visCount = 0;
            for (int co = c + 1; co < dgvDxData.RowCount; co++)
                if (dgvDxData.Rows[co].Visible)
                    visCount++;
            bool odd = visCount % 2 == 1;
            for ( int co = 0; co < dgvDxData.ColumnCount; co++)
                r.Cells[co].Style.BackColor = odd ? Color.LightGray : Color.White;

            if (!dx.isBeacon()) { 
                if (cc[1])
                {
                    return;
                }
                else if (cc[0])
                {
                    r.Cells["prefix"].Style.BackColor = Color.SteelBlue;
                    r.Cells["prefix"].Style.ForeColor = Color.White;
                }
                else
                {
                    r.Cells["prefix"].Style.BackColor = Color.Tomato;
                    r.Cells["prefix"].Style.ForeColor = Color.White;
                }
            }
        }

        private void dgvDxDataScrollToLast()
        {
            for (int c = dgvDxData.RowCount - 1; c > 0; c--)
                if (dgvDxData.Rows[c].Visible)
                {
                    dgvDxData.FirstDisplayedScrollingRowIndex = c;
                    break;
                }
        }

        private void dgvDxData_SelectionChanged(object sender, EventArgs e)
        {            
            /*Trace.WriteLine("selection changed");
            for ( int c = 0; c < dgvDxData.SelectedCells.Count; c++)
                Trace.WriteLine(blDxData[dgvDxData.SelectedCells[c].RowIndex].cs);*/
            if (dgvDxData.SelectedCells.Count > 0)
            {
                int r = dgvDxData.SelectedCells[0].RowIndex;
                dgvDxData.ClearSelection();
                dgvDxData.CurrentCell = null;
                dgvDxDrawRow(r);
            }

        }

        private void dgvDxData_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
        }

        private void FMain_ResizeEnd(object sender, EventArgs e)
        {
            if (loaded)
            {
                System.Drawing.Rectangle bounds = this.WindowState != FormWindowState.Normal ? this.RestoreBounds : this.DesktopBounds;
                settings.formLocation = bounds.Location;
                settings.formSize = bounds.Size;
                writeConfig();
            }
        }
    }
}


