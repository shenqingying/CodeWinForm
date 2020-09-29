namespace FKSY.FK
{
    partial class FKLK
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
            this.txtSMQ = new System.Windows.Forms.TextBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCLEAR = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "访客条码：";
            // 
            // txtSMQ
            // 
            this.txtSMQ.Location = new System.Drawing.Point(83, 6);
            this.txtSMQ.Name = "txtSMQ";
            this.txtSMQ.Size = new System.Drawing.Size(178, 21);
            this.txtSMQ.TabIndex = 1;
            this.txtSMQ.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSMQ_KeyDown);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(39, 35);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 2;
            this.btnOK.Text = "确认";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCLEAR
            // 
            this.btnCLEAR.Location = new System.Drawing.Point(149, 35);
            this.btnCLEAR.Name = "btnCLEAR";
            this.btnCLEAR.Size = new System.Drawing.Size(75, 23);
            this.btnCLEAR.TabIndex = 2;
            this.btnCLEAR.Text = "取消";
            this.btnCLEAR.UseVisualStyleBackColor = true;
            this.btnCLEAR.Click += new System.EventHandler(this.btnCLEAR_Click);
            // 
            // FKLK
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(273, 70);
            this.Controls.Add(this.btnCLEAR);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.txtSMQ);
            this.Controls.Add(this.label1);
            this.Name = "FKLK";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "访客离开";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtSMQ;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCLEAR;
    }
}