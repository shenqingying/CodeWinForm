using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bussiness.STSY;
using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;
using System.IO;

namespace STSY.YFGL
{
    public partial class FPYZGLDH : System.Web.UI.Page
    {
        CKService ckService = new CKService();
        RKService rkService = new RKService();
        WLService wlService = new WLService();
        FPService fpService = new FPService();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LOGINTIMEOUT();
                lbRI.Text = Request.QueryString["RI"];
                lbLB.Text = Request.QueryString["LB"];
                lbGYS.Text = Request.QueryString["GYSBH"];
                lbRKDRI.Text = "0";
                lbISSHOW.Text = "FALSE";
                BANDDDLCK();
                BANDddlGYSBH();
                BANDddlLBCKBH();
                BANDddlLBGYSBH();
                divRKDTX.Visible = true;
                divRKDLB.Visible = false;
                rbl.SelectedValue = "0";
                txtLBRKRQE.Text = DateTime.Now.ToString("yyyy-MM-dd");
                DateTime now = DateTime.Now;
                DateTime d1 = new DateTime(now.Year, now.Month, 1);
                txtLBRKRQS.Text = d1.ToString("yyyy-MM-dd");
                LinkButton1.BackColor = System.Drawing.Color.White;
                LinkButton2.BackColor = System.Drawing.Color.DarkGray;
                divRKDTX.Visible = false;
                divRKDLB.Visible = true;
                cbRKRQ.Checked = false;
                txtLBRKRQS.Enabled = false;
                txtLBRKRQE.Enabled = false;
                ddlLBGYSBH.SelectedValue = lbGYS.Text;
                BANDLB();
            }
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
        }

        private void BANDddlGYSBH()
        {
            DataTable dt = rkService.GETGYSINFOCX();
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
        }

        private void BANDddlLBGYSBH()
        {
            DataTable dt = rkService.GETGYSINFOCX();
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
            BAND(RuKDID.Text.Trim());
            lbRKDRI.Text = RuKDID.Text.Trim();
            LinkButton1.BackColor = System.Drawing.Color.DarkGray;
            LinkButton2.BackColor = System.Drawing.Color.White;
            divRKDTX.Visible = true;
            divRKDLB.Visible = false;
            lbISSHOW.Text = "TRUE";
        }

        protected void cbRKRQ_CheckedChanged(object sender, EventArgs e)
        {
            if (cbRKRQ.Checked == true)
            {
                txtLBRKRQS.Enabled = true;
                txtLBRKRQE.Enabled = true;
            }
            else
            {
                txtLBRKRQS.Enabled = false;
                txtLBRKRQE.Enabled = false;
            }
        }

        private void BANDLB()
        {
            string RukDATAs = "";
            string RukDATAe = "";
            if (cbRKRQ.Checked == true)
            {
                RukDATAs = txtLBRKRQS.Text.Trim();
                RukDATAe = txtLBRKRQE.Text.Trim();
            }
            DataTable dt = fpService.GETLBINFO(rbl.SelectedValue, ddlLBCKBH.SelectedValue, ddlLBGYSBH.SelectedValue, RukDATAs, RukDATAe, ddlLBRKLX.SelectedValue, txtLBRKDH.Text.Trim());
            dt.Columns.Add("RuKDate1", typeof(string));
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DateTime dtime = Convert.ToDateTime(dt.Rows[i]["RuKDate"].ToString());
                    dt.Rows[i]["RuKDate1"] = dtime.ToString("yyyy-MM-dd");
                }
            }
            gdRKDLB.DataSource = dt;
            gdRKDLB.DataBind();
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    CheckBox cbSH = (CheckBox)gdRKDLB.Rows[i].FindControl("cbSH");
                    cbSH.Checked = Convert.ToBoolean(dt.Rows[i]["isAudi"].ToString());
                }
            }
        }

        protected void btnCXSX_Click(object sender, EventArgs e)
        {
            BANDLB();
        }

        private void BAND(string RI)
        {
            DataTable dt = rkService.SELECTRKDBYID(RI);
            if (dt.Rows.Count > 0)
            {
                ddlCKBH.SelectedValue = dt.Rows[0]["StockID"].ToString();
                ddlGYSBH.SelectedValue = dt.Rows[0]["ClientID"].ToString();
                txtRKRQ.Text = Convert.ToDateTime(dt.Rows[0]["RuKDate"].ToString()).ToString("yyyy-MM-dd");
                ddlRKLX.SelectedValue = dt.Rows[0]["TypeID"].ToString();
                txtRKDH.Text = dt.Rows[0]["RuKDNO"].ToString();
                txtRKBZ.Text = dt.Rows[0]["RuKMem"].ToString();
                txtZDR.Text = dt.Rows[0]["fillName"].ToString();
                txtZDSJ.Text = dt.Rows[0]["fillTime"].ToString();
            }
            DataTable dtMX = rkService.SELECTRKDMXBYID(RI);
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
                    dtband.Rows[i][9] = dtMX.Rows[i]["RuKMXID"].ToString();
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
                }
            }
            BANDHJ();
        }

        protected void LinkButton2_Click(object sender, EventArgs e)
        {
            LinkButton1.BackColor = System.Drawing.Color.White;
            LinkButton2.BackColor = System.Drawing.Color.DarkGray;
            divRKDTX.Visible = false;
            divRKDLB.Visible = true;
            lbISSHOW.Text = "FALSE";
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            LinkButton1.BackColor = System.Drawing.Color.DarkGray;
            LinkButton2.BackColor = System.Drawing.Color.White;
            divRKDTX.Visible = true;
            divRKDLB.Visible = false;
            lbISSHOW.Text = "TRUE";
        }

        protected void btnDC_Click(object sender, EventArgs e)
        {
            DataTable dtDC = new DataTable();
            dtDC.Columns.Add("仓库编号", typeof(string));
            dtDC.Columns.Add("仓库名称", typeof(string));
            dtDC.Columns.Add("供应商编号", typeof(string));
            dtDC.Columns.Add("供应商名称", typeof(string));
            dtDC.Columns.Add("入库单编号", typeof(string));
            dtDC.Columns.Add("总金额", typeof(string));
            dtDC.Columns.Add("账期", typeof(string));
            dtDC.Columns.Add("审核", typeof(string));
            dtDC.Columns.Add("入库日期", typeof(string));
            dtDC.Columns.Add("入库备注	", typeof(string));
            dtDC.Columns.Add("制单人", typeof(string));
            dtDC.Columns.Add("制单时间", typeof(string));
            for (int i = 0; i < gdRKDLB.Rows.Count; i++)
            {
                dtDC.Rows.Add();
                dtDC.Rows[i][0] = ((Label)gdRKDLB.Rows[i].FindControl("StockSN")).Text;
                dtDC.Rows[i][1] = ((Label)gdRKDLB.Rows[i].FindControl("StockName")).Text;
                dtDC.Rows[i][2] = ((Label)gdRKDLB.Rows[i].FindControl("ClSN")).Text;
                dtDC.Rows[i][3] = ((Label)gdRKDLB.Rows[i].FindControl("ClName")).Text;
                dtDC.Rows[i][4] = ((Label)gdRKDLB.Rows[i].FindControl("RuKDNO")).Text;
                dtDC.Rows[i][5] = ((Label)gdRKDLB.Rows[i].FindControl("RuKTotal")).Text;
                dtDC.Rows[i][6] = ((Label)gdRKDLB.Rows[i].FindControl("SMonY")).Text;
                dtDC.Rows[i][7] = ((CheckBox)gdRKDLB.Rows[i].FindControl("cbSH")).Checked.ToString();
                dtDC.Rows[i][8] = ((Label)gdRKDLB.Rows[i].FindControl("RuKDate")).Text;
                dtDC.Rows[i][9] = ((Label)gdRKDLB.Rows[i].FindControl("RuKMem")).Text;
                dtDC.Rows[i][10] = ((Label)gdRKDLB.Rows[i].FindControl("fillName")).Text;
                dtDC.Rows[i][11] = ((Label)gdRKDLB.Rows[i].FindControl("fillTime")).Text;
            }
            NpoiExcel(dtDC, "关联表单");
        }

        public void NpoiExcel(DataTable dt, string title)
        {
            NPOI.HSSF.UserModel.HSSFWorkbook book = new NPOI.HSSF.UserModel.HSSFWorkbook();
            NPOI.SS.UserModel.ISheet sheet = book.CreateSheet("Sheet1");
            NPOI.SS.UserModel.IRow headerrow = sheet.CreateRow(0);
            ICellStyle style = book.CreateCellStyle();
            style.Alignment = HorizontalAlignment.CENTER;
            style.VerticalAlignment = VerticalAlignment.CENTER;
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                ICell cell = headerrow.CreateCell(i);
                cell.CellStyle = style;
                cell.SetCellValue(dt.Columns[i].ColumnName);

            }
            for (int I = 0; I <= dt.Rows.Count - 1; I++)
            {
                HSSFRow row2 = (HSSFRow)sheet.CreateRow(I + 1);
                for (int j = 0; j <= dt.Columns.Count - 1; j++)
                {
                    string DgvValue = dt.Rows[I][j].ToString();
                    row2.CreateCell(j).SetCellValue(DgvValue);
                    sheet.SetColumnWidth(j, 20 * 150);
                }
            }
            MemoryStream ms = new MemoryStream();
            book.Write(ms);
            Response.AddHeader("Content-Disposition", string.Format("attachment; filename={0}.xls", HttpUtility.UrlEncode(title + "_" + DateTime.Now.ToString("yyyy-MM-dd"), System.Text.Encoding.UTF8)));
            Response.BinaryWrite(ms.ToArray());
            Response.End();
            book = null;
            ms.Close();
            ms.Dispose();
        }

        protected void btnQRTJ_Click(object sender, EventArgs e)
        {
            if (lbISSHOW.Text == "TRUE" && lbRKDRI.Text != "" && lbRKDRI.Text != "0")
            {
                fpService.UPDATE_CaiGFPYZ_RKBD(lbRKDRI.Text, lbRI.Text);
                if (lbLB.Text == "1")
                {
                    Response.Redirect("FPRR.aspx?RI=" + lbRI.Text + "");

                }
                if (lbLB.Text == "2")
                {
                    Response.Redirect("FPSH.aspx?RI=" + lbRI.Text + "");
                }
            }
            else
            {
                string s = "";
                if (gdRKDLB.Rows.Count > 0)
                {
                    for (int i = 0; i < gdRKDLB.Rows.Count; i++)
                    {
                        CheckBox cbXZ = (CheckBox)gdRKDLB.Rows[i].FindControl("cbXZ");
                        Label RuKDID = (Label)gdRKDLB.Rows[i].FindControl("RuKDID");
                        if (cbXZ.Checked == true)
                        {
                            s = s + " " + RuKDID.Text;
                        }
                    }
                    if (s != "")
                    {
                        fpService.UPDATE_CaiGFPYZ_RKBD(s, lbRI.Text);
                        if (lbLB.Text == "1")
                        {
                            Response.Redirect("FPRR.aspx?RI=" + lbRI.Text + "");
                        }
                        if (lbLB.Text == "2")
                        {
                            Response.Redirect("FPSH.aspx?RI=" + lbRI.Text + "");
                        }
                    }
                    else
                    {
                        this.Page.RegisterStartupScript("onclick", "<script>alert('请选择对应的单据进行添加！')</script>");
                    }
                }
            }
        }

        protected void btnTC_Click(object sender, EventArgs e)
        {
            if (lbLB.Text == "1")
            {
                Response.Redirect("FPRR.aspx?RI=" + lbRI.Text + "");
            }
            if (lbLB.Text == "2")
            {
                Response.Redirect("FPSH.aspx?RI=" + lbRI.Text + "");
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
            foot.Cells[4].HorizontalAlign = HorizontalAlign.Right;
            foot.Cells[4].Text = sumSL.ToString();
            foot.Cells[6].HorizontalAlign = HorizontalAlign.Right;
            foot.Cells[6].Text = sumZJE.ToString();
        }
    }
}