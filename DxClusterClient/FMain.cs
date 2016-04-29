using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AsyncConnectionNS;
using System.IO;
using System.Text.RegularExpressions;
using System.Xml.Serialization;
using System.Diagnostics;
using System.Globalization;
using System.Threading;

namespace DxClusterClient
{
    public partial class FMain : Form
    {

        public class Diap
        {
            public Double l;
            public Double h;
            public string name;
        }

        public static List<Diap> Bands = new List<Diap> {
                new Diap { name = "2190M", l = 136, h = 137 },
                new Diap { name = "630M", l = 472, h = 479 },
                new Diap { name = "560M", l = 501, h = 504 },
                new Diap { name = "160M", l = 1800, h = 2000 },
                new Diap { name = "80M", l = 3500, h = 4000 },
                new Diap { name = "60M", l = 5102, h = 5406.5 },
                new Diap { name = "40M", l = 7000, h = 7300 },
                new Diap { name = "30M", l = 10000, h = 10150 },
                new Diap { name = "20M", l = 14000, h = 14350 },
                new Diap { name = "17M", l = 18068, h = 18168 },
                new Diap { name = "15M", l = 21000, h = 21450 },
                new Diap { name = "12M", l = 24890, h = 24990 },
                new Diap { name = "10M", l = 28000, h = 29700 },
                new Diap { name = "6M", l = 50000, h = 54000 },
                new Diap { name = "4M", l = 70000, h = 71000 },
                new Diap { name = "2M", l = 144000, h = 148000 }
        };

        class DxItem
        {
            string _de;
            string _cs;
            string _prefix;
            string _freq;
            string _mode;
            string _band;
            string _text;
            string _time;

            public string cs
            {
                get { return _cs; }
                set
                {
                    _cs = value;
                }
            }

            public string de
            {
                get { return _de; }
                set
                {
                    _de = value;
                }
            }

            public string prefix
            {
                get { return _prefix; }
                set
                {
                    _prefix = value;
                }
            }

            public string freq
            {
                get { return _freq; }
                set
                {
                    _freq = value;
                }
            }

            public string mode
            {
                get { return _mode; }
                set
                {
                    _mode = value;
                }
            }

            public string band
            {
                get { return _band; }
                set
                {
                    _band = value;
                }
            }


            public string text
            {
                get { return _text; }
                set
                {
                    _text = value;
                }
            }

            public string time
            {
                get { return _time; }
                set
                {
                    _time = value;
                }
            }


        }

        public class AppSettings
        {
            public string cs;
            public string host;
            public int port;
            public string adifFP;
            public List<int> dgvDXColumnWidth = new List<int>();
        }

        public class ADIFHeader
        {
            public string prefix;
            public string band;
            public string mode;

            public override bool Equals(object other)
            {
                var otherAH = other as ADIFHeader;
                if (otherAH == null)
                    return false;
                return ( prefix == otherAH.prefix ) && ( band == otherAH.band ) && ( mode == otherAH.mode );
            }

            public override int GetHashCode()
            {
                return ( prefix + " " + band + " " + mode ).GetHashCode();
            }

        }

        public class ADIFState
        {
            public bool contact;
            public bool qsl;
            public bool lotw;
            public bool eqsl;
        }

        public class ADIFData : Dictionary<ADIFHeader, ADIFState> { }

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


        public FMain()
        {
            InitializeComponent();
            bsDxData = new BindingSource(blDxData, null);

            dgvDxData.AutoGenerateColumns = false;
            dgvDxData.DataSource = bsDxData;

            readConfig();

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
                Trace.WriteLine(e.Message);
            }

            Trace.WriteLine("Finished reading bandMap.txt");

            using (StreamReader sr = new StreamReader(Application.StartupPath + "\\CountryCode.txt"))
            {
                do
                {
                    string line = sr.ReadLine();
                    string[] parts = line.Split( ",".ToCharArray(), 3);
                    if ( !parts[1].Equals( string.Empty ) )
                        countryCodes.Add(parts[0], parts[1]);
                } while (sr.Peek() >= 0);

            }

            Trace.WriteLine("Finished reading CountryCode.csv");
            Trace.WriteLine("FMain initialized");

            if (settings.adifFP != "" && File.Exists(settings.adifFP))
                loadADIF(settings.adifFP);


        }

