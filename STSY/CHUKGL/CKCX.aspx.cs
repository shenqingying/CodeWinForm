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
    public partial class CKCX : System.Web.UI.Page
    {
        ChuKService chukService = new ChuKService();
        CKService ckService = new CKService();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LOGINTIMEOUT();
                BANDDDLCK();
                BANDddlKHBH();
                BANDddlLBCKBH();
                BANDddlLBKHBH();
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
                BAND();
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

        protected void lbtnCKDLB_Click(object sender, EventArgs e)
        {
            lbtnCKDTX.BackColor = System.Drawing.Color.White;
            lbtnCKDLB.BackColor = System.Drawing.Color.DarkGray;
            divRKDTX.Visible = false;
            divRKDLB.Visible = true;
            lbISLB.Text = "TRUE";
        }

        protected void lbtnCKDTX_Click(object sender, EventArgs e)
        {
            lbtnCKDTX.BackColor = System.Drawing.Color.DarkGray;
            lbtnCKDLB.BackColor = System.Drawing.Color.White;
            divRKDTX.Visible = true;
            divRKDLB.Visible = false;
            lbISLB.Text = "FALSE";
        }

        protected void gdCKDH_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow) //判断是否是DataRow，以防止鼠标经过Header也有效果   
            {
                e.Row.Attributes.Add("onmouseover", "e=this.style.backgroundColor; this.style.backgroundColor='#CCCCCC'");
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=e");
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
            gdCKDH.DataSource = null;
            gdCKDH.DataBind();
            if (lbRI.Text == "0")
            {
                DataTable dt = new DataTable();
                dt.Columns.Add(new DataColumn("mtSN", typeof(string)));
                dt.Columns.Add(new DataColumn("mtName", typeof(string)));
                dt.Columns.Add(new DataColumn("mtSpec", typeof(string)));
                dt.Columns.Add(new DataColumn("mtUnit", typeof(string)));
                dt.Columns.Add(new DataColumn("mtNumber", typeof(string)));
                dt.Columns.Add(new DataColumn("mtPrice", typeof(string)));
                dt.Columns.Add(new DataColumn("mtTotal", typeof(string)));
                dt.Columns.Add(new DataColumn("PlaceName", typeof(string)));
                dt.Columns.Add(new DataColumn("MXMemo", typeof(string)));
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
                DataTable dtMX = chukService.SELECTCKDMXBYID1(lbRI.Text);
                dtMX.Columns.Add("PlaceName", typeof(string));
                if (dtMX.Rows.Count > 0)
                {
                    for (int i = 0; i < dtMX.Rows.Count; i++)
                    {
                        if (dtMX.Rows[i]["PlaceID"].ToString().Trim() != "0")
                        {
                            dtMX.Rows[i]["PlaceName"] = ckService.GETKWINFOBYKWID(dtMX.Rows[i]["PlaceID"].ToString()).Rows[0]["PlaceName"].ToString();
                        }
                    }
                }
                gdCKDH.DataSource = dtMX;
                gdCKDH.DataBind();
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
                    Label mtNumber = ((Label)gdCKDH.Rows[i].FindControl("mtNumber"));
                    Label mtTotal = ((Label)gdCKDH.Rows[i].FindControl("mtTotal"));
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
            GridViewRow foot = gdCKDH.FooterRow;
            foot.Cells[4].HorizontalAlign = HorizontalAlign.Right;
            foot.Cells[4].Text = sumSL.ToString();
            foot.Cells[6].HorizontalAlign = HorizontalAlign.Right;
            foot.Cells[6].Text = sumZJE.ToString();
        }
    }
}