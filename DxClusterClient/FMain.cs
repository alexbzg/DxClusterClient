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

namespace DxClusterClient
{
    public partial class FMain : Form
    {
        class Mode
        {
            public Double l;
            public Double h;
            public string name;
        }

        class DxItem
        {
            string _de;
            string _cs;
            string _prefix;
            string _freq;
            string _mode;
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
        }

        private Dictionary<string, string>[] prefixes =  new Dictionary<string, string>[2] { new Dictionary<string, string>(), new Dictionary<string, string>() };
        private Regex rgxDX = new Regex(@"DX de (\S+):\s+(\d+\.\d+)\s+(\S+)\s+(.+)\s(\d\d\d\dZ)");
        private List<Mode> modes = new List<Mode>();
        private BindingList<DxItem> blDxData = new BindingList<DxItem>();
        private BindingSource bsDxData;
        private AppSettings settings = new AppSettings();
        


        public FMain()
        {
            InitializeComponent();
            bsDxData = new BindingSource(blDxData, null);

            dgvDxData.AutoGenerateColumns = false;
            dgvDxData.DataSource = bsDxData;

            readConfig();

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

            using (StreamReader srM = new StreamReader(Application.StartupPath + "\\bandMap.txt"))
            {
                Regex rgxMd = new Regex(@"^(\d+\.?\d*)\s*-?(\d+\.?\d*)\s+(\S+)$");
                do
                {
                    string line = srM.ReadLine();
                    Match mtchMd = rgxMd.Match(line);
                    if (mtchMd.Success)
                        modes.Add(new Mode
                        {
                            l = Convert.ToDouble(mtchMd.Groups[1].Value.Replace( '.', ',' ) ),
                            h = Convert.ToDouble(mtchMd.Groups[2].Value.Replace( '.', ',' ) ),
                            name = mtchMd.Groups[3].Value
                        });
                } while (srM.Peek() >= 0);

            }
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

                    AsyncConnection clusterCn = new AsyncConnection();
                    clusterCn.lineReceived += lineReceived;
                    if (clusterCn.connect(host, port))
                    {
                        clusterCn.sendCommand(callsign);
                        this.Text += " connected to " + host + ":" + port.ToString();
                    }

                } else
                    Close();
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
                Double freq = Convert.ToDouble(mtchDX.Groups[2].Value.Replace('.', ','));
                string mode = "";
                foreach ( Mode m in modes )
                    if ( freq >= m.l && freq <= m.h  )
                    {
                        mode = m.name;
                        if ( freq < m.h )
                            break;
                    }
                this.Invoke((MethodInvoker)delegate {
                    blDxData.Add(new DxItem
                    {
                        cs = cs,
                        prefix = country,
                        de = mtchDX.Groups[1].Value,
                        freq = mtchDX.Groups[2].Value,
                        mode = mode,
                        text = mtchDX.Groups[4].Value,
                        time = mtchDX.Groups[5].Value,
                    });
                    dgvDxData.ClearSelection();
                    dgvDxData.FirstDisplayedScrollingRowIndex = dgvDxData.RowCount - 1;
                });

            }
        }

        private void FMain_Load(object sender, EventArgs e)
        {
            login();
        }

    }
}


