using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Bussiness.FKSY;

namespace FKSY.FK
{
    public partial class BFRY2 : Form
    {
        public string GSMC = "";
        public string DEPT = "";
        public string BFNAME = "";
        public string GH = "";
        MainService mainService = new MainService();
        FK_ServiceLXR serviceLXR = new FK_ServiceLXR();
        private string GSDM = "";
        public BFRY2()
        {
            InitializeComponent();
            this.GSDM = "1";
        }

        public BFRY2(string GSDM)
        {
            InitializeComponent();
            this.GSDM = GSDM;
        }

        private void BFRY2_Load(object sender, EventArgs e)
        {
            BindTreeView();
            GVLXR.AutoGenerateColumns = false;
            BANDddlDEPT();
        }

        private void BindTreeView()
        {
            DataTable dt = serviceLXR.Select_FK_GSDMQX(GSDM);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    TreeNode father = new TreeNode();
                    father.Name = dt.Rows[i]["GSDEPT"].ToString();
                    father.Text = dt.Rows[i]["GSDEPTMS"].ToString();
                    tvDEPT.Nodes.Add(father);
                    DataTable dtChild = serviceLXR.Select_FK_DEPT(dt.Rows[i]["GSDEPT"].ToString());
                    if (dtChild.Rows.Count > 0)
                    {
                        for (int j = 0; j < dtChild.Rows.Count; j++)
                        {
                            TreeNode child = new TreeNode();
                            if (dtChild.Rows[j]["JGBM"].ToString().Length == 10)
                            {
                                child.Name = dtChild.Rows[j]["JGBM"].ToString();
                                child.Text = dtChild.Rows[j]["MC"].ToString();
                                father.Nodes.Add(child);
                                DataTable dtChild2 = serviceLXR.Select_FK_DEPT(dtChild.Rows[j]["JGBM"].ToString());
                                if (dtChild2.Rows.Count > 0)
                                {
                                    for (int k = 0; k < dtChild2.Rows.Count; k++)
                                    {
                                        TreeNode child2 = new TreeNode();
                                        if (dtChild2.Rows[k]["JGBM"].ToString().Length == 13)
                                        {
                                            child2.Name = dtChild2.Rows[k]["JGBM"].ToString();
                                            child2.Text = dtChild2.Rows[k]["MC"].ToString();
                                            child.Nodes.Add(child2);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private void tvDEPT_Click(object sender, EventArgs e)
        {
            string BMBM = tvDEPT.SelectedNode.Name;
            BANDGVLXR(BMBM);
        }

        private void BANDGVLXR(string BMBM)
        {
            DataTable dtISTBRY = mainService.Select_FK_BL("ISTBRY");
            if (dtISTBRY.Rows.Count > 0)
            {
                if (dtISTBRY.Rows[0]["BL"].ToString().ToUpper() == "TRUE")
                {
                    MessageBox.Show("正在更新数据库，请稍等！");
                    return;
                }
            }
            else
            {
                MessageBox.Show("查询变量出错，请联系管理员！");
                return;
            }
            DataTable dt = serviceLXR.Select_FK_LXRINFO(BMBM);
            GVLXR.DataSource = dt;
        }

        private void tvDEPT_DoubleClick(object sender, EventArgs e)
        {
            string BMBM = tvDEPT.SelectedNode.Name;
            BANDGVLXR(BMBM);
        }

        private void btnFIND_Click(object sender, EventArgs e)
        {
            DataTable dtISTBRY = mainService.Select_FK_BL("ISTBRY");
            if (dtISTBRY.Rows.Count > 0)
            {
                if (dtISTBRY.Rows[0]["BL"].ToString().ToUpper() == "TRUE")
                {
                    MessageBox.Show("正在更新数据库，请稍等！");
                    return;
                }
            }
            else
            {
                MessageBox.Show("查询变量出错，请联系管理员！");
                return;
            }
            DataTable dt = mainService.Select_FK_LXRINFO(txtGH.Text, txtBFNAME.Text.Trim(), ddlDEPT.SelectedValue.ToString());
            GVLXR.DataSource = dt;
        }

        private void btnCLEAR_Click(object sender, EventArgs e)
        {
            txtGH.Text = "";
            txtBFNAME.Text = "";
            ddlDEPT.SelectedValue = "A";
        }


        private void BANDddlDEPT()
        {
            DataTable dt = mainService.Select_FK_DEPT();
            DataRow dr = dt.NewRow();
            dr["JGBM"] = "A";
            dr["MC"] = "=请选择=";
            dt.Rows.Add(dr);
            DataTable dtCopy = dt.Copy();
            DataView dv = dt.DefaultView;
            dv.Sort = "JGBM";
            dtCopy = dv.ToTable();
            ddlDEPT.DataSource = dtCopy;
            ddlDEPT.ValueMember = "JGBM";
            ddlDEPT.DisplayMember = "MC";
        }

        private void GVLXR_DoubleClick(object sender, EventArgs e)
        {
            GSMC = GVLXR.CurrentRow.Cells[4].Value.ToString();
            DEPT = GVLXR.CurrentRow.Cells[3].Value.ToString();
            BFNAME = GVLXR.CurrentRow.Cells[1].Value.ToString();
            GH = GVLXR.CurrentRow.Cells[0].Value.ToString();
            this.DialogResult = DialogResult.OK;
        }
    }
}
