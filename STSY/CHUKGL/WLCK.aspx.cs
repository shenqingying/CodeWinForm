using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bussiness.STSY;
using System.Data;

namespace STSY.CHUKGL
{
    public partial class WLCK : System.Web.UI.Page
    {
        CKService ckService = new CKService();
        WLService wlService = new WLService();
        ChuKService chukService = new ChuKService();
        Mainservice mainService = new Mainservice();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LOGINTIMEOUT();
                STAFFQX();
                BANDDDLCK();
                BANDddlKHBH();
                BANDddlLBCKBH();
                BANDddlLBKHBH();
                BAND();
                divRKDTX.Visible = true;
                divRKDLB.Visible = false;
                rbl.SelectedValue = "0";
                txtZDSJ.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                txtZDR.Text = Session["STAFFNAME"].ToString();
                txtLBCKRQE.Text = DateTime.Now.ToString("yyyy-MM-dd");
                DateTime now = DateTime.Now;
                DateTime d1 = new DateTime(now.Year, now.Month, 1);
                txtLBCKRQS.Text = d1.ToString("yyyy-MM-dd");
                txtCKRQ.Text = DateTime.Now.ToString("yyyy-MM-dd");
                //btnCXSX.BackColor = System.Drawing.Color.FromName("#3498db");
                BANDBOTTON("1,4,5,6,7,9");
                lbtnCKDTX.BackColor = System.Drawing.Color.DarkGray;
                lbtnCKDLB.BackColor = System.Drawing.Color.White;
                KZALL(true);
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
            bool s = mainService.SELECT_HG_RIGHT(Session["STAFFID"].ToString(), "210");
            if (s == false)
            {
                Response.Redirect("../Index/Index.aspx?RI=1");
            }
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
            btnPLXZ.Enabled = false;
            btnPLXZ.BackColor = System.Drawing.Color.Gray;
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
                        btnPLXZ.Enabled = true;
                        btnPLXZ.BackColor = System.Drawing.Color.FromName("#3498db");
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
                }
                gdCKDH.DataSource = dt;
                gdCKDH.DataBind();
            }
            else
            {
                DataTable dt = chukService.SELECTCKDBYID(lbRI.Text);
                if (dt.Rows.Count > 0)
                {
                    ddlCKBH.SelectedValue = dt.Rows[0]["StockID"].ToString();
                    ddlKHBH.SelectedValue = dt.Rows[0]["ClientID"].ToString();
                    txtCKRQ.Text = Convert.ToDateTime(dt.Rows[0]["ChuKDate"].ToString()).ToString("yyyy-MM-dd");
                    ddlCKLX.SelectedValue = dt.Rows[0]["TypeID"].ToString();
                    txtCKDH.Text = dt.Rows[0]["ChuKDNO"].ToString();
                    txtCKBZ.Text = dt.Rows[0]["ChuKMem"].ToString();
                    txtZDR.Text = dt.Rows[0]["fillName"].ToString();
                    txtZDSJ.Text = dt.Rows[0]["fillTime"].ToString();
                }
                DataTable dtMX = chukService.SELECTCKDMXBYID(lbRI.Text);
                if (dtMX.Rows.Count > 0)
                {
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
                    for (int i = 0; i < dtMX.Rows.Count; i++)
                    {
                        dtband.Rows.Add();
                        DataTable dtlbbh = wlService.GETWLTYPEbymaterialID(dtMX.Rows[i]["materialID"].ToString());
                        dtband.Rows[i][0] = dtlbbh.Rows[0]["typeID"].ToString();
                        dtband.Rows[i][1] = dtMX.Rows[i]["materialID"].ToString();
                        dtband.Rows[i][2] = "";
                        dtband.Rows[i][3] = dtMX.Rows[i]["mtUnit"].ToString();
                        dtband.Rows[i][4] = dtMX.Rows[i]["mtNumber"].ToString();
                        dtband.Rows[i][5] = dtMX.Rows[i]["mtPrice"].ToString();
                        dtband.Rows[i][6] = dtMX.Rows[i]["mtTotal"].ToString();
                        dtband.Rows[i][7] = dtMX.Rows[i]["PlaceID"].ToString();
                        dtband.Rows[i][8] = dtMX.Rows[i]["MXMemo"].ToString();
                        dtband.Rows[i][9] = false;
                    }
                    gdCKDH.DataSource = dtband;
                    gdCKDH.DataBind();
                    for (int i = 0; i < dtband.Rows.Count; i++)
                    {
                        if (dtband.Rows[i][0].ToString() == "")
                        {
                            dtband.Rows[i][0] = "0";
                        }
                        DropDownList ddlLBBH = (DropDownList)gdCKDH.Rows[i].FindControl("ddlLBBH");
                        ddlLBBH.SelectedValue = dtband.Rows[i][0].ToString();
                        if (dtband.Rows[i][1].ToString() == "")
                        {
                            dtband.Rows[i][1] = "0";
                        }
                        DataTable dtWL = wlService.GETWLTYPE(dtband.Rows[i][0].ToString());
                        if (dtWL.Rows.Count > 0)
                        {
                            for (int j = 0; j < dtWL.Rows.Count; j++)
                            {
                                dtWL.Rows[j]["mtName"] = dtWL.Rows[j]["mtSN"].ToString() + "  " + dtWL.Rows[j]["mtName"].ToString();
                            }
                        }
                        DropDownList ddlLBMC = ((DropDownList)gdCKDH.Rows[i].FindControl("ddlLBMC"));
                        ddlLBMC.DataSource = dtWL;
                        ddlLBMC.DataTextField = "mtName";
                        ddlLBMC.DataValueField = "materialID";
                        ddlLBMC.DataBind();
                        ListItem item = new ListItem();
                        item.Text = "=请选择=";
                        item.Value = "0";
                        ddlLBMC.Items.Insert(0, item);
                        ddlLBMC.SelectedValue = dtband.Rows[i][1].ToString();
                        if (dtband.Rows[i][7].ToString() == "")
                        {
                            dtband.Rows[i][7] = "0";
                        }
                        DropDownList ddlKWMC = (DropDownList)gdCKDH.Rows[i].FindControl("ddlKWMC");
                        ddlKWMC.SelectedValue = dtband.Rows[i][7].ToString();

                        CheckBox cbXZ = (CheckBox)gdCKDH.Rows[i].FindControl("cbXZ");
                        cbXZ.Checked = Convert.ToBoolean(dtband.Rows[i][9].ToString());
                    }
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

        private void BANDddlKHBH()
        {
            DataTable dt = chukService.GETKHINFO();
            dt.Columns.Add("NAME", typeof(string));
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dt.Rows[i]["NAME"] = dt.Rows[i]["ClSN"].ToString() + "  " + dt.Rows[i]["ClName"].ToString();
                }
            }
            ddlKHBH.DataSource = dt;
            ddlKHBH.DataTextField = "NAME";
            ddlKHBH.DataValueField = "ClientID";
            ddlKHBH.DataBind();
            ListItem item = new ListItem();
            item.Text = "=请选择=";
            item.Value = "0";
            ddlKHBH.Items.Insert(0, item);
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

        private void BANDddlLBKHBH()
        {
            DataTable dt = chukService.GETKHINFO();
            dt.Columns.Add("NAME", typeof(string));
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dt.Rows[i]["NAME"] = dt.Rows[i]["ClSN"].ToString() + "  " + dt.Rows[i]["ClName"].ToString();
                }
            }
            ddlLBKHBH.DataSource = dt;
            ddlLBKHBH.DataTextField = "NAME";
            ddlLBKHBH.DataValueField = "ClientID";
            ddlLBKHBH.DataBind();
            ListItem item = new ListItem();
            item.Text = "=请选择=";
            item.Value = "0";
            ddlLBKHBH.Items.Insert(0, item);
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow) //判断是否是DataRow，以防止鼠标经过Header也有效果   
            {
                e.Row.Attributes.Add("onmouseover", "e=this.style.backgroundColor; this.style.backgroundColor='#BBFFFF'");
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=e");
                DropDownList ddlLBBH = ((DropDownList)e.Row.FindControl("ddlLBBH"));

                DataTable dt = wlService.GETWLTYPEKC();
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        dt.Rows[i]["typeSN"] = dt.Rows[i]["typeSN"].ToString() + "  " + dt.Rows[i]["typeName"].ToString();
                    }
                }
                ddlLBBH.DataSource = dt;
                ddlLBBH.DataTextField = "typeSN";
                ddlLBBH.DataValueField = "typeID";
                ddlLBBH.DataBind();
                ListItem item = new ListItem();
                item.Text = "=请选择=";
                item.Value = "0";
                ddlLBBH.Items.Insert(0, item);

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

        protected void ddlLBBH_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList t = (DropDownList)sender;
            GridViewRow drv = (GridViewRow)t.NamingContainer;
            int rowIndex = drv.RowIndex;
            string coid = ((DropDownList)gdCKDH.Rows[drv.RowIndex].FindControl("ddlLBBH")).SelectedValue;
            DataTable dt = wlService.GETWLTYPEKC(coid);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dt.Rows[i]["mtName"] = dt.Rows[i]["mtSN"].ToString() + "  " + dt.Rows[i]["mtName"].ToString();
                }
            }
            DropDownList ddlLBMC = ((DropDownList)gdCKDH.Rows[drv.RowIndex].FindControl("ddlLBMC"));
            ddlLBMC.DataSource = dt;
            ddlLBMC.DataTextField = "mtName";
            ddlLBMC.DataValueField = "materialID";
            ddlLBMC.DataBind();
            ListItem item = new ListItem();
            item.Text = "=请选择=";
            item.Value = "0";
            ddlLBMC.Items.Insert(0, item);
        }

        protected void ddlLBMC_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList t = (DropDownList)sender;
            GridViewRow drv = (GridViewRow)t.NamingContainer;
            int rowIndex = drv.RowIndex;
            string coid = ((DropDownList)gdCKDH.Rows[drv.RowIndex].FindControl("ddlLBMC")).SelectedValue;
            DataTable dt = wlService.GETWLTYPEbymaterialID(coid);
            TextBox txtDW = ((TextBox)gdCKDH.Rows[drv.RowIndex].FindControl("txtDW"));
            txtDW.Text = dt.Rows[0]["mtUnit"].ToString();
            TextBox txtWLGG = ((TextBox)gdCKDH.Rows[drv.RowIndex].FindControl("txtWLGG"));
            txtWLGG.Text = dt.Rows[0]["mtSpec"].ToString();

            DataTable dtKC = chukService.GETWLKCINFO(coid);
            TextBox txtSL = ((TextBox)gdCKDH.Rows[drv.RowIndex].FindControl("txtSL"));
            txtSL.Text = dtKC.Rows[0]["mtNumber"].ToString();
            TextBox txtZJE = ((TextBox)gdCKDH.Rows[drv.RowIndex].FindControl("txtZJE"));
            txtZJE.Text = dtKC.Rows[0]["mtTotal"].ToString();
            TextBox txtDJ = ((TextBox)gdCKDH.Rows[drv.RowIndex].FindControl("txtDJ"));
            if (Convert.ToDouble(txtZJE.Text) == 0)
            {
                txtDJ.Text = "0";
            }
            else
            {
                txtZJE.Text = Math.Round(Convert.ToDouble(txtZJE.Text), 2, MidpointRounding.AwayFromZero).ToString();
                //txtDJ.Text = Math.Round((Convert.ToDouble(txtZJE.Text) / Convert.ToDouble(txtSL.Text)), 4).ToString();
                txtDJ.Text = (Convert.ToDouble(txtZJE.Text) / Convert.ToDouble(txtSL.Text)).ToString();
            }
            BANDHJ();
        }

        protected void txtSL_TextChanged(object sender, EventArgs e)
        {
            TextBox t = (TextBox)sender;
            GridViewRow drv = (GridViewRow)t.NamingContainer;
            int rowIndex = drv.RowIndex;
            TextBox txtSL = ((TextBox)gdCKDH.Rows[drv.RowIndex].FindControl("txtSL"));
            TextBox txtDJ = ((TextBox)gdCKDH.Rows[drv.RowIndex].FindControl("txtDJ"));
            TextBox txtZJE = ((TextBox)gdCKDH.Rows[drv.RowIndex].FindControl("txtZJE"));
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
            if (rowIndex < gdCKDH.Rows.Count)
            {
                TextBox txtSLNEXT = ((TextBox)gdCKDH.Rows[rowIndex].FindControl("txtSL"));
                txtSLNEXT.Focus();
                txtSLNEXT.Attributes.Add("onfocus", "this.select()");
            }
        }

        protected void txtDJ_TextChanged(object sender, EventArgs e)
        {
            TextBox t = (TextBox)sender;
            GridViewRow drv = (GridViewRow)t.NamingContainer;
            int rowIndex = drv.RowIndex;
            TextBox txtSL = ((TextBox)gdCKDH.Rows[drv.RowIndex].FindControl("txtSL"));
            TextBox txtDJ = ((TextBox)gdCKDH.Rows[drv.RowIndex].FindControl("txtDJ"));
            TextBox txtZJE = ((TextBox)gdCKDH.Rows[drv.RowIndex].FindControl("txtZJE"));
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
            if (rowIndex < gdCKDH.Rows.Count)
            {
                TextBox txtDJNEXT = ((TextBox)gdCKDH.Rows[rowIndex].FindControl("txtDJ"));
                txtDJNEXT.Focus();
                txtDJNEXT.Attributes.Add("onfocus", "this.select()");
            }
        }

        protected void btnCXSX_Click(object sender, EventArgs e)
        {
            LOGINTIMEOUT();
            if (lbISLB.Text.ToUpper() == "TRUE")
            {
                DataTable dt = chukService.SELECTCKD(rbl.SelectedValue, ddlLBCKBH.SelectedValue, ddlLBKHBH.SelectedValue, txtLBCKRQS.Text.Trim(), txtLBCKRQE.Text.Trim(), ddlLBCKLX.SelectedValue, txtLBCKDH.Text, Session["STAFFID"].ToString(), "TRUE");
                dt.Columns.Add("ChuKDate1", typeof(string));
                dt.Columns.Add("ISSH", typeof(bool));
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        dt.Rows[i]["ChuKDate1"] = Convert.ToDateTime(dt.Rows[i]["ChuKDate"].ToString()).ToString("yyyy-MM-dd");
                        if (dt.Rows[i]["isAudi"].ToString().ToUpper() == "TRUE")
                        {
                            dt.Rows[i]["ISSH"] = true;
                        }
                        else
                        {
                            dt.Rows[i]["ISSH"] = false;
                        }
                    }
                }
                gdCKDLB.DataSource = dt;
                gdCKDLB.DataBind();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    CheckBox cbSH = ((CheckBox)gdCKDLB.Rows[i].FindControl("cbSH"));
                    bool z = Convert.ToBoolean(dt.Rows[i]["ISSH"].ToString());
                    cbSH.Checked = z;
                }
            }
        }

        protected void btnXZ_Click(object sender, EventArgs e)
        {
            lbRI.Text = "0";
            BAND();
            BANDBOTTON("1,4,5,6,7,9");
            lbISADD.Text = "true";
            lbISED.Text = "false";
            lbtnCKDTX.BackColor = System.Drawing.Color.DarkGray;
            lbtnCKDLB.BackColor = System.Drawing.Color.White;
            divRKDTX.Visible = true;
            divRKDLB.Visible = false;
            lbISLB.Text = "FALSE";
            CLEARALL();
            KZALL(true);
            lbnStatus.Text = "0";
        }

        protected void btnBJ_Click(object sender, EventArgs e)
        {
            if (lbnStatus.Text != "0")
            {
                this.Page.RegisterStartupScript("onclick", "<script>alert('单据已提交，不能进行修改!')</script>");
            }
            lbISED.Text = "true";
            BANDBOTTON("1,4,5,6,7,9");
            lbISADD.Text = "false";
            lbISED.Text = "true";
            lbtnCKDTX.BackColor = System.Drawing.Color.DarkGray;
            lbtnCKDLB.BackColor = System.Drawing.Color.White;
            divRKDTX.Visible = true;
            divRKDLB.Visible = false;
            lbISLB.Text = "FALSE";
            KZALL(true);
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
            for (int i = 0; i < gdCKDH.Rows.Count; i++)
            {
                dt.Rows.Add();
                dt.Rows[i][0] = ((DropDownList)gdCKDH.Rows[i].FindControl("ddlLBBH")).SelectedValue;
                dt.Rows[i][1] = ((DropDownList)gdCKDH.Rows[i].FindControl("ddlLBMC")).SelectedValue;
                dt.Rows[i][2] = ((TextBox)gdCKDH.Rows[i].FindControl("txtWLGG")).Text;
                dt.Rows[i][3] = ((TextBox)gdCKDH.Rows[i].FindControl("txtDW")).Text;
                dt.Rows[i][4] = ((TextBox)gdCKDH.Rows[i].FindControl("txtSL")).Text;
                dt.Rows[i][5] = ((TextBox)gdCKDH.Rows[i].FindControl("txtDJ")).Text;
                dt.Rows[i][6] = ((TextBox)gdCKDH.Rows[i].FindControl("txtZJE")).Text;
                dt.Rows[i][7] = ((DropDownList)gdCKDH.Rows[i].FindControl("ddlKWMC")).SelectedValue;
                dt.Rows[i][8] = ((TextBox)gdCKDH.Rows[i].FindControl("txtSM")).Text;
                dt.Rows[i][9] = ((CheckBox)gdCKDH.Rows[i].FindControl("cbXZ")).Checked;
            }
            for (int i = gdCKDH.Rows.Count; i < 5 + gdCKDH.Rows.Count; i++)
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
            }
            gdCKDH.DataSource = dt;
            gdCKDH.DataBind();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i][0].ToString() == "")
                {
                    dt.Rows[i][0] = "0";
                }
                DropDownList ddlLBBH = (DropDownList)gdCKDH.Rows[i].FindControl("ddlLBBH");
                ddlLBBH.SelectedValue = dt.Rows[i][0].ToString();
                if (dt.Rows[i][1].ToString() == "")
                {
                    dt.Rows[i][1] = "0";
                }
                DataTable dtWL = wlService.GETWLTYPE(dt.Rows[i][0].ToString());
                if (dtWL.Rows.Count > 0)
                {
                    for (int j = 0; j < dtWL.Rows.Count; j++)
                    {
                        dtWL.Rows[j]["mtName"] = dtWL.Rows[j]["mtSN"].ToString() + "  " + dtWL.Rows[j]["mtName"].ToString();
                    }
                }
                DropDownList ddlLBMC = ((DropDownList)gdCKDH.Rows[i].FindControl("ddlLBMC"));
                ddlLBMC.DataSource = dtWL;
                ddlLBMC.DataTextField = "mtName";
                ddlLBMC.DataValueField = "materialID";
                ddlLBMC.DataBind();
                ListItem item = new ListItem();
                item.Text = "=请选择=";
                item.Value = "0";
                ddlLBMC.Items.Insert(0, item);
                ddlLBMC.SelectedValue = dt.Rows[i][1].ToString();
                if (dt.Rows[i][7].ToString() == "")
                {
                    dt.Rows[i][7] = "0";
                }
                DropDownList ddlKWMC = (DropDownList)gdCKDH.Rows[i].FindControl("ddlKWMC");
                ddlKWMC.SelectedValue = dt.Rows[i][7].ToString();

                CheckBox cbXZ = (CheckBox)gdCKDH.Rows[i].FindControl("cbXZ");
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
            if (gdCKDH.Rows.Count > 0)
            {
                int s = 0;
                for (int i = 0; i < gdCKDH.Rows.Count; i++)
                {
                    if (((CheckBox)gdCKDH.Rows[i].FindControl("cbXZ")).Checked == false)
                    {
                        dt.Rows.Add();
                        dt.Rows[s][0] = ((DropDownList)gdCKDH.Rows[i].FindControl("ddlLBBH")).SelectedValue;
                        dt.Rows[s][1] = ((DropDownList)gdCKDH.Rows[i].FindControl("ddlLBMC")).SelectedValue;
                        dt.Rows[s][2] = ((TextBox)gdCKDH.Rows[i].FindControl("txtWLGG")).Text;
                        dt.Rows[s][3] = ((TextBox)gdCKDH.Rows[i].FindControl("txtDW")).Text;
                        dt.Rows[s][4] = ((TextBox)gdCKDH.Rows[i].FindControl("txtSL")).Text;
                        dt.Rows[s][5] = ((TextBox)gdCKDH.Rows[i].FindControl("txtDJ")).Text;
                        dt.Rows[s][6] = ((TextBox)gdCKDH.Rows[i].FindControl("txtZJE")).Text;
                        dt.Rows[s][7] = ((DropDownList)gdCKDH.Rows[i].FindControl("ddlKWMC")).SelectedValue;
                        dt.Rows[s][8] = ((TextBox)gdCKDH.Rows[i].FindControl("txtSM")).Text;
                        dt.Rows[s][9] = ((CheckBox)gdCKDH.Rows[i].FindControl("cbXZ")).Checked;
                        s = s + 1;
                    }
                }
                gdCKDH.DataSource = dt;
                gdCKDH.DataBind();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i][0].ToString() == "")
                    {
                        dt.Rows[i][0] = "0";
                    }
                    DropDownList ddlLBBH = (DropDownList)gdCKDH.Rows[i].FindControl("ddlLBBH");
                    ddlLBBH.SelectedValue = dt.Rows[i][0].ToString();
                    if (dt.Rows[i][1].ToString() == "")
                    {
                        dt.Rows[i][1] = "0";
                    }
                    DataTable dtWL = wlService.GETWLTYPE(dt.Rows[i][0].ToString());
                    if (dtWL.Rows.Count > 0)
                    {
                        for (int j = 0; j < dtWL.Rows.Count; j++)
                        {
                            dtWL.Rows[j]["mtName"] = dtWL.Rows[j]["mtSN"].ToString() + "  " + dtWL.Rows[j]["mtName"].ToString();
                        }
                    }
                    DropDownList ddlLBMC = ((DropDownList)gdCKDH.Rows[i].FindControl("ddlLBMC"));
                    ddlLBMC.DataSource = dtWL;
                    ddlLBMC.DataTextField = "mtName";
                    ddlLBMC.DataValueField = "materialID";
                    ddlLBMC.DataBind();
                    ListItem item = new ListItem();
                    item.Text = "=请选择=";
                    item.Value = "0";
                    ddlLBMC.Items.Insert(0, item);
                    ddlLBMC.SelectedValue = dt.Rows[i][1].ToString();
                    if (dt.Rows[i][7].ToString() == "")
                    {
                        dt.Rows[i][7] = "0";
                    }
                    DropDownList ddlKWMC = ((DropDownList)gdCKDH.Rows[i].FindControl("ddlKWMC"));
                    ddlKWMC.SelectedValue = dt.Rows[i][7].ToString();

                    CheckBox cbXZ = (CheckBox)gdCKDH.Rows[i].FindControl("cbXZ");
                    cbXZ.Checked = Convert.ToBoolean(dt.Rows[i][9].ToString());
                }
            }
        }

        protected void btnSAVE_Click(object sender, EventArgs e)
        {
            LOGINTIMEOUT();
            if (lbISED.Text == "true" && lbRI.Text != "0")
            {
                chukService.DELETECKDMX(lbRI.Text);
                chukService.DELETECKD(lbRI.Text);
            }
            if ((lbISADD.Text == "true" && lbRI.Text == "0") || (lbISED.Text == "true" && lbRI.Text != "0"))
            {
                string s = chukService.insertCKDH(txtCKDH.Text.Trim(), ddlCKBH.SelectedValue, ddlKHBH.SelectedValue, txtCKRQ.Text.Trim(), txtCKBZ.Text.Trim(), Session["STAFFID"].ToString(), txtZDR.Text.Trim(), txtZDSJ.Text.Trim(), "0", "0", ddlCKLX.SelectedValue);
                for (int i = 0; i < gdCKDH.Rows.Count; i++)
                {
                    string[] str = new string[10];
                    str[0] = ((DropDownList)gdCKDH.Rows[i].FindControl("ddlLBBH")).SelectedValue;
                    str[1] = ((DropDownList)gdCKDH.Rows[i].FindControl("ddlLBMC")).SelectedValue;
                    str[2] = ((TextBox)gdCKDH.Rows[i].FindControl("txtWLGG")).Text;
                    str[3] = ((TextBox)gdCKDH.Rows[i].FindControl("txtDW")).Text;
                    str[4] = ((TextBox)gdCKDH.Rows[i].FindControl("txtSL")).Text;
                    str[5] = ((TextBox)gdCKDH.Rows[i].FindControl("txtDJ")).Text;
                    str[6] = ((TextBox)gdCKDH.Rows[i].FindControl("txtZJE")).Text;
                    str[7] = ((DropDownList)gdCKDH.Rows[i].FindControl("ddlKWMC")).SelectedValue;
                    str[8] = ((TextBox)gdCKDH.Rows[i].FindControl("txtSM")).Text;
                    if (str[0] != "0" && str[0] != "" && str[1] != "0" && str[1] != "")
                    {
                        chukService.insertCKDMX(s, str[1], str[4], str[3], str[5], str[6], str[7], str[8], str[5], str[6]);
                    }
                    else
                    {
                        break;
                    }
                }
                lbRI.Text = s;
            }
            if (lbISED.Text.ToUpper() == "FALSE")
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
            CLEARALL();
            BAND();
            KZALL(false);
        }

        protected void btnQX_Click(object sender, EventArgs e)
        {
            lbISADD.Text = "false";
            lbISED.Text = "false";
            BANDBOTTON("1,2,3,8");
            CLEARALL();
            BAND();
            KZALL(false);
        }

        protected void btnTJ_Click(object sender, EventArgs e)
        {
            if (lbnStatus.Text == "0")
            {
                if (lbRI.Text != "" && lbRI.Text != "0")
                {
                    if (lbISADD.Text.ToUpper() != "TRUE" && lbISED.Text.ToUpper() != "TRUE")
                    {
                        if (ddlCKBH.SelectedValue == "0")
                        {
                            this.Page.RegisterStartupScript("onclick", "<script>alert('请选择仓库!')</script>");
                            return;
                        }
                        if (txtCKRQ.Text.Trim() == "")
                        {
                            this.Page.RegisterStartupScript("onclick", "<script>alert('请输入日期!')</script>");
                            return;
                        }
                        //if (txtCKDH.Text.Trim() == "")
                        //{
                        //    this.Page.RegisterStartupScript("onclick", "<script>alert('请输入出库单号!')</script>");
                        //    return;
                        //}
                        try
                        {
                            string s = Convert.ToDateTime(txtCKRQ.Text.Trim()).ToString("yyyyMM");
                            chukService.UPDATETJ(s, "1", lbRI.Text);
                            this.Page.RegisterStartupScript("onclick", "<script>alert('提交成功!')</script>");
                            lbnStatus.Text = "1";
                        }
                        catch
                        {
                            this.Page.RegisterStartupScript("onclick", "<script>alert('提交失败!')</script>");
                        }
                    }
                    else
                    {
                        this.Page.RegisterStartupScript("onclick", "<script>alert('提交单据时请不要在新增或修改状态!')</script>");
                        return;
                    }
                }
                else
                {
                    this.Page.RegisterStartupScript("onclick", "<script>alert('请先选择查看所要提交的单据!')</script>");
                    return;
                }
            }
            else
            {
                this.Page.RegisterStartupScript("onclick", "<script>alert('单据已提交，无需重复提交!')</script>");
                return;
            }
        }

        protected void lbtnCKDTX_Click(object sender, EventArgs e)
        {
            lbtnCKDTX.BackColor = System.Drawing.Color.DarkGray;
            lbtnCKDLB.BackColor = System.Drawing.Color.White;
            divRKDTX.Visible = true;
            divRKDLB.Visible = false;
            lbISLB.Text = "FALSE";
        }

        protected void lbtnCKDLB_Click(object sender, EventArgs e)
        {
            lbtnCKDTX.BackColor = System.Drawing.Color.White;
            lbtnCKDLB.BackColor = System.Drawing.Color.DarkGray;
            divRKDTX.Visible = false;
            divRKDLB.Visible = true;
            lbISLB.Text = "TRUE";
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
            Label ChuKDID = ((Label)gdCKDLB.Rows[drv.RowIndex].FindControl("ChuKDID"));
            Label nStatus = ((Label)gdCKDLB.Rows[drv.RowIndex].FindControl("nStatus"));
            lbRI.Text = ChuKDID.Text;
            lbnStatus.Text = nStatus.Text;
            BAND();
            lbtnCKDTX.BackColor = System.Drawing.Color.DarkGray;
            lbtnCKDLB.BackColor = System.Drawing.Color.White;
            divRKDTX.Visible = true;
            divRKDLB.Visible = false;
            BANDBOTTON("1,2,3,8");
            lbISADD.Text = "false";
            lbISED.Text = "false";
            KZALL(false);
        }

        private void CLEARALL()
        {
            ddlKHBH.SelectedValue = "0";
            ddlCKLX.SelectedValue = "-1";
            txtCKDH.Text = "";
            txtCKBZ.Text = "";
            txtZDR.Text = Session["STAFFNAME"].ToString();
            txtZDSJ.Text = DateTime.Now.ToString();
        }

        private void KZALL(bool s)
        {
            ddlCKBH.Enabled = s;
            ddlKHBH.Enabled = s;
            txtCKRQ.Enabled = s;
            ddlCKLX.Enabled = s;
            txtCKDH.Enabled = s;
            gdCKDH.Enabled = s;
            txtCKBZ.Enabled = s;
            txtZDR.Enabled = s;
            txtZDSJ.Enabled = s;
        }

        protected void btnPLXZ_Click(object sender, EventArgs e)
        {
            DataTable dt = chukService.insert_CHUKYZ(ddlCKBH.SelectedValue, ddlKHBH.SelectedValue, txtCKRQ.Text, ddlCKLX.SelectedValue, txtCKDH.Text, txtCKBZ.Text, txtZDR.Text, txtZDSJ.Text);
            string RI = dt.Rows[0][0].ToString();
            if (gdCKDH.Rows.Count > 0)
            {
                for (int i = 0; i < gdCKDH.Rows.Count; i++)
                {
                    DropDownList ddlLBBH = ((DropDownList)gdCKDH.Rows[i].FindControl("ddlLBBH"));
                    if (ddlLBBH.SelectedValue != "0")
                    {
                        DropDownList ddlLBMC = ((DropDownList)gdCKDH.Rows[i].FindControl("ddlLBMC"));
                        if (ddlLBMC.SelectedValue != "0")
                        {
                            TextBox txtWLGG = ((TextBox)gdCKDH.Rows[i].FindControl("txtWLGG"));
                            TextBox txtDW = ((TextBox)gdCKDH.Rows[i].FindControl("txtDW"));
                            TextBox txtSL = ((TextBox)gdCKDH.Rows[i].FindControl("txtSL"));
                            TextBox txtDJ = ((TextBox)gdCKDH.Rows[i].FindControl("txtDJ"));
                            TextBox txtZJE = ((TextBox)gdCKDH.Rows[i].FindControl("txtZJE"));
                            DropDownList ddlKWMC = ((DropDownList)gdCKDH.Rows[i].FindControl("ddlKWMC"));
                            TextBox txtSM = ((TextBox)gdCKDH.Rows[i].FindControl("txtSM"));
                            chukService.insert_CHUKYZMX(RI, ddlLBMC.SelectedValue, txtWLGG.Text, txtDW.Text, txtSL.Text, txtDJ.Text, txtZJE.Text, ddlKWMC.SelectedValue, "", txtSM.Text);
                        }
                    }
                }
            }
            Response.Redirect("PLCK.aspx?RI=" + dt.Rows[0][0].ToString());
        }

        private void BANDTX()
        {
            DataTable dt = chukService.SELECT_CHUKYZ(Request.QueryString["RI"]);
            if (dt.Rows.Count > 0)
            {
                ddlCKBH.SelectedValue = dt.Rows[0]["CK"].ToString();
                ddlKHBH.SelectedValue = dt.Rows[0]["KH"].ToString();
                txtCKRQ.Text = Convert.ToDateTime(dt.Rows[0]["CKRQ"].ToString()).ToString("yyyy-MM-dd");
                ddlCKLX.SelectedValue = dt.Rows[0]["CKLX"].ToString();
                txtCKDH.Text = dt.Rows[0]["CKDH"].ToString();
                txtCKBZ.Text = dt.Rows[0]["CKBZ"].ToString();
                txtZDR.Text = dt.Rows[0]["ZDR"].ToString();
                txtZDSJ.Text = dt.Rows[0]["ZDSJ"].ToString();
            }
            DataTable dtMX = chukService.SELECT_CHUKYZMX(Request.QueryString["RI"]);
            if (dtMX.Rows.Count > 0)
            {
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
                for (int i = 0; i < dtMX.Rows.Count; i++)
                {
                    dtband.Rows.Add();
                    DataTable dtlbbh = wlService.GETWLTYPEbymaterialID(dtMX.Rows[i]["materialID"].ToString());
                    dtband.Rows[i][0] = dtlbbh.Rows[0]["typeID"].ToString();
                    dtband.Rows[i][1] = dtMX.Rows[i]["materialID"].ToString();
                    dtband.Rows[i][2] = dtMX.Rows[i]["mtSpec"].ToString();
                    dtband.Rows[i][3] = dtMX.Rows[i]["mtUnit"].ToString();
                    dtband.Rows[i][4] = dtMX.Rows[i]["mtNumber"].ToString();
                    dtband.Rows[i][5] = dtMX.Rows[i]["PJJ"].ToString();
                    dtband.Rows[i][6] = dtMX.Rows[i]["mtTotal"].ToString();
                    dtband.Rows[i][7] = dtMX.Rows[i]["KW"].ToString();
                    dtband.Rows[i][8] = dtMX.Rows[i]["SM"].ToString();
                    dtband.Rows[i][9] = false;
                }
                gdCKDH.DataSource = dtband;
                gdCKDH.DataBind();
                for (int i = 0; i < dtband.Rows.Count; i++)
                {
                    if (dtband.Rows[i][0].ToString() == "")
                    {
                        dtband.Rows[i][0] = "0";
                    }
                    DropDownList ddlLBBH = (DropDownList)gdCKDH.Rows[i].FindControl("ddlLBBH");
                    ddlLBBH.SelectedValue = dtband.Rows[i][0].ToString();
                    if (dtband.Rows[i][1].ToString() == "")
                    {
                        dtband.Rows[i][1] = "0";
                    }
                    DataTable dtWL = wlService.GETWLTYPE(dtband.Rows[i][0].ToString());
                    if (dtWL.Rows.Count > 0)
                    {
                        for (int j = 0; j < dtWL.Rows.Count; j++)
                        {
                            dtWL.Rows[j]["mtName"] = dtWL.Rows[j]["mtSN"].ToString() + "  " + dtWL.Rows[j]["mtName"].ToString();
                        }
                    }
                    DropDownList ddlLBMC = ((DropDownList)gdCKDH.Rows[i].FindControl("ddlLBMC"));
                    ddlLBMC.DataSource = dtWL;
                    ddlLBMC.DataTextField = "mtName";
                    ddlLBMC.DataValueField = "materialID";
                    ddlLBMC.DataBind();
                    ListItem item = new ListItem();
                    item.Text = "=请选择=";
                    item.Value = "0";
                    ddlLBMC.Items.Insert(0, item);
                    ddlLBMC.SelectedValue = dtband.Rows[i][1].ToString();
                    if (dtband.Rows[i][7].ToString() == "")
                    {
                        dtband.Rows[i][7] = "0";
                    }
                    DropDownList ddlKWMC = (DropDownList)gdCKDH.Rows[i].FindControl("ddlKWMC");
                    ddlKWMC.SelectedValue = dtband.Rows[i][7].ToString();

                    CheckBox cbXZ = (CheckBox)gdCKDH.Rows[i].FindControl("cbXZ");
                    cbXZ.Checked = Convert.ToBoolean(dtband.Rows[i][9].ToString());

                    TextBox txtSL = (TextBox)gdCKDH.Rows[i].FindControl("txtSL");
                    TextBox txtDJ = (TextBox)gdCKDH.Rows[i].FindControl("txtDJ");
                    TextBox txtZJE = (TextBox)gdCKDH.Rows[i].FindControl("txtZJE");
                    txtZJE.Text = Math.Round(Convert.ToDouble(txtZJE.Text), 2, MidpointRounding.AwayFromZero).ToString();
                    //txtDJ.Text = Math.Round((Convert.ToDouble(txtZJE.Text) / Convert.ToDouble(txtSL.Text)), 4).ToString();
                    txtDJ.Text = (Convert.ToDouble(txtZJE.Text) / Convert.ToDouble(txtSL.Text)).ToString();
                }
            }
            BANDHJ();
        }

        private void BANDHJ()
        {
            double sumSL = 0;
            double sumZJE = 0;
            if (gdCKDH.Rows.Count > 0)
            {
                for (int i = 0; i < gdCKDH.Rows.Count; i++)
                {
                    TextBox txtSL = ((TextBox)gdCKDH.Rows[i].FindControl("txtSL"));
                    TextBox txtZJE = ((TextBox)gdCKDH.Rows[i].FindControl("txtZJE"));
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
            GridViewRow foot = gdCKDH.FooterRow;
            foot.Cells[5].HorizontalAlign = HorizontalAlign.Right;
            foot.Cells[5].Text = sumSL.ToString();
            foot.Cells[7].HorizontalAlign = HorizontalAlign.Right;
            foot.Cells[7].Text = sumZJE.ToString();
        }
    }
}