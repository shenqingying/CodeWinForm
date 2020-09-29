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

namespace STSY.CKRBGL
{
    public partial class RKRBB : System.Web.UI.Page
    {
        CKService ckService = new CKService();
        WLService wlService = new WLService();
        CKRBService ckrbService = new CKRBService();
        RKService rkService = new RKService();
        Mainservice mainService = new Mainservice();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LOGINTIMEOUT();
                STAFFQX();
                txtYEAR.Text = DateTime.Now.Year.ToString();
                txtMOUTH.Text = DateTime.Now.Month.ToString();
                BANDDDLCK();
                BANDDDLLB();
                BANDddlGYSBH();
                rbl1.SelectedValue = "0";
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
            bool s = mainService.SELECT_HG_RIGHT(Session["STAFFID"].ToString(), "222");
            if (s == false)
            {
                Response.Redirect("../Index/Index.aspx?RI=1");
            }
        }
        protected void btnSCBB_Click(object sender, EventArgs e)
        {

            string GYSBH = "";
            if (ddlGYSBH.SelectedValue != "0")
            {
                GYSBH = ddlGYSBH.SelectedValue;
            }
            string typeID = "";
            if (ddlLB.SelectedValue != "0")
            {
                typeID = ddlLB.SelectedValue;
            }
            DataTable dt = ckrbService.SELECT_MSMaterial("", typeID);
            if (dt.Rows.Count > 0)
            {
                DataTable dti = ckrbService.SELECT_MSRptItem();
                DataTable dtMX = new DataTable();
                if (dt.Rows.Count > 50)
                {
                    DateTime dtime = new DateTime(Convert.ToInt32(txtYEAR.Text), Convert.ToInt32(txtMOUTH.Text), 1);
                    this.Page.RegisterStartupScript("onclick", "<script>alert('根据品名生成的报表已大于50列，改用类别汇总报表!')</script>");
                    DataTable dttype = ckrbService.SELECT_MSType("", typeID);
                    dtMX = ckrbService.SCRKBB(dttype, 0, dtime.ToString("yyyy-MM-dd"), dtime.AddMonths(1).ToString("yyyy-MM-dd"), ddlCKBH.SelectedValue, typeID, rbl1.SelectedValue, GYSBH);
                    if (dtMX.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtMX.Rows.Count; i++)
                        {
                            dtMX.Rows[i][0] = Convert.ToDateTime(dtMX.Rows[i][0].ToString()).ToString("yyyy-MM-dd");
                        }
                    }
                }
                else
                {
                    DateTime dtime = new DateTime(Convert.ToInt32(txtYEAR.Text), Convert.ToInt32(txtMOUTH.Text), 1);
                    dtMX = ckrbService.SCRKBB(dt, 1, dtime.ToString("yyyy-MM-dd"), dtime.AddMonths(1).ToString("yyyy-MM-dd"), ddlCKBH.SelectedValue, typeID, rbl1.SelectedValue, GYSBH);
                    if (dtMX.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtMX.Rows.Count; i++)
                        {
                            dtMX.Rows[i][0] = Convert.ToDateTime(dtMX.Rows[i][0].ToString()).ToString("yyyy-MM-dd");
                        }
                    }
                }
                DataTable dtmxs = new DataTable();
                for (int i = 0; i < dtMX.Columns.Count; i++)
                {
                    dtmxs.Columns.Add(i.ToString(), typeof(string));
                }
                if (dtMX.Rows.Count > 0)
                {
                    for (int i = 0; i < dtMX.Rows.Count; i++)
                    {
                        dtmxs.Rows.Add();
                        for (int j = 0; j < dtMX.Columns.Count; j++)
                        {
                            if (j == 0)
                            {
                                dtmxs.Rows[i][j] = Convert.ToDateTime(dtMX.Rows[i][0].ToString()).ToString("yyyy-MM-dd");
                            }
                            else
                            {
                                if (dtMX.Rows[i][j].ToString() == "")
                                {
                                    dtmxs.Rows[i][j] = "";
                                }
                                else if (Convert.ToDouble(dtMX.Rows[i][j].ToString()) == 0)
                                {
                                    dtmxs.Rows[i][j] = "";
                                }
                                else
                                {
                                    dtmxs.Rows[i][j] = Convert.ToDouble(dtMX.Rows[i][j].ToString()).ToString();
                                }
                            }
                        }
                    }
                    Label7.Text = "";
                }
                else
                {
                    Label7.Text = "没有数据！";
                }
                GridView1.DataSource = dtmxs;
                GridView1.DataBind();
                BANDHJ(dtmxs);

            }

        }

        protected void btnDC_Click(object sender, EventArgs e)
        {
            string GYSBH = "";
            if (ddlGYSBH.SelectedValue != "0")
            {
                GYSBH = ddlGYSBH.SelectedValue;
            }
            string typeID = "";
            if (ddlLB.SelectedValue != "0")
            {
                typeID = ddlLB.SelectedValue;
            }
            DataTable dt = ckrbService.SELECT_MSMaterial("", typeID);
            if (dt.Rows.Count > 0)
            {
                DataTable dti = ckrbService.SELECT_MSRptItem();
                DataTable dttype = ckrbService.SELECT_MSType("", typeID);
                DataTable dtMX = new DataTable();
                if (dt.Rows.Count > 50)
                {
                    DateTime dtime = new DateTime(Convert.ToInt32(txtYEAR.Text), Convert.ToInt32(txtMOUTH.Text), 1);
                    this.Page.RegisterStartupScript("onclick", "<script>alert('根据品名生成的报表已大于50列，改用类别汇总报表!')</script>");
                    dtMX = ckrbService.SCRKBB(dttype, 0, dtime.ToString("yyyy-MM-dd"), dtime.AddMonths(1).ToString("yyyy-MM-dd"), ddlCKBH.SelectedValue, typeID, rbl1.SelectedValue, GYSBH);
                }
                else
                {
                    DateTime dtime = new DateTime(Convert.ToInt32(txtYEAR.Text), Convert.ToInt32(txtMOUTH.Text), 1);
                    dtMX = ckrbService.SCRKBB(dt, 1, dtime.ToString("yyyy-MM-dd"), dtime.AddMonths(1).ToString("yyyy-MM-dd"), ddlCKBH.SelectedValue, typeID, rbl1.SelectedValue, GYSBH);
                }
                if (dtMX.Rows.Count > 0)
                {
                    DataTable dtDC = new DataTable();
                    dtDC.Columns.Add("日期", typeof(string));
                    string s = "";
                    s = "日期,";
                    for (int i = 0; i < dtMX.Columns.Count; i++)
                    {
                        dtDC.Columns.Add(i.ToString(), typeof(string));
                    }
                    if (dt.Rows.Count > 50)
                    {
                        for (int i = 0; i < dttype.Rows.Count; i++)
                        {
                            //dtDC.Columns.Add(dttype.Rows[i]["typeName"].ToString(), typeof(string));
                            s = s + dttype.Rows[i]["typeName"].ToString() + ",";
                        }
                    }
                    else
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            //dtDC.Columns.Add(dt.Rows[i]["mtName"].ToString(), typeof(string));
                            s = s + dt.Rows[i]["mtName"].ToString() + ",";
                        }
                    }
                    if (rbl1.SelectedValue == "1")
                    {
                        dtDC.Columns.Add("小计", typeof(string));
                        s = s + "小计,";
                    }
                    for (int i = 0; i < dtMX.Rows.Count; i++)
                    {
                        dtDC.Rows.Add();
                        for (int j = 0; j < dtMX.Columns.Count; j++)
                        {

                            dtDC.Rows[i][j] = dtMX.Rows[i][j].ToString();
                        }
                    }
                    if (dtDC.Columns.Count > 1)
                    {
                        if (dtDC.Rows.Count > 0)
                        {
                            DataRow dr = dtDC.NewRow();
                            dr[0] = "合计：";
                            for (int i = 1; i < dtDC.Columns.Count; i++)
                            {
                                double sum = 0;
                                for (int j = 0; j < dtDC.Rows.Count; j++)
                                {
                                    if (dtDC.Rows[j][i].ToString() != "")
                                    {
                                        sum = sum + Convert.ToDouble(dtDC.Rows[j][i].ToString());
                                    }
                                }
                                dr[i] = sum.ToString();
                            }
                            dtDC.Rows.Add(dr);
                        }
                    }
                    NpoiExcel(dtDC, "入库日报表", s);
                }
            }
        }


        protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
        {
            string GYSBH = "";
            if (ddlGYSBH.SelectedValue != "0")
            {
                GYSBH = ddlGYSBH.SelectedValue;
            }
            string typeID = "";
            if (ddlLB.SelectedValue != "0")
            {
                typeID = ddlLB.SelectedValue;
            }
            DataTable dt = ckrbService.SELECT_MSMaterial("", typeID);
            DataTable dti = ckrbService.SELECT_MSRptItem();
            DataTable dttype = ckrbService.SELECT_MSType("", typeID);
            if (dt.Rows.Count > 50)
            {
                switch (e.Row.RowType)
                {
                    case DataControlRowType.Header://行是标题行
                        TableCellCollection personHeader = e.Row.Cells;//标题行的单元格集合
                        personHeader.Clear();//清空
                        personHeader.Add(new TableHeaderCell());
                        personHeader[0].Text = "日期";
                        int s = 0;
                        if (dttype.Rows.Count > 0)
                        {
                            for (int i = 0; i < dttype.Rows.Count; i++)
                            {
                                personHeader.Add(new TableHeaderCell());
                                personHeader[s + 1].Text = dttype.Rows[i]["typeName"].ToString();
                                s = s + 1;
                            }
                        }
                        if (rbl1.SelectedValue == "1")
                        {
                            personHeader.Add(new TableHeaderCell());
                            personHeader[s + 1].Text = "小计";
                            s = s + 1;
                        }
                        break;
                }
            }
            else
            {
                switch (e.Row.RowType)
                {
                    case DataControlRowType.Header://行是标题行
                        TableCellCollection personHeader = e.Row.Cells;//标题行的单元格集合
                        personHeader.Clear();//清空

                        personHeader.Add(new TableHeaderCell());
                        personHeader[0].Text = "日期";
                        int s = 0;
                        if (dt.Rows.Count > 0)
                        {
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                personHeader.Add(new TableHeaderCell());
                                personHeader[s + 1].Text = dt.Rows[i]["mtName"].ToString();
                                s = s + 1;
                            }
                        }
                        if (rbl1.SelectedValue == "1")
                        {
                            personHeader.Add(new TableHeaderCell());
                            personHeader[s + 1].Text = "小计";
                            s = s + 1;
                        }
                        break;
                }
            }
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow) //判断是否是DataRow，以防止鼠标经过Header也有效果   
            {
                e.Row.Attributes.Add("onmouseover", "e=this.style.backgroundColor; this.style.backgroundColor='#CCCCCC'");
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=e");
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

        private void BANDDDLLB()
        {
            DataTable dt = wlService.GETWLTYPE();
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dt.Rows[i]["typeSN"] = dt.Rows[i]["typeSN"].ToString() + "  " + dt.Rows[i]["typeName"].ToString();
                }
            }
            ddlLB.DataSource = dt;
            ddlLB.DataTextField = "typeSN";
            ddlLB.DataValueField = "typeID";
            ddlLB.DataBind();
            ListItem item = new ListItem();
            item.Text = "=请选择=";
            item.Value = "0";
            ddlLB.Items.Insert(0, item);
        }

        public void NpoiExcel(DataTable dt, string title, string s)
        {
            NPOI.HSSF.UserModel.HSSFWorkbook book = new NPOI.HSSF.UserModel.HSSFWorkbook();
            NPOI.SS.UserModel.ISheet sheet = book.CreateSheet("Sheet1");

            NPOI.SS.UserModel.IRow headerrow = sheet.CreateRow(0);
            ICellStyle style = book.CreateCellStyle();
            style.Alignment = HorizontalAlignment.CENTER;
            style.VerticalAlignment = VerticalAlignment.CENTER;

            string[] ss = s.Split(',');
            for (int i = 0; i < ss.Length; i++)
            {
                ICell cell = headerrow.CreateCell(i);
                cell.CellStyle = style;
                cell.SetCellValue(ss[i]);

            }
            for (int I = 0; I <= dt.Rows.Count - 1; I++)
            {
                HSSFRow row2 = (HSSFRow)sheet.CreateRow(I + 1);
                for (int j = 0; j <= dt.Columns.Count - 1; j++)
                {
                    //double a = 0;
                    //string b = "";
                    //try
                    //{
                    //    a = Convert.ToDouble(dt.Rows[I][j].ToString());
                    //    row2.CreateCell(j).SetCellValue(a);
                    //    sheet.SetColumnWidth(j, 20 * 150);
                    //}
                    //catch
                    //{
                    //    b = dt.Rows[I][j].ToString();
                    //    row2.CreateCell(j).SetCellValue(b);
                    //    sheet.SetColumnWidth(j, 20 * 150);
                    //}
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

        private void BANDHJ(DataTable dt)
        {
            GridViewRow foot = GridView1.FooterRow;
            if (dt.Columns.Count > 1 && dt.Rows.Count > 0)
            {
                foot.Cells[0].HorizontalAlign = HorizontalAlign.Right;
                foot.Cells[0].Text = "合计";
                for (int i = 1; i < dt.Columns.Count; i++)
                {
                    double sum = 0;
                    for (int j = 0; j < dt.Rows.Count; j++)
                    {
                        if (dt.Rows[j][i].ToString() != "")
                        {
                            sum = sum + Convert.ToDouble(dt.Rows[j][i].ToString());
                        }
                    }
                    foot.Cells[i].HorizontalAlign = HorizontalAlign.Right;
                    foot.Cells[i].Text = sum.ToString();
                }
            }
        }
    }
}