        private void loadADIF( string adifFP )
        {
            adifData = new ADIFData();
            bool eoh = false;
            string errorsFP = Path.GetDirectoryName(adifFP) + "\\AdifErrors.txt";
            using (StreamReader sr = new StreamReader(adifFP))
            using (StreamWriter errorsSW = new StreamWriter(errorsFP))
            {
                do
                {
                    string line = sr.ReadLine();
                    if (!eoh)
                    {
                        if (line.Contains("<EOH>"))
                            eoh = true;
                        continue;
                    }
                    if (!line.StartsWith("<"))
                        continue;
                    string mode = getADIFField(line, "MODE");
                    string dxcc = getADIFField(line, "DXCC");
                    string pfx = "";
                    if (countryCodes.ContainsKey(dxcc))
                        pfx = countryCodes[dxcc];
                    string band = getADIFField(line, "BAND");
                    if (band == "" || dxcc == "" || pfx == "")
                    {
                        errorsSW.WriteLine(line);
                        continue;
                    }
                    bool qsl = getADIFField(line, "QSL_RCVD").Equals("Y");
                    bool lotw = getADIFField(line, "LOTW_QSL_RCVD").Equals("Y");
                    bool eqsl = getADIFField(line, "EQSL_QSL_RCVD").Equals("Y"); 
                    ADIFHeader adifH = new ADIFHeader { prefix = pfx, band = band, mode = mode };
                    if (adifData.ContainsKey(adifH))
                    {
                        adifData[adifH].qsl |= qsl;
                        adifData[adifH].lotw |= lotw;
                        adifData[adifH].eqsl |= eqsl;
                    }
                    else
                        adifData[adifH] = new ADIFState { contact = true, qsl = qsl, lotw = lotw };
                } while (sr.Peek() >= 0);

            }

            miOpenDXCC.Enabled = true;
            settings.adifFP = adifFP;
            writeConfig();

        }

        public static string getADIFField( string line, string field )
        {
            int iheader = line.IndexOf("<" + field + ":");
            if (iheader < 0)
                return "";
            int ibeg = line.IndexOf(">", iheader) + 1;
            int iend = line.IndexOf(" ", ibeg);
            return line.Substring(ibeg, iend - ibeg);
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
                        System.Diagnostics.Debug.WriteLine(e.Message);
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
                Double freq = Convert.ToDouble(mtchDX.Groups[2].Value.Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator));
                string mode = getDiap( modes, freq );
                string band = getDiap(Bands, freq);
                foreach ( Diap m in modes )
                    if ( freq >= m.l && freq <= m.h  )
                    {
                        mode = m.name;
                        if ( freq < m.h )
                            break;
                    }
                try
                {
                    if (!closed)
                    Invoke((MethodInvoker)delegate
                        {
                            if (!closed)
                            {
                                blDxData.Add(new DxItem
                                {
                                    cs = cs,
                                    prefix = country,
                                    de = mtchDX.Groups[1].Value,
                                    freq = mtchDX.Groups[2].Value,
                                    mode = mode,
                                    band = band,
                                    text = mtchDX.Groups[4].Value,
                                    time = mtchDX.Groups[5].Value,
                                });
                                dgvDxData.ClearSelection();
                                dgvDxData.FirstDisplayedScrollingRowIndex = dgvDxData.RowCount - 1;
                            }
                        });
                } catch (Exception e )
                {
                    Debug.WriteLine(e.Message);
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
            loaded = true;
            Trace.WriteLine("FMain loaded");
            login();
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
                foreach (ToolStripMenuItem mi in new ToolStripMenuItem[] { miSelectPrefix, miSelectBand, miSelectMode })
                    mi.Checked = mi.Equals(miSender);
            dgvDxData.Refresh();                
        }

        private void miConfirmQSL_CheckedChanged(object sender, EventArgs e)
        {
            dgvDxData.Refresh();
        }

        private void dgvDxData_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if ( adifData != null && e.RowIndex >= 0 && 
                dgvDxData.Columns[e.ColumnIndex].DataPropertyName == "prefix") {
                DxItem record = blDxData[e.RowIndex];
                if (record.prefix == "")
                    return;
                bool contact = false;
                bool confirm = false;
                Predicate<ADIFHeader> adifFilter;
                if (miSelectPrefix.Checked)
                    adifFilter = new Predicate<ADIFHeader>(delegate (ADIFHeader x)
                   {
                       return x.prefix == record.prefix;
                   });
                else if ( miSelectBand.Checked )
                    adifFilter = new Predicate<ADIFHeader>(delegate (ADIFHeader x)
                    {
                        return x.prefix == record.prefix && x.band == record.band;
                    });
                else
                    adifFilter = new Predicate<ADIFHeader>(delegate (ADIFHeader x)
                    {
                        return x.prefix == record.prefix && x.band == record.band && x.mode == record.mode;
                    });
                foreach ( KeyValuePair<ADIFHeader,ADIFState> hs in adifData.Where( x => adifFilter( x.Key ) ))
                {
                    if ( !contact )
                        contact = true;
                    if ( (hs.Value.qsl && miConfirmQSL.Checked) || (hs.Value.eqsl && miConfirmEQSL.Checked) || (hs.Value.lotw && miConfirmLOTW.Checked) )
                    {
                        confirm = true;
                        break;
                    }
                }
                if (confirm)
                    e.CellStyle.BackColor = Color.White;
                else if (contact)
                {
                    e.CellStyle.BackColor = Color.SteelBlue;
                    e.CellStyle.ForeColor = Color.White;
                }
                else
                {
                    e.CellStyle.BackColor = Color.Tomato;
                    e.CellStyle.ForeColor = Color.White;
                }
            }
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
    }
}


