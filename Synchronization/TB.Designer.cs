namespace Synchronization
{
    partial class fmTB
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
            this.btnTBRY = new System.Windows.Forms.Button();
            this.btnTBBM = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnTBRY
            // 
            this.btnTBRY.Location = new System.Drawing.Point(12, 12);
            this.btnTBRY.Name = "btnTBRY";
            this.btnTBRY.Size = new System.Drawing.Size(184, 23);
            this.btnTBRY.TabIndex = 0;
            this.btnTBRY.Text = "同步人员";
            this.btnTBRY.UseVisualStyleBackColor = true;
            this.btnTBRY.Click += new System.EventHandler(this.btnTBRY_Click);
            // 
            // btnTBBM
            // 
            this.btnTBBM.Location = new System.Drawing.Point(12, 41);
            this.btnTBBM.Name = "btnTBBM";
            this.btnTBBM.Size = new System.Drawing.Size(184, 23);
            this.btnTBBM.TabIndex = 1;
            this.btnTBBM.Text = "同步部门";
            this.btnTBBM.UseVisualStyleBackColor = true;
            this.btnTBBM.Click += new System.EventHandler(this.btnTBBM_Click);
            // 
            // fmTB
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(208, 341);
            this.Controls.Add(this.btnTBBM);
            this.Controls.Add(this.btnTBRY);
            this.Name = "fmTB";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "同步";
            this.Load += new System.EventHandler(this.fmTB_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnTBRY;
        private System.Windows.Forms.Button btnTBBM;
    }
}