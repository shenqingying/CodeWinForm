using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bussiness.STSY;

namespace STSY.JDGL
{
    public partial class JDDJ : System.Web.UI.Page
    {
        ZDService zdService = new ZDService();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LOGINTIMEOUT();
                lbtnJDDJ.BackColor = System.Drawing.Color.DarkGray;
                lbtnJDDJLB.BackColor = System.Drawing.Color.White;
                divJDDJ.Visible = true;
                divJDDJLB.Visible = false;
                rbLB.SelectedValue = "0";
                BANDddlTZBM();
                BANDddlLXR();
                BANDddlLBTZBM();
                BANDddlLBLXR();
                BANDddlLBDD();
                BANDddlLBLX();
                cbLBXQRQ.Checked = true;
                txtLBXQRQS.Enabled = true;
                txtLBXQRQE.Enabled = true;
                DateTime now = DateTime.Now;
                DateTime d1 = new DateTime(now.Year, now.Month, 1);
                txtLBXQRQS.Text = d1.ToString("yyyy-MM-dd");
                txtLBXQRQE.Text = now.ToString("yyyy-MM-dd");
                BAND();
                BANDBOTTON("1,4,5,6,7");
                txtZDR.Text = Session["STAFFNAME"].ToString();
                txtZDSJ.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            }
        }

        protected void btnCXSX_Click(object sender, EventArgs e)
        {
            if (lbISLB.Text.ToUpper() == "TRUE")
            {
                BANDLB();
            }
        }

        protected void btnADD_Click(object sender, EventArgs e)
        {
            if (lbISLB.Text.ToUpper() == "FALSE")
            {
                ISENABLED(true);
                CLEARALL();
                BANDBOTTON("1,4,5,6,7");
            }
        }

        protected void btnED_Click(object sender, EventArgs e)
        {
            if (lbISLB.Text.ToUpper() == "FALSE")
            {
                if (lbZDSTATUS.Text == "0")
                {
                    ISENABLED(true);
                    BANDBOTTON("1,4,5,6,7");
                }
                else
                {
                    this.Page.RegisterStartupScript("onclick", "<script>alert('单据已提交不能修改!')</script>");
                }
            }
        }

        protected void btnADDROW_Click(object sender, EventArgs e)
        {
            if (lbISLB.Text.ToUpper() == "FALSE")
            {
                ADDROW();
            }
        }

        protected void btnDELETEROW_Click(object sender, EventArgs e)
        {
            if (lbISLB.Text.ToUpper() == "FALSE")
            {
                DELETEROW();
            }
        }

        protected void btnSAVE_Click(object sender, EventArgs e)
        {
            if (ddlTZBM.SelectedValue == "0")
            {
                this.Page.RegisterStartupScript("onclick", "<script>alert('请选择通知部门!')</script>");
                return;
            }
            if (ddlLXR.SelectedValue == "0")
            {
                this.Page.RegisterStartupScript("onclick", "<script>alert('请选择联系人!')</script>");
                return;
            }
            if (lbISLB.Text.ToUpper() == "FALSE")
            {
                if (lbRI.Text == "0")
                {
                    int s = Convert.ToInt32(zdService.SELECT_CRM_ZD_RI().Rows[0][0].ToString());
                    s = s + 1;
                    string TZBM = "";
                    if (ddlTZBM.SelectedValue != "0")
                    {
                        TZBM = ddlTZBM.SelectedValue;
                    }
                    string TZR = "";
                    string TZDH = "";
                    if (ddlLXR.SelectedValue != "0")
                    {
                        string[] str = ddlLXR.SelectedItem.Text.Split(' ');
                        if (str.Length == 2)
                        {
                            TZR = str[0];
                            TZDH = str[1];
                        }
                        else if (str.Length == 1)
                        {
                            TZR = str[0];
                        }
                    }
                    zdService.insertCRM_ZD(s.ToString(), "0", "0", "1", "1", TZBM, TZR, TZDH, Session["STAFFID"].ToString(), Session["STAFFNAME"].ToString(), DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), txtBZ.Text);
                    zdService.DELETE_CRM_ZDXM(s.ToString());
                    if (gdZDDJ.Rows.Count > 0)
                    {
                        int s1 = 1;
                        for (int i = 0; i < gdZDDJ.Rows.Count; i++)
                        {
                            if (((DropDownList)gdZDDJ.Rows[i].FindControl("ddlLX")).SelectedValue != "0")
                            {
                                string LXID = ((DropDownList)gdZDDJ.Rows[i].FindControl("ddlLX")).SelectedValue;
                                string LXNAME = ((DropDownList)gdZDDJ.Rows[i].FindControl("ddlLX")).SelectedItem.Text;
                                string XQRQ = ((TextBox)gdZDDJ.Rows[i].FindControl("txtXQRQ")).Text;
                                string XQSJ = ((DropDownList)gdZDDJ.Rows[i].FindControl("ddlXQSJ")).SelectedValue;
                                if (XQSJ == "0")
                                {
                                    XQSJ = "";
                                }
                                string dd = ((DropDownList)gdZDDJ.Rows[i].FindControl("ddlDD")).SelectedValue;
                                if (dd == "0")
                                {
                                    dd = "";
                                }
                                string sl = ((TextBox)gdZDDJ.Rows[i].FindControl("txtSL")).Text;
                                string XQSM = ((TextBox)gdZDDJ.Rows[i].FindControl("txtXQSM")).Text;
                                zdService.insertCRM_ZDXM(s1.ToString(), s.ToString(), "0", LXID, LXNAME, XQRQ, XQSJ, dd, sl, "0", "0", "0", XQSM);
                                s1 = s1 + 1;
                            }
                        }
                    }
                    zdService.DELETE_CRM_ZDXM(lbRI.Text);
                    zdService.DELETE_CRM_ZD(lbRI.Text);
                    this.Page.RegisterStartupScript("onclick", "<script>alert('保存成功!')</script>");
                    lbRI.Text = s.ToString();
                    ISENABLED(false);
                    lbISED.Text = "FALSE";
                    BANDBOTTON("1,2,3,8");
                }
                else
                {
                    int s = Convert.ToInt32(zdService.SELECT_CRM_ZD_RI().Rows[0][0].ToString());
                    s = s + 1;
                    string TZBM = "";
                    if (ddlTZBM.SelectedValue != "0")
                    {
                        TZBM = ddlTZBM.SelectedValue;
                    }
                    string TZR = "";
                    string TZDH = "";
                    if (ddlLXR.SelectedValue != "0")
                    {
                        string[] str = ddlLXR.SelectedItem.Text.Split(' ');
                        if (str.Length == 2)
                        {
                            TZR = str[0];
                            TZDH = str[1];
                        }
                        else if (str.Length == 1)
                        {
                            TZR = str[0];
                        }
                    }
                    zdService.insertCRM_ZD(s.ToString(), "0", "0", "1", "1", TZBM, TZR, TZDH, Session["STAFFID"].ToString(), Session["STAFFNAME"].ToString(), DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), txtBZ.Text);
                    zdService.DELETE_CRM_ZDXM(s.ToString());
                    if (gdZDDJ.Rows.Count > 0)
                    {
                        int s1 = 1;
                        for (int i = 0; i < gdZDDJ.Rows.Count; i++)
                        {
                            if (((DropDownList)gdZDDJ.Rows[i].FindControl("ddlLX")).SelectedValue != "0")
                            {
                                string LXID = ((DropDownList)gdZDDJ.Rows[i].FindControl("ddlLX")).SelectedValue;
                                string LXNAME = ((DropDownList)gdZDDJ.Rows[i].FindControl("ddlLX")).SelectedItem.Text;
                                string XQRQ = ((TextBox)gdZDDJ.Rows[i].FindControl("txtXQRQ")).Text;
                                string XQSJ = ((DropDownList)gdZDDJ.Rows[i].FindControl("ddlXQSJ")).SelectedValue;
                                if (XQSJ == "0")
                                {
                                    XQSJ = "";
                                }
                                string dd = ((DropDownList)gdZDDJ.Rows[i].FindControl("ddlDD")).SelectedValue;
                                if (dd == "0")
                                {
                                    dd = "";
                                }
                                string sl = ((TextBox)gdZDDJ.Rows[i].FindControl("txtSL")).Text;
                                string XQSM = ((TextBox)gdZDDJ.Rows[i].FindControl("txtXQSM")).Text;
                                zdService.insertCRM_ZDXM(s1.ToString(), s.ToString(), "0", LXID, LXNAME, XQRQ, XQSJ, dd, sl, "0", "0", "0", XQSM);
                                s1 = s1 + 1;
                            }
                        }
                    }
                    this.Page.RegisterStartupScript("onclick", "<script>alert('修改成功!')</script>");
                    zdService.DELETE_CRM_ZDXM(lbRI.Text);
                    zdService.DELETE_CRM_ZD(lbRI.Text);
                    lbRI.Text = s.ToString();
                    ISENABLED(false);
                    lbISED.Text = "FALSE";
                    BANDBOTTON("1,2,3,8");
                }
            }
            else
            {
                this.Page.RegisterStartupScript("onclick", "<script>alert('请在招待登记界面点击保存按钮!')</script>");
            }
        }

        protected void btnQX_Click(object sender, EventArgs e)
        {
            BAND();
            ISENABLED(false);
            BANDBOTTON("1,2,3,8");
        }

        protected void btnDJTJ_Click(object sender, EventArgs e)
        {
            if (lbRI.Text != "0")
            {
                if (lbZDSTATUS.Text == "0")
                {
                    zdService.UPDATE_CRM_ZD("1", lbRI.Text);
                    this.Page.RegisterStartupScript("onclick", "<script>alert('单据提交成功!')</script>");
                }
                else
                {
                    this.Page.RegisterStartupScript("onclick", "<script>alert('单据已提交，无需重复提交!')</script>");
                }
            }
            else
            {
                this.Page.RegisterStartupScript("onclick", "<script>alert('请先选择所需要提交的单据并点击查看!')</script>");
            }
        }

        protected void gdZDDJ_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow) //判断是否是DataRow，以防止鼠标经过Header也有效果   
            {
                e.Row.Attributes.Add("onmouseover", "e=this.style.backgroundColor; this.style.backgroundColor='#CCCCCC'");
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=e");

                DropDownList ddlLX = ((DropDownList)e.Row.FindControl("ddlLX"));
                DataTable dt = zdService.SELECT_QX(Session["STAFFID"].ToString());
                ddlLX.DataSource = dt;
                ddlLX.DataTextField = "XMTYPE";
                ddlLX.DataValueField = "XMTPYEID";
                ddlLX.DataBind();
                ListItem item = new ListItem();
                item.Text = "=请选择=";
                item.Value = "0";
                ddlLX.Items.Insert(0, item);

                DropDownList ddlDD = ((DropDownList)e.Row.FindControl("ddlDD"));
                DataTable dtddlDD = zdService.SELECT_CRM_ZDADD();
                ddlDD.DataSource = dtddlDD;
                ddlDD.DataTextField = "ADDNAME";
                ddlDD.DataValueField = "ADDNAME";
                ddlDD.DataBind();
                ListItem item1 = new ListItem();
                item1.Text = "=请选择=";
                item1.Value = "0";
                ddlDD.Items.Insert(0, item1);
            }
        }

        protected void gdZDDJLB_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow) //判断是否是DataRow，以防止鼠标经过Header也有效果   
            {
                e.Row.Attributes.Add("onmouseover", "e=this.style.backgroundColor; this.style.backgroundColor='#CCCCCC'");
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=e");
            }
        }

        protected void lbtnJDDJ_Click(object sender, EventArgs e)
        {
            lbtnJDDJ.BackColor = System.Drawing.Color.DarkGray;
            lbtnJDDJLB.BackColor = System.Drawing.Color.White;
            divJDDJ.Visible = true;
            divJDDJLB.Visible = false;
            lbISLB.Text = "FALSE";
        }

        protected void lbtnJDDJLB_Click(object sender, EventArgs e)
        {
            lbtnJDDJ.BackColor = System.Drawing.Color.White;
            lbtnJDDJLB.BackColor = System.Drawing.Color.DarkGray;
            divJDDJ.Visible = false;
            divJDDJLB.Visible = true;
            lbISLB.Text = "TRUE";
        }

        protected void cbLBXQRQ_CheckedChanged(object sender, EventArgs e)
        {
            if (cbLBXQRQ.Checked == true)
            {
                txtLBXQRQS.Enabled = true;
                txtLBXQRQE.Enabled = true;
            }
            else
            {
                txtLBXQRQS.Enabled = false;
                txtLBXQRQE.Enabled = false;
            }
        }

        protected void ddlLX_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList t = (DropDownList)sender;
            GridViewRow drv = (GridViewRow)t.NamingContainer;
            int rowIndex = drv.RowIndex;
            string coid = ((DropDownList)gdZDDJ.Rows[drv.RowIndex].FindControl("ddlLX")).SelectedValue;
            TextBox txtXQRQ = ((TextBox)gdZDDJ.Rows[drv.RowIndex].FindControl("txtXQRQ"));
            txtXQRQ.Text = DateTime.Now.ToString("yyyy-MM-dd");
        }

        protected void lbtnFIND_OnClick(object sender, EventArgs e)
        {
            LinkButton t = (LinkButton)sender;
            GridViewRow drv = (GridViewRow)t.NamingContainer;
            int rowIndex = drv.RowIndex;
            Label ZDID = ((Label)gdZDDJLB.Rows[drv.RowIndex].FindControl("ZDID"));
            Label ZDSTATUS = ((Label)gdZDDJLB.Rows[drv.RowIndex].FindControl("ZDSTATUS"));
            lbRI.Text = ZDID.Text;
            lbZDSTATUS.Text = ZDSTATUS.Text;
            lbtnJDDJ.BackColor = System.Drawing.Color.DarkGray;
            lbtnJDDJLB.BackColor = System.Drawing.Color.White;
            divJDDJ.Visible = true;
            divJDDJLB.Visible = false;
            lbISLB.Text = "FALSE";
            ISENABLED(false);
            BANDBOTTON("1,2,3,8");
            BAND();
        }

        private void LOGINTIMEOUT()
        {
            if (Session["STAFFID"] == null)
            {
                Response.Redirect("../login.aspx?RI=1");
            }
        }

        private void BANDddlTZBM()
        {
            DataTable dt = zdService.SELECT_CRM_DEPT();
            ddlTZBM.DataSource = dt;
            ddlTZBM.DataTextField = "DEPTNAME";
            ddlTZBM.DataValueField = "DEPTNAME";
            ddlTZBM.DataBind();
            ListItem item = new ListItem();
            item.Text = "=请选择=";
            item.Value = "0";
            ddlTZBM.Items.Insert(0, item);
        }

        private void BANDddlLXR()
        {
            DataTable dt = zdService.SELECT_CRM_ZDLXR();
            dt.Columns.Add("XZ", typeof(string));
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dt.Rows[i]["XZ"] = dt.Rows[i]["LXR"].ToString() + " " + dt.Rows[i]["LXDH"].ToString();
                }
            }
            ddlLXR.DataSource = dt;
            ddlLXR.DataTextField = "XZ";
            ddlLXR.DataValueField = "LXR";
            ddlLXR.DataBind();
            ListItem item = new ListItem();
            item.Text = "=请选择=";
            item.Value = "0";
            ddlLXR.Items.Insert(0, item);
        }

        private void BANDddlLBTZBM()
        {
            DataTable dt = zdService.SELECT_CRM_DEPT();
            ddlLBTZBM.DataSource = dt;
            ddlLBTZBM.DataTextField = "DEPTNAME";
            ddlLBTZBM.DataValueField = "DEPTID";
            ddlLBTZBM.DataBind();
            ListItem item = new ListItem();
            item.Text = "=请选择=";
            item.Value = "0";
            ddlLBTZBM.Items.Insert(0, item);
        }

        private void BANDddlLBLXR()
        {
            DataTable dt = zdService.SELECT_CRM_ZDLXR();
            ddlLBLXR.DataSource = dt;
            ddlLBLXR.DataTextField = "LXR";
            ddlLBLXR.DataValueField = "LXRID";
            ddlLBLXR.DataBind();
            ListItem item = new ListItem();
            item.Text = "=请选择=";
            item.Value = "0";
            ddlLBLXR.Items.Insert(0, item);
        }

        private void BANDddlLBDD()
        {
            DataTable dt = zdService.SELECT_CRM_ZDADD();
            ddlLBDD.DataSource = dt;
            ddlLBDD.DataTextField = "ADDNAME";
            ddlLBDD.DataValueField = "ADDID";
            ddlLBDD.DataBind();
            ListItem item = new ListItem();
            item.Text = "=请选择=";
            item.Value = "0";
            ddlLBDD.Items.Insert(0, item);
        }

        private void BANDddlLBLX()
        {
            DataTable dt = zdService.SELECT_QX(Session["STAFFID"].ToString());
            ddlLBLX.DataSource = dt;
            ddlLBLX.DataTextField = "XMTYPE";
            ddlLBLX.DataValueField = "XMTPYEID";
            ddlLBLX.DataBind();
            ListItem item = new ListItem();
            item.Text = "=请选择=";
            item.Value = "0";
            ddlLBLX.Items.Insert(0, item);
        }

        private void BANDLB()
        {
            string TZBM = "";
            if (ddlLBTZBM.SelectedValue != "0")
            {
                TZBM = ddlLBTZBM.SelectedItem.Text;
            }
            string LXR = "";
            if (ddlLBLXR.SelectedValue != "0")
            {
                LXR = ddlLBLXR.SelectedItem.Text;
            }
            string XQRQS = "";
            string XQRQE = "";
            if (cbLBXQRQ.Checked == true)
            {
                XQRQS = txtLBXQRQS.Text;
                XQRQE = txtLBXQRQE.Text;
            }
            string DD = "";
            if (ddlLBDD.SelectedValue != "0")
            {
                DD = ddlLBDD.SelectedItem.Text;
            }
            DataTable dt = zdService.SELECT_ZDXM(Session["STAFFID"].ToString(), rbLB.SelectedValue, TZBM, LXR, txtLBBZ.Text, XQRQS, XQRQE, DD, ddlLBLX.SelectedValue, txtLBXQSM.Text);
            dt.Columns.Add("XMDATE1", typeof(string));
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dt.Rows[i]["XMDATE1"] = Convert.ToDateTime(dt.Rows[i]["XMDATE"].ToString()).ToString("yyyy-MM-dd");
                }
            }
            gdZDDJLB.DataSource = dt;
            gdZDDJLB.DataBind();
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    CheckBox YQR = (CheckBox)gdZDDJLB.Rows[i].FindControl("YQR");
                    YQR.Checked = Convert.ToBoolean(dt.Rows[i]["ISCONFIG"].ToString());
                    CheckBox YJS = (CheckBox)gdZDDJLB.Rows[i].FindControl("YJS");
                    YJS.Checked = Convert.ToBoolean(dt.Rows[i]["ISEND"].ToString());
                }
            }
        }

        private void BAND()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("LX", typeof(string));
            dt.Columns.Add("XQRQ", typeof(string));
            dt.Columns.Add("XQSJ", typeof(string));
            dt.Columns.Add("DD", typeof(string));
            dt.Columns.Add("SL", typeof(string));
            dt.Columns.Add("YQR", typeof(bool));
            dt.Columns.Add("YJS", typeof(bool));
            dt.Columns.Add("XQSM", typeof(string));
            if (lbRI.Text == "0")
            {
                ddlTZBM.SelectedValue = "0";
                ddlLXR.SelectedValue = "0";
                txtBZ.Text = "";
                txtZDR.Text = Session["STAFFNAME"].ToString();
                txtZDSJ.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm;ss");
                for (int i = 0; i < 5; i++)
                {
                    dt.Rows.Add();
                    dt.Rows[i]["LX"] = "0";
                    dt.Rows[i]["XQRQ"] = "";
                    dt.Rows[i]["XQSJ"] = "0";
                    dt.Rows[i]["DD"] = "0";
                    dt.Rows[i]["SL"] = "";
                    dt.Rows[i]["YQR"] = false;
                    dt.Rows[i]["YJS"] = false;
                    dt.Rows[i]["XQSM"] = "";
                }
            }
            else
            {
                DataTable dtZD = zdService.SELECT_CRM_ZD(lbRI.Text);
                if (dtZD.Rows.Count > 0)
                {
                    if (dtZD.Rows[0]["DEPTNAME"].ToString() == "")
                    {
                        ddlTZBM.SelectedValue = "0";
                    }
                    else
                    {
                        ddlTZBM.SelectedValue = dtZD.Rows[0]["DEPTNAME"].ToString();
                    }
                    if (dtZD.Rows[0]["STAFFNAME"].ToString() == "")
                    {
                        ddlLXR.SelectedValue = "0";
                    }
                    else
                    {
                        ddlLXR.SelectedValue = dtZD.Rows[0]["STAFFNAME"].ToString();
                    }
                    txtBZ.Text = dtZD.Rows[0]["ZDMEMO"].ToString();
                    txtZDR.Text = dtZD.Rows[0]["fillName"].ToString();
                    txtZDSJ.Text = dtZD.Rows[0]["fillTime"].ToString();
                }
                DataTable dtZDXM = zdService.SELECT_CRM_ZDXM_QX(lbRI.Text, Session["STAFFID"].ToString());
                if (dtZDXM.Rows.Count > 0)
                {
                    for (int i = 0; i < dtZDXM.Rows.Count; i++)
                    {
                        dt.Rows.Add();
                        dt.Rows[i]["LX"] = dtZDXM.Rows[i]["XMTPYEID"].ToString();
                        dt.Rows[i]["XQRQ"] = Convert.ToDateTime(dtZDXM.Rows[i]["XMDATE"].ToString()).ToString("yyyy-MM-dd");
                        if (dtZDXM.Rows[i]["XMTIME"].ToString() == "")
                        {
                            dt.Rows[i]["XQSJ"] = "0";
                        }
                        else
                        {
                            dt.Rows[i]["XQSJ"] = dtZDXM.Rows[i]["XMTIME"].ToString();
                        }
                        if (dtZDXM.Rows[i]["XMADD"].ToString() == "")
                        {
                            dt.Rows[i]["DD"] = "0";
                        }
                        else
                        {
                            dt.Rows[i]["DD"] = dtZDXM.Rows[i]["XMADD"].ToString();
                        }
                        dt.Rows[i]["SL"] = dtZDXM.Rows[i]["XMNUM"].ToString();
                        dt.Rows[i]["YQR"] = dtZDXM.Rows[i]["ISCONFIG"].ToString();
                        dt.Rows[i]["YJS"] = dtZDXM.Rows[i]["ISEND"].ToString();
                        dt.Rows[i]["XQSM"] = dtZDXM.Rows[i]["XMMEMO"].ToString();
                    }
                }
            }
            gdZDDJ.DataSource = dt;
            gdZDDJ.DataBind();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DropDownList ddlLX = (DropDownList)gdZDDJ.Rows[i].FindControl("ddlLX");
                ddlLX.SelectedValue = dt.Rows[i]["LX"].ToString();
                DropDownList ddlXQSJ = (DropDownList)gdZDDJ.Rows[i].FindControl("ddlXQSJ");
                ddlXQSJ.SelectedValue = dt.Rows[i]["XQSJ"].ToString();
                DropDownList ddlDD = (DropDownList)gdZDDJ.Rows[i].FindControl("ddlDD");
                ddlDD.SelectedValue = dt.Rows[i]["DD"].ToString();
                CheckBox cbYQR = (CheckBox)gdZDDJ.Rows[i].FindControl("cbYQR");
                cbYQR.Checked = Convert.ToBoolean(dt.Rows[i]["YQR"]);
                CheckBox cbYJS = (CheckBox)gdZDDJ.Rows[i].FindControl("cbYJS");
                cbYJS.Checked = Convert.ToBoolean(dt.Rows[i]["YJS"]);
            }
        }

        private void BANDBOTTON(string str)
        {
            string[] str1 = str.Split(',');
            btnCXSX.Enabled = false;
            btnCXSX.BackColor = System.Drawing.Color.Gray;
            btnADD.Enabled = false;
            btnADD.BackColor = System.Drawing.Color.Gray;
            btnED.Enabled = false;
            btnED.BackColor = System.Drawing.Color.Gray;
            btnADDROW.Enabled = false;
            btnADDROW.BackColor = System.Drawing.Color.Gray;
            btnDELETEROW.Enabled = false;
            btnDELETEROW.BackColor = System.Drawing.Color.Gray;
            btnSAVE.Enabled = false;
            btnSAVE.BackColor = System.Drawing.Color.Gray;
            btnQX.Enabled = false;
            btnQX.BackColor = System.Drawing.Color.Gray;
            btnDJTJ.Enabled = false;
            btnDJTJ.BackColor = System.Drawing.Color.Gray;
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
                        btnED.Enabled = true;
                        btnED.BackColor = System.Drawing.Color.FromName("#3498db");
                    }
                    else if (str1[i] == "4")
                    {
                        btnADDROW.Enabled = true;
                        btnADDROW.BackColor = System.Drawing.Color.FromName("#3498db");
                    }
                    else if (str1[i] == "5")
                    {
                        btnDELETEROW.Enabled = true;
                        btnDELETEROW.BackColor = System.Drawing.Color.FromName("#3498db");
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
                    else if (str1[i] == "8")
                    {
                        btnDJTJ.Enabled = true;
                        btnDJTJ.BackColor = System.Drawing.Color.FromName("#3498db");
                    }

                }
            }
        }

        private void CLEARALL()
        {
            lbISLB.Text = "FALSE";
            lbRI.Text = "0";
            divJDDJ.Visible = true;
            divJDDJLB.Visible = false;
            lbtnJDDJ.BackColor = System.Drawing.Color.DarkGray;
            lbtnJDDJLB.BackColor = System.Drawing.Color.White;
            BAND();
            ddlTZBM.SelectedValue = "0";
            ddlLXR.SelectedValue = "0";
        }

        private void ISENABLED(bool i)
        {
            ddlTZBM.Enabled = i;
            ddlLXR.Enabled = i;
            gdZDDJ.Enabled = i;
            txtBZ.Enabled = i;
            txtZDR.Enabled = i;
            txtZDSJ.Enabled = i;
        }

        private void ADDROW()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("XZ", typeof(bool));
            dt.Columns.Add("LX", typeof(string));
            dt.Columns.Add("XQRQ", typeof(string));
            dt.Columns.Add("XQSJ", typeof(string));
            dt.Columns.Add("DD", typeof(string));
            dt.Columns.Add("SL", typeof(string));
            dt.Columns.Add("YQR", typeof(bool));
            dt.Columns.Add("YJS", typeof(bool));
            dt.Columns.Add("XQSM", typeof(string));
            for (int i = 0; i < gdZDDJ.Rows.Count; i++)
            {
                dt.Rows.Add();
                dt.Rows[i]["XZ"] = ((CheckBox)gdZDDJ.Rows[i].FindControl("cbXZ")).Checked;
                dt.Rows[i]["LX"] = ((DropDownList)gdZDDJ.Rows[i].FindControl("ddlLX")).SelectedValue;
                dt.Rows[i]["XQRQ"] = ((TextBox)gdZDDJ.Rows[i].FindControl("txtXQRQ")).Text;
                dt.Rows[i]["XQSJ"] = ((DropDownList)gdZDDJ.Rows[i].FindControl("ddlXQSJ")).SelectedValue;
                dt.Rows[i]["DD"] = ((DropDownList)gdZDDJ.Rows[i].FindControl("ddlDD")).SelectedValue;
                dt.Rows[i]["SL"] = ((TextBox)gdZDDJ.Rows[i].FindControl("txtSL")).Text;
                dt.Rows[i]["YQR"] = ((CheckBox)gdZDDJ.Rows[i].FindControl("cbYQR")).Checked;
                dt.Rows[i]["YJS"] = ((CheckBox)gdZDDJ.Rows[i].FindControl("cbYJS")).Checked;
                dt.Rows[i]["XQSM"] = ((TextBox)gdZDDJ.Rows[i].FindControl("txtXQSM")).Text;
            }
            for (int i = gdZDDJ.Rows.Count; i < 5 + gdZDDJ.Rows.Count; i++)
            {
                dt.Rows.Add();
                dt.Rows[i]["XZ"] = false;
                dt.Rows[i]["LX"] = "0";
                dt.Rows[i]["XQRQ"] = "";
                dt.Rows[i]["XQSJ"] = "0";
                dt.Rows[i]["DD"] = "0";
                dt.Rows[i]["SL"] = "";
                dt.Rows[i]["YQR"] = false;
                dt.Rows[i]["YJS"] = false;
                dt.Rows[i]["XQSM"] = "";
            }
            gdZDDJ.DataSource = dt;
            gdZDDJ.DataBind();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                CheckBox cbXZ = (CheckBox)gdZDDJ.Rows[i].FindControl("cbXZ");
                cbXZ.Checked = Convert.ToBoolean(dt.Rows[i]["XZ"].ToString());
                DropDownList ddlLX = (DropDownList)gdZDDJ.Rows[i].FindControl("ddlLX");
                ddlLX.SelectedValue = dt.Rows[i]["LX"].ToString();
                DropDownList ddlXQSJ = (DropDownList)gdZDDJ.Rows[i].FindControl("ddlXQSJ");
                ddlXQSJ.SelectedValue = dt.Rows[i]["XQSJ"].ToString();
                DropDownList ddlDD = (DropDownList)gdZDDJ.Rows[i].FindControl("ddlDD");
                ddlDD.SelectedValue = dt.Rows[i]["DD"].ToString();
                CheckBox cbYQR = (CheckBox)gdZDDJ.Rows[i].FindControl("cbYQR");
                cbYQR.Checked = Convert.ToBoolean(dt.Rows[i]["YQR"]);
                CheckBox cbYJS = (CheckBox)gdZDDJ.Rows[i].FindControl("cbYJS");
                cbYJS.Checked = Convert.ToBoolean(dt.Rows[i]["YJS"]);
            }
        }

        private void DELETEROW()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("XZ", typeof(bool));
            dt.Columns.Add("LX", typeof(string));
            dt.Columns.Add("XQRQ", typeof(string));
            dt.Columns.Add("XQSJ", typeof(string));
            dt.Columns.Add("DD", typeof(string));
            dt.Columns.Add("SL", typeof(string));
            dt.Columns.Add("YQR", typeof(bool));
            dt.Columns.Add("YJS", typeof(bool));
            dt.Columns.Add("XQSM", typeof(string));
            if (gdZDDJ.Rows.Count > 0)
            {
                int s = 0;
                for (int i = 0; i < gdZDDJ.Rows.Count; i++)
                {
                    if (((CheckBox)gdZDDJ.Rows[i].FindControl("cbXZ")).Checked == false)
                    {
                        dt.Rows.Add();
                        dt.Rows[s]["XZ"] = ((CheckBox)gdZDDJ.Rows[i].FindControl("cbXZ")).Checked;
                        dt.Rows[s]["LX"] = ((DropDownList)gdZDDJ.Rows[i].FindControl("ddlLX")).SelectedValue;
                        dt.Rows[s]["XQRQ"] = ((TextBox)gdZDDJ.Rows[i].FindControl("txtXQRQ")).Text;
                        dt.Rows[s]["XQSJ"] = ((DropDownList)gdZDDJ.Rows[i].FindControl("ddlXQSJ")).SelectedValue;
                        dt.Rows[s]["DD"] = ((DropDownList)gdZDDJ.Rows[i].FindControl("ddlDD")).SelectedValue;
                        dt.Rows[s]["SL"] = ((TextBox)gdZDDJ.Rows[i].FindControl("txtSL")).Text;
                        dt.Rows[s]["YQR"] = ((CheckBox)gdZDDJ.Rows[i].FindControl("cbYQR")).Checked;
                        dt.Rows[s]["YJS"] = ((CheckBox)gdZDDJ.Rows[i].FindControl("cbYJS")).Checked;
                        dt.Rows[s]["XQSM"] = ((TextBox)gdZDDJ.Rows[i].FindControl("txtXQSM")).Text;
                        s = s + 1;
                    }
                }
            }
            gdZDDJ.DataSource = dt;
            gdZDDJ.DataBind();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                CheckBox cbXZ = (CheckBox)gdZDDJ.Rows[i].FindControl("cbXZ");
                cbXZ.Checked = Convert.ToBoolean(dt.Rows[i]["XZ"].ToString());
                DropDownList ddlLX = (DropDownList)gdZDDJ.Rows[i].FindControl("ddlLX");
                ddlLX.SelectedValue = dt.Rows[i]["LX"].ToString();
                DropDownList ddlXQSJ = (DropDownList)gdZDDJ.Rows[i].FindControl("ddlXQSJ");
                ddlXQSJ.SelectedValue = dt.Rows[i]["XQSJ"].ToString();
                DropDownList ddlDD = (DropDownList)gdZDDJ.Rows[i].FindControl("ddlDD");
                ddlDD.SelectedValue = dt.Rows[i]["DD"].ToString();
                CheckBox cbYQR = (CheckBox)gdZDDJ.Rows[i].FindControl("cbYQR");
                cbYQR.Checked = Convert.ToBoolean(dt.Rows[i]["YQR"]);
                CheckBox cbYJS = (CheckBox)gdZDDJ.Rows[i].FindControl("cbYJS");
                cbYJS.Checked = Convert.ToBoolean(dt.Rows[i]["YJS"]);
            }
        }
    }
}