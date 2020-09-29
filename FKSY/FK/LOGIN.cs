using Bussiness.FKSY;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using Bussiness.CRM.HG_STAFFService;
using Bussiness.CRM.CRM_LoginService;
using FKSY.Models;
using Bussiness.SSO.TOKEN_TOKENIDINFOService;

namespace FKSY.FK
{
    public partial class LOGIN : Form
    {
        MainService mainService = new MainService();
        CRMModels crmModels = new CRMModels();
        SSOModels ssoModels = new SSOModels();
        public LOGIN()
        {
            InitializeComponent();
        }

        private void btnLOGIN_Click(object sender, EventArgs e)
        {
            LOGINmain();
        }
        public delegate string LoginFun(string Section, string key);
        public LoginFun loginfun;

        private void BANDDDLMW()
        {
            DataTable dt = mainService.Select_FK_MWINFO("");
            DataRow dr = dt.NewRow();
            dr["RI"] = "0";
            dr["MWMS"] = "=请选择=";
            dt.Rows.Add(dr);
            DataTable dtCopy = dt.Copy();
            DataView dv = dt.DefaultView;
            dv.Sort = "RI";
            dtCopy = dv.ToTable();
            ddlMW.DataSource = dtCopy;
            ddlMW.ValueMember = "RI";
            ddlMW.DisplayMember = "MWMS";
        }

        private void txtUSERNAME_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtPASSWORD.Focus();
            }
        }

        private void txtPASSWORD_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (ddlMW.SelectedValue.ToString() != "0")
                {
                    LOGINmain();
                }
                else
                {
                    ddlMW.Focus();
                }
            }
        }

        private void LOGINmain()
        {
            if (txtUSERNAME.Text == "")
            {
                MessageBox.Show("请输入账户！");
                return;
            }
            if (txtPASSWORD.Text == "")
            {
                MessageBox.Show("请输入密码！");
                return;
            }
            if (ddlMW.SelectedValue.ToString() == "0")
            {
                MessageBox.Show("请选择门卫！");
                return;
            }
           

            CRM_LoginInfo tokeninfo = crmModels.CRM_Login.Login(txtUSERNAME.Text.Trim(), txtPASSWORD.Text.Trim(), "", "", 1, 0);
            if (tokeninfo.TokenInfo.access_token == null)
            {
                if (tokeninfo.TokenInfo.MSG == "E")
                {
                    MessageBox.Show(tokeninfo.TokenInfo.MESSAGE);
                    return;
                }
                else
                {
                    MessageBox.Show("用户名或密码错误！");
                    return;
                }
            }
            else
            {
                //登录成功

                SSO_TOKEN_USERNAMEDY cxmodel = new SSO_TOKEN_USERNAMEDY();
                cxmodel.STAFFID = tokeninfo.TokenInfo.STAFFID;
                cxmodel.ZHLB = 6;
                SSO_TOKEN_USERNAMEDY_SELECT data = ssoModels.TOKEN_TOKENIDINFO.USERNAMEDY_SELECT(cxmodel, tokeninfo.TokenInfo.access_token);
                if (data.MES_RETURN.TYPE == "S")
                {
                    if (data.SSO_TOKEN_USERNAMEDY.Length > 0)
                    {
                        int ISLOHIN = mainService.CheckMWQX(data.SSO_TOKEN_USERNAMEDY[0].ZHUSERID, ddlMW.SelectedValue.ToString());
                        if (ISLOHIN == 2)
                        {
                            MessageBox.Show("没有该门卫权限！");
                            return;
                        }
                        else
                        {
                            ////储存选择的机器到本地
                            //StreamWriter sw = new StreamWriter


                            WritePrivateProfileString(strSec, "USERNAME", txtUSERNAME.Text, strFilePath);
                            WritePrivateProfileString(strSec, "ddlMW", ddlMW.SelectedValue.ToString(), strFilePath);
                            
                            Appclass.token = tokeninfo.TokenInfo.access_token;
                            Appclass.STAFFID = tokeninfo.TokenInfo.STAFFID;
                            this.Hide();
                            main Main = new main(ddlMW.SelectedValue.ToString(), data.SSO_TOKEN_USERNAMEDY[0].ZHUSERNAME,loginfun = this.ContentValue);
                            Main.Show();
                            
                        }
                    }
                    else
                    {
                        MessageBox.Show("没有对应的帐号！");
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("登录失败！");
                    return;
                }

                
            }











            //int ISLOHIN = mainService.LOGIN(txtUSERNAME.Text, txtPASSWORD.Text, ddlMW.SelectedValue.ToString());
            //if (ISLOHIN == 1)
            //{
            //    MessageBox.Show("用户名或密码错误！");
            //    return;
            //}
            //else if (ISLOHIN == 2)
            //{
            //    MessageBox.Show("没有该门卫权限！");
            //    return;
            //}
            //else
            //{
            //    WritePrivateProfileString(strSec, "USERNAME", txtUSERNAME.Text, strFilePath);
            //    WritePrivateProfileString(strSec, "ddlMW", ddlMW.SelectedValue.ToString(), strFilePath);
            //    this.Hide();
            //    main Main = new main(ddlMW.SelectedValue.ToString(), txtUSERNAME.Text);
            //    Main.Show();
            //}
        }


        /// <summary>
        /// 写入INI文件
        /// </summary>
        /// <param name="section">节点名称[如[TypeName]]</param>
        /// <param name="key">键</param>
        /// <param name="val">值</param>
        /// <param name="filepath">文件路径</param>
        /// <returns></returns>
        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filepath);
        /// <summary>
        /// 读取INI文件
        /// </summary>
        /// <param name="section">节点名称</param>
        /// <param name="key">键</param>
        /// <param name="def">值</param>
        /// <param name="retval">stringbulider对象</param>
        /// <param name="size">字节大小</param>
        /// <param name="filePath">文件路径</param>
        /// <returns></returns>
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retval, int size, string filePath);
        private string strFilePath = Application.StartupPath + "\\FileConfig.ini";//获取INI文件路径
        private string strSec = ""; //INI文件名

        private string ContentValue(string Section, string key)
        {
            StringBuilder temp = new StringBuilder(1024);
            GetPrivateProfileString(Section, key, "", temp, 1024, strFilePath);
            return temp.ToString();
        }

        private void LOGIN_Load(object sender, EventArgs e)
        {
            BANDDDLMW();
            if (File.Exists(strFilePath))//读取时先要判读INI文件是否存在
            {

                strSec = Path.GetFileNameWithoutExtension(strFilePath);
                //txtName.Text = ContentValue(strSec, "Name");
                txtUSERNAME.Text = ContentValue(strSec, "USERNAME");
                string MW = ContentValue(strSec, "ddlMW");
                string machine = ContentValue(strSec, "ddlmachine");
                if (MW != "")
                {
                    ddlMW.SelectedValue = MW;
                }
                if (txtUSERNAME.Text != "")
                {
                    //txtPASSWORD.Focus();
                }
            }
            else
            {
                MessageBox.Show("INI文件不存在");
            }
        }

        private void btnCLOSE_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
    }
}
