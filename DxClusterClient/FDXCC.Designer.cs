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
            this.clbModes = new System.Windows.Forms.CheckedListBox();
            this.clbConfirmation = new System.Windows.Forms.CheckedListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDXCC)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvDXCC
            // 
            this.dgvDXCC.AllowUserToAddRows = false;
            this.dgvDXCC.AllowUserToDeleteRows = false;
            this.dgvDXCC.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvDXCC.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDXCC.Location = new System.Drawing.Point(0, 0);
            this.dgvDXCC.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dgvDXCC.Name = "dgvDXCC";
            this.dgvDXCC.ReadOnly = true;
            this.dgvDXCC.Size = new System.Drawing.Size(1359, 894);
            this.dgvDXCC.TabIndex = 0;
            this.dgvDXCC.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvDXCC_CellFormatting);
            this.dgvDXCC.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.dgvDXCC_CellPainting);
            this.dgvDXCC.ColumnAdded += new System.Windows.Forms.DataGridViewColumnEventHandler(this.dgvDXCC_ColumnAdded);
            // 
            // clbModes
            // 
            this.clbModes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.clbModes.CheckOnClick = true;
            this.clbModes.FormattingEnabled = true;
            this.clbModes.Location = new System.Drawing.Point(1359, 194);
            this.clbModes.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.clbModes.Name = "clbModes";
            this.clbModes.Size = new System.Drawing.Size(228, 256);
            this.clbModes.TabIndex = 1;
            this.clbModes.SelectedIndexChanged += new System.EventHandler(this.clbModes_SelectedIndexChanged);
            // 
            // clbConfirmation
            // 
            this.clbConfirmation.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.clbConfirmation.CheckOnClick = true;
            this.clbConfirmation.FormattingEnabled = true;
            this.clbConfirmation.Items.AddRange(new object[] {
            "QSL",
            "LOTW"});
            this.clbConfirmation.Location = new System.Drawing.Point(1359, 31);
            this.clbConfirmation.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.clbConfirmation.Name = "clbConfirmation";
            this.clbConfirmation.Size = new System.Drawing.Size(228, 130);
            this.clbConfirmation.TabIndex = 2;
            this.clbConfirmation.SelectedIndexChanged += new System.EventHandler(this.clbModes_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(1366, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(99, 20);
            this.label1.TabIndex = 3;
            this.label1.Text = "Confirmation";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(1366, 169);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 20);
            this.label2.TabIndex = 4;
            this.label2.Text = "Modes";
            // 
            // FDXCC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1588, 894);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.clbConfirmation);
            this.Controls.Add(this.clbModes);
            this.Controls.Add(this.dgvDXCC);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "FDXCC";
            this.Text = "DXCC";
            ((System.ComponentModel.ISupportInitialize)(this.dgvDXCC)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvDXCC;
        private System.Windows.Forms.CheckedListBox clbModes;
        private System.Windows.Forms.CheckedListBox clbConfirmation;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}