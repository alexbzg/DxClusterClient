using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static DxClusterClient.FMain;

namespace DxClusterClient
{
    public partial class FDXCC : Form
    {
        public static string[] BandsList = new string[] { "10M", "15M" };

        public FDXCC( ADIFData adifData )
        {
            InitializeComponent();
            List<string> countries = new List<string>();
            foreach (ADIFHeader ah in adifData.Keys)
                if (!countries.Contains(ah.prefix))
                    countries.Add(ah.prefix);


            DataTable dtAdif = new DataTable();
            DataColumn dcPfx = new DataColumn();
            dcPfx.DataType = Type.GetType("System.String");
            dcPfx.ColumnName = "prefix";
            dcPfx.ReadOnly = true;
            dcPfx.Caption = "Prefix";
            dcPfx.Unique = true;
            dtAdif.Columns.Add(dcPfx);

            Dictionary<string, ADIFState> data = new Dictionary<string, ADIFState>();

            foreach ( string band in BandsList)
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
                foreach ( string band in BandsList )
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
            if (e.ColumnIndex > 0 && e.RowIndex >= 0)
            {
                bool contact = false;
                bool confirm = false;
                Dictionary<string, ADIFState> data = (Dictionary < string, ADIFState>) e.Value;
                foreach ( KeyValuePair<string,ADIFState> kv in data)
                {
                    contact |= kv.Value.contact;
                    confirm |= kv.Value.lotw || kv.Value.qsl;
                    if (confirm)
                        break;
                }
                if (confirm)
                    e.CellStyle.BackColor = Color.Red;
                else if (contact)
                    e.CellStyle.BackColor = Color.Green;
                e.CellStyle.ForeColor = e.CellStyle.BackColor;
                e.CellStyle.SelectionForeColor = e.CellStyle.SelectionBackColor;
            }
        }
    }
}
