using Bussiness.STSY;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace STSY.YFGL
{
    public partial class YFCX : System.Web.UI.Page
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
                BANDddlGYS();
                BANDddlLBGYSBH();
                txtRZRQ.Text = DateTime.Now.ToString("yyyy-MM-dd");
                txtFPRQ.Text = DateTime.Now.ToString("yyyy-MM-dd");
                txtFPJE.Text = "0";
                txtFKJE.Text = "0";
                txtRKJE.Text = "0";
                txtJKK.Text = "0";
                txtLBRZRQS.Enabled = false;
                txtLBRZRQE.Enabled = false;
                txtLBFPRQS.Enabled = true;
                txtLBFPRQE.Enabled = true;
                cbLBFPRQ.Checked = true;
                cbLBRZRQ.Checked = false;
                lbISLB.Text = "TRUE";
                rbCX.SelectedValue = "1";
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
                BANDHJFPLB();
            }
            else
            {
                BAND();
            }
        }

        protected void btnDC_Click(object sender, EventArgs e)
        {
            try
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
                if (rbCX.SelectedValue == "0")
                {
                    LOGINTIMEOUT();
                    dt = fpService.GETFPLBINFO(Session["STAFFID"].ToString(), ddlLBGYSBH.SelectedValue, RZRQS, RZRQE, FPRQS, FPRQE, txtLBFPHM.Text.Trim(), rbCX.SelectedValue, ddlRZLXLB.SelectedValue);
                }
                else
                {
                    dt = fpService.GETFPLBINFO("", ddlLBGYSBH.SelectedValue, RZRQS, RZRQE, FPRQS, FPRQE, txtLBFPHM.Text.Trim(), rbCX.SelectedValue, ddlRZLXLB.SelectedValue);

                }
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

                DataTable dtDC = new DataTable();
                dtDC.Columns.Add("供应商编号", typeof(string));
                dtDC.Columns.Add("供应商名称", typeof(string));
                dtDC.Columns.Add("发票号码", typeof(string));
                dtDC.Columns.Add("发票日期", typeof(string));
                dtDC.Columns.Add("入账日期", typeof(string));
                dtDC.Columns.Add("类", typeof(string));
                dtDC.Columns.Add("发票金额", typeof(string));
                dtDC.Columns.Add("入库金额", typeof(string));
                dtDC.Columns.Add("加扣款", typeof(string));
                dtDC.Columns.Add("审核人", typeof(string));
                dtDC.Columns.Add("已审核", typeof(string));
                dtDC.Columns.Add("审核时间", typeof(string));
                dtDC.Columns.Add("付款确认", typeof(string));
                dtDC.Columns.Add("已付款", typeof(string));
                dtDC.Columns.Add("付款时间", typeof(string));
                dtDC.Columns.Add("付款金额", typeof(string));
                dtDC.Columns.Add("回单确认", typeof(string));
                dtDC.Columns.Add("已回单", typeof(string));
                dtDC.Columns.Add("回单时间", typeof(string));
                dtDC.Columns.Add("回单确认说明", typeof(string));
                dtDC.Columns.Add("填写人", typeof(string));
                dtDC.Columns.Add("填写时间", typeof(string));
                dtDC.Columns.Add("发票说明", typeof(string));
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dtDC.Rows.Add();
                    dtDC.Rows[i][0] = dt.Rows[i]["ClientID"].ToString();
                    dtDC.Rows[i][1] = dt.Rows[i]["ClName"].ToString();
                    dtDC.Rows[i][2] = dt.Rows[i]["CaiGFPNO"].ToString();
                    dtDC.Rows[i][3] = dt.Rows[i]["FaPDate1"].ToString();
                    dtDC.Rows[i][4] = dt.Rows[i]["ACDate1"].ToString();
                    dtDC.Rows[i][5] = dt.Rows[i]["ACType"].ToString();
                    dtDC.Rows[i][6] = dt.Rows[i]["FaPTotal1"].ToString();
                    dtDC.Rows[i][7] = dt.Rows[i]["RuKTotal1"].ToString();
                    dtDC.Rows[i][8] = dt.Rows[i]["JKK"].ToString();
                    dtDC.Rows[i][9] = dt.Rows[i]["AudiName"].ToString();
                    dtDC.Rows[i][10] = dt.Rows[i]["isAudi"].ToString();
                    dtDC.Rows[i][11] = dt.Rows[i]["AudiTime"].ToString();
                    dtDC.Rows[i][12] = dt.Rows[i]["PayName"].ToString();
                    dtDC.Rows[i][13] = dt.Rows[i]["isPay"].ToString();
                    dtDC.Rows[i][14] = dt.Rows[i]["PayTime"].ToString();
                    dtDC.Rows[i][15] = dt.Rows[i]["PayTotal1"].ToString();
                    dtDC.Rows[i][16] = dt.Rows[i]["RePayName"].ToString();
                    dtDC.Rows[i][17] = dt.Rows[i]["isRePay"].ToString();
                    dtDC.Rows[i][18] = dt.Rows[i]["RePayTime"].ToString();
                    dtDC.Rows[i][19] = dt.Rows[i]["RePayMemo"].ToString();
                    dtDC.Rows[i][20] = dt.Rows[i]["fillName"].ToString();
                    dtDC.Rows[i][21] = dt.Rows[i]["fillTime"].ToString();
                    dtDC.Rows[i][22] = dt.Rows[i]["FaPMem"].ToString();
                }

                NpoiExcel(dtDC, "应付查询");
                this.Page.RegisterStartupScript("onclick", "<script>alert('导出成功！')</script>");
            }
            catch
            {
                this.Page.RegisterStartupScript("onclick", "<script>alert('导出失败！')</script>");
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

        protected void btnADDGLBD_Click(object sender, EventArgs e)
        {

        }

        protected void btnDELETEGLBD_Click(object sender, EventArgs e)
        {

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
            Label nStatus = ((Label)gdFPMXLB.Rows[drv.RowIndex].FindControl("nStatus"));
            lbCaiGFPID.Text = CaiGFPID.Text;
            lbnStatus.Text = nStatus.Text;
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
            if (lbISLB.Text.ToUpper() == "TRUE")
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
                if (rbCX.SelectedValue == "0")
                {
                    LOGINTIMEOUT();
                    dt = fpService.GETFPLBINFO(Session["STAFFID"].ToString(), ddlLBGYSBH.SelectedValue, RZRQS, RZRQE, FPRQS, FPRQE, txtLBFPHM.Text.Trim(), rbCX.SelectedValue, ddlRZLXLB.SelectedValue);
                }
                else
                {
                    dt = fpService.GETFPLBINFO("", ddlLBGYSBH.SelectedValue, RZRQS, RZRQE, FPRQS, FPRQE, txtLBFPHM.Text.Trim(), rbCX.SelectedValue, ddlRZLXLB.SelectedValue);

                }
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
                    //double rkje = 0;
                    if (dtRuKD.Rows.Count > 0)
                    {
                        //for (int j = 0; j < dtRuKD.Rows.Count; j++)
                        //{
                        //    rkje = rkje + Convert.ToDouble(dtRuKD.Rows[j]["RuKTotal"].ToString());
                        //}
                        gdRKDLB.DataSource = dtRuKD;
                        gdRKDLB.DataBind();
                    }
                    //txtFPJE.Text = Math.Round(rkje, 2).ToString();
                    //txtRKJE.Text = Math.Round(rkje, 2).ToString();
                    //txtJKK.Text = "0";
                }
            }
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

        private void BANDHJFPLB()
        {
            double sumFAP = 0;
            double sumRUK = 0;
            if (gdFPMXLB.Rows.Count > 0)
            {
                for (int i = 0; i < gdFPMXLB.Rows.Count; i++)
                {
                    Label FaPTotal = ((Label)gdFPMXLB.Rows[i].FindControl("FaPTotal"));
                    Label RuKTotal = ((Label)gdFPMXLB.Rows[i].FindControl("RuKTotal"));
                    try
                    {
                        sumFAP = sumFAP + Convert.ToDouble(FaPTotal.Text);
                    }
                    catch
                    {
                        sumFAP = sumFAP + 0;
                    }
                    try
                    {
                        sumRUK = sumRUK + Convert.ToDouble(RuKTotal.Text);
                    }
                    catch
                    {
                        sumRUK = sumRUK + 0;
                    }
                }
                GridViewRow foot = gdFPMXLB.FooterRow;
                foot.Cells[7].HorizontalAlign = HorizontalAlign.Right;
                foot.Cells[7].Font.Bold = true;
                foot.Cells[7].Text = sumFAP.ToString();
                foot.Cells[8].HorizontalAlign = HorizontalAlign.Right;
                foot.Cells[8].Font.Bold = true;
                foot.Cells[8].Text = sumRUK.ToString();
            }
        }
    }
}