namespace FKSY.FK
{
    partial class BFRY
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ddlDEPT = new System.Windows.Forms.ComboBox();
            this.btnFIND = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txtBFNAME = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.GVLXR = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GVLXR)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.ddlDEPT);
            this.groupBox1.Controls.Add(this.btnFIND);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtBFNAME);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(552, 100);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "查询条件";
            // 
            // ddlDEPT
            // 
            this.ddlDEPT.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlDEPT.FormattingEnabled = true;
            this.ddlDEPT.Location = new System.Drawing.Point(221, 24);
            this.ddlDEPT.Name = "ddlDEPT";
            this.ddlDEPT.Size = new System.Drawing.Size(121, 20);
            this.ddlDEPT.TabIndex = 3;
            // 
            // btnFIND
            // 
            this.btnFIND.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnFIND.Location = new System.Drawing.Point(6, 71);
            this.btnFIND.Name = "btnFIND";
            this.btnFIND.Size = new System.Drawing.Size(75, 23);
            this.btnFIND.TabIndex = 2;
            this.btnFIND.Text = "查询";
            this.btnFIND.UseVisualStyleBackColor = true;
            this.btnFIND.Click += new System.EventHandler(this.btnFIND_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label2.Location = new System.Drawing.Point(174, 27);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "部门：";
            // 
            // txtBFNAME
            // 
            this.txtBFNAME.Location = new System.Drawing.Point(53, 24);
            this.txtBFNAME.Name = "txtBFNAME";
            this.txtBFNAME.Size = new System.Drawing.Size(100, 21);
            this.txtBFNAME.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label1.Location = new System.Drawing.Point(6, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "名称：";
            // 
            // GVLXR
            // 
            this.GVLXR.AllowUserToAddRows = false;
            this.GVLXR.AllowUserToDeleteRows = false;
            this.GVLXR.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.GVLXR.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4});
            this.GVLXR.Location = new System.Drawing.Point(12, 118);
            this.GVLXR.Name = "GVLXR";
            this.GVLXR.ReadOnly = true;
            this.GVLXR.RowTemplate.Height = 23;
            this.GVLXR.Size = new System.Drawing.Size(552, 437);
            this.GVLXR.TabIndex = 1;
            this.GVLXR.DoubleClick += new System.EventHandler(this.GVLXR_DoubleClick);
            // 
            // Column1
            // 
            this.Column1.DataPropertyName = "GH";
            this.Column1.HeaderText = "工号";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Width = 60;
            // 
            // Column2
            // 
            this.Column2.DataPropertyName = "BFNAME";
            this.Column2.HeaderText = "姓名";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Width = 80;
            // 
            // Column3
            // 
            this.Column3.DataPropertyName = "SEX";
            this.Column3.HeaderText = "性别";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column3.Width = 60;
            // 
            // Column4
            // 
            this.Column4.DataPropertyName = "DEPT";
            this.Column4.HeaderText = "部门";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            // 
            // BFRY
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(576, 571);
            this.Controls.Add(this.GVLXR);
            this.Controls.Add(this.groupBox1);
            this.Name = "BFRY";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "BFRY";
            this.Load += new System.EventHandler(this.BFRY_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GVLXR)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtBFNAME;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnFIND;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox ddlDEPT;
        private System.Windows.Forms.DataGridView GVLXR;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
    }
}