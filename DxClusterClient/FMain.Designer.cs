using System;
using System.Windows.Forms;

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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FMain));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.aDIFToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.miLoadADIF = new System.Windows.Forms.ToolStripMenuItem();
            this.miBands = new System.Windows.Forms.ToolStripMenuItem();
            this.dXCCToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.miSelect = new System.Windows.Forms.ToolStripMenuItem();
            this.miSelectPrefix = new System.Windows.Forms.ToolStripMenuItem();
            this.miSelectBand = new System.Windows.Forms.ToolStripMenuItem();
            this.miSelectMode = new System.Windows.Forms.ToolStripMenuItem();
            this.miConfirm = new System.Windows.Forms.ToolStripMenuItem();
            this.miModes = new System.Windows.Forms.ToolStripMenuItem();
            this.miOpenDXCC = new System.Windows.Forms.ToolStripMenuItem();
            this.dgvDxData = new System.Windows.Forms.DataGridView();
            this.de = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cs = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.L = new System.Windows.Forms.DataGridViewTextBoxColumn();
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
            this.tsFilter = new System.Windows.Forms.ToolStrip();
            this.tsbNoCfm = new System.Windows.Forms.ToolStripButton();
            this.ofDialog = new System.Windows.Forms.OpenFileDialog();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDxData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tsFilter.SuspendLayout();
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
            this.miLoadADIF,
            this.miBands});
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
            // miBands
            // 
            this.miBands.Name = "miBands";
            this.miBands.Size = new System.Drawing.Size(128, 22);
            this.miBands.Text = "Bands";
            this.miBands.DropDownOpening += new System.EventHandler(this.miBands_DropDownOpening);
            // 
            // dXCCToolStripMenuItem
            // 
            this.dXCCToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miSelect,
            this.miConfirm,
            this.miModes,
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
            this.miSelect.Size = new System.Drawing.Size(145, 22);
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
            this.miConfirm.Name = "miConfirm";
            this.miConfirm.Size = new System.Drawing.Size(145, 22);
            this.miConfirm.Text = "Confirmation";
            this.miConfirm.DropDownOpening += new System.EventHandler(this.miConfirm_DropDownOpening);
            // 
            // miModes
            // 
            this.miModes.Name = "miModes";
            this.miModes.Size = new System.Drawing.Size(145, 22);
            this.miModes.Text = "Modes";
            this.miModes.DropDownOpening += new System.EventHandler(this.miModes_DropDownOpening);
            // 
            // miOpenDXCC
            // 
            this.miOpenDXCC.Enabled = false;
            this.miOpenDXCC.Name = "miOpenDXCC";
            this.miOpenDXCC.Size = new System.Drawing.Size(145, 22);
            this.miOpenDXCC.Text = "Open table";
            this.miOpenDXCC.Click += new System.EventHandler(this.miOpenDXCC_Click);
            // 
            // dgvDxData
            // 
            this.dgvDxData.AccessibleRole = System.Windows.Forms.AccessibleRole.ScrollBar;
            this.dgvDxData.AllowUserToAddRows = false;
            this.dgvDxData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvDxData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDxData.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.de,
            this.cs,
            this.L,
            this.mode,
            this.band,
            this.freq,
            this.prefix,
            this.text,
            this.time});
            this.dgvDxData.Location = new System.Drawing.Point(0, 29);
            this.dgvDxData.Name = "dgvDxData";
            this.dgvDxData.ReadOnly = true;
            this.dgvDxData.Size = new System.Drawing.Size(1078, 484);
            this.dgvDxData.TabIndex = 1;
            this.dgvDxData.ColumnWidthChanged += new System.Windows.Forms.DataGridViewColumnEventHandler(this.dgvDxData_ColumnWidthChanged);
            this.dgvDxData.SelectionChanged += new System.EventHandler(this.dgvDxData_SelectionChanged);
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
            // L
            // 
            this.L.DataPropertyName = "l";
            this.L.HeaderText = "L";
            this.L.Name = "L";
            this.L.ReadOnly = true;
            this.L.Width = 20;
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
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.band.DefaultCellStyle = dataGridViewCellStyle1;
            this.band.HeaderText = "BAND";
            this.band.Name = "band";
            this.band.ReadOnly = true;
            // 
            // freq
            // 
            this.freq.DataPropertyName = "freq";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.freq.DefaultCellStyle = dataGridViewCellStyle2;
            this.freq.HeaderText = "FREQ";
            this.freq.Name = "freq";
            this.freq.ReadOnly = true;
            // 
            // prefix
            // 
            this.prefix.DataPropertyName = "prefix";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.prefix.DefaultCellStyle = dataGridViewCellStyle3;
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
            this.splitContainer1.Panel2.Controls.Add(this.tsFilter);
            this.splitContainer1.Panel2.Controls.Add(this.dgvDxData);
            this.splitContainer1.Size = new System.Drawing.Size(1078, 625);
            this.splitContainer1.SplitterDistance = 108;
            this.splitContainer1.TabIndex = 3;
            // 
            // bSendCmd
            // 
            this.bSendCmd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bSendCmd.Location = new System.Drawing.Point(953, 78);
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
            this.tbCmd.Location = new System.Drawing.Point(0, 79);
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
            this.tbCluster.Size = new System.Drawing.Size(1075, 74);
            this.tbCluster.TabIndex = 0;
            // 
            // tsFilter
            // 
            this.tsFilter.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbNoCfm});
            this.tsFilter.Location = new System.Drawing.Point(0, 0);
            this.tsFilter.Name = "tsFilter";
            this.tsFilter.Size = new System.Drawing.Size(1078, 25);
            this.tsFilter.TabIndex = 2;
            this.tsFilter.Text = "toolStrip1";
            // 
            // tsbNoCfm
            // 
            this.tsbNoCfm.CheckOnClick = true;
            this.tsbNoCfm.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbNoCfm.Image = ((System.Drawing.Image)(resources.GetObject("tsbNoCfm.Image")));
            this.tsbNoCfm.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbNoCfm.Name = "tsbNoCfm";
            this.tsbNoCfm.Size = new System.Drawing.Size(57, 22);
            this.tsbNoCfm.Text = "NO CFM";
            this.tsbNoCfm.CheckedChanged += new System.EventHandler(this.dgvDxDataFiltersChanged);
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
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tsFilter.ResumeLayout(false);
            this.tsFilter.PerformLayout();
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
        private System.Windows.Forms.ToolStripMenuItem miOpenDXCC;
        private System.Windows.Forms.OpenFileDialog ofDialog;
        private System.Windows.Forms.ToolStripMenuItem aDIFToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem miLoadADIF;
        private System.Windows.Forms.ToolStripMenuItem miBands;
        private ToolStripMenuItem miModes;
        private DataGridViewTextBoxColumn de;
        private DataGridViewTextBoxColumn cs;
        private DataGridViewTextBoxColumn L;
        private DataGridViewTextBoxColumn mode;
        private DataGridViewTextBoxColumn band;
        private DataGridViewTextBoxColumn freq;
        private DataGridViewTextBoxColumn prefix;
        private DataGridViewTextBoxColumn text;
        private DataGridViewTextBoxColumn time;
        private ToolStrip tsFilter;
        private ToolStripButton tsbNoCfm;
    }
}

