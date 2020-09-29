using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bussiness.STSY;

namespace STSY.CKGL
{
    public partial class CKXX : System.Web.UI.Page
    {
        public bool ISXZLB = false;
        public bool ISXZKW = false;
        CKService ckService = new CKService();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LOGINTIMEOUT();
                BANDLOAD();
            }
        }

        private void BANDDDLCK()
        {
            DataTable dt = ckService.GETCKINFO();
            ddlKH.DataSource = dt;
            ddlKH.DataTextField = "StockSN";
            ddlKH.DataValueField = "StockID";
            ddlKH.DataBind();
            ListItem item = new ListItem();
            item.Text = "=请选择=";
            item.Value = "0";
            ddlKH.Items.Insert(0, item);
        }

        private void BANDLOAD()
        {
            TreeView1.Nodes.Clear();
            DataTable dt = ckService.GETCKINFO();
            BANDTREEFATHER(dt);
            divBT.Visible = false;
            divLB.Visible = false;
            divKW.Visible = false;
            BANDDDLCK();
        }

        private void BANDTREEFATHER(DataTable dt)
        {
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    TreeNode father = new TreeNode();
                    string fathertext = dt.Rows[i]["StockSN"].ToString();
                    int length = fathertext.Length;
                    for (int j = 0; j < 12 - length; j++)
                    {
                        fathertext = fathertext + "&nbsp;";
                    }
                    fathertext = fathertext + dt.Rows[i]["StockName"].ToString();
                    //father.Text = dt.Rows[i]["StockSN"].ToString() + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + dt.Rows[i]["StockName"].ToString();
                    father.Text = fathertext;
                    father.Value = "0," + dt.Rows[i]["StockID"].ToString();
                    this.TreeView1.Nodes.Add(father);
                    DataTable dtChild = ckService.GETKWINFO(dt.Rows[i]["StockID"].ToString());
                    if (dtChild.Rows.Count > 0)
                    {
                        for (int j = 0; j < dtChild.Rows.Count; j++)
                        {
                            TreeNode child = new TreeNode();
                            //child.Text = dtChild.Rows[j]["PlaceSN"].ToString() + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + dtChild.Rows[j]["mtName"].ToString();
                            string childtext = dtChild.Rows[j]["PlaceSN"].ToString();
                            int length1 = childtext.Length;
                            for (int k = 0; k < 12 - length; k++)
                            {
                                childtext = childtext + "&nbsp;";
                            }
                            childtext = childtext + dtChild.Rows[j]["PlaceName"].ToString();
                            child.Text = childtext;
                            child.Value = "1," + dtChild.Rows[j]["PlaceID"].ToString();
                            father.ChildNodes.Add(child);
                        }
                    }
                }
            }
        }
        protected void btnSX_Click(object sender, EventArgs e)
        {
            BANDLOAD();
            ISXZLB = false;
            ISXZKW = false;
        }

        protected void btnZJCK_Click(object sender, EventArgs e)
        {
            CLEARTEXT();
            divBT.Visible = true;
            divLB.Visible = true;
            divKW.Visible = false;
            ISXZLB = true;
            ISXZKW = false;
        }

        protected void btnSCCK_Click(object sender, EventArgs e)
        {
            string[] DJ = TreeView1.SelectedNode.Value.Split(',');
            if (DJ.Length == 2)
            {
                if (DJ[0] != "0")
                {
                    this.Page.RegisterStartupScript("onclick", "<script>alert('请选中要删除的仓库！')</script>");
                    return;
                }
                //wlService.DELETELB(DJ[1]);
                ckService.DELETECK(DJ[1]);
                this.Page.RegisterStartupScript("onclick", "<script>alert('删除成功！')</script>");
                BANDLOAD();
            }
            else
            {
                this.Page.RegisterStartupScript("onclick", "<script>alert('数据异常，请刷新！')</script>");
                return;
            }
        }

        protected void btnZJKW_Click(object sender, EventArgs e)
        {
            CLEARTEXT();
            divBT.Visible = true;
            divLB.Visible = false;
            divKW.Visible = true;
            ISXZLB = false;
            ISXZKW = true;
        }

        protected void btnSCKW_Click(object sender, EventArgs e)
        {

        }

        protected void btnSAVE_Click(object sender, EventArgs e)
        {

        }

        protected void btnQX_Click(object sender, EventArgs e)
        {
            BANDLOAD();
            CLEARTEXT();
        }

        protected void TreeView1_SelectedNodeChanged(object sender, EventArgs e)
        {
            CLEARTEXT();
            ISXZLB = false;
            ISXZKW = false;
            TreeView1.SelectedNodeStyle.BackColor = System.Drawing.Color.BurlyWood;
            string[] JD = TreeView1.SelectedNode.Value.Split(',');
            if (JD.Length == 2)
            {
                if (JD[0] == "0")
                {
                    divBT.Visible = true;
                    divLB.Visible = true;
                    divKW.Visible = false;
                    string StockID = JD[1];
                    DataTable dt = ckService.GETCKINFO(StockID);
                    txtCKBH.Text = dt.Rows[0]["StockSN"].ToString();
                    txtCKMC.Text = dt.Rows[0]["StockName"].ToString();
                    ddlGZR.SelectedValue = dt.Rows[0]["SMonDay"].ToString();
                    cbKSSY.Checked = Convert.ToBoolean(dt.Rows[0]["isUse"].ToString());
                    txtDQQS.Text = dt.Rows[0]["SMonY"].ToString();
                    txtNQS.Text = dt.Rows[0]["Smons"].ToString();
                    CBKC.Checked = Convert.ToBoolean(dt.Rows[0]["isPlus"].ToString());
                }
                if (JD[0] == "1")
                {
                    divBT.Visible = true;
                    divLB.Visible = false;
                    divKW.Visible = true;
                    string PlaceID = JD[1];
                    DataTable dt = ckService.GETKWINFOBYKWID(PlaceID);
                    DataTable dt1 = ckService.GETCKINFO(dt.Rows[0]["StockID"].ToString());
                    ddlKH.SelectedValue = dt1.Rows[0]["StockID"].ToString();
                    txtKM.Text = dt1.Rows[0]["StockName"].ToString();
                    txtBH.Text = dt.Rows[0]["PlaceSN"].ToString();
                    txtMC.Text = dt.Rows[0]["PlaceName"].ToString();
                    txtBZ.Text = dt.Rows[0]["PlaceMem"].ToString();
                }
            }
        }

        private void CLEARTEXT()
        {
            txtCKBH.Text = "";
            txtCKMC.Text = "";
            ddlGZR.SelectedValue = "0";
            cbKSSY.Checked = false;
            txtDQQS.Text = "";
            txtNQS.Text = "";
            CBKC.Checked = false;
            ddlKH.SelectedValue = "0";
            txtKM.Text = "";
            txtBH.Text = "";
            txtMC.Text = "";
            txtBZ.Text = "";
            ISXZLB = false;
            ISXZKW = false;
        }

        protected void ddlKH_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = ckService.GETCKINFO(ddlKH.SelectedValue);
            txtKM.Text = dt.Rows[0]["StockName"].ToString();
        }

        private void LOGINTIMEOUT()
        {
            if (Session["STAFFID"] == null)
            {
                Response.Redirect("../login.aspx?RI=1");
            }
        }
    }
}