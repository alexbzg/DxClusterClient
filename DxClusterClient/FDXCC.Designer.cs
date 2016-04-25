namespace DxClusterClient
{
    partial class FDXCC
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dgvDXCC = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDXCC)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvDXCC
            // 
            this.dgvDXCC.AllowUserToAddRows = false;
            this.dgvDXCC.AllowUserToDeleteRows = false;
            this.dgvDXCC.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDXCC.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvDXCC.Location = new System.Drawing.Point(0, 0);
            this.dgvDXCC.Name = "dgvDXCC";
            this.dgvDXCC.ReadOnly = true;
            this.dgvDXCC.Size = new System.Drawing.Size(784, 501);
            this.dgvDXCC.TabIndex = 0;
            this.dgvDXCC.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.dgvDXCC_CellPainting);
            // 
            // FDXCC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 501);
            this.Controls.Add(this.dgvDXCC);
            this.Name = "FDXCC";
            this.Text = "DXCC";
            ((System.ComponentModel.ISupportInitialize)(this.dgvDXCC)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvDXCC;
    }
}