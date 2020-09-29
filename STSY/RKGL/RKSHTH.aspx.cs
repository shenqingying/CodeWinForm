using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bussiness.STSY;
using System.Data;

namespace STSY.RKGL
{
    public partial class RKSHTH : System.Web.UI.Page
    {
        CKService ckService = new CKService();
        RKService rkService = new RKService();
        WLService wlService = new WLService();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LOGINTIMEOUT();
                BANDDDLCK();
                BANDddlGYSBH();
                BANDddlLBCKBH();
                BANDddlLBGYSBH();
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
                //btnCXSX.BackColor = System.Drawing.Color.FromName("#3498db");
                BANDBOTTON("1");
            }
        }

        private void LOGINTIMEOUT()
        {
            if (Session["STAFFID"] == null)
            {
                Response.Redirect("../login.aspx?RI=1");
            }
        }

        private void BANDBOTTON(string str)
        {
            string[] str1 = str.Split(',');
            btnCXSX.Enabled = false;
            btnCXSX.BackColor = System.Drawing.Color.Gray;
            btnDJTH.Enabled = false;
            btnDJTH.BackColor = System.Drawing.Color.Gray;
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
                        btnDJTH.Enabled = true;
                        btnDJTH.BackColor = System.Drawing.Color.FromName("#3498db");
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
                DataTable dtMX = rkService.SELECTRKDMXBYID(lbRI.Text);
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
                    dtband.Columns.Add(new DataColumn("RuKMXID", typeof(string)));
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
                        dtband.Rows[i][10] = dtMX.Rows[i]["RuKMXID"].ToString();
                    }
                    gdRKDH.DataSource = dtband;
                    gdRKDH.DataBind();
                    for (int i = 0; i < dtband.Rows.Count; i++)
                    {
                        if (dtband.Rows[i][0].ToString() == "")
                        {
                            dtband.Rows[i][0] = "0";
                        }
                        DropDownList ddlLBBH = (DropDownList)gdRKDH.Rows[i].FindControl("ddlLBBH");
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
                        DropDownList ddlLBMC = ((DropDownList)gdRKDH.Rows[i].FindControl("ddlLBMC"));
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
                        DropDownList ddlKWMC = (DropDownList)gdRKDH.Rows[i].FindControl("ddlKWMC");
                        ddlKWMC.SelectedValue = dtband.Rows[i][7].ToString();

                        CheckBox cbXZ = (CheckBox)gdRKDH.Rows[i].FindControl("cbXZ");
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
            ListItem item = new ListItem();
            item.Text = "=请选择=";
            item.Value = "0";
            ddlCKBH.Items.Insert(0, item);
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

        private string DDLCKBHCHANGED()
        {
            if (ddlCKBH.SelectedValue != "0")
            {
                string ck = ddlCKBH.SelectedItem.Text;
                string[] ckname = ck.Split(' ');
                return ckname[2];
            }
            else
            {
                return "";
            }
        }

        private string DDLGYSBHCHANGED()
        {
            string ck = ddlGYSBH.SelectedItem.Text;
            string[] ckname = ck.Split(' ');
            if (ddlGYSBH.SelectedValue != "0")
            {
                return ckname[2];
            }
            else
            {
                return "";
            }
        }

        private string DDLLBCKBHCHANGED()
        {
            if (ddlLBCKBH.SelectedValue != "0")
            {
                string ck = ddlLBCKBH.SelectedItem.Text;
                string[] ckname = ck.Split(' ');
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


        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow) //判断是否是DataRow，以防止鼠标经过Header也有效果   
            {
                e.Row.Attributes.Add("onmouseover", "e=this.style.backgroundColor; this.style.backgroundColor='#BBFFFF'");
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=e");
                DropDownList ddlLBBH = ((DropDownList)e.Row.FindControl("ddlLBBH"));

                DataTable dt = wlService.GETWLTYPE();
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
            dt.Columns.Add("ISHAVEFP", typeof(string));
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dt.Rows[i]["RuKDate1"] = Convert.ToDateTime(dt.Rows[i]["RuKDate"].ToString()).ToString("yyyy-MM-dd");
                    if (dt.Rows[i]["nStatus"].ToString() == "0" || dt.Rows[i]["nStatus"].ToString() == "1")
                    {
                        dt.Rows[i]["ISSH"] = false;
                    }
                    else if (dt.Rows[i]["nStatus"].ToString() == "2")
                    {
                        dt.Rows[i]["ISSH"] = true;
                    }
                    DataTable dtishavefp = rkService.SELECTISHAVEFP(dt.Rows[i]["RuKDID"].ToString());
                    if (dtishavefp.Rows.Count == 0)
                    {
                        dt.Rows[i]["ISHAVEFP"] = "0";
                    }
                    else
                    {
                        dt.Rows[i]["ISHAVEFP"] = "1";
                    }
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

        protected void btnDJTH_Click(object sender, EventArgs e)             //单据退回
        {
            if (lbnStatus.Text == "0" || lbnStatus.Text == "1")
            {
                this.Page.RegisterStartupScript("onclick", "<script>alert('单据未审核，无需审核退回！')</script>");
                return;
            }
            DataTable dtishavefp = rkService.SELECTISHAVEFP(lbRI.Text);
            if (dtishavefp.Rows.Count == 0)
            {
                lbISHAVEFP.Text = "0";
            }
            else
            {
                lbISHAVEFP.Text = "1";
            }
            if (lbISHAVEFP.Text == "1")
            {
                this.Page.RegisterStartupScript("onclick", "<script>alert('单据已入发票，不能审核退回！')</script>");
                return;
            }
            else if (lbnStatus.Text == "2" && lbISHAVEFP.Text == "0")
            {
                LOGINTIMEOUT();
                if (lbRI.Text != "" && lbRI.Text != "0")
                {
                    if (gdRKDH.Rows.Count > 0)
                    {
                        for (int i = 0; i < gdRKDH.Rows.Count; i++)                   //为防止物料出库后对相应的入库单进行退回导致的数量为负的情况
                        {
                            string mtNumber = "";
                            DropDownList ddlLBMC = (DropDownList)gdRKDH.Rows[i].FindControl("ddlLBMC");
                            DropDownList ddlKWMC = (DropDownList)gdRKDH.Rows[i].FindControl("ddlKWMC");
                            DropDownList ddlLBBH = (DropDownList)gdRKDH.Rows[i].FindControl("ddlLBBH");
                            TextBox txtSL = (TextBox)gdRKDH.Rows[i].FindControl("txtSL");

                            string leibie = ddlLBBH.SelectedItem.Text;     //将会为负的物料类别
                            string name = ddlLBMC.SelectedItem.Text;    //将会为负的物料名称
                            DataTable dtRealStock = rkService.SELECTMSRealStock(ddlCKBH.SelectedValue, ddlLBMC.SelectedValue, ddlKWMC.SelectedValue);
                            mtNumber = (Convert.ToDouble(dtRealStock.Rows[0]["mtNumber"].ToString()) - Convert.ToDouble(txtSL.Text)).ToString();
                            if (Convert.ToDouble(mtNumber) < 0)
                            {
                                this.Page.RegisterStartupScript("onclick", "<script>alert('" + leibie + " " + name + "库存将为负数!禁止退回')</script>");
                                return;
                            }
                        }

                        for (int i = 0; i < gdRKDH.Rows.Count; i++)
                        {
                            string mtNumber = "";
                            string mtTotal = "";
                            DropDownList ddlLBMC = (DropDownList)gdRKDH.Rows[i].FindControl("ddlLBMC");
                            DropDownList ddlKWMC = (DropDownList)gdRKDH.Rows[i].FindControl("ddlKWMC");
                            TextBox txtSL = (TextBox)gdRKDH.Rows[i].FindControl("txtSL");
                            TextBox txtZJE = (TextBox)gdRKDH.Rows[i].FindControl("txtZJE");
                            DataTable dtRealStock = rkService.SELECTMSRealStock(ddlCKBH.SelectedValue, ddlLBMC.SelectedValue, ddlKWMC.SelectedValue);
                            if (dtRealStock.Rows.Count > 0)
                            {
                                mtNumber = (Convert.ToDouble(dtRealStock.Rows[0]["mtNumber"].ToString()) - Convert.ToDouble(txtSL.Text)).ToString();
                                mtTotal = (Convert.ToDouble(dtRealStock.Rows[0]["mtTotal"].ToString()) - Convert.ToDouble(txtZJE.Text)).ToString();
                                rkService.UPDATEMSRealStock(mtNumber, mtTotal, ddlCKBH.SelectedValue, ddlLBMC.SelectedValue, ddlKWMC.SelectedValue);
                            }
                        }
                        DateTime dttime = Convert.ToDateTime(txtRKRQ.Text);
                        rkService.UPDATEMSRuKD("", "1", "0", Session["STAFFNAME"].ToString(), Session["STAFFID"].ToString(), lbRI.Text);

                        
                        DateTime now = DateTime.Now;
                        rkService.save_record("ruku", txtRKDH.Text, now.ToString());





                        this.Page.RegisterStartupScript("onclick", "<script>alert('单据列表退回成功!')</script>");
                        BANDBOTTON("1");
                    }
                }
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
            BANDBOTTON("1,2");
            lbnStatus.Text = nStatus.Text;
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
            foot.Cells[5].HorizontalAlign = HorizontalAlign.Right;
            foot.Cells[5].Text = sumSL.ToString();
            foot.Cells[7].HorizontalAlign = HorizontalAlign.Right;
            foot.Cells[7].Text = sumZJE.ToString();
        }
    }
}