using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DxClusterClient
{

   

    public partial class FLogin : Form
    {
        public class DxCluster
        {
            private string _cs;
            private string _host;
            private int _port;

            public string cs
            {
                get { return _cs; }
                set { _cs = value; }
            }

            public int port
            {
                get { return _port; }
                set { _port = value; }
            }

            public string host
            {
                get { return _host; }
                set { _host = value; }
            }
        }

        private BindingList<DxCluster> blDxCl = new BindingList<DxCluster>();
        private BindingSource bsDxCl;
        private bool loaded = false;

        public FLogin(string clustersFP)
        {
            InitializeComponent();
            using (StreamReader srC = new StreamReader(clustersFP))
            {
                do
                {
                    string line = srC.ReadLine();
                    string[] parts = line.Split(',', ':');
                    blDxCl.Add(new DxCluster
                    {
                        cs = parts[0],
                        host = parts[1],
                        port = Convert.ToInt32(parts[2])
                    });
                } while (srC.Peek() >= 0);
            }
            bsDxCl = new BindingSource(blDxCl, null);
            dgvDxCl.AutoGenerateColumns = false;
            dgvDxCl.DataSource = bsDxCl;
        }

        private void dgvDxCl_SelectionChanged(object sender, EventArgs e)
        {
            if (loaded && dgvDxCl.SelectedCells.Count > 0)
            {
                int idx = dgvDxCl.SelectedCells[0].RowIndex;
                tbHost.Text = blDxCl[idx].host;
                tbPort.Text = blDxCl[idx].port.ToString();
            }
        }

        private void FLogin_Load(object sender, EventArgs e)
        {
            loaded = true;
        }
    }
}
