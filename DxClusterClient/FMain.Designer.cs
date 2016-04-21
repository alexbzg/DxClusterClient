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
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tbCluster = new System.Windows.Forms.TextBox();
            this.tbCmd = new System.Windows.Forms.TextBox();
            this.bSendCmd = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDxData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
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
            this.dgvDxData.Location = new System.Drawing.Point(0, 0);
            this.dgvDxData.Name = "dgvDxData";
            this.dgvDxData.ReadOnly = true;
            this.dgvDxData.Size = new System.Drawing.Size(1078, 309);
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
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 650);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1078, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 24);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.bSendCmd);
            this.splitContainer1.Panel1.Controls.Add(this.tbCmd);
            this.splitContainer1.Panel1.Controls.Add(this.tbCluster);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.dgvDxData);
            this.splitContainer1.Size = new System.Drawing.Size(1078, 626);
            this.splitContainer1.SplitterDistance = 313;
            this.splitContainer1.TabIndex = 3;
            // 
            // tbCluster
            // 
            this.tbCluster.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbCluster.Location = new System.Drawing.Point(0, 3);
            this.tbCluster.Multiline = true;
            this.tbCluster.Name = "tbCluster";
            this.tbCluster.ReadOnly = true;
            this.tbCluster.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbCluster.Size = new System.Drawing.Size(1075, 279);
            this.tbCluster.TabIndex = 0;
            // 
            // tbCmd
            // 
            this.tbCmd.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbCmd.Location = new System.Drawing.Point(0, 284);
            this.tbCmd.Name = "tbCmd";
            this.tbCmd.Size = new System.Drawing.Size(953, 26);
            this.tbCmd.TabIndex = 1;
            // 
            // bSendCmd
            // 
            this.bSendCmd.Location = new System.Drawing.Point(953, 283);
            this.bSendCmd.Name = "bSendCmd";
            this.bSendCmd.Size = new System.Drawing.Size(123, 28);
            this.bSendCmd.TabIndex = 2;
            this.bSendCmd.Text = "Send";
            this.bSendCmd.UseVisualStyleBackColor = true;
            // 
            // FMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1078, 672);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "FMain";
            this.Text = "DxClusterClient";
            this.Load += new System.EventHandler(this.FMain_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDxData)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
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
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TextBox tbCmd;
        private System.Windows.Forms.TextBox tbCluster;
        private System.Windows.Forms.Button bSendCmd;
    }
}

