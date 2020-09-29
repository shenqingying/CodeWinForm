using Bussiness.FKSY;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Bussiness.Modles;
using System.Text.RegularExpressions;
using FKSY.Models;
using Bussiness.CRM.HG_STAFFService;
using System.Web.Security;

namespace FKSY.FK
{
    public partial class ChangePassword : Form
    {
        MainService mainService = new MainService();
        MODLESTAFF modleSTAFF = new MODLESTAFF();
        CRMModels crmModels = new CRMModels();
        
        public ChangePassword()
        {
            InitializeComponent();
        }

        public ChangePassword(MODLESTAFF modleSTAFF)
        {
            InitializeComponent();
            this.modleSTAFF = modleSTAFF;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            string token = Appclass.token;
            string pattern = @"(?=.*[a-zA-Z])(?=.*[0-9])";
            bool result = Regex.IsMatch(txtPasswordNew.Text, pattern);
            if (txtPasswordNew.Text != txtPasswordNew1.Text)
            {
                MessageBox.Show("2遍新密码请保持一致！");
                return;  
            }
            if (result == false)
            {
                MessageBox.Show("复杂度不够！");
                return;             //复杂度不够
            }
            if (txtPasswordNew.Text.Length < 8)
            {
                MessageBox.Show("长度少于8！");
                return;            //长度少于8
            }



            CRM_HG_STAFF staffmodel = crmModels.HG_STAFF.ReadBySTAFFID(Appclass.STAFFID, token);
            string oldpass = FormsAuthentication.HashPasswordForStoringInConfigFile(txtPasswordOLD.Text, "MD5").ToLower();
            if (oldpass != staffmodel.STAFFPW.ToLower())
            {
                MessageBox.Show("原密码不对！");
                return;             //原密码不对
            }
            staffmodel.STAFFPW = FormsAuthentication.HashPasswordForStoringInConfigFile(txtPasswordNew.Text, "MD5").ToLower();
            int i = crmModels.HG_STAFF.Update(staffmodel, token);


            if (i > 0)
            {
                if (MessageBox.Show("修改成功,点击重新启动程序", "消息框", MessageBoxButtons.OK) == DialogResult.OK)
                {
                    Application.Exit();
                    System.Diagnostics.Process.Start(System.Reflection.Assembly.GetExecutingAssembly().Location);
                }
            }
            else
            {
                MessageBox.Show("修改失败！");
                return;
            }
            







            //if (txtPasswordOLD.Text == "")
            //{
            //    MessageBox.Show("原密码不能为空！");
            //    return;
            //}
            //if (txtPasswordNew.Text == "" || txtPasswordNew1.Text == "")
            //{
            //    MessageBox.Show("新密码不能为空！");
            //    return;
            //}
            //if (txtPasswordNew.Text != txtPasswordNew1.Text)
            //{
            //    MessageBox.Show("2遍新密码请保持一致！");
            //    return;
            //}
            //int loginRETURN = mainService.LOGIN(modleSTAFF.STAFFUSER, txtPasswordOLD.Text, "");
            //if (loginRETURN == 1)
            //{
            //    MessageBox.Show("原密码错误！");
            //    return;
            //}
            //mainService.Update_ChangePassWord(modleSTAFF.STAFFUSER, txtPasswordNew.Text);
            //this.DialogResult = DialogResult.OK;
        }

        private void btnCLOSE_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        


    }
}
