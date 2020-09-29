using Bussiness.CRM;
using Bussiness.CRM.CRM_LoginService;
using Bussiness.RuK.DuiBiReportService;
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
    public partial class RKDBB : System.Web.UI.Page
    {
        Mainservice mainService = new Mainservice();
        DuiBiReport Report = new DuiBiReport();




        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LOGINTIMEOUT();
                STAFFQX();
                txtYEAR.Text = DateTime.Now.Year.ToString();
                txtMOUTH.Text = DateTime.Now.Month.ToString();
                rbl1.SelectedValue = "1";
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
            bool s = mainService.SELECT_HG_RIGHT(Session["STAFFID"].ToString(), "236");
            if (s == false)
            {
                Response.Redirect("../Index/Index.aspx?RI=1");
            }
        }
        protected void btnSCBB_Click(object sender, EventArgs e)
        {
            Label6.Text = "";
            RuK_DuiBiReport model = new RuK_DuiBiReport(); ;
            model.YEAR = txtYEAR.Text;
            model.MONTH = txtMOUTH.Text;
            //  model.LB = Convert.ToInt32(rbl1.SelectedItem.Text);
            model.LB = Convert.ToInt32(rbl1.SelectedValue);
            string token = Convert.ToString(HttpContext.Current.Session["token"]);

            DataTable dt = Report.RuK_DuiBiReport(model, token);

           
            dt.Columns.Add("平均单价环比", typeof(string));
            dt.Columns.Add("平均单价同比", typeof(string));




            if (dt.Rows.Count > 0)
            {

                foreach (DataRow dr in dt.Rows)
                {

                    if (Convert.ToDecimal(dr["price1"]) == 0)
                    {
                        dr["平均单价环比"] = 0.00 + "%";
                    }
                    else
                    {
                        dr["平均单价环比"] = Math.Round(Convert.ToDecimal((Convert.ToDecimal(dr["price2"]) - Convert.ToDecimal(dr["price1"])) / Convert.ToDecimal(dr["price1"]) * 100), 2, MidpointRounding.AwayFromZero) + "%";
                    }
                    if (Convert.ToDecimal(dr["Lastprice2"]) == 0)
                    {
                        dr["平均单价同比"] = 0.00 + "%";
                    }
                    else
                    {
                        dr["平均单价同比"] = Math.Round(Convert.ToDecimal(((Convert.ToDecimal(dr["price2"]) - Convert.ToDecimal(dr["Lastprice2"])) / Convert.ToDecimal(dr["Lastprice2"]) * 100)), 2, MidpointRounding.AwayFromZero) + "%";
                    }
                }
                if (rbl1.SelectedValue == "1")
                {
                dt.Columns["mtName"].ColumnName = "物料名称";
                dt.Columns["typeName"].ColumnName = "类别名称";
                dt.Columns["mtUnit"].ColumnName = "单位";
                dt.Columns["LastmtNumber2"].ColumnName = "上年" + Convert.ToInt32(txtMOUTH.Text) + "月采购数量";
                dt.Columns["LastmtTotal2"].ColumnName = "上年" + Convert.ToInt32(txtMOUTH.Text) + "月采购金额";
                dt.Columns["Lastprice2"].ColumnName = "上年" + Convert.ToInt32(txtMOUTH.Text) + "月平均单价";
                dt.Columns["mtNumber1"].ColumnName = "上月采购数量";
                dt.Columns["mtTotal1"].ColumnName = "上月采购金额";
                dt.Columns["price1"].ColumnName = "上月平均单价";
                dt.Columns["mtNumber2"].ColumnName = txtMOUTH.Text + "月采购数量";
                dt.Columns["mtTotal2"].ColumnName = txtMOUTH.Text + "月采购金额";
                dt.Columns["price2"].ColumnName = txtMOUTH.Text + "月平均单价";
                }
                if (rbl1.SelectedValue == "2")
                {
                  //  dt.Columns["mtName"].ColumnName = "物料名称";
                    dt.Columns["typeName"].ColumnName = "类别名称";
                    dt.Columns["mtUnit"].ColumnName = "单位";
                    dt.Columns["LastmtNumber2"].ColumnName = "上年" + Convert.ToInt32(txtMOUTH.Text) + "月采购数量";
                    dt.Columns["LastmtTotal2"].ColumnName = "上年" + Convert.ToInt32(txtMOUTH.Text) + "月采购金额";
                    dt.Columns["Lastprice2"].ColumnName = "上年" + Convert.ToInt32(txtMOUTH.Text) + "月平均单价";
                    dt.Columns["mtNumber1"].ColumnName = "上月采购数量";
                    dt.Columns["mtTotal1"].ColumnName = "上月采购金额";
                    dt.Columns["price1"].ColumnName = "上月平均单价";
                    dt.Columns["mtNumber2"].ColumnName = txtMOUTH.Text + "月采购数量";
                    dt.Columns["mtTotal2"].ColumnName = txtMOUTH.Text + "月采购金额";
                    dt.Columns["price2"].ColumnName = txtMOUTH.Text + "月平均单价";
                }





              
                //  BANDHJ(dt);
            }
            else
            {
                Label6.Text = "没有数据！";
            }
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }
        protected void btnDC_Click(object sender, EventArgs e)
        {
            RuK_DuiBiReport model = new RuK_DuiBiReport(); ;
            model.YEAR = txtYEAR.Text;
            model.MONTH = txtMOUTH.Text;
            model.LB = 1;
            string token = Convert.ToString(HttpContext.Current.Session["token"]);

            DataTable dt = Report.RuK_DuiBiReport(model, token);
            AddColumns(dt);
            model.LB = 2;
            DataTable dt2 = Report.RuK_DuiBiReport(model, token);
            AddColumns(dt2);
            //dt.Columns.Add("平均单价环比", typeof(string));
            //dt.Columns.Add("平均单价同比", typeof(string));
            if (dt.Rows.Count > 0 && dt2.Rows.Count > 0)
            {
                //foreach (DataRow dr in dt.Rows)
                //{
                //    if (Convert.ToDecimal(dr["price1"]) == 0)
                //    {
                //        dr["平均单价环比"] = 0.00 + "%";
                //    }
                //    else
                //    {
                //        dr["平均单价环比"] = Math.Round(Convert.ToDecimal((Convert.ToDecimal(dr["price2"]) - Convert.ToDecimal(dr["price1"])) / Convert.ToDecimal(dr["price1"]) * 100), 2, MidpointRounding.AwayFromZero) + "%";
                //    }
                //    if (Convert.ToDecimal(dr["Lastprice2"]) == 0)
                //    {
                //        dr["平均单价同比"] = 0.00 + "%";
                //    }
                //    else
                //    {
                //        dr["平均单价同比"] = Math.Round(Convert.ToDecimal(((Convert.ToDecimal(dr["price2"]) - Convert.ToDecimal(dr["Lastprice2"])) / Convert.ToDecimal(dr["Lastprice2"]) * 100)), 2, MidpointRounding.AwayFromZero) + "%";
                //    }
                //}

                
                dt.Columns["mtName"].ColumnName = "物料名称";
                dt.Columns["typeName"].ColumnName = "类别名称";
                dt.Columns["mtUnit"].ColumnName = "单位";
                dt.Columns["LastmtNumber2"].ColumnName = "上年" + Convert.ToInt32(txtMOUTH.Text) + "月采购数量";
                dt.Columns["LastmtTotal2"].ColumnName = "上年" + Convert.ToInt32(txtMOUTH.Text) + "月采购金额";
                dt.Columns["Lastprice2"].ColumnName = "上年" + Convert.ToInt32(txtMOUTH.Text) + "月平均单价";
                dt.Columns["mtNumber1"].ColumnName = "上月采购数量";
                dt.Columns["mtTotal1"].ColumnName = "上月采购金额";
                dt.Columns["price1"].ColumnName = "上月平均单价";
                dt.Columns["mtNumber2"].ColumnName = txtMOUTH.Text + "月采购数量";
                dt.Columns["mtTotal2"].ColumnName = txtMOUTH.Text + "月采购金额";
                dt.Columns["price2"].ColumnName = txtMOUTH.Text + "月平均单价";
               
                    //  dt.Columns["mtName"].ColumnName = "物料名称";
                dt2.Columns["typeName"].ColumnName = "类别名称";
                dt2.Columns["mtUnit"].ColumnName = "单位";
                dt2.Columns["LastmtNumber2"].ColumnName = "上年" + Convert.ToInt32(txtMOUTH.Text) + "月采购数量";
                dt2.Columns["LastmtTotal2"].ColumnName = "上年" + Convert.ToInt32(txtMOUTH.Text) + "月采购金额";
                dt2.Columns["Lastprice2"].ColumnName = "上年" + Convert.ToInt32(txtMOUTH.Text) + "月平均单价";
                dt2.Columns["mtNumber1"].ColumnName = "上月采购数量";
                dt2.Columns["mtTotal1"].ColumnName = "上月采购金额";
                dt2.Columns["price1"].ColumnName = "上月平均单价";
                dt2.Columns["mtNumber2"].ColumnName = txtMOUTH.Text + "月采购数量";
                dt2.Columns["mtTotal2"].ColumnName = txtMOUTH.Text + "月采购金额";
                dt2.Columns["price2"].ColumnName = txtMOUTH.Text + "月平均单价";
               


                NpoiExcel(dt,dt2, "入库对比表");

            }
        }
        public void NpoiExcel(DataTable dt, DataTable dt2, string title)
        {
            NPOI.HSSF.UserModel.HSSFWorkbook book = new NPOI.HSSF.UserModel.HSSFWorkbook();
            NPOI.SS.UserModel.ISheet sheet1 = book.CreateSheet("物料汇总");
            NPOI.SS.UserModel.ISheet sheet2 = book.CreateSheet("类别汇总");

            NPOI.SS.UserModel.IRow headerrow1 = sheet1.CreateRow(0);
            NPOI.SS.UserModel.IRow headerrow2 = sheet2.CreateRow(0);
            ICellStyle style = book.CreateCellStyle();
            style.Alignment = HorizontalAlignment.CENTER;
            style.VerticalAlignment = VerticalAlignment.CENTER;


            for (int i = 0; i < dt.Columns.Count; i++)
            {
                ICell cell = headerrow1.CreateCell(i);
                cell.CellStyle = style;
                cell.SetCellValue(dt.Columns[i].ColumnName);
            }
            for (int I = 0; I <= dt.Rows.Count - 1; I++)
            {
                HSSFRow row2 = (HSSFRow)sheet1.CreateRow(I + 1);
                for (int j = 0; j <= dt.Columns.Count - 1; j++)
                {
                    string DgvValue = dt.Rows[I][j].ToString();
                    row2.CreateCell(j).SetCellValue(DgvValue);
                    sheet1.SetColumnWidth(j, 20 * 150);
                }
            }
            for (int i = 0; i < dt2.Columns.Count; i++)
            {
                ICell cell = headerrow2.CreateCell(i);
                cell.CellStyle = style;
                cell.SetCellValue(dt2.Columns[i].ColumnName);
            }
            for (int I = 0; I <= dt2.Rows.Count - 1; I++)
            {
                HSSFRow row2 = (HSSFRow)sheet2.CreateRow(I + 1);
                for (int j = 0; j <= dt2.Columns.Count - 1; j++)
                {
                    string DgvValue = dt2.Rows[I][j].ToString();
                    row2.CreateCell(j).SetCellValue(DgvValue);
                    sheet2.SetColumnWidth(j, 20 * 150);
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


        public DataTable AddColumns(DataTable dt)
        {
            dt.Columns.Add("平均单价环比", typeof(string));
            dt.Columns.Add("平均单价同比", typeof(string));
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    if (Convert.ToDecimal(dr["price1"]) == 0)
                    {
                        dr["平均单价环比"] = 0.00 + "%";
                    }
                    else
                    {
                        dr["平均单价环比"] = Math.Round(Convert.ToDecimal((Convert.ToDecimal(dr["price2"]) - Convert.ToDecimal(dr["price1"])) / Convert.ToDecimal(dr["price1"]) * 100), 2, MidpointRounding.AwayFromZero) + "%";
                    }
                    if (Convert.ToDecimal(dr["Lastprice2"]) == 0)
                    {
                        dr["平均单价同比"] = 0.00 + "%";
                    }
                    else
                    {
                        dr["平均单价同比"] = Math.Round(Convert.ToDecimal(((Convert.ToDecimal(dr["price2"]) - Convert.ToDecimal(dr["Lastprice2"])) / Convert.ToDecimal(dr["Lastprice2"]) * 100)), 2, MidpointRounding.AwayFromZero) + "%";
                    }
                }
            }
            return dt;
        }
       
    }
}