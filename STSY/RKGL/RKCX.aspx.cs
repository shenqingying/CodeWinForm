using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bussiness.STSY;
using System.Data;
using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;
using System.IO;

namespace STSY.RKGL
{
    public partial class RKCX : System.Web.UI.Page
    {
        CKService ckService = new CKService();
        RKService rkService = new RKService();
        WLService wlService = new WLService();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LOGINTIMEOUT();
                BANDddlLBCKBH();
                BANDddlLBGYSBH();
                divRKDTX.Visible = true;
                divRKDLB.Visible = false;
                rbl.SelectedValue = "0";
                txtZDSJ.Text = DateTime.Now.ToString();
                txtZDR.Text = Session["STAFFNAME"].ToString();
                txtLBRKRQE.Text = DateTime.Now.ToString("yyyy-MM-dd");
                DateTime now = DateTime.Now;
                DateTime d1 = new DateTime(now.Year, now.Month, 1);
                txtLBRKRQS.Text = d1.ToString("yyyy-MM-dd");

                LinkButton1.BackColor = System.Drawing.Color.White;
                LinkButton2.BackColor = System.Drawing.Color.DarkGray;
                divRKDTX.Visible = false;
                divRKDLB.Visible = true;
            }
        }

        private void LOGINTIMEOUT()
        {
            if (Session["STAFFID"] == null)
            {
                Response.Redirect("../login.aspx?RI=1");
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
                DataTable dt = rkService.SELECTRKDBYIDCX(lbRI.Text);
                if (dt.Rows.Count > 0)
                {
                    txtCK.Text = dt.Rows[0]["Stock"].ToString();
                    txtGYS.Text = dt.Rows[0]["Client"].ToString();
                    txtRKRQ.Text = Convert.ToDateTime(dt.Rows[0]["RuKDate"].ToString()).ToString("yyyy-MM-dd");
                    ddlRKLX.SelectedValue = dt.Rows[0]["TypeID"].ToString();
                    txtRKDH.Text = dt.Rows[0]["RuKDNO"].ToString();
                    txtRKBZ.Text = dt.Rows[0]["RuKMem"].ToString();
                    txtZDR.Text = dt.Rows[0]["fillName"].ToString();
                    txtZDSJ.Text = Convert.ToDateTime(dt.Rows[0]["fillTime"].ToString()).ToString("yyyy-MM-dd HH:mm:ss");
                }
                DataTable dtMX = rkService.SELECTRKDMXBYIDCX(lbRI.Text);
                dtMX.Columns.Add("PlaceNAME", typeof(string));
                if (dtMX.Rows.Count > 0)
                {
                    for (int i = 0; i < dtMX.Rows.Count; i++)
                    {
                        if (dtMX.Rows[i]["PlaceID"].ToString() == "0")
                        {
                            dtMX.Rows[i]["PlaceNAME"] = "";
                        }
                        else
                        {
                            DataTable dtplace = rkService.SELECT_MSPlace(dtMX.Rows[i]["PlaceID"].ToString());
                            if (dtplace.Rows.Count > 0)
                            {
                                dtMX.Rows[i]["PlaceNAME"] = dtplace.Rows[0][0].ToString(); ;
                            }
                        }
                    }
                }
                if (dtMX.Rows.Count > 0)
                {
                    gdRKDH.DataSource = dtMX;
                    gdRKDH.DataBind();
                }
            }
            BANDHJ();
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
            lbnStatus.Text = nStatus.Text;
        }

        protected void btnDC_Click(object sender, EventArgs e)
        {
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

            DataTable dtDC = new DataTable();
            dtDC.Columns.Add("仓库编号", typeof(string));
            dtDC.Columns.Add("仓库名称", typeof(string));
            dtDC.Columns.Add("供应商编号", typeof(string));
            dtDC.Columns.Add("供应商名称", typeof(string));
            dtDC.Columns.Add("入库单编号", typeof(string));
            dtDC.Columns.Add("账期", typeof(string));
            dtDC.Columns.Add("审核", typeof(string));
            dtDC.Columns.Add("入库日期", typeof(string));
            dtDC.Columns.Add("入库备注", typeof(string));
            dtDC.Columns.Add("制单人", typeof(string));
            dtDC.Columns.Add("制单时间", typeof(string));

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dtDC.Rows.Add();
                    dtDC.Rows[i]["仓库编号"] = dt.Rows[i]["StockSN"].ToString();
                    dtDC.Rows[i]["仓库名称"] = dt.Rows[i]["StockNAME"].ToString();
                    dtDC.Rows[i]["供应商编号"] = dt.Rows[i]["ClSN"].ToString();
                    dtDC.Rows[i]["供应商名称"] = dt.Rows[i]["ClName"].ToString();
                    dtDC.Rows[i]["入库单编号"] = dt.Rows[i]["RuKDNO"].ToString();
                    dtDC.Rows[i]["账期"] = dt.Rows[i]["SMonY"].ToString();
                    dtDC.Rows[i]["审核"] = dt.Rows[i]["isAudi"].ToString();
                    dtDC.Rows[i]["入库日期"] = dt.Rows[i]["RuKDate1"].ToString();
                    dtDC.Rows[i]["入库备注"] = dt.Rows[i]["RuKMem"].ToString();
                    dtDC.Rows[i]["制单人"] = dt.Rows[i]["fillName"].ToString();
                    dtDC.Rows[i]["制单时间"] = dt.Rows[i]["fillTime"].ToString();
                }
            }

            NpoiExcel(dtDC, "入库查询");
        }

        protected void gdRKDH_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gdRKDH.PageIndex = e.NewPageIndex;
            BAND();
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
    }
}