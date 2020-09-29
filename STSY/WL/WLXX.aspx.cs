using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bussiness.STSY;
using System.Data;
namespace STSY.WL
{
    public partial class WLXX : System.Web.UI.Page
    {
        WLService wlService = new WLService();
        CKService ckService = new CKService();
        Mainservice mainService = new Mainservice();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LOGINTIMEOUT();
                STAFFQX();
                BANDLOAD();
                BANDBOTTON("1,2,4");
            }
        }

        private void LOGINTIMEOUT()
        {
            if (Session["STAFFID"] == null)
            {
                Response.Redirect("../login.aspx?RI=1");
            }
        }
        private void STAFFQX()
        {
            if (Session["STAFFID"] == null)
            {
                Response.Redirect("../login.aspx?RI=1");
            }
            bool s = mainService.SELECT_HG_RIGHT(Session["STAFFID"].ToString(), "290");
            if (s == false)
            {
                Response.Redirect("../Index/Index.aspx?RI=1");
            }
        }
        private void BANDLOAD()
        {
            TreeView1.Nodes.Clear();
            DataTable dt = wlService.GETWLTYPE();
            BANDTREEFATHER(dt);
            divLB.Visible = true;
            divWL.Visible = false;
            BANDDDLWLLB();
        }

        private void BANDLOADwithoutclear()
        {
            
            DataTable dt = wlService.GETWLTYPE();
            BANDTREEFATHER(dt);
            divLB.Visible = true;
            divWL.Visible = false;
            BANDDDLWLLB();
        }
        protected void TreeView1_SelectedNodeChanged(object sender, EventArgs e)
        {
            lbISXZLB.Text = "FALSE";
            lbISXZWL.Text = "FALSE";
            TreeView1.SelectedNodeStyle.BackColor = System.Drawing.Color.BurlyWood;
            string[] JD = TreeView1.SelectedNode.Value.Split(',');
            if (JD.Length == 2)
            {
                if (JD[0] == "0")
                {
                    divLB.Visible = true;
                    divWL.Visible = false;
                    string typeID = JD[1];
                    DataTable dt = wlService.GETLBTYPE(typeID);
                    if (dt.Rows.Count > 0)
                    {
                        txtLBBH.Text = dt.Rows[0]["typeSN"].ToString();
                        ddlLBZL.SelectedValue = dt.Rows[0]["typeType"].ToString();
                        txtLBMC.Text = dt.Rows[0]["typeName"].ToString();
                        txtLBBZ.Text = dt.Rows[0]["typeMem"].ToString();
                    }
                    BANDBOTTON("1,2,3,4,6,7");
                }
                if (JD[0] == "1")
                {
                    divLB.Visible = false;
                    divWL.Visible = true;
                    string materialID = JD[1];
                    DataTable dt = wlService.GETWLTYPEbymaterialID(materialID);
                    if (dt.Rows.Count > 0)
                    {
                        DataTable dttype = wlService.GETLBTYPE(dt.Rows[0]["typeID"].ToString());
                        if (dttype.Rows.Count > 0)
                        {
                            ddlWLLB.SelectedValue = dttype.Rows[0]["typeID"].ToString();
                        }
                        txtWLBH.Text = dt.Rows[0]["mtSN"].ToString();
                        txtWLTM.Text = dt.Rows[0]["mtNO"].ToString();
                        txtWLMC.Text = dt.Rows[0]["mtName"].ToString();
                        txtWLGG.Text = dt.Rows[0]["mtSpec"].ToString();
                        txtWLDW.Text = dt.Rows[0]["mtUnit"].ToString();
                        txtWLDJ.Text = dt.Rows[0]["mtBuyPrice"].ToString();
                        txtWLSX.Text = dt.Rows[0]["MaxNum"].ToString();
                        txtWLXX.Text = dt.Rows[0]["MinNum"].ToString();
                        txtWLBZ.Text = dt.Rows[0]["mtMemo"].ToString();
                    }
                    BANDBOTTON("1,2,4,5,6,7");
                }
            }
        }

        private void BANDTREEFATHER(DataTable dt)
        {
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    TreeNode father = new TreeNode();
                    father.Text = dt.Rows[i]["typeSN"].ToString() + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + dt.Rows[i]["typeName"].ToString();
                    father.Value = "0," + dt.Rows[i]["typeID"].ToString();
                    this.TreeView1.Nodes.Add(father);
                    DataTable dtChild = wlService.GETWLTYPE(dt.Rows[i]["typeID"].ToString());
                    if (dtChild.Rows.Count > 0)
                    {
                        for (int j = 0; j < dtChild.Rows.Count; j++)
                        {
                            TreeNode child = new TreeNode();
                            child.Text = dtChild.Rows[j]["mtSN"].ToString() + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + dtChild.Rows[j]["mtName"].ToString();
                            child.Value = "1," + dtChild.Rows[j]["materialID"].ToString();
                            father.ChildNodes.Add(child);
                        }
                    }
                }
            }
        }

        private void BANDDDLWLLB()
        {
            DataTable dt = wlService.GETWLTYPE();
            dt.Columns.Add("SNNAME", typeof(string));
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dt.Rows[i]["SNNAME"] = dt.Rows[i]["typeSN"].ToString() + " " + dt.Rows[i]["typeName"].ToString();
                }
            }
            ddlWLLB.DataSource = dt;
            ddlWLLB.DataTextField = "SNNAME";
            ddlWLLB.DataValueField = "typeID";
            ddlWLLB.DataBind();
            ListItem item = new ListItem();
            item.Text = "=请选择=";
            item.Value = "0";
            ddlWLLB.Items.Insert(0, item);
        }

        protected void btnSX_Click(object sender, EventArgs e)
        {
            BANDLOAD();
            BANDBOTTON("1,2,4");
        }

        protected void btnZJLB_Click(object sender, EventArgs e)
        {
            txtLBBH.Text = "";
            ddlLBZL.SelectedValue = "-1";
            txtLBMC.Text = "";
            txtLBBZ.Text = "";
            lbISXZLB.Text = "TRUE";
            lbISXZWL.Text = "FALSE";
            divLB.Visible = true;
            divWL.Visible = false;
            BANDBOTTON("1,2,4,6,7");
        }

        protected void btnSCLB_Click(object sender, EventArgs e)
        {
            string[] DJ = TreeView1.SelectedNode.Value.Split(',');
            if (DJ.Length == 2)
            {
                if (DJ[0] != "0")
                {
                    this.Page.RegisterStartupScript("onclick", "<script>alert('请选中要删除的类别！')</script>");
                    return;
                }
                wlService.DELETELB(DJ[1]);
                this.Page.RegisterStartupScript("onclick", "<script>alert('删除成功！')</script>");
                BANDLOAD();
            }
            else
            {
                this.Page.RegisterStartupScript("onclick", "<script>alert('数据异常，请刷新！')</script>");
                return;
            }
            BANDLOAD();
            BANDBOTTON("1,2,4");
        }

        protected void btnZJWL_Click(object sender, EventArgs e)
        {
            ddlWLLB.SelectedValue = "0";
            txtWLBH.Text = "";
            txtWLTM.Text = "";
            txtWLMC.Text = "";
            txtWLGG.Text = "";
            txtWLDW.Text = "";
            txtWLDJ.Text = "0";
            txtWLSX.Text = "100";
            txtWLXX.Text = "10";
            txtWLBZ.Text = "";
            lbISXZLB.Text = "FALSE";
            lbISXZWL.Text = "TRUE";
            divLB.Visible = false;
            divWL.Visible = true;
            BANDBOTTON("1,2,4,6,7");
        }

        protected void btnSCWL_Click(object sender, EventArgs e)
        {
            string[] DJ = TreeView1.SelectedNode.Value.Split(',');
            if (DJ.Length == 2)
            {
                if (DJ[0] != "1")
                {
                    this.Page.RegisterStartupScript("onclick", "<script>alert('请选中要删除的物料！')</script>");
                    return;
                }
                wlService.DELETEWL(DJ[1]);
                this.Page.RegisterStartupScript("onclick", "<script>alert('删除成功！')</script>");
                BANDLOAD();
            }
            else
            {
                this.Page.RegisterStartupScript("onclick", "<script>alert('数据异常，请刷新！')</script>");
                return;
            }
            BANDLOAD();
            BANDBOTTON("1,2,4");
        }

        protected void btnSAVE_Click(object sender, EventArgs e)
        {
            if (lbISXZLB.Text.ToUpper() == "TRUE")
            {
                if (txtLBBH.Text.Trim() == "")
                {
                    this.Page.RegisterStartupScript("onclick", "<script>alert('请输入新增类别的类别编号！')</script>");
                    return;
                }
                if (ddlLBZL.SelectedValue == "-1")
                {
                    this.Page.RegisterStartupScript("onclick", "<script>alert('请选择新增类别的类别种类！')</script>");
                    return;
                }
                if (txtLBMC.Text.Trim() == "")
                {
                    this.Page.RegisterStartupScript("onclick", "<script>alert('请输入新增类别的类别名称！')</script>");
                    return;
                }
                try
                {
                    if (wlService.XZISHAVELB(txtLBBH.Text.Trim()).Rows.Count > 0)
                    {
                        this.Page.RegisterStartupScript("onclick", "<script>alert('新增类别失败，该编号已存在！')</script>");
                        return;
                    }
                    wlService.insertWLLB(txtLBBH.Text.Trim(), txtLBMC.Text.Trim(), ddlLBZL.SelectedValue, txtLBBZ.Text.Trim());

                    this.Page.RegisterStartupScript("onclick", "<script>alert('新增类别成功！')</script>");
                    BANDLOADwithoutclear();
                    BANDBOTTON("1,2,4,6");
                    divLB.Visible = true;
                    divWL.Visible = false;
                    return;
                }
                catch
                {
                    this.Page.RegisterStartupScript("onclick", "<script>alert('新增类别失败，请联系管理员！')</script>");
                    return;
                }

            }
            if (lbISXZWL.Text.ToUpper() == "TRUE")
            {
                if (ddlWLLB.SelectedValue == "0")
                {
                    this.Page.RegisterStartupScript("onclick", "<script>alert('请选择新增物料的类别！')</script>");
                    return;
                }
                if (txtWLBH.Text.Trim() == "")
                {
                    this.Page.RegisterStartupScript("onclick", "<script>alert('请输入新增物料的编号！')</script>");
                    return;
                }
                if (txtWLMC.Text.Trim() == "")
                {
                    this.Page.RegisterStartupScript("onclick", "<script>alert('请输入新增物料的名称！')</script>");
                    return;
                }
                if (txtWLDJ.Text.Trim() == "")
                {
                    this.Page.RegisterStartupScript("onclick", "<script>alert('请输入新增物料的单价！')</script>");
                    return;
                }
                else
                {
                    try
                    {
                        double s = Convert.ToDouble(txtWLDJ.Text);
                    }
                    catch
                    {
                        this.Page.RegisterStartupScript("onclick", "<script>alert('单价出现非数字文本！')</script>");
                        return;
                    }
                }
                if (txtWLSX.Text.Trim() == "")
                {
                    this.Page.RegisterStartupScript("onclick", "<script>alert('请输入新增物料的上限！')</script>");
                    return;
                }
                else
                {
                    try
                    {
                        double s = Convert.ToDouble(txtWLSX.Text);
                    }
                    catch
                    {
                        this.Page.RegisterStartupScript("onclick", "<script>alert('单价出现非数字文本！')</script>");
                        return;
                    }
                }
                if (txtWLXX.Text.Trim() == "")
                {
                    this.Page.RegisterStartupScript("onclick", "<script>alert('请输入新增物料的下限！')</script>");
                    return;
                }
                else
                {
                    try
                    {
                        double s = Convert.ToDouble(txtWLXX.Text);
                    }
                    catch
                    {
                        this.Page.RegisterStartupScript("onclick", "<script>alert('单价出现非数字文本！')</script>");
                        return;
                    }
                }
                try
                {
                    if (wlService.XZISHAVEWL(txtWLBH.Text.Trim()).Rows.Count > 0)
                    {
                        this.Page.RegisterStartupScript("onclick", "<script>alert('新增物料失败，该编号已存在！')</script>");
                        return;
                    }
                    string s = wlService.insertWL(ddlWLLB.SelectedValue, txtWLMC.Text.Trim(), txtWLTM.Text.Trim(), txtWLBH.Text.Trim(), txtWLGG.Text.Trim(), txtWLDW.Text.Trim(), txtWLDJ.Text.Trim(), txtWLXX.Text.Trim(), txtWLSX.Text.Trim(), txtWLBZ.Text.Trim());
                    DataTable dt = ckService.GETCKINFO();
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            DataTable dtKW = ckService.GETKWINFO(dt.Rows[i]["StockID"].ToString());
                            wlService.INSERT_MSRealStock(dt.Rows[i]["StockID"].ToString(), s, "0", txtWLDW.Text, "0", "0", "0");
                            if (dtKW.Rows.Count > 0)
                            {
                                for (int j = 0; j < dtKW.Rows.Count; j++)
                                {
                                    wlService.INSERT_MSRealStock(dt.Rows[i]["StockID"].ToString(), s, dtKW.Rows[j]["PlaceID"].ToString(), txtWLDW.Text, "0", "0", "0");
                                }
                            }
                        }
                    }
                    //wlService.INSERT_MSRealStock();
                    this.Page.RegisterStartupScript("onclick", "<script>alert('新增物料成功！')</script>");
                    BANDLOADwithoutclear();
                    BANDBOTTON("1,2,4,6");
                    divLB.Visible = false;
                    divWL.Visible = true;
                    return;
                }
                catch
                {
                    this.Page.RegisterStartupScript("onclick", "<script>alert('新增物料失败，请联系管理员！')</script>");
                    return;
                }


            }
            if (lbISXZLB.Text.ToUpper() == "FALSE" && lbISXZWL.Text.ToUpper() == "FALSE")
            {
                string[] TR = TreeView1.SelectedNode.Value.Split(',');
                if (TR.Length == 2)
                {
                    if (TR[0] == "0")
                    {
                        if (txtLBBH.Text.Trim() == "")
                        {
                            this.Page.RegisterStartupScript("onclick", "<script>alert('请输入类别的类别编号！')</script>");
                            return;
                        }
                        if (ddlLBZL.SelectedValue == "-1")
                        {
                            this.Page.RegisterStartupScript("onclick", "<script>alert('请选择类别的类别种类！')</script>");
                            return;
                        }
                        if (txtLBMC.Text.Trim() == "")
                        {
                            this.Page.RegisterStartupScript("onclick", "<script>alert('请输入类别的类别名称！')</script>");
                            return;
                        }
                        try
                        {
                            if (wlService.ISHAVELB(txtLBBH.Text.Trim(), TR[1]).Rows.Count > 0)
                            {
                                this.Page.RegisterStartupScript("onclick", "<script>alert('该编号已存在，请修改编号！')</script>");
                                return;
                            }
                            wlService.UPDATELB(txtLBBH.Text.Trim(), txtLBMC.Text.Trim(), ddlLBZL.SelectedValue, txtLBBZ.Text.Trim(), TR[1]);
                            this.Page.RegisterStartupScript("onclick", "<script>alert('类别修改成功！')</script>");
                            BANDLOAD();
                            BANDBOTTON("1,2,4");
                            return;
                        }
                        catch
                        {
                            this.Page.RegisterStartupScript("onclick", "<script>alert('修改类别失败，请联系管理员！')</script>");
                            return;
                        }
                    }
                    if (TR[0] == "1")
                    {
                        if (ddlWLLB.SelectedValue == "0")
                        {
                            this.Page.RegisterStartupScript("onclick", "<script>alert('请选择物料的类别！')</script>");
                            return;
                        }
                        if (txtWLBH.Text.Trim() == "")
                        {
                            this.Page.RegisterStartupScript("onclick", "<script>alert('请输入物料的编号！')</script>");
                            return;
                        }
                        if (txtWLMC.Text.Trim() == "")
                        {
                            this.Page.RegisterStartupScript("onclick", "<script>alert('请输入物料的名称！')</script>");
                            return;
                        }
                        if (txtWLDJ.Text.Trim() == "")
                        {
                            this.Page.RegisterStartupScript("onclick", "<script>alert('请输入物料的单价！')</script>");
                            return;
                        }
                        if (txtWLSX.Text.Trim() == "")
                        {
                            this.Page.RegisterStartupScript("onclick", "<script>alert('请输入物料的上限！')</script>");
                            return;
                        }
                        if (txtWLXX.Text.Trim() == "")
                        {
                            this.Page.RegisterStartupScript("onclick", "<script>alert('请输入物料的下限！')</script>");
                            return;
                        }
                        try
                        {
                            if (wlService.ISHAVEWL(txtWLBH.Text.Trim(), TR[1]).Rows.Count > 0)
                            {
                                this.Page.RegisterStartupScript("onclick", "<script>alert('该编号已存在，请修改编号！')</script>");
                                return;
                            }
                            wlService.UPDATEWL(ddlWLLB.SelectedValue, txtWLMC.Text.Trim(), txtWLTM.Text.Trim(), txtWLBH.Text.Trim(), txtWLGG.Text.Trim(), txtWLDW.Text.Trim(), txtWLDJ.Text.Trim(), txtWLXX.Text.Trim(), txtWLSX.Text.Trim(), txtWLBZ.Text.Trim(), TR[1]);
                            wlService.UPDATE_MSRealStock(TR[1], txtWLDW.Text, txtWLDJ.Text);
                            this.Page.RegisterStartupScript("onclick", "<script>alert('物料修改成功！')</script>");
                            BANDLOAD();
                            BANDBOTTON("1,2,4");
                            return;
                        }
                        catch
                        {
                            this.Page.RegisterStartupScript("onclick", "<script>alert('修改类别失败，请联系管理员！')</script>");
                            return;
                        }
                    }
                }
                else
                {
                    this.Page.RegisterStartupScript("onclick", "<script>alert('修改失败请刷新后重试！')</script>");
                    return;
                }
            }
            BANDLOAD();
        }

        protected void btnQX_Click(object sender, EventArgs e)
        {
            lbISXZLB.Text = "FALSE";
            lbISXZWL.Text = "FALSE";
            BANDLOAD();
            BANDBOTTON("1,2,4");
        }

        private void BANDBOTTON(string str)
        {
            string[] str1 = str.Split(',');
            btnSX.Enabled = false;
            btnSX.BackColor = System.Drawing.Color.Gray;
            btnZJLB.Enabled = false;
            btnZJLB.BackColor = System.Drawing.Color.Gray;
            btnSCLB.Enabled = false;
            btnSCLB.BackColor = System.Drawing.Color.Gray;
            btnZJWL.Enabled = false;
            btnZJWL.BackColor = System.Drawing.Color.Gray;
            btnSCWL.Enabled = false;
            btnSCWL.BackColor = System.Drawing.Color.Gray;
            btnSAVE.Enabled = false;
            btnSAVE.BackColor = System.Drawing.Color.Gray;
            btnQX.Enabled = false;
            btnQX.BackColor = System.Drawing.Color.Gray;
            if (str1.Length > 0)
            {
                for (int i = 0; i < str1.Length; i++)
                {
                    if (str1[i] == "1")
                    {
                        btnSX.Enabled = true;
                        btnSX.BackColor = System.Drawing.Color.FromName("#3498db");
                    }
                    else if (str1[i] == "2")
                    {
                        btnZJLB.Enabled = true;
                        btnZJLB.BackColor = System.Drawing.Color.FromName("#3498db");
                    }
                    else if (str1[i] == "3")
                    {
                        btnSCLB.Enabled = true;
                        btnSCLB.BackColor = System.Drawing.Color.FromName("#3498db");
                    }
                    else if (str1[i] == "4")
                    {
                        btnZJWL.Enabled = true;
                        btnZJWL.BackColor = System.Drawing.Color.FromName("#3498db");
                    }
                    else if (str1[i] == "5")
                    {
                        btnSCWL.Enabled = true;
                        btnSCWL.BackColor = System.Drawing.Color.FromName("#3498db");
                    }
                    else if (str1[i] == "6")
                    {
                        btnSAVE.Enabled = true;
                        btnSAVE.BackColor = System.Drawing.Color.FromName("#3498db");
                    }
                    else if (str1[i] == "7")
                    {
                        btnQX.Enabled = true;
                        btnQX.BackColor = System.Drawing.Color.FromName("#3498db");
                    }
                }
            }
        }
    }
}