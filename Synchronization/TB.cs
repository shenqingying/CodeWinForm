using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Bussiness.Synchronization;
//using Microsoft.International.Converters.PinYinConverter;


namespace Synchronization
{
    public partial class fmTB : Form
    {
        Synchronization_main synchronization_Main = new Synchronization_main();
        public fmTB()
        {
            InitializeComponent();
        }

        private void btnTBRY_Click(object sender, EventArgs e)
        {
            SynchronizationRY();
        }

        private void btnTBBM_Click(object sender, EventArgs e)
        {

        }

        private void SynchronizationRY()
        {
            DataTable dt = synchronization_Main.Select_Lala_Employee();
            if (dt.Rows.Count > 0)
            {
                synchronization_Main.DELETE_FK_LXRINFO();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string BFNAMEPY = "";
                    string BFNAME = dt.Rows[i]["姓名"].ToString().Trim();
                    if (BFNAME.Length > 0)
                    {
                        char[] ch = BFNAME.ToCharArray();
                        for (int j = 0; j < ch.Length; j++)
                        {
                            //ChineseChar chn = new ChineseChar(ch[j]);
                            //BFNAMEPY = BFNAMEPY + chn.Pinyins[0].Substring(0, 1);
                        }
                    }
                    synchronization_Main.Insert_FK_LXRINFO(dt.Rows[i]["工号"].ToString(), dt.Rows[i]["姓名"].ToString(), BFNAMEPY, dt.Rows[i]["性别"].ToString(), dt.Rows[i]["归属部门"].ToString(), dt.Rows[i]["归属编码"].ToString(), dt.Rows[i]["联系电话"].ToString(), dt.Rows[i]["在职状态"].ToString());
                }
            }
        }

        private void fmTB_Load(object sender, EventArgs e)
        {

        }
    }
}
