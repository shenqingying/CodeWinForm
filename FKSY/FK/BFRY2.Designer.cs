namespace FKSY.FK
{
    partial class BFRY2
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
            this.tvDEPT = new System.Windows.Forms.TreeView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnCLEAR = new System.Windows.Forms.Button();
            this.btnFIND = new System.Windows.Forms.Button();
            this.ddlDEPT = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtBFNAME = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtGH = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.GVLXR = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.公司名称 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GVLXR)).BeginInit();
            this.SuspendLayout();
            // 
            // tvDEPT
            // 
            this.tvDEPT.Location = new System.Drawing.Point(12, 118);
            this.tvDEPT.Name = "tvDEPT";
            this.tvDEPT.Size = new System.Drawing.Size(228, 581);
            this.tvDEPT.TabIndex = 0;
            this.tvDEPT.DoubleClick += new System.EventHandler(this.tvDEPT_DoubleClick);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnCLEAR);
            this.groupBox1.Controls.Add(this.btnFIND);
            this.groupBox1.Controls.Add(this.ddlDEPT);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtBFNAME);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtGH);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(683, 100);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "查询条件";
            // 
            // btnCLEAR
            // 
            this.btnCLEAR.Location = new System.Drawing.Point(87, 71);
            this.btnCLEAR.Name = "btnCLEAR";
            this.btnCLEAR.Size = new System.Drawing.Size(75, 23);
            this.btnCLEAR.TabIndex = 6;
            this.btnCLEAR.Text = "清空";
            this.btnCLEAR.UseVisualStyleBackColor = true;
            this.btnCLEAR.Click += new System.EventHandler(this.btnCLEAR_Click);
            // 
            // btnFIND
            // 
            this.btnFIND.Location = new System.Drawing.Point(6, 71);
            this.btnFIND.Name = "btnFIND";
            this.btnFIND.Size = new System.Drawing.Size(75, 23);
            this.btnFIND.TabIndex = 6;
            this.btnFIND.Text = "查询";
            this.btnFIND.UseVisualStyleBackColor = true;
            this.btnFIND.Click += new System.EventHandler(this.btnFIND_Click);
            // 
            // ddlDEPT
            // 
            this.ddlDEPT.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlDEPT.FormattingEnabled = true;
            this.ddlDEPT.Location = new System.Drawing.Point(359, 25);
            this.ddlDEPT.Name = "ddlDEPT";
            this.ddlDEPT.Size = new System.Drawing.Size(121, 20);
            this.ddlDEPT.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label3.Location = new System.Drawing.Point(312, 28);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "部门：";
            // 
            // txtBFNAME
            // 
            this.txtBFNAME.Location = new System.Drawing.Point(206, 25);
            this.txtBFNAME.Name = "txtBFNAME";
            this.txtBFNAME.Size = new System.Drawing.Size(100, 21);
            this.txtBFNAME.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(159, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "姓名：";
            // 
            // txtGH
            // 
            this.txtGH.Location = new System.Drawing.Point(53, 25);
            this.txtGH.Name = "txtGH";
            this.txtGH.Size = new System.Drawing.Size(100, 21);
            this.txtGH.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "工号：";
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
            this.Column4,
            this.公司名称});
            this.GVLXR.Location = new System.Drawing.Point(246, 118);
            this.GVLXR.Name = "GVLXR";
            this.GVLXR.ReadOnly = true;
            this.GVLXR.RowTemplate.Height = 23;
            this.GVLXR.Size = new System.Drawing.Size(652, 581);
            this.GVLXR.TabIndex = 2;
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
            // 公司名称
            // 
            this.公司名称.DataPropertyName = "GSMC";
            this.公司名称.HeaderText = "公司名称";
            this.公司名称.Name = "公司名称";
            this.公司名称.ReadOnly = true;
            this.公司名称.Width = 200;
            // 
            // BFRY2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(910, 711);
            this.Controls.Add(this.GVLXR);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.tvDEPT);
            this.Name = "BFRY2";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "BFRY2";
            this.Load += new System.EventHandler(this.BFRY2_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GVLXR)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView tvDEPT;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView GVLXR;
        private System.Windows.Forms.TextBox txtBFNAME;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtGH;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox ddlDEPT;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnCLEAR;
        private System.Windows.Forms.Button btnFIND;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn 公司名称;
    }
}