using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DxData
{
    public class Diap
    {
        public Double l;
        public Double h;
        public string name;
    }

    public class Mode
    {
        public string name;
        public string dxGridViewName;
        public List<string> aliases;
        public List<Mode> subItems;
    }


    public class DxConsts
    {
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
                    new Diap { name = "2M", l = 144000, h = 148000 },
                    new Diap { name = "70cm", l = 420000, h = 450000 },
                    new Diap { name = "33cm", l = 902000, h = 928000 },
                    new Diap { name = "23cm", l = 1240000, h = 1300000 },
                    new Diap { name = "13cm", l = 2300000, h = 2450000 },
                    new Diap { name = "9cm", l = 3300000, h = 3500000 },
                    new Diap { name = "6cm", l = 5650000, h = 5925000 },
                    new Diap { name = "3cm", l = 10000000, h = 10500000 },
                    new Diap { name = "1.25cm", l = 24000000, h = 24250000 },
                    new Diap { name = "6mm", l = 47000000, h = 47200000 },
                    new Diap { name = "4mm", l = 75500000, h = 81000000 },
                    new Diap { name = "2.5mm", l = 119980000, h = 120020000 },
                    new Diap { name = "2mm", l = 142000000, h = 149000000 },
                    new Diap { name = "1mm", l = 241000000, h = 250000000 }
            };


        public static List<Mode> Modes = new List<Mode> {
                new Mode { name = "CW" },
                new Mode { name = "FONE",
                    aliases = new List<string> { "USB", "LSB", "FM", "SSB" },
                    dxGridViewName = "SSB"
                },
                new Mode { name = "DIGI",
                    subItems = new List<Mode>
                    {
                        new Mode { name = "RTTY" },
                        new Mode { name = "PSK" },
                        new Mode { name = "JT65" },
                        new Mode { name = "FSK" },
                        new Mode { name = "OLIVIA" },
                        new Mode { name = "SSTV" }
                    }
                }
            };

        public static List<string> ConfirmationTypes = new List<string> { "Paper", "eQSL", "LOTW" };

    }

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
        string _l;
        public DateTime dt;
        public double nFreq;

        public bool isBeacon()
        {
            string text = _text.ToLower();
            return (text.Contains("ncdxf") || text.Contains("beacon") || text.Contains("bcn") || _cs.ToLower().EndsWith(@"/b") || _mode == "BCN");
        }

        public string l
        {
            get { return _l; }
            set { _l = value; }
        }

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
            return (prefix == otherAH.prefix) && (band == otherAH.band) && (mode == otherAH.mode);
        }

        public override int GetHashCode()
        {
            return (prefix + " " + band + " " + mode).GetHashCode();
        }

    }

    public class ADIFState
    {
        public bool contact;
        public List<bool> confirmation = new List<bool>();

        public ADIFState()
        {
            contact = true;
            foreach (string ct in DxConsts.ConfirmationTypes)
                confirmation.Add(false);
        }
    }

    public class ADIFData : Dictionary<ADIFHeader, ADIFState> {
        public static Dictionary<string, string> buildCountryCodes( string ccFP )
        {
            Dictionary<string, string> countryCodes = new Dictionary<string, string>();

            using (StreamReader sr = new StreamReader(ccFP))
            {
                do
                {
                    string line = sr.ReadLine();
                    string[] parts = line.Split(",".ToCharArray(), 3);
                    if (!parts[1].Equals(string.Empty))
                        countryCodes.Add(parts[0], parts[1]);
                } while (sr.Peek() >= 0);

            }

            return countryCodes;
        }

        public ADIFData(string adifFP, Dictionary<string, string> countryCodes) : base()
        {
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
                    if (pfx.Contains("#"))
                    {
                        string cs = getADIFField(line, "CALL");
                        pfx = cs.Substring(0, pfx.Length);
                    }
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
                    if (!this.ContainsKey(adifH))
                        this[adifH] = new ADIFState();
                    this[adifH].confirmation[0] |= qsl;
                    this[adifH].confirmation[1] |= eqsl;
                    this[adifH].confirmation[2] |= lotw;
                } while (sr.Peek() >= 0);

            }


        }

        public static string getADIFField(string line, string field)
        {
            int iheader = line.IndexOf("<" + field + ":");
            if (iheader < 0)
                return "";
            int ibeg = line.IndexOf(">", iheader) + 1;
            int iend = line.IndexOfAny(" <".ToCharArray(), ibeg);
            if (iend < 0)
                iend = line.Length - 1;
            return line.Substring(ibeg, iend - ibeg);
        }

    }


}
