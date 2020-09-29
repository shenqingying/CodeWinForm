using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bussiness.STSY;

namespace STSY.RKGL
{
    public partial class RKDJ : System.Web.UI.Page
    {
        CKService ckService = new CKService();
        RKService rkService = new RKService();
        WLService wlService = new WLService();
        Mainservice mainService = new Mainservice();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LOGINTIMEOUT();
                STAFFQX();
                BANDDDLCK();
                BANDddlGYSBH();
                BANDddlLBCKBH();
                BANDddlLBGYSBH();
                BANDddlWLLB();           //物料类别下拉框
                BAND();
                divRKDTX.Visible = true;
                divRKDLB.Visible = false;
                rbl.SelectedValue = "0";
                txtZDSJ.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                txtZDR.Text = Session["STAFFNAME"].ToString();
                txtLBRKRQE.Text = DateTime.Now.ToString("yyyy-MM-dd");
                DateTime now = DateTime.Now;
                DateTime d1 = new DateTime(now.Year, now.Month, 1);
                txtLBRKRQS.Text = d1.ToString("yyyy-MM-dd");
                txtRKRQ.Text = DateTime.Now.ToString("yyyy-MM-dd");
                BANDBOTTON("1,4,5,6,7,9");
                BANDEnabled(true);
                if (Request.QueryString["RI"] != null)
                {
                    BANDTX();
                }
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
            bool s = mainService.SELECT_HG_RIGHT(Session["STAFFID"].ToString(), "201");
            if (s == false)
            {
                Response.Redirect("../Index/Index.aspx?RI=1");
            }
        }

        private void BANDddlWLLB()             //物料类别下拉框
        {
            DataTable dt = wlService.GETWLTYPELB();
            ddlWLLB.DataSource = dt;
            ddlWLLB.DataTextField = "NAME";
            ddlWLLB.DataValueField = "typeID";
            ddlWLLB.DataBind();
            ListItem item = new ListItem();
            item.Text = "=请选择=";
            item.Value = "0";
            ddlWLLB.Items.Insert(0, item);
        }

