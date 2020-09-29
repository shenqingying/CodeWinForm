using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bussiness.STSY;
using System.Text;

namespace STSY.YFGL
{
    public partial class FPRR : System.Web.UI.Page
    {
        FPService fpService = new FPService();
        Mainservice mainService = new Mainservice();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lbRKDID.Text = "0";
                LOGINTIMEOUT();
                STAFFQX();
                LinkButton1.BackColor = System.Drawing.Color.DarkGray;
                LinkButton2.BackColor = System.Drawing.Color.White;
                divFPLR.Visible = true;
                divFPCXLB.Visible = false;
                BANDddlGYS();
                BANDddlLBGYSBH();
                txtRZRQ.Text = DateTime.Now.ToString("yyyy-MM-dd");
                txtFPRQ.Text = DateTime.Now.ToString("yyyy-MM-dd");
                txtFPJE.Text = "0";
                txtFKJE.Text = "0";
                txtRKJE.Text = "0";
                txtJKK.Text = "0";
                rbCX.SelectedValue = "0";
                txtLBRZRQS.Enabled = false;
                txtLBRZRQE.Enabled = false;
                txtLBFPRQS.Enabled = true;
                txtLBFPRQE.Enabled = true;
                cbLBFPRQ.Checked = true;
                cbLBRZRQ.Checked = false;
                lbISLB.Text = "FALSE";
                BANDBOTTON("1,5,6,7,8");
                if (Request.QueryString["RI"] != null)
                {
                    BANDTX();
                }
                BANDEnabled(true);
                DateTime now = DateTime.Now;
                DateTime d1 = new DateTime(now.Year, now.Month, 1);
                txtLBRZRQS.Text = d1.ToString("yyyy-MM-dd");
                txtLBRZRQE.Text = DateTime.Now.ToString("yyyy-MM-dd");
                txtLBFPRQS.Text = d1.ToString("yyyy-MM-dd");
                txtLBFPRQE.Text = DateTime.Now.ToString("yyyy-MM-dd");
            }
        }
        private void STAFFQX()
        {
            LOGINTIMEOUT();
            bool s = mainService.SELECT_HG_RIGHT(Session["STAFFID"].ToString(), "205");
            if (s == false)
            {
                Response.Redirect("../Index/Index.aspx?RI=1");
            }
        }
        private void BANDTX()
        {
            DataTable dt = fpService.GETCaiGFPYZ(Request.QueryString["RI"].ToString());
            if (dt.Rows.Count > 0)
            {
                ddlGYS.SelectedValue = dt.Rows[0]["GYS"].ToString();
                txtRZRQ.Text = dt.Rows[0]["RZRQ"].ToString();
                ddlRZLX.SelectedValue = dt.Rows[0]["RZLX"].ToString();
                txtFPRQ.Text = dt.Rows[0]["FPRQ"].ToString();
                txtFPJE.Text = dt.Rows[0]["FPJE"].ToString();
                txtFKJE.Text = dt.Rows[0]["FKJE"].ToString();
                txtFPHM.Text = dt.Rows[0]["FPHM"].ToString();
                txtRKJE.Text = dt.Rows[0]["RKJE"].ToString();
                txtJKK.Text = dt.Rows[0]["JKK"].ToString();
                txtFPSM.Text = dt.Rows[0]["FPSM"].ToString();
                txtHDQRSM.Text = dt.Rows[0]["HDQRSM"].ToString();
                string[] RUKD = dt.Rows[0]["RKBD"].ToString().Split(' ');
                double rkje = 0;
                if (RUKD.Length > 0)
                {
                    string str = "";
                    for (int i = RUKD.Length - 1; i >= 0; i--)
                    {
                        if (RUKD[i] != "" && RUKD[i] != " ")
                        {
                            if (str == "")
                            {
                                str = RUKD[i];
                            }
                            else
                            {
                                str = str + "," + RUKD[i];
                            }
                        }
                    }
                    if (str.Trim() != "")
                    {
                        DataTable dtruk = fpService.GETFaPRuKDstring(str);
                        gdRKDLB.DataSource = dtruk;
                        gdRKDLB.DataBind();
                        BANDRKDLB();
                        if (dtruk.Rows.Count > 0)
                        {
                            for (int j = 0; j < dtruk.Rows.Count; j++)
                            {
                                rkje = rkje + Convert.ToDouble(dtruk.Rows[j]["RuKTotal"].ToString());
                            }
                        }
                    }
                    else
                    {
                        gdRKDLB.DataSource = null;
                        gdRKDLB.DataBind();
                        BANDRKDLB();
                    }
                }
                txtFPJE.Text = Math.Round(rkje, 2).ToString();
                txtRKJE.Text = Math.Round(rkje, 2).ToString();
                txtJKK.Text = "0";
            }
        }

        private void BANDBOTTON(string str)
        {
            string[] str1 = str.Split(',');
            btnCXSX.Enabled = false;
            btnCXSX.BackColor = System.Drawing.Color.Gray;
            btnADD.Enabled = false;
            btnADD.BackColor = System.Drawing.Color.Gray;
            btnEDITOR.Enabled = false;
            btnEDITOR.BackColor = System.Drawing.Color.Gray;
            btnDELETE.Enabled = false;
            btnDELETE.BackColor = System.Drawing.Color.Gray;
            btnADDROW.Enabled = false;
            btnADDROW.BackColor = System.Drawing.Color.Gray;
            btnDELETEROW.Enabled = false;
            btnDELETEROW.BackColor = System.Drawing.Color.Gray;
            btnSAVE.Enabled = false;
            btnSAVE.BackColor = System.Drawing.Color.Gray;
            btnQX.Enabled = false;
            btnQX.BackColor = System.Drawing.Color.Gray;
            btnTJ.Enabled = false;
            btnTJ.BackColor = System.Drawing.Color.Gray;
            btnADDGLBD.Enabled = false;
            btnDELETEGLBD.Enabled = false;
            if (str1.Length > 0)
            {
                for (int i = 0; i < str1.Length; i++)
                {
                    if (str1[i] == "1")
                    {
                        btnCXSX.Enabled = true;
                        btnCXSX.BackColor = System.Drawing.Color.FromName("#3498db");
                    }
                    else if (str1[i] == "2")
                    {
                        btnADD.Enabled = true;
                        btnADD.BackColor = System.Drawing.Color.FromName("#3498db");
                    }
                    else if (str1[i] == "3")
                    {
                        btnEDITOR.Enabled = true;
                        btnEDITOR.BackColor = System.Drawing.Color.FromName("#3498db");
                    }
                    else if (str1[i] == "4")
                    {
                        btnDELETE.Enabled = true;
                        btnDELETE.BackColor = System.Drawing.Color.FromName("#3498db");
                    }
                    else if (str1[i] == "5")
                    {
                        btnADDROW.Enabled = true;
                        btnADDROW.BackColor = System.Drawing.Color.FromName("#3498db");
                        btnADDGLBD.Enabled = true;
                    }
                    else if (str1[i] == "6")
                    {
                        btnDELETEROW.Enabled = true;
                        btnDELETEROW.BackColor = System.Drawing.Color.FromName("#3498db");
                        btnDELETEGLBD.Enabled = true;
                    }
                    else if (str1[i] == "7")
                    {
                        btnSAVE.Enabled = true;
                        btnSAVE.BackColor = System.Drawing.Color.FromName("#3498db");
                    }
                    else if (str1[i] == "8")
                    {
                        btnQX.Enabled = true;
                        btnQX.BackColor = System.Drawing.Color.FromName("#3498db");
                    }
                    else if (str1[i] == "9")
                    {
                        btnTJ.Enabled = true;
                        btnTJ.BackColor = System.Drawing.Color.FromName("#3498db");
                    }

                }
            }
        }

        private void BANDddlGYS()
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

        private string DDLGYSBHCHANGED()
        {
            string ck = ddlGYS.SelectedItem.Text;
            string[] ckname = ck.Split(' ');
            if (ddlGYS.SelectedValue != "0")
            {
                return ckname[2];
            }
            else
            {
                return "";
            }
        }

        private string DDLLBGYSBHCHANGED()
        {
            string ck = ddlLBGYSBH.SelectedItem.Text;
            string[] ckname = ck.Split(' ');
            if (ddlLBGYSBH.SelectedValue != "0")
            {
                return ckname[2];
            }
            else
            {
                return "";
            }
        }

        protected void btnCXSX_Click(object sender, EventArgs e)
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
                DataTable dt = fpService.GETFPLBINFO(Session["STAFFID"].ToString(), ddlLBGYSBH.SelectedValue, RZRQS, RZRQE, FPRQS, FPRQE, txtLBFPHM.Text.Trim(), rbCX.SelectedValue, ddlRZLXLB.SelectedValue);
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
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        CheckBox cbYSH = (CheckBox)gdFPMXLB.Rows[i].FindControl("cbYSH");
                        CheckBox cbYFK = (CheckBox)gdFPMXLB.Rows[i].FindControl("cbYFK");
                        CheckBox cbYHD = (CheckBox)gdFPMXLB.Rows[i].FindControl("cbYHD");
                        cbYSH.Checked = Convert.ToBoolean(dt.Rows[i]["isAudi"].ToString());
                        cbYFK.Checked = Convert.ToBoolean(dt.Rows[i]["isPay"].ToString());
                        cbYHD.Checked = Convert.ToBoolean(dt.Rows[i]["isRePay"].ToString());
                    }
                }
            }
        }

        protected void btnADD_Click(object sender, EventArgs e)
        {
            if (lbISLB.Text == "FALSE")
            {
                BANDEnabled(true);
                BANDCLEAR();
                lbCaiGFPID.Text = "0";
                lbnStatus.Text = "0";
                BANDBOTTON("1,5,6,7,8");
            }
        }

        protected void btnEDITOR_Click(object sender, EventArgs e)
        {
            if (lbISLB.Text == "FALSE")
            {
                if (lbnStatus.Text != "0")
                {
                    this.Page.RegisterStartupScript("onclick", "<script>alert('已提交或已审核，无法修改！')</script>");
                    return;
                }
                BANDEnabled(true);
                BANDBOTTON("1,5,6,7,8");
            }
        }

        protected void btnDELETE_Click(object sender, EventArgs e)
        {
            if (lbISLB.Text == "FALSE")
            {
                if (lbnStatus.Text != "0")
                {
                    this.Page.RegisterStartupScript("onclick", "<script>alert('已提交或已审核，无法删除！')</script>");
                    return;
                }
                fpService.DELETEFaPRuKD(lbCaiGFPID.Text);
                fpService.DELETE_CaiGFP(lbCaiGFPID.Text);
                this.Page.RegisterStartupScript("onclick", "<script>alert('删除成功！')</script>");
                BANDEnabled(true);
                BANDCLEAR();
                lbCaiGFPID.Text = "0";
                lbnStatus.Text = "0";
                BANDBOTTON("1,5,6,7,8");
            }
        }

        protected void btnADDROW_Click(object sender, EventArgs e)
        {
            INSERT_CaiGFPYZ();
        }

        protected void btnDELETEROW_Click(object sender, EventArgs e)
        {
            DELETEROW();
        }

        protected void btnSAVE_Click(object sender, EventArgs e)
        {
            if (txtFPHM.Text.Trim() == "")
            {
                this.Page.RegisterStartupScript("onclick", "<script>alert('请输入发票号码！')</script>");
                return;
            }
            try
            {
                if (lbCaiGFPID.Text != "0")
                {
                    fpService.DELETEFaPRuKD(lbCaiGFPID.Text);
                    fpService.DELETE_CaiGFP(lbCaiGFPID.Text);
                }
                LOGINTIMEOUT();
                int sum = fpService.INSERT_CaiGFP(txtFPHM.Text.Trim(), txtFPRQ.Text.Trim(), ddlGYS.SelectedValue, txtFPSM.Text.Trim(), txtFPJE.Text.Trim(), "0", "0", txtFKJE.Text.Trim(), "0", Session["STAFFID"].ToString(), Session["STAFFNAME"].ToString(), DateTime.Now.ToString(), "0", txtRZRQ.Text.Trim(), ddlRZLX.Text.Trim(), txtRKJE.Text.Trim());
                fpService.DELETEFaPRuKD(sum.ToString());
                if (gdRKDLB.Rows.Count > 0)
                {
                    for (int i = 0; i < gdRKDLB.Rows.Count; i++)
                    {
                        Label RuKDID = ((Label)gdRKDLB.Rows[i].FindControl("RuKDID"));
                        fpService.INSERTFaPRuKD(sum.ToString(), RuKDID.Text);
                    }
                }
                this.Page.RegisterStartupScript("onclick", "<script>alert('保存成功！')</script>");
                lbCaiGFPID.Text = sum.ToString();
                lbnStatus.Text = "0";
                BANDBOTTON("1,2,3,4,9");
            }
            catch
            {
                this.Page.RegisterStartupScript("onclick", "<script>alert('保存出错！')</script>");
            }
        }

        protected void btnQX_Click(object sender, EventArgs e)
        {
            BAND();
            BANDEnabled(false);
            BANDBOTTON("1,2,3,4,9");
        }

        protected void btnTJ_Click(object sender, EventArgs e)
        {
            if (lbISLB.Text == "FALSE")
            {
                if (lbCaiGFPID.Text != "0")
                {
                    if (lbnStatus.Text == "0")
                    {
                        try
                        {
                            fpService.UPDATE_CaiGFP(lbCaiGFPID.Text);
                            lbnStatus.Text = "1";
                            this.Page.RegisterStartupScript("onclick", "<script>alert('提交成功！')</script>");
                        }
                        catch
                        {
                            this.Page.RegisterStartupScript("onclick", "<script>alert('提交失败！')</script>");
                        }
                    }
                    else
                    {
                        this.Page.RegisterStartupScript("onclick", "<script>alert('该单据已提交，无需再次提交！')</script>");
                    }
                }
                else
                {
                    this.Page.RegisterStartupScript("onclick", "<script>alert('请选择需要提交的单据！')</script>");
                }
            }
            else
            {
                this.Page.RegisterStartupScript("onclick", "<script>alert('请选择需要提交的单据！')</script>");
            }
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            LinkButton1.BackColor = System.Drawing.Color.DarkGray;
            LinkButton2.BackColor = System.Drawing.Color.White;
            divFPLR.Visible = true;
            divFPCXLB.Visible = false;
            lbISLB.Text = "FALSE";
        }

        protected void LinkButton2_Click(object sender, EventArgs e)
        {
            LinkButton1.BackColor = System.Drawing.Color.White;
            LinkButton2.BackColor = System.Drawing.Color.DarkGray;
            divFPLR.Visible = false;
            divFPCXLB.Visible = true;
            lbISLB.Text = "TRUE";
        }

        protected void btnADDGLBD_Click(object sender, EventArgs e)
        {
            INSERT_CaiGFPYZ();
        }

        protected void gdFPMXLB_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow) //判断是否是DataRow，以防止鼠标经过Header也有效果   
            {
                e.Row.Attributes.Add("onmouseover", "e=this.style.backgroundColor; this.style.backgroundColor='#CCCCCC'");
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=e");
            }
        }

        protected void gdRKDH_RowDataBound(object sender, GridViewRowEventArgs e)
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
            Label nStatus = ((Label)gdFPMXLB.Rows[drv.RowIndex].FindControl("nStatus"));
            lbCaiGFPID.Text = CaiGFPID.Text;
            lbnStatus.Text = nStatus.Text;
            BAND();
            divFPCXLB.Visible = false;
            divFPLR.Visible = true;
            LinkButton1.BackColor = System.Drawing.Color.DarkGray;
            LinkButton2.BackColor = System.Drawing.Color.White;
            lbISLB.Text = "FALSE";
            BANDBOTTON("1,2,3,4,9");
            BANDEnabled(false);
            //lbRKDRI.Text = RuKDID.Text.Trim();
            //LinkButton1.BackColor = System.Drawing.Color.DarkGray;
            //LinkButton2.BackColor = System.Drawing.Color.White;
            //divRKDTX.Visible = true;
            //divRKDLB.Visible = false;
            //lbISSHOW.Text = "TRUE";
        }

        private void LOGINTIMEOUT()
        {
            if (Session["STAFFID"] == null)
            {
                Response.Redirect("../login.aspx?RI=1");
            }
        }

        protected void txtFPJE_TextChanged(object sender, EventArgs e)
        {
            double sum = 0;
            try
            {
                sum = Convert.ToDouble(txtFPJE.Text.Trim());
            }
            catch
            {
                this.Page.RegisterStartupScript("onclick", "<script>alert('发票金额框填写数据异常，调整为入库金额！')</script>");
                sum = Convert.ToDouble(txtRKJE.Text.Trim());
            }
            txtFPJE.Text = Math.Round(sum, 2).ToString();
            txtJKK.Text = Math.Round((Convert.ToDouble(txtFPJE.Text.Trim()) - Convert.ToDouble(txtRKJE.Text.Trim())), 2).ToString();
        }

        private void INSERT_CaiGFPYZ()
        {
            //int sum = LBRKDMX.Items.Count;
            //string rkdh = "";
            //if (sum > 0)
            //{
            //    for (int i = 0; i < sum; i++)
            //    {
            //        rkdh = rkdh + " " + LBRKDMX.Items[i].Value;
            //    }
            //}
            string rkdh = "";
            if (gdRKDLB.Rows.Count > 0)
            {
                for (int i = 0; i < gdRKDLB.Rows.Count; i++)
                {
                    Label RuKDID = ((Label)gdRKDLB.Rows[i].FindControl("RuKDID"));
                    rkdh = rkdh + " " + RuKDID.Text;
                }
            }
            DataTable dt = fpService.INSERT_CaiGFPYZ(ddlGYS.SelectedValue, txtRZRQ.Text.Trim(), ddlRZLX.SelectedValue, txtFPRQ.Text.Trim(), txtFPJE.Text.Trim(), txtFKJE.Text.Trim(), txtFPHM.Text.Trim(), txtRKJE.Text.Trim(), txtJKK.Text.Trim(), txtFPSM.Text.Trim(), txtHDQRSM.Text.Trim(), rkdh);
            Response.Redirect("FPYZGLDH.aspx?RI=" + dt.Rows[0][0].ToString() + "&LB=1&GYSBH=" + ddlGYS.SelectedValue + "");
        }

        protected void btnDELETEGLBD_Click(object sender, EventArgs e)
        {
            DELETEROW();
        }

        private void DELETEROW()
        {
            //DataTable dt = fpService.GETrukd(LBRKDMX.SelectedValue);
            //double sum = 0;
            //if (dt.Rows.Count > 0)
            //{
            //    sum = Convert.ToDouble(dt.Rows[0]["RuKTotal"].ToString());
            //}
            //txtRKJE.Text = Math.Round((Convert.ToDouble(txtRKJE.Text) - sum), 2).ToString();
            //txtFPJE.Text = txtRKJE.Text;
            //txtJKK.Text = "0";
            //LBRKDMX.Items.Remove(LBRKDMX.SelectedItem);
            double Total = 0;
            DataTable dtINFO = new DataTable();
            dtINFO.Columns.Add("RuKDNO", typeof(string));
            dtINFO.Columns.Add("RuKTotal", typeof(string));
            dtINFO.Columns.Add("RuKDate", typeof(string));
            dtINFO.Columns.Add("RuKDID", typeof(string));
            if (gdRKDLB.Rows.Count > 0)
            {
                int sum = 0;
                for (int i = 0; i < gdRKDLB.Rows.Count; i++)
                {
                    Label RuKDNO = (Label)gdRKDLB.Rows[i].FindControl("RuKDNO");
                    Label RuKTotal = (Label)gdRKDLB.Rows[i].FindControl("RuKTotal");
                    Label RuKDate = (Label)gdRKDLB.Rows[i].FindControl("RuKDate");
                    Label RuKDID = (Label)gdRKDLB.Rows[i].FindControl("RuKDID");
                    if (RuKDID.Text != lbRKDID.Text)
                    {
                        dtINFO.Rows.Add();
                        dtINFO.Rows[sum]["RuKDNO"] = RuKDNO.Text;
                        dtINFO.Rows[sum]["RuKTotal"] = RuKTotal.Text;
                        dtINFO.Rows[sum]["RuKDate"] = RuKDate.Text;
                        dtINFO.Rows[sum]["RuKDID"] = RuKDID.Text;
                        sum = sum + 1;
                    }
                    else
                    {
                        Total = Convert.ToDouble(RuKTotal.Text);
                    }

                }
            }
            gdRKDLB.DataSource = dtINFO;
            gdRKDLB.DataBind();
            BANDRKDLB();
            txtRKJE.Text = Math.Round((Convert.ToDouble(txtRKJE.Text) - Total), 2).ToString();
            txtFPJE.Text = txtRKJE.Text;
            txtJKK.Text = "0";
        }

        private void BAND()
        {
            if (lbCaiGFPID.Text != "0" && lbCaiGFPID.Text != "")
            {
                //LBRKDMX.Items.Clear();
                gdRKDLB.DataSource = null;
                gdRKDLB.DataBind();
                BANDRKDLB();
                DataTable dt = fpService.GETCaiGFP(lbCaiGFPID.Text);
                if (dt.Rows.Count > 0)
                {
                    ddlGYS.SelectedValue = dt.Rows[0]["ClientID"].ToString();
                    txtRZRQ.Text = Convert.ToDateTime(dt.Rows[0]["ACDate"].ToString()).ToString("yyyy-MM-dd");
                    ddlRZLX.SelectedValue = dt.Rows[0]["ACType"].ToString();
                    txtFPRQ.Text = Convert.ToDateTime(dt.Rows[0]["FaPDate"].ToString()).ToString("yyyy-MM-dd");
                    txtFPJE.Text = Math.Round(Convert.ToDouble(dt.Rows[0]["FaPTotal"].ToString()), 2).ToString();
                    txtFKJE.Text = Math.Round(Convert.ToDouble(dt.Rows[0]["PayTotal"].ToString()), 2).ToString();
                    txtFPHM.Text = dt.Rows[0]["CaiGFPNO"].ToString();
                    txtRKJE.Text = Math.Round(Convert.ToDouble(dt.Rows[0]["RuKTotal"].ToString()), 2).ToString();
                    txtJKK.Text = Math.Round(Convert.ToDouble(dt.Rows[0]["FaPTotal"].ToString()) - Convert.ToDouble(dt.Rows[0]["RuKTotal"].ToString()), 2).ToString();
                    txtFPSM.Text = dt.Rows[0]["FaPMem"].ToString();
                    txtHDQRSM.Text = dt.Rows[0]["RePayMemo"].ToString();
                    DataTable dtRuKD = fpService.GETFaPRuKDALL(lbCaiGFPID.Text);
                    double rkje = 0;
                    if (dtRuKD.Rows.Count > 0)
                    {
                        //for (int i = dtRuKD.Rows.Count - 1; i >= 0; i--)
                        //{
                        //    DataTable dtruk = fpService.GETrukd(dtRuKD.Rows[i]["RuKDID"].ToString());
                        //    if (dtruk.Rows.Count > 0)
                        //    {
                        //        ListItem item = new ListItem();
                        //        item.Text = dtruk.Rows[0]["RuKDNO"].ToString() + "    ¥" + Math.Round(Convert.ToDouble(dtruk.Rows[0]["RuKTotal"].ToString()), 2) + "    " + Convert.ToDateTime(dtruk.Rows[0]["RuKDate"].ToString()).ToString("yyyy-MM-dd");
                        //        item.Value = dtRuKD.Rows[i]["RuKDID"].ToString();
                        //        LBRKDMX.Items.Insert(0, item);
                        //        rkje = rkje + Convert.ToDouble(dtruk.Rows[0]["RuKTotal"].ToString());
                        //    }

                        //}
                        //if (dtRuKD.Rows.Count > 0)
                        //{
                        //    for (int j = 0; j < dtRuKD.Rows.Count; j++)
                        //    {
                        //        rkje = rkje + Convert.ToDouble(dtRuKD.Rows[j]["RuKTotal"].ToString());
                        //    }
                        //}
                        gdRKDLB.DataSource = dtRuKD;
                        gdRKDLB.DataBind();
                        BANDRKDLB();
                    }
                    //txtFPJE.Text = Math.Round(rkje, 2).ToString();
                    //txtRKJE.Text = Math.Round(rkje, 2).ToString();
                    //txtJKK.Text = "0";
                }
            }
        }

        private void BANDEnabled(bool s)
        {
            //ddlGYS.Enabled = s;
            //txtRZRQ.Enabled = s;
            //ddlRZLX.Enabled = s;
            //txtFPRQ.Enabled = s;
            //txtFPJE.Enabled = s;
            //txtFKJE.Enabled = s;
            //txtFPHM.Enabled = s;
            //txtFPSM.Enabled = s;
        }

        private void BANDCLEAR()
        {
            ddlGYS.SelectedValue = "0";
            txtRZRQ.Text = DateTime.Now.ToString("yyyy-MM-dd");
            ddlRZLX.SelectedValue = "0";
            txtFPRQ.Text = DateTime.Now.ToString("yyyy-MM-dd");
            txtFPJE.Text = "0";
            txtFKJE.Text = "0";
            txtFPHM.Text = "";
            txtRKJE.Text = "0";
            txtJKK.Text = "0";
            txtFPSM.Text = "";
            txtHDQRSM.Text = "";
            txtRKCKBH.Text = "";
            txtRKCKMC.Text = "";
            txtRKGYSBH.Text = "";
            txtRKGYSMC.Text = "";
            txtRKRKRQ.Text = "";
            ddlRKRKLX.SelectedValue = "-1";
            txtRKRKDH.Text = "";
            gdRKDH.DataSource = null;
            gdRKDH.DataBind();
            txtRKRKBZ.Text = "";
            txtRKZDR.Text = "";
            txtRKZDSJ.Text = "";
            gdRKDLB.DataSource = null;
            gdRKDLB.DataBind();
            BANDRKDLB();
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
            lbRKDID.Text = RuKDID.Text;
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

        private void BANDRKDLB()
        {
            double sumZJE = 0;
            if (gdRKDLB.Rows.Count > 0)
            {
                for (int i = 0; i < gdRKDLB.Rows.Count; i++)
                {
                    Label RuKTotal = ((Label)gdRKDLB.Rows[i].FindControl("RuKTotal"));
                    try
                    {
                        sumZJE = sumZJE + Convert.ToDouble(RuKTotal.Text);
                    }
                    catch
                    {
                        sumZJE = sumZJE + 0;
                    }
                }
                GridViewRow foot = gdRKDLB.FooterRow;
                foot.Cells[1].HorizontalAlign = HorizontalAlign.Right;
                foot.Cells[1].Text = sumZJE.ToString();
            }
        }
    }
}