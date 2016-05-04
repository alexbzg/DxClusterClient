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
            this.aDIFToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.miLoadADIF = new System.Windows.Forms.ToolStripMenuItem();
            this.dXCCToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.miSelect = new System.Windows.Forms.ToolStripMenuItem();
            this.miSelectPrefix = new System.Windows.Forms.ToolStripMenuItem();
            this.miSelectBand = new System.Windows.Forms.ToolStripMenuItem();
            this.miSelectMode = new System.Windows.Forms.ToolStripMenuItem();
            this.miConfirm = new System.Windows.Forms.ToolStripMenuItem();
            this.miConfirmQSL = new System.Windows.Forms.ToolStripMenuItem();
            this.miConfirmEQSL = new System.Windows.Forms.ToolStripMenuItem();
            this.miConfirmLOTW = new System.Windows.Forms.ToolStripMenuItem();
            this.miOpenDXCC = new System.Windows.Forms.ToolStripMenuItem();
            this.dgvDxData = new System.Windows.Forms.DataGridView();
            this.de = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cs = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.band = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.freq = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.prefix = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.text = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.time = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.bSendCmd = new System.Windows.Forms.Button();
            this.tbCmd = new System.Windows.Forms.TextBox();
            this.tbCluster = new System.Windows.Forms.TextBox();
            this.ofDialog = new System.Windows.Forms.OpenFileDialog();
            this.miBands = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDxData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aDIFToolStripMenuItem,
            this.dXCCToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(9, 3, 0, 3);
            this.menuStrip1.Size = new System.Drawing.Size(1078, 25);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // aDIFToolStripMenuItem
            // 
            this.aDIFToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miLoadADIF});
            this.aDIFToolStripMenuItem.Name = "aDIFToolStripMenuItem";
            this.aDIFToolStripMenuItem.Size = new System.Drawing.Size(44, 19);
            this.aDIFToolStripMenuItem.Text = "ADIF";
            // 
            // miLoadADIF
            // 
            this.miLoadADIF.Name = "miLoadADIF";
            this.miLoadADIF.Size = new System.Drawing.Size(128, 22);
            this.miLoadADIF.Text = "Load ADIF";
            this.miLoadADIF.Click += new System.EventHandler(this.miLoadADIF_Click);
            // 
            // dXCCToolStripMenuItem
            // 
            this.dXCCToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miSelect,
            this.miConfirm,
            this.miBands,
            this.miOpenDXCC});
            this.dXCCToolStripMenuItem.Name = "dXCCToolStripMenuItem";
            this.dXCCToolStripMenuItem.Size = new System.Drawing.Size(50, 19);
            this.dXCCToolStripMenuItem.Text = "DXCC";
            // 
            // miSelect
            // 
            this.miSelect.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miSelectPrefix,
            this.miSelectBand,
            this.miSelectMode});
            this.miSelect.Name = "miSelect";
            this.miSelect.Size = new System.Drawing.Size(152, 22);
            this.miSelect.Text = "Select";
            // 
            // miSelectPrefix
            // 
            this.miSelectPrefix.Checked = true;
            this.miSelectPrefix.CheckState = System.Windows.Forms.CheckState.Checked;
            this.miSelectPrefix.Name = "miSelectPrefix";
            this.miSelectPrefix.Size = new System.Drawing.Size(105, 22);
            this.miSelectPrefix.Text = "Prefix";
            this.miSelectPrefix.Click += new System.EventHandler(this.miSelectPrefix_Click);
            // 
            // miSelectBand
            // 
            this.miSelectBand.Name = "miSelectBand";
            this.miSelectBand.Size = new System.Drawing.Size(105, 22);
            this.miSelectBand.Text = "Band";
            this.miSelectBand.Click += new System.EventHandler(this.miSelectPrefix_Click);
            // 
            // miSelectMode
            // 
            this.miSelectMode.Name = "miSelectMode";
            this.miSelectMode.Size = new System.Drawing.Size(105, 22);
            this.miSelectMode.Text = "Mode";
            this.miSelectMode.Click += new System.EventHandler(this.miSelectPrefix_Click);
            // 
            // miConfirm
            // 
            this.miConfirm.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miConfirmQSL,
            this.miConfirmEQSL,
            this.miConfirmLOTW});
            this.miConfirm.Name = "miConfirm";
            this.miConfirm.Size = new System.Drawing.Size(152, 22);
            this.miConfirm.Text = "Confirmation";
            // 
            // miConfirmQSL
            // 
            this.miConfirmQSL.Checked = true;
            this.miConfirmQSL.CheckOnClick = true;
            this.miConfirmQSL.CheckState = System.Windows.Forms.CheckState.Checked;
            this.miConfirmQSL.Name = "miConfirmQSL";
            this.miConfirmQSL.Size = new System.Drawing.Size(106, 22);
            this.miConfirmQSL.Text = "Paper";
            this.miConfirmQSL.CheckedChanged += new System.EventHandler(this.miFilterCheckedChanged);
            // 
            // miConfirmEQSL
            // 
            this.miConfirmEQSL.Checked = true;
            this.miConfirmEQSL.CheckOnClick = true;
            this.miConfirmEQSL.CheckState = System.Windows.Forms.CheckState.Checked;
            this.miConfirmEQSL.Name = "miConfirmEQSL";
            this.miConfirmEQSL.Size = new System.Drawing.Size(106, 22);
            this.miConfirmEQSL.Text = "eQSL";
            this.miConfirmEQSL.CheckedChanged += new System.EventHandler(this.miFilterCheckedChanged);
            // 
            // miConfirmLOTW
            // 
            this.miConfirmLOTW.Checked = true;
            this.miConfirmLOTW.CheckOnClick = true;
            this.miConfirmLOTW.CheckState = System.Windows.Forms.CheckState.Checked;
            this.miConfirmLOTW.Name = "miConfirmLOTW";
            this.miConfirmLOTW.Size = new System.Drawing.Size(106, 22);
            this.miConfirmLOTW.Text = "LOTW";
            this.miConfirmLOTW.CheckedChanged += new System.EventHandler(this.miFilterCheckedChanged);
            // 
            // miOpenDXCC
            // 
            this.miOpenDXCC.Enabled = false;
            this.miOpenDXCC.Name = "miOpenDXCC";
            this.miOpenDXCC.Size = new System.Drawing.Size(152, 22);
            this.miOpenDXCC.Text = "Open table";
            this.miOpenDXCC.Click += new System.EventHandler(this.miOpenDXCC_Click);
            // 
            // dgvDxData
            // 
            this.dgvDxData.AccessibleRole = System.Windows.Forms.AccessibleRole.ScrollBar;
            this.dgvDxData.AllowUserToAddRows = false;
            this.dgvDxData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDxData.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.de,
            this.cs,
            this.mode,
            this.band,
            this.freq,
            this.prefix,
            this.text,
            this.time});
            this.dgvDxData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvDxData.Location = new System.Drawing.Point(0, 0);
            this.dgvDxData.Name = "dgvDxData";
            this.dgvDxData.ReadOnly = true;
            this.dgvDxData.Size = new System.Drawing.Size(1078, 309);
            this.dgvDxData.TabIndex = 1;
            this.dgvDxData.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvDxData_CellFormatting);
            this.dgvDxData.ColumnWidthChanged += new System.Windows.Forms.DataGridViewColumnEventHandler(this.dgvDxData_ColumnWidthChanged);
            // 
            // de
            // 
            this.de.DataPropertyName = "de";
            this.de.HeaderText = "SPOTTER";
            this.de.Name = "de";
            this.de.ReadOnly = true;
            // 
            // cs
            // 
            this.cs.DataPropertyName = "cs";
            this.cs.HeaderText = "CALLSIGN";
            this.cs.Name = "cs";
            this.cs.ReadOnly = true;
            // 
            // mode
            // 
            this.mode.DataPropertyName = "mode";
            this.mode.HeaderText = "MODE";
            this.mode.Name = "mode";
            this.mode.ReadOnly = true;
            // 
            // band
            // 
            this.band.DataPropertyName = "band";
            this.band.HeaderText = "BAND";
            this.band.Name = "band";
            this.band.ReadOnly = true;
            // 
            // freq
            // 
            this.freq.DataPropertyName = "freq";
            this.freq.HeaderText = "FREQ";
            this.freq.Name = "freq";
            this.freq.ReadOnly = true;
            // 
            // prefix
            // 
            this.prefix.DataPropertyName = "prefix";
            this.prefix.HeaderText = "DXCC";
            this.prefix.Name = "prefix";
            this.prefix.ReadOnly = true;
            // 
            // text
            // 
            this.text.DataPropertyName = "text";
            this.text.HeaderText = "TEXT";
            this.text.Name = "text";
            this.text.ReadOnly = true;
            this.text.Width = 300;
            // 
            // time
            // 
            this.time.DataPropertyName = "time";
            this.time.HeaderText = "TIME";
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
            this.splitContainer1.Location = new System.Drawing.Point(0, 25);
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
            this.splitContainer1.Size = new System.Drawing.Size(1078, 625);
            this.splitContainer1.SplitterDistance = 312;
            this.splitContainer1.TabIndex = 3;
            // 
            // bSendCmd
            // 
            this.bSendCmd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bSendCmd.Location = new System.Drawing.Point(953, 283);
            this.bSendCmd.Name = "bSendCmd";
            this.bSendCmd.Size = new System.Drawing.Size(123, 28);
            this.bSendCmd.TabIndex = 2;
            this.bSendCmd.Text = "Send";
            this.bSendCmd.UseVisualStyleBackColor = true;
            this.bSendCmd.Click += new System.EventHandler(this.bSendCmd_Click);
            // 
            // tbCmd
            // 
            this.tbCmd.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbCmd.Location = new System.Drawing.Point(0, 283);
            this.tbCmd.Name = "tbCmd";
            this.tbCmd.Size = new System.Drawing.Size(953, 26);
            this.tbCmd.TabIndex = 1;
            this.tbCmd.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbCmd_KeyDown);
            // 
            // tbCluster
            // 
            this.tbCluster.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbCluster.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbCluster.Location = new System.Drawing.Point(0, 3);
            this.tbCluster.Multiline = true;
            this.tbCluster.Name = "tbCluster";
            this.tbCluster.ReadOnly = true;
            this.tbCluster.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbCluster.Size = new System.Drawing.Size(1075, 278);
            this.tbCluster.TabIndex = 0;
            // 
            // miBands
            // 
            this.miBands.Name = "miBands";
            this.miBands.Size = new System.Drawing.Size(152, 22);
            this.miBands.Text = "Bands";
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
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FMain_FormClosing);
            this.Load += new System.EventHandler(this.FMain_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
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
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TextBox tbCmd;
        private System.Windows.Forms.TextBox tbCluster;
        private System.Windows.Forms.Button bSendCmd;
        private System.Windows.Forms.ToolStripMenuItem dXCCToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem miSelect;
        private System.Windows.Forms.ToolStripMenuItem miSelectPrefix;
        private System.Windows.Forms.ToolStripMenuItem miSelectBand;
        private System.Windows.Forms.ToolStripMenuItem miSelectMode;
        private System.Windows.Forms.ToolStripMenuItem miConfirm;
        private System.Windows.Forms.ToolStripMenuItem miConfirmQSL;
        private System.Windows.Forms.ToolStripMenuItem miConfirmEQSL;
        private System.Windows.Forms.ToolStripMenuItem miConfirmLOTW;
        private System.Windows.Forms.ToolStripMenuItem miOpenDXCC;
        private System.Windows.Forms.OpenFileDialog ofDialog;
        private System.Windows.Forms.DataGridViewTextBoxColumn de;
        private System.Windows.Forms.DataGridViewTextBoxColumn cs;
        private System.Windows.Forms.DataGridViewTextBoxColumn mode;
        private System.Windows.Forms.DataGridViewTextBoxColumn band;
        private System.Windows.Forms.DataGridViewTextBoxColumn freq;
        private System.Windows.Forms.DataGridViewTextBoxColumn prefix;
        private System.Windows.Forms.DataGridViewTextBoxColumn text;
        private System.Windows.Forms.DataGridViewTextBoxColumn time;
        private System.Windows.Forms.ToolStripMenuItem aDIFToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem miLoadADIF;
        private System.Windows.Forms.ToolStripMenuItem miBands;
    }
}