        private void BANDBOTTON(string str)
        {
            string[] str1 = str.Split(',');
            btnCXSX.Enabled = false;
            btnCXSX.BackColor = System.Drawing.Color.Gray;
            btnXZ.Enabled = false;
            btnXZ.BackColor = System.Drawing.Color.Gray;
            btnBJ.Enabled = false;
            btnBJ.BackColor = System.Drawing.Color.Gray;
            btnaddrow.Enabled = false;
            btnaddrow.BackColor = System.Drawing.Color.Gray;
            btndeleterow.Enabled = false;
            btndeleterow.BackColor = System.Drawing.Color.Gray;
            btnSAVE.Enabled = false;
            btnSAVE.BackColor = System.Drawing.Color.Gray;
            btnQX.Enabled = false;
            btnQX.BackColor = System.Drawing.Color.Gray;
            btnTJ.Enabled = false;
            btnTJ.BackColor = System.Drawing.Color.Gray;
            btnWLTJ.Enabled = false;
            btnWLTJ.BackColor = System.Drawing.Color.Gray;
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
                        btnXZ.Enabled = true;
                        btnXZ.BackColor = System.Drawing.Color.FromName("#3498db");
                    }
                    else if (str1[i] == "3")
                    {
                        btnBJ.Enabled = true;
                        btnBJ.BackColor = System.Drawing.Color.FromName("#3498db");
                    }
                    else if (str1[i] == "4")
                    {
                        btnaddrow.Enabled = true;
                        btnaddrow.BackColor = System.Drawing.Color.FromName("#3498db");
                    }
                    else if (str1[i] == "5")
                    {
                        btndeleterow.Enabled = true;
                        btndeleterow.BackColor = System.Drawing.Color.FromName("#3498db");
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
                        btnTJ.Enabled = true;
                        btnTJ.BackColor = System.Drawing.Color.FromName("#3498db");
                    }
                    else if (str1[i] == "9")
                    {
                        btnWLTJ.Enabled = true;
                        btnWLTJ.BackColor = System.Drawing.Color.FromName("#3498db");
                    }
                }
            }
        }

        private void BAND()
        {
            if (lbRI.Text == "0")
            {
                DataTable dt = new DataTable();
                dt.Columns.Add(new DataColumn("LBBH", typeof(string)));
                dt.Columns.Add(new DataColumn("LBMC", typeof(string)));
                dt.Columns.Add(new DataColumn("WLGG", typeof(string)));
                dt.Columns.Add(new DataColumn("DW", typeof(string)));
                dt.Columns.Add(new DataColumn("SL", typeof(string)));
                dt.Columns.Add(new DataColumn("DJ", typeof(string)));
                dt.Columns.Add(new DataColumn("ZJE", typeof(string)));
                dt.Columns.Add(new DataColumn("KWMC", typeof(string)));
                dt.Columns.Add(new DataColumn("SM", typeof(string)));
                dt.Columns.Add(new DataColumn("XZ", typeof(bool)));
                dt.Columns.Add(new DataColumn("RuKMXID", typeof(string)));
                dt.Columns.Add(new DataColumn("materialID", typeof(string)));
                for (int i = 0; i < 5; i++)
                {
                    dt.Rows.Add();
                    dt.Rows[i][0] = "";
                    dt.Rows[i][1] = "";
                    dt.Rows[i][2] = "";
                    dt.Rows[i][3] = "";
                    dt.Rows[i][4] = "";
                    dt.Rows[i][5] = "";
                    dt.Rows[i][6] = "";
                    dt.Rows[i][7] = "";
                    dt.Rows[i][8] = "";
                    dt.Rows[i][9] = false;
                    dt.Rows[i][10] = "0";
                    dt.Rows[i][11] = "0";
                }
                gdRKDH.DataSource = dt;
                gdRKDH.DataBind();
            }
            else
            {
                DataTable dt = rkService.SELECTRKDBYID(lbRI.Text);
                if (dt.Rows.Count > 0)
                {
                    ddlCKBH.SelectedValue = dt.Rows[0]["StockID"].ToString();
                    ddlGYSBH.SelectedValue = dt.Rows[0]["ClientID"].ToString();
                    txtRKRQ.Text = Convert.ToDateTime(dt.Rows[0]["RuKDate"].ToString()).ToString("yyyy-MM-dd");
                    ddlRKLX.SelectedValue = dt.Rows[0]["TypeID"].ToString();
                    txtRKDH.Text = dt.Rows[0]["RuKDNO"].ToString();
                    txtRKBZ.Text = dt.Rows[0]["RuKMem"].ToString();
                    txtZDR.Text = dt.Rows[0]["fillName"].ToString();
                    txtZDSJ.Text = Convert.ToDateTime(dt.Rows[0]["fillTime"].ToString()).ToString("yyyy-MM-dd HH:mm:ss");
                }
                DataTable dtband = new DataTable();
                dtband.Columns.Add(new DataColumn("LBBH", typeof(string)));
                dtband.Columns.Add(new DataColumn("LBMC", typeof(string)));
                dtband.Columns.Add(new DataColumn("WLGG", typeof(string)));
                dtband.Columns.Add(new DataColumn("DW", typeof(string)));
                dtband.Columns.Add(new DataColumn("SL", typeof(string)));
                dtband.Columns.Add(new DataColumn("DJ", typeof(string)));
                dtband.Columns.Add(new DataColumn("ZJE", typeof(string)));
                dtband.Columns.Add(new DataColumn("KWMC", typeof(string)));
                dtband.Columns.Add(new DataColumn("SM", typeof(string)));
                dtband.Columns.Add(new DataColumn("XZ", typeof(bool)));
                dtband.Columns.Add(new DataColumn("RuKMXID", typeof(string)));
                dtband.Columns.Add(new DataColumn("materialID", typeof(string)));

                DataTable dtMX = rkService.SELECTRKDMXBYID(lbRI.Text);
                if (dtMX.Rows.Count > 0)
                {
                    for (int i = 0; i < dtMX.Rows.Count; i++)
                    {
                        dtband.Rows.Add();
                        DataTable dtlbbh = wlService.SELECT_MSType_MSMaterial(dtMX.Rows[i]["materialID"].ToString());
                        DataTable dtlbWL = wlService.GETWLTYPEbymaterialID(dtMX.Rows[i]["materialID"].ToString());
                        dtband.Rows[i][0] = dtlbbh.Rows[0]["typeSN"].ToString() + " " + dtlbbh.Rows[0]["typeName"].ToString();
                        dtband.Rows[i][1] = dtlbWL.Rows[0]["mtSN"].ToString() + " " + dtlbWL.Rows[0]["mtName"].ToString();
                        dtband.Rows[i][2] = "";
                        dtband.Rows[i][3] = dtMX.Rows[i]["mtUnit"].ToString();
                        dtband.Rows[i][4] = dtMX.Rows[i]["mtNumber"].ToString();
                        dtband.Rows[i][5] = dtMX.Rows[i]["mtPrice"].ToString();
                        dtband.Rows[i][6] = dtMX.Rows[i]["mtTotal"].ToString();
                        dtband.Rows[i][7] = dtMX.Rows[i]["PlaceID"].ToString();
                        dtband.Rows[i][8] = dtMX.Rows[i]["MXMemo"].ToString();
                        dtband.Rows[i][9] = false;
                        dtband.Rows[i][10] = dtMX.Rows[i]["RuKMXID"].ToString();
                        dtband.Rows[i][11] = dtMX.Rows[i]["materialID"].ToString();
                    }
                    gdRKDH.DataSource = dtband;
                    gdRKDH.DataBind();
                    for (int i = 0; i < dtband.Rows.Count; i++)
                    {
                        if (dtband.Rows[i][7].ToString() == "")
                        {
                            dtband.Rows[i][7] = "0";
                        }
                        DropDownList ddlKWMC = (DropDownList)gdRKDH.Rows[i].FindControl("ddlKWMC");
                        ddlKWMC.SelectedValue = dtband.Rows[i][7].ToString();

                        CheckBox cbXZ = (CheckBox)gdRKDH.Rows[i].FindControl("cbXZ");
                        cbXZ.Checked = Convert.ToBoolean(dtband.Rows[i][9].ToString());
                    }
                }
                else
                {
                    for (int i = 0; i < 5; i++)
                    {
                        dtband.Rows.Add();
                        dtband.Rows[i][0] = "";
                        dtband.Rows[i][1] = "";
                        dtband.Rows[i][2] = "";
                        dtband.Rows[i][3] = "";
                        dtband.Rows[i][4] = "";
                        dtband.Rows[i][5] = "";
                        dtband.Rows[i][6] = "";
                        dtband.Rows[i][7] = "";
                        dtband.Rows[i][8] = "";
                        dtband.Rows[i][9] = false;
                        dtband.Rows[i][10] = "0";
                        dtband.Rows[i][11] = "0";
                    }
                    gdRKDH.DataSource = dtband;
                    gdRKDH.DataBind();
                }
            }
            BANDHJ();
        }

        private void BANDDDLCK()
        {
            DataTable dt = ckService.GETCKINFOISUSER();
            dt.Columns.Add("NAME", typeof(string));
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dt.Rows[i]["NAME"] = dt.Rows[i]["StockSN"].ToString() + "  " + dt.Rows[i]["StockName"].ToString();
                }
            }
            ddlCKBH.DataSource = dt;
            ddlCKBH.DataTextField = "NAME";
            ddlCKBH.DataValueField = "StockID";
            ddlCKBH.DataBind();
        }

        private void BANDddlGYSBH()
        {
            DataTable dt = rkService.GETGYSINFO();
            dt.Columns.Add("NAME", typeof(string));
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dt.Rows[i]["NAME"] = dt.Rows[i]["ClSN"].ToString() + "  " + dt.Rows[i]["ClName"].ToString();
                }
            }
            ddlGYSBH.DataSource = dt;
            ddlGYSBH.DataTextField = "NAME";
            ddlGYSBH.DataValueField = "ClientID";
            ddlGYSBH.DataBind();
            ListItem item = new ListItem();
            item.Text = "=请选择=";
            item.Value = "0";
            ddlGYSBH.Items.Insert(0, item);
        }

        private void BANDddlLBCKBH()
        {
            DataTable dt = ckService.GETCKINFOISUSER();
            dt.Columns.Add("NAME", typeof(string));
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dt.Rows[i]["NAME"] = dt.Rows[i]["StockSN"].ToString() + "  " + dt.Rows[i]["StockName"].ToString();
                }
            }
            ddlLBCKBH.DataSource = dt;
            ddlLBCKBH.DataTextField = "NAME";
            ddlLBCKBH.DataValueField = "StockID";
            ddlLBCKBH.DataBind();
            ListItem item = new ListItem();
            item.Text = "=请选择=";
            item.Value = "0";
            ddlLBCKBH.Items.Insert(0, item);
        }

        private void BANDddlLBGYSBH()
        {
            DataTable dt = rkService.GETGYSINFO();
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

        protected void gdRKDH_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow) //判断是否是DataRow，以防止鼠标经过Header也有效果   
            {
                e.Row.Attributes.Add("onmouseover", "e=this.style.backgroundColor; this.style.backgroundColor='#BBFFFF'");
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=e");
                DropDownList ddlKWMC = ((DropDownList)e.Row.FindControl("ddlKWMC"));
                DataTable dtKW = ckService.GETKWINFO();
                if (dtKW.Rows.Count > 0)
                {
                    for (int i = 0; i < dtKW.Rows.Count; i++)
                    {
                        dtKW.Rows[i]["PlaceSN"] = dtKW.Rows[i]["PlaceSN"].ToString() + "  " + dtKW.Rows[i]["PlaceName"].ToString();
                    }
                }
                ddlKWMC.DataSource = dtKW;
                ddlKWMC.DataTextField = "PlaceSN";
                ddlKWMC.DataValueField = "PlaceID";
                ddlKWMC.DataBind();
                ListItem item1 = new ListItem();
                item1.Text = "=请选择=";
                item1.Value = "0";
                ddlKWMC.Items.Insert(0, item1);
            }
        }
        protected void txtSL_TextChanged(object sender, EventArgs e)
        {
            TextBox t = (TextBox)sender;
            GridViewRow drv = (GridViewRow)t.NamingContainer;
            int rowIndex = drv.RowIndex;
            TextBox txtSL = ((TextBox)gdRKDH.Rows[drv.RowIndex].FindControl("txtSL"));
            TextBox txtDJ = ((TextBox)gdRKDH.Rows[drv.RowIndex].FindControl("txtDJ"));
            TextBox txtZJE = ((TextBox)gdRKDH.Rows[drv.RowIndex].FindControl("txtZJE"));
            if (txtSL.Text == "")
            {
                txtSL.Text = "0";
            }
            if (txtDJ.Text == "")
            {
                txtDJ.Text = "0";
            }
            txtZJE.Text = Math.Round((Convert.ToDouble(txtSL.Text) * Convert.ToDouble(txtDJ.Text)), 2, MidpointRounding.AwayFromZero).ToString();
            BANDHJ();
            rowIndex = rowIndex + 1;
            if (rowIndex < gdRKDH.Rows.Count)
            {
                TextBox txtSLNEXT = ((TextBox)gdRKDH.Rows[rowIndex].FindControl("txtSL"));
                txtSLNEXT.Focus();
                txtSLNEXT.Attributes.Add("onfocus", "this.select()");
            }
        }

        protected void txtDJ_TextChanged(object sender, EventArgs e)
        {
            TextBox t = (TextBox)sender;
            GridViewRow drv = (GridViewRow)t.NamingContainer;
            int rowIndex = drv.RowIndex;
            TextBox txtSL = ((TextBox)gdRKDH.Rows[drv.RowIndex].FindControl("txtSL"));
            TextBox txtDJ = ((TextBox)gdRKDH.Rows[drv.RowIndex].FindControl("txtDJ"));
            TextBox txtZJE = ((TextBox)gdRKDH.Rows[drv.RowIndex].FindControl("txtZJE"));
            if (txtSL.Text == "")
            {
                txtSL.Text = "0";
            }
            if (txtDJ.Text == "")
            {
                txtDJ.Text = "0";
            }
            txtZJE.Text = Math.Round((Convert.ToDouble(txtSL.Text) * Convert.ToDouble(txtDJ.Text)), 2, MidpointRounding.AwayFromZero).ToString();
            BANDHJ();
            rowIndex = rowIndex + 1;
            if (rowIndex < gdRKDH.Rows.Count)
            {
                TextBox txtDJNEXT = ((TextBox)gdRKDH.Rows[rowIndex].FindControl("txtDJ"));
                txtDJNEXT.Focus();
                txtDJNEXT.Attributes.Add("onfocus", "this.select()");
            }
        }

        private void BANDRuKLLB()
        {
            LOGINTIMEOUT();
            DataTable dt = new DataTable();
            if (rbl.SelectedValue == "0")
            {
                dt = rkService.SELECTRKD(rbl.SelectedValue, ddlLBCKBH.SelectedValue, ddlLBGYSBH.SelectedValue, txtLBRKRQS.Text.Trim(), txtLBRKRQE.Text.Trim(), ddlLBRKLX.SelectedValue, txtLBRKDH.Text, Session["STAFFID"].ToString());
            }
            else
            {
                dt = rkService.SELECTRKD(rbl.SelectedValue, ddlLBCKBH.SelectedValue, ddlLBGYSBH.SelectedValue, txtLBRKRQS.Text.Trim(), txtLBRKRQE.Text.Trim(), ddlLBRKLX.SelectedValue, txtLBRKDH.Text, "");
            }
            dt.Columns.Add("RuKDate1", typeof(string));
            dt.Columns.Add("ISSH", typeof(bool));
            dt.Columns.Add("fillTime1", typeof(string));
            dt.Columns.Add("XH", typeof(string));
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dt.Rows[i]["XH"] = (i + 1).ToString();
                    dt.Rows[i]["RuKDate1"] = Convert.ToDateTime(dt.Rows[i]["RuKDate"].ToString()).ToString("yyyy-MM-dd");
                    if (dt.Rows[i]["nStatus"].ToString() == "0" || dt.Rows[i]["nStatus"].ToString() == "1")
                    {
                        dt.Rows[i]["ISSH"] = false;
                    }
                    else if (dt.Rows[i]["nStatus"].ToString() == "2")
                    {
                        dt.Rows[i]["ISSH"] = true;
                    }
                    dt.Rows[i]["fillTime1"] = Convert.ToDateTime(dt.Rows[i]["fillTime"].ToString()).ToString("yyyy-MM-dd HH:mm:ss");
                }
            }
            gdRKDLB.DataSource = dt;
            gdRKDLB.DataBind();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                CheckBox cbSH = ((CheckBox)gdRKDLB.Rows[i].FindControl("cbSH"));
                bool z = Convert.ToBoolean(dt.Rows[i]["ISSH"].ToString());
                cbSH.Checked = z;
            }
        }

        protected void btnCXSX_Click(object sender, EventArgs e)
        {
            if (divRKDLB.Visible == true)
            {
                BANDRuKLLB();
            }
        }

        protected void btnXZ_Click(object sender, EventArgs e)
        {
            lbRI.Text = "0";
            lbISADD.Text = "true";
            lbISED.Text = "false";
            lbnStatus.Text = "0";
            BAND();
            BANDBOTTON("1,4,5,6,7,9");
            BANDEnabled(true);
        }

        protected void btnBJ_Click(object sender, EventArgs e)
        {
            if (lbnStatus.Text == "0")
            {
                lbISED.Text = "true";
                BANDBOTTON("1,4,5,6,7,9");
                BANDEnabled(true);
                lbISADD.Text = "false";
                lbISED.Text = "true";
            }
            else
            {
                this.Page.RegisterStartupScript("onclick", "<script>alert('已提交，无法修改!')</script>");
            }
        }

        protected void btnaddrow_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("LBBH", typeof(string)));
            dt.Columns.Add(new DataColumn("LBMC", typeof(string)));
            dt.Columns.Add(new DataColumn("WLGG", typeof(string)));
            dt.Columns.Add(new DataColumn("DW", typeof(string)));
            dt.Columns.Add(new DataColumn("SL", typeof(string)));
            dt.Columns.Add(new DataColumn("DJ", typeof(string)));
            dt.Columns.Add(new DataColumn("ZJE", typeof(string)));
            dt.Columns.Add(new DataColumn("KWMC", typeof(string)));
            dt.Columns.Add(new DataColumn("SM", typeof(string)));
            dt.Columns.Add(new DataColumn("XZ", typeof(bool)));
            dt.Columns.Add(new DataColumn("RuKMXID", typeof(string)));
            dt.Columns.Add(new DataColumn("materialID", typeof(string)));
            for (int i = 0; i < gdRKDH.Rows.Count; i++)
            {
                dt.Rows.Add();
                dt.Rows[i][0] = ((Label)gdRKDH.Rows[i].FindControl("lbLB")).Text;
                dt.Rows[i][1] = ((Label)gdRKDH.Rows[i].FindControl("lbWL")).Text;
                dt.Rows[i][2] = ((TextBox)gdRKDH.Rows[i].FindControl("txtWLGG")).Text;
                dt.Rows[i][3] = ((TextBox)gdRKDH.Rows[i].FindControl("txtDW")).Text;
                dt.Rows[i][4] = ((TextBox)gdRKDH.Rows[i].FindControl("txtSL")).Text;
                dt.Rows[i][5] = ((TextBox)gdRKDH.Rows[i].FindControl("txtDJ")).Text;
                dt.Rows[i][6] = ((TextBox)gdRKDH.Rows[i].FindControl("txtZJE")).Text;
                dt.Rows[i][7] = ((DropDownList)gdRKDH.Rows[i].FindControl("ddlKWMC")).SelectedValue;
                dt.Rows[i][8] = ((TextBox)gdRKDH.Rows[i].FindControl("txtSM")).Text;
                dt.Rows[i][9] = ((CheckBox)gdRKDH.Rows[i].FindControl("cbXZ")).Checked;
                dt.Rows[i][10] = ((Label)gdRKDH.Rows[i].FindControl("RuKMXID")).Text;
                dt.Rows[i][11] = ((Label)gdRKDH.Rows[i].FindControl("materialID")).Text;
            }
            for (int i = gdRKDH.Rows.Count; i < 5 + gdRKDH.Rows.Count; i++)
            {
                dt.Rows.Add();
                dt.Rows[i][0] = "";
                dt.Rows[i][1] = "";
                dt.Rows[i][2] = "";
                dt.Rows[i][3] = "";
                dt.Rows[i][4] = "";
                dt.Rows[i][5] = "";
                dt.Rows[i][6] = "";
                dt.Rows[i][7] = "";
                dt.Rows[i][8] = "";
                dt.Rows[i][9] = false;
                dt.Rows[i][10] = "0";
                dt.Rows[i][11] = "0";
            }
            gdRKDH.DataSource = dt;
            gdRKDH.DataBind();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i][7].ToString() == "")
                {
                    dt.Rows[i][7] = "0";
                }
                DropDownList ddlKWMC = (DropDownList)gdRKDH.Rows[i].FindControl("ddlKWMC");
                ddlKWMC.SelectedValue = dt.Rows[i][7].ToString();

                CheckBox cbXZ = (CheckBox)gdRKDH.Rows[i].FindControl("cbXZ");
                cbXZ.Checked = Convert.ToBoolean(dt.Rows[i][9].ToString());
            }
        }

        protected void btndeleterow_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("LBBH", typeof(string)));
            dt.Columns.Add(new DataColumn("LBMC", typeof(string)));
            dt.Columns.Add(new DataColumn("WLGG", typeof(string)));
            dt.Columns.Add(new DataColumn("DW", typeof(string)));
            dt.Columns.Add(new DataColumn("SL", typeof(string)));
            dt.Columns.Add(new DataColumn("DJ", typeof(string)));
            dt.Columns.Add(new DataColumn("ZJE", typeof(string)));
            dt.Columns.Add(new DataColumn("KWMC", typeof(string)));
            dt.Columns.Add(new DataColumn("SM", typeof(string)));
            dt.Columns.Add(new DataColumn("XZ", typeof(bool)));
            dt.Columns.Add(new DataColumn("RuKMXID", typeof(string)));
            dt.Columns.Add(new DataColumn("materialID", typeof(string)));
            if (gdRKDH.Rows.Count > 0)
            {
                int s = 0;
                for (int i = 0; i < gdRKDH.Rows.Count; i++)
                {
                    if (((CheckBox)gdRKDH.Rows[i].FindControl("cbXZ")).Checked == false)
                    {
                        dt.Rows.Add();
                        dt.Rows[s][0] = ((Label)gdRKDH.Rows[i].FindControl("lbLB")).Text;
                        dt.Rows[s][1] = ((Label)gdRKDH.Rows[i].FindControl("lbWL")).Text;
                        dt.Rows[s][2] = ((TextBox)gdRKDH.Rows[i].FindControl("txtWLGG")).Text;
                        dt.Rows[s][3] = ((TextBox)gdRKDH.Rows[i].FindControl("txtDW")).Text;
                        dt.Rows[s][4] = ((TextBox)gdRKDH.Rows[i].FindControl("txtSL")).Text;
                        dt.Rows[s][5] = ((TextBox)gdRKDH.Rows[i].FindControl("txtDJ")).Text;
                        dt.Rows[s][6] = ((TextBox)gdRKDH.Rows[i].FindControl("txtZJE")).Text;
                        dt.Rows[s][7] = ((DropDownList)gdRKDH.Rows[i].FindControl("ddlKWMC")).SelectedValue;
                        dt.Rows[s][8] = ((TextBox)gdRKDH.Rows[i].FindControl("txtSM")).Text;
                        dt.Rows[s][9] = ((CheckBox)gdRKDH.Rows[i].FindControl("cbXZ")).Checked;
                        dt.Rows[s][10] = ((Label)gdRKDH.Rows[i].FindControl("RuKMXID")).Text;
                        dt.Rows[s][11] = ((Label)gdRKDH.Rows[i].FindControl("materialID")).Text;
                        s = s + 1;
                    }
                }
                gdRKDH.DataSource = dt;
                gdRKDH.DataBind();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i][7].ToString() == "")
                    {
                        dt.Rows[i][7] = "0";
                    }
                    DropDownList ddlKWMC = ((DropDownList)gdRKDH.Rows[i].FindControl("ddlKWMC"));
                    ddlKWMC.SelectedValue = dt.Rows[i][7].ToString();

                    CheckBox cbXZ = (CheckBox)gdRKDH.Rows[i].FindControl("cbXZ");
                    cbXZ.Checked = Convert.ToBoolean(dt.Rows[i][9].ToString());
                }
                BANDHJ();
            }
        }

        protected void btnSAVE_Click(object sender, EventArgs e)                            //保存按钮
        {
            txtZDSJ.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            if (ddlCKBH.SelectedValue == "0")
            {
                this.Page.RegisterStartupScript("onclick", "<script>alert('请选择仓库!')</script>");
                return;
            }
            if (ddlGYSBH.SelectedValue == "0")
            {
                this.Page.RegisterStartupScript("onclick", "<script>alert('请选择供应商!')</script>");
                return;
            }
            try
            {
                DateTime dtime = Convert.ToDateTime(txtRKRQ.Text);
            }
            catch
            {
                this.Page.RegisterStartupScript("onclick", "<script>alert('请填写正确的入库日期!')</script>");
                return;
            }
            if (ddlRKLX.SelectedValue == "-1")
            {
                this.Page.RegisterStartupScript("onclick", "<script>alert('请选择入库类型!')</script>");
                return;
            }
            if (txtRKDH.Text == "")
            {
                this.Page.RegisterStartupScript("onclick", "<script>alert('请填写入库单号!')</script>");
                return;
            }
            else
            {
                DataTable dt = rkService.SELECTcount_RuKDNO(txtRKDH.Text.Trim(), lbRI.Text, ddlGYSBH.SelectedValue);
                int sum = Convert.ToInt32(dt.Rows[0][0].ToString());
                if (sum > 0)
                {
                    this.Page.RegisterStartupScript("onclick", "<script>alert('该入库单号已存在!')</script>");
                    return;
                }
            }
            if (ddlWLLB.SelectedValue == "0")
            {
                this.Page.RegisterStartupScript("onclick", "<script>alert('请选择物料类型!')</script>");
                return;
            }
            bool isnew = false;
            LOGINTIMEOUT();
            if (lbISED.Text == "true" && lbRI.Text != "0")
            {
                rkService.DELETERKDMX(lbRI.Text);
                rkService.DELETERKD(lbRI.Text);
                isnew = true;
            }
            if ((lbISADD.Text == "true" && lbRI.Text == "0") || (lbISED.Text == "true" && lbRI.Text != "0"))
            {
                string s = rkService.insertRKDH(txtRKDH.Text.Trim(), ddlCKBH.SelectedValue, ddlGYSBH.SelectedValue, txtRKRQ.Text.Trim(), txtRKBZ.Text.Trim(), Session["STAFFID"].ToString(), txtZDR.Text.Trim(), txtZDSJ.Text.Trim(), "0", "0", ddlRKLX.SelectedValue, "0", ddlWLLB.SelectedValue);
                for (int i = 0; i < gdRKDH.Rows.Count; i++)
                {
                    string[] str = new string[10];
                    str[0] = "";//((DropDownList)gdRKDH.Rows[i].FindControl("ddlLBBH")).SelectedValue;
                    str[1] = ((Label)gdRKDH.Rows[i].FindControl("materialID")).Text;//((DropDownList)gdRKDH.Rows[i].FindControl("ddlLBMC")).SelectedValue;
                    str[2] = ((TextBox)gdRKDH.Rows[i].FindControl("txtWLGG")).Text;
                    str[3] = ((TextBox)gdRKDH.Rows[i].FindControl("txtDW")).Text;
                    str[4] = ((TextBox)gdRKDH.Rows[i].FindControl("txtSL")).Text;
                    str[5] = ((TextBox)gdRKDH.Rows[i].FindControl("txtDJ")).Text;
                    str[6] = ((TextBox)gdRKDH.Rows[i].FindControl("txtZJE")).Text;
                    str[7] = ((DropDownList)gdRKDH.Rows[i].FindControl("ddlKWMC")).SelectedValue;
                    str[8] = ((TextBox)gdRKDH.Rows[i].FindControl("txtSM")).Text;
                    if (str[1] != "" && str[1] != "0")
                    {
                        rkService.insertRKDMX(s, str[1], str[4], str[3], str[5], str[6], str[7], str[8]);
                    }
                    else
                    {
                        break;
                    }
                }
                lbRI.Text = s;
            }
            if (isnew == false)
            {
                //Response.Write("<script>alert('添加成功!')</script>");
                this.Page.RegisterStartupScript("onclick", "<script>alert('添加成功!')</script>");
            }
            else
            {
                //Response.Write("<script>alert('修改成功!')</script>");
                this.Page.RegisterStartupScript("onclick", "<script>alert('修改成功!')</script>");

            }
            lbISADD.Text = "false";
            lbISED.Text = "false";
            BANDBOTTON("1,2,3,8");
            BANDEnabled(false);
        }

        protected void btnQX_Click(object sender, EventArgs e)
        {
            lbISADD.Text = "false";
            lbISED.Text = "false";
            BANDBOTTON("1,2,3,8");
            BANDEnabled(false);
            BAND();
        }

        protected void btnTJ_Click(object sender, EventArgs e)
        {
            if (lbRI.Text != "0" && lbRI.Text != "")
            {
                if (lbnStatus.Text == "0")
                {
                    DateTime dtime = Convert.ToDateTime(txtRKRQ.Text);
                    string s = dtime.ToString("yyyyMM");
                    rkService.RuKDTJ(s, "1", lbRI.Text);
                    BANDRuKLLB();
                    this.Page.RegisterStartupScript("onclick", "<script>alert('提交成功，单据无法修改!')</script>");
                    lbnStatus.Text = "1";
                }
                else
                {
                    this.Page.RegisterStartupScript("onclick", "<script>alert('该单据已提交无需重复提交!')</script>");
                }
            }
            else
            {
                this.Page.RegisterStartupScript("onclick", "<script>alert('请选择需要提交的单据!')</script>");
            }
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            LinkButton1.BackColor = System.Drawing.Color.DarkGray;
            LinkButton2.BackColor = System.Drawing.Color.White;
            divRKDTX.Visible = true;
            divRKDLB.Visible = false;
        }

        protected void LinkButton2_Click(object sender, EventArgs e)
        {
            LinkButton1.BackColor = System.Drawing.Color.White;
            LinkButton2.BackColor = System.Drawing.Color.DarkGray;
            divRKDTX.Visible = false;
            divRKDLB.Visible = true;
        }

        protected void gdRKDLB_RowDataBound(object sender, GridViewRowEventArgs e)
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
            Label RuKDID = ((Label)gdRKDLB.Rows[drv.RowIndex].FindControl("RuKDID"));
            Label nStatus = ((Label)gdRKDLB.Rows[drv.RowIndex].FindControl("nStatus"));
            lbRI.Text = RuKDID.Text;
            BAND();
            LinkButton1.BackColor = System.Drawing.Color.DarkGray;
            LinkButton2.BackColor = System.Drawing.Color.White;
            divRKDTX.Visible = true;
            divRKDLB.Visible = false;
            BANDBOTTON("1,2,3,8");
            BANDEnabled(false);
            lbISADD.Text = "false";
            lbISED.Text = "false";
            lbnStatus.Text = nStatus.Text;
        }

        private void BANDEnabled(bool i)
        {
            ddlCKBH.Enabled = i;
            ddlGYSBH.Enabled = i;
            txtRKRQ.Enabled = i;
            ddlRKLX.Enabled = i;
            txtRKDH.Enabled = i;
            gdRKDH.Enabled = i;
            txtRKBZ.Enabled = i;
        }

        private void BANDHJ()
        {
            double sumSL = 0;
            double sumZJE = 0;
            if (gdRKDH.Rows.Count > 0)
            {
                for (int i = 0; i < gdRKDH.Rows.Count; i++)
                {
                    TextBox txtSL = ((TextBox)gdRKDH.Rows[i].FindControl("txtSL"));
                    TextBox txtZJE = ((TextBox)gdRKDH.Rows[i].FindControl("txtZJE"));
                    try
                    {
                        sumSL = sumSL + Convert.ToDouble(txtSL.Text);
                    }
                    catch
                    {
                        sumSL = sumSL + 0;
                    }
                    try
                    {
                        sumZJE = sumZJE + Convert.ToDouble(txtZJE.Text);
                    }
                    catch
                    {
                        sumZJE = sumZJE + 0;
                    }
                }
            }
            GridViewRow foot = gdRKDH.FooterRow;
            foot.Cells[6].HorizontalAlign = HorizontalAlign.Right;
            foot.Cells[6].Text = sumSL.ToString();
            foot.Cells[8].HorizontalAlign = HorizontalAlign.Right;
            foot.Cells[8].Font.Bold = true;
            foot.Cells[8].Font.Size = 14;
            foot.Cells[8].Text = sumZJE.ToString();
        }

        protected void btnWLTJ_Click(object sender, EventArgs e)
        {
            DataTable dt = rkService.INSERT_RUKYZ(ddlCKBH.SelectedValue, ddlGYSBH.SelectedValue, txtRKRQ.Text, ddlRKLX.SelectedValue, txtRKDH.Text, txtRKBZ.Text, txtZDR.Text, txtZDSJ.Text, lbRI.Text);
            string ri = dt.Rows[0][0].ToString();
            if (gdRKDH.Rows.Count > 0)
            {
                for (int i = 0; i < gdRKDH.Rows.Count; i++)
                {
                    string[] str = new string[10];
                    str[0] = "";//((DropDownList)gdRKDH.Rows[i].FindControl("ddlLBBH")).SelectedValue;
                    str[1] = ((Label)gdRKDH.Rows[i].FindControl("materialID")).Text; //((DropDownList)gdRKDH.Rows[i].FindControl("ddlLBMC")).SelectedValue;
                    str[2] = ((TextBox)gdRKDH.Rows[i].FindControl("txtWLGG")).Text;
                    str[3] = ((TextBox)gdRKDH.Rows[i].FindControl("txtDW")).Text;
                    str[4] = ((TextBox)gdRKDH.Rows[i].FindControl("txtSL")).Text;
                    str[5] = ((TextBox)gdRKDH.Rows[i].FindControl("txtDJ")).Text;
                    str[6] = ((TextBox)gdRKDH.Rows[i].FindControl("txtZJE")).Text;
                    str[7] = ((DropDownList)gdRKDH.Rows[i].FindControl("ddlKWMC")).SelectedValue;
                    str[8] = ((TextBox)gdRKDH.Rows[i].FindControl("txtSM")).Text;
                    if (str[1] != "" && str[1] != "0")
                    {
                        rkService.INSERT_RUKYZMX(ri, i.ToString(), str[1], str[3], str[4], str[5], str[6], str[7], str[8]);
                    }
                }
            }
            Response.Redirect("RKTJ.aspx?RI=" + ri + "&MXID=" + gdRKDH.Rows.Count + "");
        }

        private void BANDTX()
        {
            DataTable dt = rkService.SELECT_RUKYZ(Request.QueryString["RI"]);
            if (dt.Rows.Count > 0)
            {
                ddlCKBH.SelectedValue = dt.Rows[0]["CK"].ToString();
                ddlGYSBH.SelectedValue = dt.Rows[0]["GYS"].ToString();
                txtRKRQ.Text = Convert.ToDateTime(dt.Rows[0]["RKRQ"].ToString()).ToString("yyyy-MM-dd");
                ddlRKLX.SelectedValue = dt.Rows[0]["RKLX"].ToString();
                txtRKDH.Text = dt.Rows[0]["RKDH"].ToString();
                txtRKBZ.Text = dt.Rows[0]["RKBZ"].ToString();
                txtZDR.Text = dt.Rows[0]["ZDR"].ToString();
                txtZDSJ.Text = Convert.ToDateTime(dt.Rows[0]["ZDSJ"].ToString()).ToString("yyyy-MM-dd HH:mm:ss");
                lbRI.Text = dt.Rows[0]["RUKDID"].ToString();
                if (lbRI.Text != "0")
                {
                    lbISED.Text = "true";
                }
            }
            DataTable dtband = new DataTable();
            dtband.Columns.Add(new DataColumn("LBBH", typeof(string)));
            dtband.Columns.Add(new DataColumn("LBMC", typeof(string)));
            dtband.Columns.Add(new DataColumn("WLGG", typeof(string)));
            dtband.Columns.Add(new DataColumn("DW", typeof(string)));
            dtband.Columns.Add(new DataColumn("SL", typeof(string)));
            dtband.Columns.Add(new DataColumn("DJ", typeof(string)));
            dtband.Columns.Add(new DataColumn("ZJE", typeof(string)));
            dtband.Columns.Add(new DataColumn("KWMC", typeof(string)));
            dtband.Columns.Add(new DataColumn("SM", typeof(string)));
            dtband.Columns.Add(new DataColumn("XZ", typeof(bool)));
            dtband.Columns.Add(new DataColumn("RuKMXID", typeof(string)));
            dtband.Columns.Add(new DataColumn("materialID", typeof(string)));
            //DataTable dtMX = rkService.SELECTRKDMXBYID(lbRI.Text);
            DataTable dtMX = rkService.SELECT_RUKYZMX(Request.QueryString["RI"]);
            if (dtMX.Rows.Count > 0)
            {
                for (int i = 0; i < dtMX.Rows.Count; i++)
                {
                    dtband.Rows.Add();
                    DataTable dtlbbh = wlService.SELECT_MSType_MSMaterial(dtMX.Rows[i]["materialID"].ToString());
                    DataTable dtlbWL = wlService.GETWLTYPEbymaterialID(dtMX.Rows[i]["materialID"].ToString());
                    dtband.Rows[i][0] = dtlbbh.Rows[0]["typeSN"].ToString() + " " + dtlbbh.Rows[0]["typeName"].ToString();
                    dtband.Rows[i][1] = dtlbWL.Rows[0]["mtSN"].ToString() + " " + dtlbWL.Rows[0]["mtName"].ToString();
                    dtband.Rows[i][2] = "";
                    dtband.Rows[i][3] = dtMX.Rows[i]["mtUnit"].ToString();
                    dtband.Rows[i][4] = dtMX.Rows[i]["mtNumber"].ToString();
                    dtband.Rows[i][5] = dtMX.Rows[i]["mtPrice"].ToString();
                    dtband.Rows[i][6] = dtMX.Rows[i]["mtTotal"].ToString();
                    dtband.Rows[i][7] = dtMX.Rows[i]["PlaceID"].ToString();
                    dtband.Rows[i][8] = dtMX.Rows[i]["MXMemo"].ToString();
                    dtband.Rows[i][9] = false;
                    dtband.Rows[i][10] = dtMX.Rows[i]["RI"].ToString();
                    dtband.Rows[i][11] = dtMX.Rows[i]["materialID"].ToString();
                }
                gdRKDH.DataSource = dtband;
                gdRKDH.DataBind();
            }
            if (dtband.Rows.Count > 0)
            {
                for (int i = 0; i < dtband.Rows.Count; i++)
                {
                    if (dtband.Rows[i][7].ToString() == "")
                    {
                        dtband.Rows[i][7] = "0";
                    }
                    DropDownList ddlKWMC = (DropDownList)gdRKDH.Rows[i].FindControl("ddlKWMC");
                    ddlKWMC.SelectedValue = dtband.Rows[i][7].ToString();

                    CheckBox cbXZ = (CheckBox)gdRKDH.Rows[i].FindControl("cbXZ");
                    cbXZ.Checked = Convert.ToBoolean(dtband.Rows[i][9].ToString());
                }
            }
            BANDHJ();
        }

        private void ALLENABLE(bool s)
        {
            gdRKDH.Enabled = s;
            ddlCKBH.Enabled = s;
            ddlGYSBH.Enabled = s;
            txtRKRQ.Enabled = s;
            ddlRKLX.Enabled = s;
            txtRKDH.Enabled = s;
        }

        protected void lbtnCR_OnClick(object sender, EventArgs e)
        {
            LinkButton t = (LinkButton)sender;
            GridViewRow drv = (GridViewRow)t.NamingContainer;
            int rowIndex = drv.RowIndex;

            DataTable dt = rkService.INSERT_RUKYZ(ddlCKBH.SelectedValue, ddlGYSBH.SelectedValue, txtRKRQ.Text, ddlRKLX.SelectedValue, txtRKDH.Text, txtRKBZ.Text, txtZDR.Text, txtZDSJ.Text, lbRI.Text);
            string ri = dt.Rows[0][0].ToString();
            if (gdRKDH.Rows.Count > 0)
            {
                for (int i = 0; i < gdRKDH.Rows.Count; i++)
                {
                    int s = 0;
                    string[] str = new string[10];
                    str[0] = "";//((DropDownList)gdRKDH.Rows[i].FindControl("ddlLBBH")).SelectedValue;
                    str[1] = ((Label)gdRKDH.Rows[i].FindControl("materialID")).Text; //((DropDownList)gdRKDH.Rows[i].FindControl("ddlLBMC")).SelectedValue;
                    str[2] = ((TextBox)gdRKDH.Rows[i].FindControl("txtWLGG")).Text;
                    str[3] = ((TextBox)gdRKDH.Rows[i].FindControl("txtDW")).Text;
                    str[4] = ((TextBox)gdRKDH.Rows[i].FindControl("txtSL")).Text;
                    str[5] = ((TextBox)gdRKDH.Rows[i].FindControl("txtDJ")).Text;
                    str[6] = ((TextBox)gdRKDH.Rows[i].FindControl("txtZJE")).Text;
                    str[7] = ((DropDownList)gdRKDH.Rows[i].FindControl("ddlKWMC")).SelectedValue;
                    str[8] = ((TextBox)gdRKDH.Rows[i].FindControl("txtSM")).Text;
                    if (str[1] != "" && str[1] != "0")
                    {
                        if (i >= rowIndex)
                        {
                            s = i + 1;
                        }
                        else
                        {
                            s = i;
                        }
                        rkService.INSERT_RUKYZMX(ri, s.ToString(), str[1], str[3], str[4], str[5], str[6], str[7], str[8]);
                    }
                }
            }
            Response.Redirect("RKTJ.aspx?RI=" + ri + "&MXID=" + rowIndex + "");


            //DataTable dt = new DataTable();
            //dt.Columns.Add(new DataColumn("LBBH", typeof(string)));
            //dt.Columns.Add(new DataColumn("LBMC", typeof(string)));
            //dt.Columns.Add(new DataColumn("WLGG", typeof(string)));
            //dt.Columns.Add(new DataColumn("DW", typeof(string)));
            //dt.Columns.Add(new DataColumn("SL", typeof(string)));
            //dt.Columns.Add(new DataColumn("DJ", typeof(string)));
            //dt.Columns.Add(new DataColumn("ZJE", typeof(string)));
            //dt.Columns.Add(new DataColumn("KWMC", typeof(string)));
            //dt.Columns.Add(new DataColumn("SM", typeof(string)));
            //dt.Columns.Add(new DataColumn("XZ", typeof(bool)));
            //dt.Columns.Add(new DataColumn("RuKMXID", typeof(string)));
            //dt.Columns.Add(new DataColumn("materialID", typeof(string)));
            //if (gdRKDH.Rows.Count > 0)
            //{
            //    int s = 0;
            //    for (int i = 0; i < gdRKDH.Rows.Count; i++)
            //    {
            //        if (i == rowIndex)
            //        {
            //            dt.Rows.Add();
            //            dt.Rows[s][0] = "";
            //            dt.Rows[s][1] = "";
            //            dt.Rows[s][2] = "";
            //            dt.Rows[s][3] = "";
            //            dt.Rows[s][4] = "";
            //            dt.Rows[s][5] = "";
            //            dt.Rows[s][6] = "";
            //            dt.Rows[s][7] = "";
            //            dt.Rows[s][8] = "";
            //            dt.Rows[s][9] = false;
            //            dt.Rows[s][10] = "0";
            //            dt.Rows[s][11] = "0";
            //            s = s + 1;
            //        }
            //        dt.Rows.Add();
            //        dt.Rows[s][0] = ((Label)gdRKDH.Rows[i].FindControl("lbLB")).Text;
            //        dt.Rows[s][1] = ((Label)gdRKDH.Rows[i].FindControl("lbWL")).Text;
            //        dt.Rows[s][2] = ((TextBox)gdRKDH.Rows[i].FindControl("txtWLGG")).Text;
            //        dt.Rows[s][3] = ((TextBox)gdRKDH.Rows[i].FindControl("txtDW")).Text;
            //        dt.Rows[s][4] = ((TextBox)gdRKDH.Rows[i].FindControl("txtSL")).Text;
            //        dt.Rows[s][5] = ((TextBox)gdRKDH.Rows[i].FindControl("txtDJ")).Text;
            //        dt.Rows[s][6] = ((TextBox)gdRKDH.Rows[i].FindControl("txtZJE")).Text;
            //        dt.Rows[s][7] = ((DropDownList)gdRKDH.Rows[i].FindControl("ddlKWMC")).SelectedValue;
            //        dt.Rows[s][8] = ((TextBox)gdRKDH.Rows[i].FindControl("txtSM")).Text;
            //        dt.Rows[s][9] = ((CheckBox)gdRKDH.Rows[i].FindControl("cbXZ")).Checked;
            //        dt.Rows[s][10] = ((Label)gdRKDH.Rows[i].FindControl("RuKMXID")).Text;
            //        dt.Rows[s][11] = ((Label)gdRKDH.Rows[i].FindControl("materialID")).Text;
            //        s = s + 1;
            //    }
            //    gdRKDH.DataSource = dt;
            //    gdRKDH.DataBind();
            //    for (int i = 0; i < dt.Rows.Count; i++)
            //    {
            //        if (dt.Rows[i][7].ToString() == "")
            //        {
            //            dt.Rows[i][7] = "0";
            //        }
            //        DropDownList ddlKWMC = ((DropDownList)gdRKDH.Rows[i].FindControl("ddlKWMC"));
            //        ddlKWMC.SelectedValue = dt.Rows[i][7].ToString();

            //        CheckBox cbXZ = (CheckBox)gdRKDH.Rows[i].FindControl("cbXZ");
            //        cbXZ.Checked = Convert.ToBoolean(dt.Rows[i][9].ToString());
            //    }
            //    BANDHJ();
            //}
        }
    }
}