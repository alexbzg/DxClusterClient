namespace DxClusterClient
{
    partial class FMain
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.dgvDxData = new System.Windows.Forms.DataGridView();
            this.de = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.freq = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cs = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.prefix = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.text = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.time = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDxData)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(9, 3, 0, 3);
            this.menuStrip1.Size = new System.Drawing.Size(1078, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // dgvDxData
            // 
            this.dgvDxData.AllowUserToAddRows = false;
            this.dgvDxData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDxData.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.de,
            this.freq,
            this.mode,
            this.cs,
            this.prefix,
            this.text,
            this.time});
            this.dgvDxData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvDxData.Location = new System.Drawing.Point(0, 24);
            this.dgvDxData.Name = "dgvDxData";
            this.dgvDxData.ReadOnly = true;
            this.dgvDxData.Size = new System.Drawing.Size(1078, 648);
            this.dgvDxData.TabIndex = 1;
            // 
            // de
            // 
            this.de.DataPropertyName = "de";
            this.de.HeaderText = "de";
            this.de.Name = "de";
            this.de.ReadOnly = true;
            // 
            // freq
            // 
            this.freq.DataPropertyName = "freq";
            this.freq.HeaderText = "freq";
            this.freq.Name = "freq";
            this.freq.ReadOnly = true;
            // 
            // mode
            // 
            this.mode.DataPropertyName = "mode";
            this.mode.HeaderText = "mode";
            this.mode.Name = "mode";
            this.mode.ReadOnly = true;
            // 
            // cs
            // 
            this.cs.DataPropertyName = "cs";
            this.cs.HeaderText = "cs";
            this.cs.Name = "cs";
            this.cs.ReadOnly = true;
            // 
            // prefix
            // 
            this.prefix.DataPropertyName = "prefix";
            this.prefix.HeaderText = "prefix";
            this.prefix.Name = "prefix";
            this.prefix.ReadOnly = true;
            // 
            // text
            // 
            this.text.DataPropertyName = "text";
            this.text.HeaderText = "text";
            this.text.Name = "text";
            this.text.ReadOnly = true;
            this.text.Width = 300;
            // 
            // time
            // 
            this.time.DataPropertyName = "time";
            this.time.HeaderText = "time";
            this.time.Name = "time";
            this.time.ReadOnly = true;
            // 
            // FMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1078, 672);
            this.Controls.Add(this.dgvDxData);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "FMain";
            this.Text = "DxClusterClient";
            this.Load += new System.EventHandler(this.FMain_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDxData)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.DataGridView dgvDxData;
        private System.Windows.Forms.DataGridViewTextBoxColumn de;
        private System.Windows.Forms.DataGridViewTextBoxColumn freq;
        private System.Windows.Forms.DataGridViewTextBoxColumn mode;
        private System.Windows.Forms.DataGridViewTextBoxColumn cs;
        private System.Windows.Forms.DataGridViewTextBoxColumn prefix;
        private System.Windows.Forms.DataGridViewTextBoxColumn text;
        private System.Windows.Forms.DataGridViewTextBoxColumn time;
    }
}

