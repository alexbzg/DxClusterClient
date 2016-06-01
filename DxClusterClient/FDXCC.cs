using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DxData;

namespace DxClusterClient
{
    public partial class FDXCC : Form
    {
        public static string[] BandsList = new string[] { "10M", "15M" };

        public static long band2freq ( string band )
        {
            long mult = 1;
            string strFr = band;
            if (!Char.IsDigit(band, band.Length - 1)) {
                char last = band.Last();
                strFr = strFr.TrimEnd(last);
                if (last == 'M')
                    mult = (long)1000000;
                else if (last == 'G')
                    mult = (long)1000000000;
            }
            return Convert.ToInt64(strFr) * mult;
        }

        public static int cmpBands( string b0, string b1 )
        {
            if (b0 == b1) return 0;
            else return (band2freq(b0) > band2freq(b1) ? 1 : -1);
        }

        public FDXCC( ADIFData adifData )
        {
            InitializeComponent();
            List<string> countries = new List<string>();
            List<string> bands = new List<string>();
            List<string> modes = new List<string>();
            foreach (ADIFHeader ah in adifData.Keys)
            {
                if (!countries.Contains(ah.prefix))
                    countries.Add(ah.prefix);
                if (!bands.Contains(ah.band))
                    bands.Add(ah.band);
                if (!modes.Contains(ah.mode))
                    modes.Add(ah.mode);
            }
            bands.Sort(cmpBands);
            modes.Sort();
            foreach (string mode in modes)
                clbModes.Items.Add(mode, true);
            foreach (string ct in ConfirmationTypes)
                clbConfirmation.Items.Add(ct, true);
            


            DataTable dtAdif = new DataTable();
            DataColumn dcPfx = new DataColumn();
            dcPfx.DataType = Type.GetType("System.String");
            dcPfx.ColumnName = "prefix";
            dcPfx.ReadOnly = true;
            dcPfx.Caption = "Prefix";
            dcPfx.Unique = true;
            dtAdif.Columns.Add(dcPfx);

            Dictionary<string, ADIFState> data = new Dictionary<string, ADIFState>();

            foreach ( string band in bands)
            {
                DataColumn dcBand = new DataColumn();
                dcBand.DataType = data.GetType();
                dcBand.ColumnName = band;
                dcBand.ReadOnly = true;
                dcBand.Caption = band;
                dtAdif.Columns.Add(dcBand);
            }

            foreach ( string prefix in countries )
            {
                DataRow row = dtAdif.NewRow();
                row["prefix"] = prefix;
                foreach ( string band in bands )
                {
                    Dictionary<string, ADIFState> cellData = new Dictionary<string, ADIFState>();
                    foreach ( ADIFHeader item in adifData.Keys.Where( x => x.prefix == prefix && x.band == band ).ToList() )
                        cellData[item.mode] = adifData[item];
                    row[band] = cellData;
                }
                dtAdif.Rows.Add(row);
            }

            dgvDXCC.DataSource = dtAdif;

        }

        

        private void dgvDXCC_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
        }

        private void clbModes_SelectedIndexChanged(object sender, EventArgs e)
        {
            dgvDXCC.Refresh();
        }

        private void dgvDXCC_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex > 0 && e.RowIndex >= 0)
            {
                bool contact = false;
                bool confirm = false;
                Dictionary<string, ADIFState> data = (Dictionary<string, ADIFState>)e.Value;
                foreach (KeyValuePair<string, ADIFState> kv in data)
                    if (clbModes.CheckedItems.Contains(kv.Key))
                    {
                        contact |= kv.Value.contact;
                        int co = 0;
                        foreach ( string ct in ConfirmationTypes )
                            if ( kv.Value.confirmation[co++] && clbConfirmation.CheckedItems.Contains(ct))
                            {
                                confirm = true;
                                break;
                            }
                        if (confirm)
                            break;
                    }
                if (confirm)
                {
                    e.CellStyle.BackColor = Color.Red;
                    e.Value = "C";
                }
                else if (contact)
                {
                    e.CellStyle.BackColor = Color.Green;
                    e.Value = "S";
                }
                else
                    e.Value = "";
                //e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                e.FormattingApplied = true;
            }

        }

        private void dgvDXCC_ColumnAdded(object sender, DataGridViewColumnEventArgs e)
        {
            e.Column.DefaultCellStyle = new DataGridViewCellStyle(e.Column.DefaultCellStyle);
            e.Column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }
    }
}
