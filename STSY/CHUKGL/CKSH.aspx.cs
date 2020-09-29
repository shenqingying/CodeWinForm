using Bussiness.STSY;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace STSY.CHUKGL
{
    public partial class CKSH : System.Web.UI.Page
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
                divRKDTX.Visible = false;
                divRKDLB.Visible = true;
                rbl.SelectedValue = "0";
                txtZDSJ.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                txtZDR.Text = Session["STAFFNAME"].ToString();
                txtLBCKRQE.Text = DateTime.Now.ToString("yyyy-MM-dd");
                DateTime now = DateTime.Now;
                DateTime d1 = new DateTime(now.Year, now.Month, 1);
                txtLBCKRQS.Text = d1.ToString("yyyy-MM-dd");
                txtCKRQ.Text = DateTime.Now.ToString("yyyy-MM-dd");
                lbtnCKDTX.BackColor = System.Drawing.Color.White;
                lbtnCKDLB.BackColor = System.Drawing.Color.DarkGray;
                KZALL(false);
            }
        }
        private void STAFFQX()
        {
            if (Session["STAFFID"] == null)
            {
                Response.Redirect("../login.aspx?RI=1");
            }
            bool s = mainService.SELECT_HG_RIGHT(Session["STAFFID"].ToString(), "211");
            if (s == false)
            {
                Response.Redirect("../Index/Index.aspx?RI=1");
            }
        }
        protected void btnCXSX_Click(object sender, EventArgs e)
        {
            LOGINTIMEOUT();
            if (lbISLB.Text.ToUpper() == "TRUE")
            {
                DataTable dt = chukService.SELECTCKD(rbl.SelectedValue, ddlLBCKBH.SelectedValue, ddlLBKHBH.SelectedValue, txtLBCKRQS.Text.Trim(), txtLBCKRQE.Text.Trim(), ddlLBCKLX.SelectedValue, txtLBCKDH.Text, Session["STAFFID"].ToString(), "");
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

        protected void btnDJTH_Click(object sender, EventArgs e)
        {
            if (lbRI.Text == "0")
            {
                this.Page.RegisterStartupScript("onclick", "<script>alert('请选择对应的单据并点击查看至详情页面后点击按钮!')</script>");
                return;
            }
            if (lbISLB.Text.ToUpper() == "TRUE")
            {
                this.Page.RegisterStartupScript("onclick", "<script>alert('请选择对应的单据并点击查看至详情页面后点击按钮!')</script>");
                return;
            }
            if (lbnStatus.Text == "2")
            {
                this.Page.RegisterStartupScript("onclick", "<script>alert('单据已审核，不能退回!')</script>");
                return;
            }
            try
            {
                chukService.UPDATETJTH("0", lbRI.Text);
                this.Page.RegisterStartupScript("onclick", "<script>alert('单据已退回!')</script>");
            }
            catch
            {
                this.Page.RegisterStartupScript("onclick", "<script>alert('退回失败!')</script>");
            }
        }

        protected void btnSHCK_Click(object sender, EventArgs e)
        {
            if (lbRI.Text == "0")
            {
                this.Page.RegisterStartupScript("onclick", "<script>alert('请选择对应的单据并点击查看至详情页面后点击按钮!')</script>");
                return;
            }
            if (lbISLB.Text.ToUpper() == "TRUE")
            {
                this.Page.RegisterStartupScript("onclick", "<script>alert('请选择对应的单据并点击查看至详情页面后点击按钮!')</script>");
                return;
            }
            if (lbnStatus.Text == "2")
            {
                this.Page.RegisterStartupScript("onclick", "<script>alert('单据已审核，无需重复审核!')</script>");
                return;
            }
            try
            {
                DataTable dtMX = chukService.SELECTCKDMXBYID(lbRI.Text);
                if (dtMX.Rows.Count > 0)
                {
                    for (int i = 0; i < dtMX.Rows.Count; i++)
                    {
                        DataTable dtKC = chukService.GETWLKC(ddlCKBH.SelectedValue, dtMX.Rows[i]["materialID"].ToString(), dtMX.Rows[i]["PlaceID"].ToString());
                        double mtNumber = Convert.ToDouble(dtKC.Rows[0]["mtNumber"].ToString()) - Convert.ToDouble(dtMX.Rows[i]["mtNumber"].ToString());
                        double mtTotal = Convert.ToDouble(dtKC.Rows[0]["mtTotal"].ToString()) - Convert.ToDouble(dtMX.Rows[i]["mtTotal"].ToString());
                        chukService.UPDATEMSRealStock(mtNumber.ToString(), mtTotal.ToString(), ddlCKBH.SelectedValue, dtMX.Rows[i]["materialID"].ToString(), dtMX.Rows[i]["PlaceID"].ToString());
                    }
                }
                string SMonY = Convert.ToDateTime(txtCKRQ.Text.Trim()).ToString("yyyyMM");
                chukService.UPDATEMSChuKD(SMonY, "2", "1", Session["STAFFID"].ToString(), Session["STAFFNAME"].ToString(), lbRI.Text);
                chukService.UPDATEMSMaterial(lbRI.Text);
                lbnStatus.Text = "2";
                this.Page.RegisterStartupScript("onclick", "<script>alert('单据审核成功!')</script>");
            }
            catch
            {
                this.Page.RegisterStartupScript("onclick", "<script>alert('单据审核失败!')</script>");
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

        protected void gdCKDH_RowDataBound(object sender, GridViewRowEventArgs e)
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

        protected void gdCKDLB_RowDataBound(object sender, GridViewRowEventArgs e)
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
            lbISLB.Text = "FALSE";
        }

        private void LOGINTIMEOUT()
        {
            if (Session["STAFFID"] == null)
            {
                Response.Redirect("../login.aspx?RI=1");
            }
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
                    }
                }
            }
            BANDHJ();
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
            foot.Cells[4].HorizontalAlign = HorizontalAlign.Right;
            foot.Cells[4].Text = sumSL.ToString();
            foot.Cells[6].HorizontalAlign = HorizontalAlign.Right;
            foot.Cells[6].Text = sumZJE.ToString();
        }
    }
}