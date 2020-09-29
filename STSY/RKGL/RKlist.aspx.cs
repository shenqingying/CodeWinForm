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

namespace STSY.RKGL
{
    public partial class RKlist : System.Web.UI.Page
    {
        RKService rkService = new RKService();
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                LOGINTIMEOUT();
            }
        }

        private void LOGINTIMEOUT()
        {
            if (Session["STAFFID"] == null)
            {
                Response.Redirect("../login.aspx?RI=1");
            }
        }

        protected void btnSCBB_Click(object sender, EventArgs e)
        {
            string begin = txtBegin.Text.Trim();
            string end = txtEnd.Text.Trim();
            if (begin == "" || end == "")
            {
                this.Page.RegisterStartupScript("onclick", "<script>alert('请输入查询的日期！')</script>");
            }
            else
            {
                DataTable dt = rkService.ReadRuKList(begin, end);
                if (dt.Rows.Count > 0)
                {
                    GridView1.DataSource = dt;
                    GridView1.DataBind();



                    Label7.Text = "";
                }
                else
                {
                    Label7.Text = "没有数据！";
                }
            }

        }

        //protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
        //{
        //    string GYSBH = "";
        //    if (ddlGYSBH.SelectedValue != "0")
        //    {
        //        GYSBH = ddlGYSBH.SelectedValue;
        //    }
        //    string typeID = "";
        //    if (ddlLB.SelectedValue != "0")
        //    {
        //        typeID = ddlLB.SelectedValue;
        //    }
        //    DataTable dt = ckrbService.SELECT_MSMaterial("", typeID);
        //    DataTable dti = ckrbService.SELECT_MSRptItem();
        //    DataTable dttype = ckrbService.SELECT_MSType("", typeID);
        //    if (dt.Rows.Count > 50)
        //    {
        //        switch (e.Row.RowType)
        //        {
        //            case DataControlRowType.Header://行是标题行
        //                TableCellCollection personHeader = e.Row.Cells;//标题行的单元格集合
        //                personHeader.Clear();//清空
        //                personHeader.Add(new TableHeaderCell());
        //                personHeader[0].Text = "日期";
        //                int s = 0;
        //                if (dttype.Rows.Count > 0)
        //                {
        //                    for (int i = 0; i < dttype.Rows.Count; i++)
        //                    {
        //                        personHeader.Add(new TableHeaderCell());
        //                        personHeader[s + 1].Text = dttype.Rows[i]["typeName"].ToString();
        //                        s = s + 1;
        //                    }
        //                }
        //                if (rbl1.SelectedValue == "1")
        //                {
        //                    personHeader.Add(new TableHeaderCell());
        //                    personHeader[s + 1].Text = "小计";
        //                    s = s + 1;
        //                }
        //                break;
        //        }
        //    }
        //    else
        //    {
        //        switch (e.Row.RowType)
        //        {
        //            case DataControlRowType.Header://行是标题行
        //                TableCellCollection personHeader = e.Row.Cells;//标题行的单元格集合
        //                personHeader.Clear();//清空

        //                personHeader.Add(new TableHeaderCell());
        //                personHeader[0].Text = "日期";
        //                int s = 0;
        //                if (dt.Rows.Count > 0)
        //                {
        //                    for (int i = 0; i < dt.Rows.Count; i++)
        //                    {
        //                        personHeader.Add(new TableHeaderCell());
        //                        personHeader[s + 1].Text = dt.Rows[i]["mtName"].ToString();
        //                        s = s + 1;
        //                    }
        //                }
        //                if (rbl1.SelectedValue == "1")
        //                {
        //                    personHeader.Add(new TableHeaderCell());
        //                    personHeader[s + 1].Text = "小计";
        //                    s = s + 1;
        //                }
        //                break;
        //        }
        //    }
        //}

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow) //判断是否是DataRow，以防止鼠标经过Header也有效果   
            {
                e.Row.Attributes.Add("onmouseover", "e=this.style.backgroundColor; this.style.backgroundColor='#CCCCCC'");
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=e");
            }
        }

        protected void btnDC_Click(object sender, EventArgs e)
        {
            DataTable dtDC = rkService.ReadRuKList(txtBegin.Text, txtEnd.Text);
            //string s = "";
            //for (int i = 0; i < dtMX.Columns.Count; i++)
            //{
            //    dtDC.Columns.Add(i.ToString(), typeof(string));
            //}
            //if (dt.Rows.Count > 50)
            //{
            //    for (int i = 0; i < dttype.Rows.Count; i++)
            //    {
            //        //dtDC.Columns.Add(dttype.Rows[i]["typeName"].ToString(), typeof(string));
            //        s = s + dttype.Rows[i]["typeName"].ToString() + ",";
            //    }
            //}
            //else
            //{
            //    for (int i = 0; i < dt.Rows.Count; i++)
            //    {
            //        //dtDC.Columns.Add(dt.Rows[i]["mtName"].ToString(), typeof(string));
            //        s = s + dt.Rows[i]["mtName"].ToString() + ",";
            //    }
            //}

            //for (int i = 0; i < dtMX.Rows.Count; i++)
            //{
            //    dtDC.Rows.Add();
            //    for (int j = 0; j < dtMX.Columns.Count; j++)
            //    {

            //        dtDC.Rows[i][j] = dtMX.Rows[i][j].ToString();
            //    }
            //}


            string s = "";
            for (int i = 0; i < dtDC.Columns.Count; i++)
            {
                s = s + dtDC.Columns[i].ColumnName + ",";
            }
            NpoiExcel(dtDC, "入库日报表", s);
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
                    if (j == 8 || j == 9)
                    {
                        row2.CreateCell(j).SetCellValue(Convert.ToDouble(DgvValue));
                    }
                    else
                    {
                        row2.CreateCell(j).SetCellValue(DgvValue);
                    }
                    
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


    }
}