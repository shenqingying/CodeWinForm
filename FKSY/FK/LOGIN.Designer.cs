namespace FKSY.FK
{
    partial class LOGIN
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtUSERNAME = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtPASSWORD = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.ddlMW = new System.Windows.Forms.ComboBox();
            this.btnLOGIN = new System.Windows.Forms.Button();
            this.btnCLOSE = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(110, 74);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "账号：";
            // 
            // txtUSERNAME
            // 
            this.txtUSERNAME.Location = new System.Drawing.Point(216, 71);
            this.txtUSERNAME.Name = "txtUSERNAME";
            this.txtUSERNAME.Size = new System.Drawing.Size(145, 21);
            this.txtUSERNAME.TabIndex = 1;
            this.txtUSERNAME.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtUSERNAME_KeyDown);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(110, 101);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "密码：";
            // 
            // txtPASSWORD
            // 
            this.txtPASSWORD.Location = new System.Drawing.Point(216, 98);
            this.txtPASSWORD.Name = "txtPASSWORD";
            this.txtPASSWORD.PasswordChar = '*';
            this.txtPASSWORD.Size = new System.Drawing.Size(145, 21);
            this.txtPASSWORD.TabIndex = 2;
            this.txtPASSWORD.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPASSWORD_KeyDown);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(110, 128);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "门卫：";
            // 
            // ddlMW
            // 
            this.ddlMW.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlMW.FormattingEnabled = true;
            this.ddlMW.Items.AddRange(new object[] {
            "111",
            "122",
            "133"});
            this.ddlMW.Location = new System.Drawing.Point(216, 125);
            this.ddlMW.Name = "ddlMW";
            this.ddlMW.Size = new System.Drawing.Size(145, 20);
            this.ddlMW.TabIndex = 3;
            // 
            // btnLOGIN
            // 
            this.btnLOGIN.Location = new System.Drawing.Point(112, 181);
            this.btnLOGIN.Name = "btnLOGIN";
            this.btnLOGIN.Size = new System.Drawing.Size(75, 23);
            this.btnLOGIN.TabIndex = 4;
            this.btnLOGIN.Text = "登录";
            this.btnLOGIN.UseVisualStyleBackColor = true;
            this.btnLOGIN.Click += new System.EventHandler(this.btnLOGIN_Click);
            // 
            // btnCLOSE
            // 
            this.btnCLOSE.Location = new System.Drawing.Point(261, 181);
            this.btnCLOSE.Name = "btnCLOSE";
            this.btnCLOSE.Size = new System.Drawing.Size(75, 23);
            this.btnCLOSE.TabIndex = 5;
            this.btnCLOSE.Text = "取消";
            this.btnCLOSE.UseVisualStyleBackColor = true;
            this.btnCLOSE.Click += new System.EventHandler(this.btnCLOSE_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(124, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(212, 48);
            this.label4.TabIndex = 5;
            this.label4.Text = "访客系统";
            // 
            // LOGIN
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(487, 237);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnCLOSE);
            this.Controls.Add(this.btnLOGIN);
            this.Controls.Add(this.ddlMW);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtPASSWORD);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtUSERNAME);
            this.Controls.Add(this.label1);
            this.Name = "LOGIN";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "门卫登录";
            this.Load += new System.EventHandler(this.LOGIN_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtUSERNAME;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtPASSWORD;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox ddlMW;
        private System.Windows.Forms.Button btnLOGIN;
        private System.Windows.Forms.Button btnCLOSE;
        private System.Windows.Forms.Label label4;
    }
}