using Bussiness.FKSY;
using Bussiness.Modles;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FKSY.FK
{
    public partial class FKLK : Form
    {
        MainService mainService = new MainService();
        modleMW modlemw = new modleMW();
        MODLESTAFF modleSTAFF = new MODLESTAFF();
        public string MEG = "";
        public FKLK()
        {
            InitializeComponent();
        }

        public FKLK(modleMW modlemw, MODLESTAFF modleSTAFF)
        {
            InitializeComponent();
            this.modlemw = modlemw;
            this.modleSTAFF = modleSTAFF;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (txtSMQ.Text != "")
            {
                DataTable dt = mainService.Select_FK_FKINFO_TM(txtSMQ.Text);
                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["ISLK"].ToString() == "1" || dt.Rows[0]["ISLK"].ToString().ToUpper() == "TRUE")
                    {
                        MessageBox.Show("该访客已经离开！");
                    }
                    else
                    {
                        mainService.Update_FK_FKINFO(modlemw.RI, modlemw.MWMS, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), modleSTAFF.STAFFNAME, modleSTAFF.STAFFID, dt.Rows[0]["RI"].ToString());
                        MEG = "访客 " + dt.Rows[0]["FKNAME"].ToString() + " 离开！";
                        this.DialogResult = DialogResult.OK;
                    }
                }
                else
                {
                    MessageBox.Show("无该条码记录存在！");
                }
            }
            else
            {
                MessageBox.Show("条码框不能为空！");
            }
        }

        private void btnCLEAR_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void txtSMQ_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txtSMQ.Text != "")
                {
                    DataTable dt = mainService.Select_FK_FKINFO_TM(txtSMQ.Text);
                    if (dt.Rows.Count > 0)
                    {
                        if (dt.Rows[0]["ISLK"].ToString() == "1" || dt.Rows[0]["ISLK"].ToString().ToUpper() == "TRUE")
                        {
                            MessageBox.Show("该访客已经离开！");
                        }
                        else
                        {
                            mainService.Update_FK_FKINFO(modlemw.RI, modlemw.MWMS, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), modleSTAFF.STAFFNAME, modleSTAFF.STAFFID, dt.Rows[0]["RI"].ToString());
                            MEG = "访客 " + dt.Rows[0]["FKNAME"].ToString() + " 离开！";
                            this.DialogResult = DialogResult.OK;
                        }
                    }
                    else
                    {
                        MessageBox.Show("无该条码记录存在！");
                    }
                }
                else
                {
                    MessageBox.Show("条码框不能为空！");
                }
            }
        }
    }
}
