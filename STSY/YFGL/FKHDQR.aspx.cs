using Bussiness.STSY;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace STSY.YFGL
{
    public partial class FKHDQR : System.Web.UI.Page
    {
        FPService fpService = new FPService();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LOGINTIMEOUT();
                LinkButton2.BackColor = System.Drawing.Color.DarkGray;
                LinkButton1.BackColor = System.Drawing.Color.White;
                divFPLR.Visible = false;
                divFPCXLB.Visible = true;
                rbCX.SelectedValue = "0";
                BANDddlGYS();
                BANDddlLBGYSBH();
                txtRZRQ.Text = DateTime.Now.ToString("yyyy-MM-dd");
                txtFPRQ.Text = DateTime.Now.ToString("yyyy-MM-dd");
                txtFPJE.Text = "0";
                txtFKJE.Text = "0";
                txtLBRZRQS.Enabled = false;
                txtLBRZRQE.Enabled = false;
                txtLBFPRQS.Enabled = true;
                txtLBFPRQE.Enabled = true;
                cbLBFPRQ.Checked = true;
                cbLBRZRQ.Checked = false;
                lbISLB.Text = "TRUE";
                DateTime now = DateTime.Now;
                DateTime d1 = new DateTime(now.Year, now.Month, 1);
                txtLBRZRQS.Text = d1.ToString("yyyy-MM-dd");
                txtLBRZRQE.Text = DateTime.Now.ToString("yyyy-MM-dd");
                txtLBFPRQS.Text = d1.ToString("yyyy-MM-dd");
                txtLBFPRQE.Text = DateTime.Now.ToString("yyyy-MM-dd");
                BANDCXSX();
            }
        }

        protected void btnCXSX_Click(object sender, EventArgs e)
        {
            if (lbISLB.Text.ToUpper() == "TRUE")
            {
                BANDCXSX();
            }
            else
            {
                if (lbCaiGFPID.Text != "" && lbCaiGFPID.Text != "0")
                {
                    BAND();
                }
            }
        }

        protected void btnDJTH_Click(object sender, EventArgs e)
        {
            if (lbISLB.Text.ToUpper() == "FALSE")
            {
                if (lbisRePay.Text.ToUpper() == "FALSE")
                {
                    if (lbCaiGFPID.Text != "" && lbCaiGFPID.Text != "0")
                    {
                        fpService.UPDATE_CaiGFP_HDTH(lbCaiGFPID.Text.Trim());
                        this.Page.RegisterStartupScript("onclick", "<script>alert('退回成功！')</script>");
                    }
                    else
                    {
                        this.Page.RegisterStartupScript("onclick", "<script>alert('请选择相关的单据查看，到发票明细页面！')</script>");
                    }
                }
                else
                {
                    this.Page.RegisterStartupScript("onclick", "<script>alert('单据已确认付款回单，不能退回！')</script>");
                }
            }
            else
            {
                this.Page.RegisterStartupScript("onclick", "<script>alert('请选择相关的单据查看，到发票明细页面！')</script>");
            }
        }

        protected void btnHDQR_Click(object sender, EventArgs e)
        {
            LOGINTIMEOUT();
            if (lbISLB.Text.ToUpper() == "FALSE")
            {
                if (lbisRePay.Text.ToUpper() == "FALSE")
                {
                    if (lbCaiGFPID.Text != "" && lbCaiGFPID.Text != "0")
                    {
                        fpService.UPDATE_CaiGFP_HDQR(lbCaiGFPID.Text.Trim(), Session["STAFFID"].ToString(), Session["STAFFNAME"].ToString(), txtHDQRSM.Text.Trim(), cbXJFK.Checked.ToString(), txtRZRQ.Text.Trim(), ddlRZLX.SelectedValue);
                        lbisRePay.Text = "TRUE";
                        this.Page.RegisterStartupScript("onclick", "<script>alert('单据回单确认成功！')</script>");
                    }
                    else
                    {
                        this.Page.RegisterStartupScript("onclick", "<script>alert('请选择相关的单据查看，到发票明细页面！')</script>");
                    }
                }
                else
                {
                    this.Page.RegisterStartupScript("onclick", "<script>alert('已确认发票付款回单，无需重复确认付款回单！')</script>");
                }
            }
            else
            {
                this.Page.RegisterStartupScript("onclick", "<script>alert('请选择相关的单据查看，到发票明细页面！')</script>");
            }
        }

        protected void LinkButton2_Click(object sender, EventArgs e)
        {
            LinkButton1.BackColor = System.Drawing.Color.White;
            LinkButton2.BackColor = System.Drawing.Color.DarkGray;
            divFPLR.Visible = false;
            divFPCXLB.Visible = true;
            lbISLB.Text = "TRUE";
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            LinkButton1.BackColor = System.Drawing.Color.DarkGray;
            LinkButton2.BackColor = System.Drawing.Color.White;
            divFPLR.Visible = true;
            divFPCXLB.Visible = false;
            lbISLB.Text = "FALSE";
        }

        protected void gdRKDH_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow) //判断是否是DataRow，以防止鼠标经过Header也有效果   
            {
                e.Row.Attributes.Add("onmouseover", "e=this.style.backgroundColor; this.style.backgroundColor='#CCCCCC'");
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=e");
            }
        }

        protected void cbLBRZRQ_CheckedChanged(object sender, EventArgs e)
        {
            if (cbLBRZRQ.Checked == true)
            {
                txtLBRZRQS.Enabled = true;
                txtLBRZRQE.Enabled = true;
            }
            else
            {
                txtLBRZRQS.Enabled = false;
                txtLBRZRQE.Enabled = false;
            }
        }

        protected void cbLBFPRQ_CheckedChanged(object sender, EventArgs e)
        {
            if (cbLBFPRQ.Checked == true)
            {
                txtLBFPRQS.Enabled = true;
                txtLBFPRQE.Enabled = true;
            }
            else
            {
                txtLBFPRQS.Enabled = false;
                txtLBFPRQE.Enabled = false;
            }
        }

        protected void gdFPMXLB_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow) //判断是否是DataRow，以防止鼠标经过Header也有效果   
            {
                e.Row.Attributes.Add("onmouseover", "e=this.style.backgroundColor; this.style.backgroundColor='#CCCCCC'");
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=e");
            }
        }

        protected void lbtnFIND_OnClick(object sender, EventArgs e)
        {
            LinkButton t = (LinkButton)sender;
            GridViewRow drv = (GridViewRow)t.NamingContainer;
            int rowIndex = drv.RowIndex;
            Label CaiGFPID = ((Label)gdFPMXLB.Rows[drv.RowIndex].FindControl("CaiGFPID"));
            Label isRePay = ((Label)gdFPMXLB.Rows[drv.RowIndex].FindControl("isRePay"));
            lbCaiGFPID.Text = CaiGFPID.Text;
            lbisRePay.Text = isRePay.Text;
            BAND();
            divFPCXLB.Visible = false;
            divFPLR.Visible = true;
            LinkButton1.BackColor = System.Drawing.Color.DarkGray;
            LinkButton2.BackColor = System.Drawing.Color.White;
            lbISLB.Text = "FALSE";
        }

        private void LOGINTIMEOUT()
        {
            if (Session["STAFFID"] == null)
            {
                Response.Redirect("../login.aspx?RI=1");
            }
        }

        private void BANDddlGYS()
        {
            DataTable dt = fpService.GETGYSINFOALL();
            dt.Columns.Add("NAME", typeof(string));
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dt.Rows[i]["NAME"] = dt.Rows[i]["ClSN"].ToString() + "  " + dt.Rows[i]["ClName"].ToString();
                }
            }
            ddlGYS.DataSource = dt;
            ddlGYS.DataTextField = "NAME";
            ddlGYS.DataValueField = "ClientID";
            ddlGYS.DataBind();
            ListItem item = new ListItem();
            item.Text = "=请选择=";
            item.Value = "0";
            ddlGYS.Items.Insert(0, item);
        }

        private void BANDddlLBGYSBH()
        {
            DataTable dt = fpService.GETGYSINFO();
            dt.Columns.Add("NAME", typeof(string));
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dt.Rows[i]["NAME"] = dt.Rows[i]["ClSN"].ToString() + "  " + dt.Rows[i]["ClName"].ToString();
                }
            }
            ddlLBGYSBH.DataSource = dt;
            ddlLBGYSBH.DataTextField = "NAME";
            ddlLBGYSBH.DataValueField = "ClientID";
            ddlLBGYSBH.DataBind();
            ListItem item = new ListItem();
            item.Text = "=请选择=";
            item.Value = "0";
            ddlLBGYSBH.Items.Insert(0, item);
        }

        private void BANDCXSX()
        {
            if (lbISLB.Text == "TRUE")
            {
                LOGINTIMEOUT();
                string RZRQS = "";
                string RZRQE = "";
                string FPRQS = "";
                string FPRQE = "";
                if (cbLBRZRQ.Checked == true)
                {
                    RZRQS = txtLBRZRQS.Text.Trim();
                    RZRQE = txtLBRZRQE.Text.Trim();
                }
                if (cbLBFPRQ.Checked == true)
                {
                    FPRQS = txtLBFPRQS.Text.Trim();
                    FPRQE = txtLBFPRQE.Text.Trim();
                }
                DataTable dt = new DataTable();
                LOGINTIMEOUT();
                dt = fpService.GETFPLBINFO_HDQR("", ddlLBGYSBH.SelectedValue, RZRQS, RZRQE, FPRQS, FPRQE, txtLBFPHM.Text.Trim(), rbCX.SelectedValue);
                dt.Columns.Add("JKK", typeof(double));
                dt.Columns.Add("FaPDate1", typeof(string));
                dt.Columns.Add("ACDate1", typeof(string));
                dt.Columns.Add("FaPTotal1", typeof(string));
                dt.Columns.Add("RuKTotal1", typeof(string));
                dt.Columns.Add("PayTotal1", typeof(string));
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        try
                        {
                            dt.Rows[i]["FaPDate1"] = Convert.ToDateTime(dt.Rows[i]["FaPDate"].ToString()).ToString("yyyy-MM-dd");
                        }
                        catch
                        {
                            dt.Rows[i]["FaPDate1"] = "";
                        }
                        try
                        {
                            dt.Rows[i]["ACDate1"] = Convert.ToDateTime(dt.Rows[i]["ACDate"].ToString()).ToString("yyyy-MM-dd");
                        }
                        catch
                        {
                            dt.Rows[i]["ACDate1"] = "";
                        }
                        dt.Rows[i]["FaPTotal1"] = Math.Round((Convert.ToDouble(dt.Rows[i]["FaPTotal"].ToString())), 2).ToString();
                        dt.Rows[i]["RuKTotal1"] = Math.Round((Convert.ToDouble(dt.Rows[i]["RuKTotal"].ToString())), 2).ToString();
                        dt.Rows[i]["PayTotal1"] = Math.Round((Convert.ToDouble(dt.Rows[i]["PayTotal"].ToString())), 2).ToString();
                        dt.Rows[i]["JKK"] = Math.Round((Convert.ToDouble(dt.Rows[i]["FaPTotal"].ToString()) - Convert.ToDouble(dt.Rows[i]["RuKTotal"].ToString())), 2).ToString();
                    }
                }
                gdFPMXLB.DataSource = dt;
                gdFPMXLB.DataBind();
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < gdFPMXLB.Rows.Count; i++)
                    {
                        CheckBox cbYSH = (CheckBox)gdFPMXLB.Rows[i].FindControl("cbYSH");
                        CheckBox cbYFK = (CheckBox)gdFPMXLB.Rows[i].FindControl("cbYFK");
                        CheckBox cbYHD = (CheckBox)gdFPMXLB.Rows[i].FindControl("cbYHD");
                        CheckBox cbXJ = (CheckBox)gdFPMXLB.Rows[i].FindControl("cbXJ");
                        cbYSH.Checked = Convert.ToBoolean(dt.Rows[i]["isAudi"].ToString());
                        cbYFK.Checked = Convert.ToBoolean(dt.Rows[i]["isPay"].ToString());
                        cbYHD.Checked = Convert.ToBoolean(dt.Rows[i]["isRePay"].ToString());
                        cbXJ.Checked = Convert.ToBoolean(dt.Rows[i]["PayCash"].ToString());
                    }
                }
            }
        }

        private void BAND()
        {
            if (lbCaiGFPID.Text != "0" && lbCaiGFPID.Text != "")
            {
                gdRKDLB.DataSource = null;
                gdRKDLB.DataBind();
                DataTable dt = fpService.GETCaiGFP(lbCaiGFPID.Text);
                if (dt.Rows.Count > 0)
                {
                    ddlGYS.SelectedValue = dt.Rows[0]["ClientID"].ToString();
                    if (dt.Rows[0]["ACDate"].ToString() == "")
                    {
                        txtRZRQ.Text = "1899-12-30";
                    }
                    else
                    {
                        txtRZRQ.Text = Convert.ToDateTime(dt.Rows[0]["ACDate"].ToString()).ToString("yyyy-MM-dd");
                    }
                    if (dt.Rows[0]["ACType"].ToString() == "")
                    {
                        ddlRZLX.SelectedValue = "0";
                    }
                    else
                    {
                        ddlRZLX.SelectedValue = dt.Rows[0]["ACType"].ToString();
                    }
                    txtFPRQ.Text = Convert.ToDateTime(dt.Rows[0]["FaPDate"].ToString()).ToString("yyyy-MM-dd");
                    txtFPJE.Text = Math.Round(Convert.ToDouble(dt.Rows[0]["FaPTotal"].ToString()), 2).ToString();
                    txtFKJE.Text = Math.Round(Convert.ToDouble(dt.Rows[0]["PayTotal"].ToString()), 2).ToString();
                    txtFPHM.Text = dt.Rows[0]["CaiGFPNO"].ToString();
                    txtFPSM.Text = dt.Rows[0]["FaPMem"].ToString();
                    txtHDQRSM.Text = dt.Rows[0]["RePayMemo"].ToString();
                    if (dt.Rows[0]["PayCash"].ToString() == "")
                    {
                        cbXJFK.Checked = false;
                    }
                    else
                    {
                        cbXJFK.Checked = Convert.ToBoolean(dt.Rows[0]["PayCash"].ToString());
                    }
                    DataTable dtRuKD = fpService.GETFaPRuKDALL(lbCaiGFPID.Text);
                    if (dtRuKD.Rows.Count > 0)
                    {
                        gdRKDLB.DataSource = dtRuKD;
                        gdRKDLB.DataBind();
                    }
                    btnHDQR.Attributes.Clear();
                    if (lbCaiGFPID.Text != "" && lbCaiGFPID.Text != "0")
                    {
                        btnHDQR.Attributes.Add("onclick", "return confirm('确认付款回单后将结束发票流程 是否继续？');");
                    }
                }
            }
        }

        private void BANDHJ()
        {
            double sumSL = 0;
            double sumZJE = 0;
            if (gdRKDH.Rows.Count > 0)
            {
                for (int i = 0; i < gdRKDH.Rows.Count; i++)
                {
                    Label mtNumber = ((Label)gdRKDH.Rows[i].FindControl("mtNumber"));
                    Label mtTotal = ((Label)gdRKDH.Rows[i].FindControl("mtTotal"));
                    try
                    {
                        sumSL = sumSL + Convert.ToDouble(mtNumber.Text);
                    }
                    catch
                    {
                        sumSL = sumSL + 0;
                    }
                    try
                    {
                        sumZJE = sumZJE + Convert.ToDouble(mtTotal.Text);
                    }
                    catch
                    {
                        sumZJE = sumZJE + 0;
                    }
                }
            }
            GridViewRow foot = gdRKDH.FooterRow;
            foot.Cells[4].HorizontalAlign = HorizontalAlign.Right;
            foot.Cells[4].Text = sumSL.ToString();
            foot.Cells[6].HorizontalAlign = HorizontalAlign.Right;
            foot.Cells[6].Text = sumZJE.ToString();
        }

        protected void gdRKDLB_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow) //判断是否是DataRow，以防止鼠标经过Header也有效果   
            {
                e.Row.Attributes.Add("onmouseover", "e=this.style.backgroundColor; this.style.backgroundColor='#CCCCCC'");
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=e");
            }
            switch (e.Row.RowType)
            {
                case DataControlRowType.DataRow:
                    e.Row.Attributes["style"] = "cursor:hand";
                    #region   //点击行触发SelectedIndexChanged事件
                    PostBackOptions myPostBackOptions = new PostBackOptions(this);
                    myPostBackOptions.AutoPostBack = false;
                    myPostBackOptions.PerformValidation = false;
                    myPostBackOptions.RequiresJavaScriptProtocol = true; //加入javascript:头
                    String evt = Page.ClientScript.GetPostBackClientHyperlink(sender as GridView, "Select$" + e.Row.RowIndex.ToString());
                    e.Row.Attributes.Add("onclick", evt);
                    #endregion
                    break;
            }
        }

        protected void gdRKDLB_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            Label RuKDID = (Label)gdRKDLB.Rows[e.NewSelectedIndex].FindControl("RuKDID");
            DataTable dtBT = fpService.GETrukd1(RuKDID.Text);
            DataTable dtMX = fpService.GETrukd2(RuKDID.Text);
            if (dtBT.Rows.Count > 0)
            {
                txtRKCKBH.Text = dtBT.Rows[0]["StockSN"].ToString();
                txtRKCKMC.Text = dtBT.Rows[0]["StockName"].ToString();
                txtRKGYSBH.Text = dtBT.Rows[0]["ClSN"].ToString();
                txtRKGYSMC.Text = dtBT.Rows[0]["ClName"].ToString();
                txtRKRKRQ.Text = Convert.ToDateTime(dtBT.Rows[0]["RuKDate"].ToString()).ToString("yyyy-MM-dd");
                ddlRKRKLX.SelectedValue = dtBT.Rows[0]["TypeID"].ToString();
                txtRKRKDH.Text = dtBT.Rows[0]["RuKDNO"].ToString();
                txtRKRKBZ.Text = dtBT.Rows[0]["RuKMem"].ToString();
                txtRKZDR.Text = dtBT.Rows[0]["fillName"].ToString();
                txtRKZDSJ.Text = dtBT.Rows[0]["fillTime"].ToString();
            }
            gdRKDH.DataSource = dtMX;
            gdRKDH.DataBind();
            BANDHJ();
            for (int i = 0; i < gdRKDLB.Rows.Count; i++)
            {
                if (i == e.NewSelectedIndex)
                {
                    gdRKDLB.Rows[i].BackColor = System.Drawing.Color.Gray;
                }
                else
                {
                    gdRKDLB.Rows[i].BackColor = System.Drawing.Color.White;
                }
            }
        }
    }
}