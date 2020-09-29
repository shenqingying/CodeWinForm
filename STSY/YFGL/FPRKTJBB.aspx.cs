using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bussiness.STSY;
using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;
using System.IO;

namespace STSY.YFGL
{
    public partial class FPRKTJBB : System.Web.UI.Page
    {
        CKService ckService = new CKService();
        WLService wlService = new WLService();
        FPService fpService = new FPService();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtYEAR.Text = DateTime.Now.Year.ToString();
                txtMOUTH.Text = DateTime.Now.Month.ToString();
                BANDDDLCK();
                BANDDDLLB();
            }
        }

        protected void btnSCBB_Click(object sender, EventArgs e)          //生成报表
        {
            if (cb1.Checked == false)
            {
                DateTime dtime = new DateTime(Convert.ToInt32(txtYEAR.Text), Convert.ToInt32(txtMOUTH.Text), 1);
                DataTable dt = fpService.SELECT_RKBB(dtime.ToString("yyyy-MM-dd"), dtime.AddMonths(1).ToString("yyyy-MM-dd"), ddlLBZL.SelectedValue, ddlCKBH.SelectedValue, ddlLB.SelectedValue);
                gdRKBB.DataSource = dt;
                gdRKBB.DataBind();
                BANDHJ();
                if(dt.Rows.Count > 0)
                {
                    jkkhj.Text = fpService.GETjkkhj(dtime.ToString("yyyy-MM-dd"), dtime.AddMonths(1).ToString("yyyy-MM-dd")).ToString();
                    //Label8.Visible = true;
                    //jkkhj.Visible = true; 
                }
            }
            else
            {
                DateTime dtime = new DateTime(Convert.ToInt32(txtYEAR.Text), Convert.ToInt32(txtMOUTH.Text), 1);
                DataTable dt = fpService.SELECT_RKBB_1(dtime.ToString("yyyy-MM-dd"), dtime.AddMonths(1).ToString("yyyy-MM-dd"), dtime.AddMonths(-1).ToString("yyyy-MM-dd"), ddlLBZL.SelectedValue, ddlCKBH.SelectedValue, ddlLB.SelectedValue);
                gdRKBB.DataSource = dt;
                gdRKBB.DataBind();
                BANDHJ();
                if (dt.Rows.Count > 0)
                {
                    jkkhj.Text = fpService.GETjkkhj(dtime.ToString("yyyy-MM-dd"), dtime.AddMonths(1).ToString("yyyy-MM-dd")).ToString();
                    //Label8.Visible = true;
                    //jkkhj.Visible = true;
                }
            }
            
            
        }

        protected void btnDC_Click(object sender, EventArgs e)               //导出
        {
            DataTable dt = new DataTable();
            if (cb1.Checked == false)
            {
                DateTime dtime = new DateTime(Convert.ToInt32(txtYEAR.Text), Convert.ToInt32(txtMOUTH.Text), 1);
                dt = fpService.SELECT_RKBB(dtime.ToString("yyyy-MM-dd"), dtime.AddMonths(1).ToString("yyyy-MM-dd"), ddlLBZL.SelectedValue, ddlCKBH.SelectedValue, ddlLB.SelectedValue);
            }
            else
            {
                DateTime dtime = new DateTime(Convert.ToInt32(txtYEAR.Text), Convert.ToInt32(txtMOUTH.Text), 1);
                dt = fpService.SELECT_RKBB_1(dtime.ToString("yyyy-MM-dd"), dtime.AddMonths(1).ToString("yyyy-MM-dd"), dtime.AddMonths(-1).ToString("yyyy-MM-dd"), ddlLBZL.SelectedValue, ddlCKBH.SelectedValue, ddlLB.SelectedValue);
            }


            int jkkhj = 0;
            if (dt.Rows.Count > 0)
            {
                DateTime dtime = new DateTime(Convert.ToInt32(txtYEAR.Text), Convert.ToInt32(txtMOUTH.Text), 1);
                jkkhj = fpService.GETjkkhj(dtime.ToString("yyyy-MM-dd"), dtime.AddMonths(1).ToString("yyyy-MM-dd"));

            }
            
            DataTable dtDC = new DataTable();
            dtDC.Columns.Add("物料类别", typeof(string));
            dtDC.Columns.Add("物料名称", typeof(string));

            dtDC.Columns.Add("当期合计|数量", typeof(decimal));
            dtDC.Columns.Add("当期合计|金额", typeof(decimal));
            dtDC.Columns.Add("当期合计|单价", typeof(decimal));
            dtDC.Columns.Add("上期合计|数量", typeof(decimal));
            dtDC.Columns.Add("上期合计|金额", typeof(decimal));
            dtDC.Columns.Add("上期合计|单价", typeof(decimal));

            for (int i = 0; i < 31; i++)
            {
                dtDC.Columns.Add("" + (i + 1).ToString() + "日|数量", typeof(string));
                dtDC.Columns.Add("" + (i + 1).ToString() + "日|金额", typeof(string));
            }

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dtDC.Rows.Add();
                    dtDC.Rows[i][0] = dt.Rows[i]["typeName"].ToString();
                    dtDC.Rows[i][1] = dt.Rows[i]["mtName"].ToString();
                    dtDC.Rows[i][2] = dt.Rows[i]["mtNumber"].ToString();
                    dtDC.Rows[i][3] = dt.Rows[i]["mtTotal"].ToString();
                    dtDC.Rows[i][4] = dt.Rows[i]["mtPrice"].ToString();
                    dtDC.Rows[i][5] = dt.Rows[i]["pmtNumber"].ToString();
                    dtDC.Rows[i][6] = dt.Rows[i]["pmtTotal"].ToString();
                    dtDC.Rows[i][7] = dt.Rows[i]["pmtPrice"].ToString();
                    int count = 1;
                    for (int j = 0; j < 31; j++)
                    {
                        dtDC.Rows[i][7 + count] = dt.Rows[i]["s" + (j + 1) + ""].ToString();
                        count = count + 1;
                        dtDC.Rows[i][7 + count] = dt.Rows[i]["j" + (j + 1) + ""].ToString();
                        count = count + 1;
                    }
                }
            }
            if (dtDC.Rows.Count > 0)
            {
                string s = "";
                dtDC.Rows.Add();
                for (int i = 0; i < dtDC.Rows.Count - 1; i++)
                {
                    for (int j = 2; j < dtDC.Columns.Count; j++)
                    {
                        if (dtDC.Rows[i][j].ToString() != "")
                        {
                            if (dtDC.Rows[dtDC.Rows.Count - 1][j].ToString() != "")
                            {
                                dtDC.Rows[dtDC.Rows.Count - 1][j] = (Convert.ToDouble(dtDC.Rows[i][j].ToString()) + Convert.ToDouble(dtDC.Rows[dtDC.Rows.Count - 1][j].ToString())).ToString();
                            }
                            else
                            {
                                dtDC.Rows[dtDC.Rows.Count - 1][j] = (Convert.ToDouble(dtDC.Rows[i][j].ToString())).ToString();
                            }
                        }
                    }
                }
                dtDC.Rows.Add();
                dtDC.Rows.Add();
                dtDC.Rows[dtDC.Rows.Count - 1][1] = "合计加扣款：";
                dtDC.Rows[dtDC.Rows.Count - 1][3] = jkkhj.ToString();
            }



            NpoiExcel(dtDC, "发票入库统计表");

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

        protected void gdRKBB_RowCreated(object sender, GridViewRowEventArgs e)
        {
            switch (e.Row.RowType)
            {
                case DataControlRowType.Header://行是标题行
                    TableCellCollection personHeader = e.Row.Cells;//标题行的单元格集合
                    personHeader.Clear();//清空

                    personHeader.Add(new TableHeaderCell());
                    personHeader[0].Attributes.Add("rowspan", "2"); //跨2行
                    personHeader[0].Attributes.Add("colspan", "1"); //跨1列
                    personHeader[0].Attributes.Add("width", "300"); //跨1列
                    personHeader[0].Text = "物料类别";

                    personHeader.Add(new TableHeaderCell());
                    personHeader[1].Attributes.Add("rowspan", "2"); //跨2行
                    personHeader[1].Attributes.Add("colspan", "1"); //跨1列
                    personHeader[1].Text = "物料名称";

                    personHeader.Add(new TableHeaderCell());
                    personHeader[2].Attributes.Add("colspan", "3"); //跨3列
                    personHeader[2].Text = "当期合计";

                    personHeader.Add(new TableHeaderCell());
                    personHeader[3].Attributes.Add("colspan", "3"); //跨3列
                    personHeader[3].Text = "上期合计";

                    personHeader.Add(new TableHeaderCell());
                    personHeader[4].Attributes.Add("colspan", "2");
                    personHeader[4].Text = "1日";

                    personHeader.Add(new TableHeaderCell());
                    personHeader[5].Attributes.Add("colspan", "2");
                    personHeader[5].Text = "2日";

                    personHeader.Add(new TableHeaderCell());
                    personHeader[6].Attributes.Add("colspan", "2");
                    personHeader[6].Text = "3日";

                    personHeader.Add(new TableHeaderCell());
                    personHeader[7].Attributes.Add("colspan", "2");
                    personHeader[7].Text = "4日";

                    personHeader.Add(new TableHeaderCell());
                    personHeader[8].Attributes.Add("colspan", "2");
                    personHeader[8].Text = "5日";

                    personHeader.Add(new TableHeaderCell());
                    personHeader[9].Attributes.Add("colspan", "2");
                    personHeader[9].Text = "6日";

                    personHeader.Add(new TableHeaderCell());
                    personHeader[10].Attributes.Add("colspan", "2");
                    personHeader[10].Text = "7日";

                    personHeader.Add(new TableHeaderCell());
                    personHeader[11].Attributes.Add("colspan", "2");
                    personHeader[11].Text = "8日";

                    personHeader.Add(new TableHeaderCell());
                    personHeader[12].Attributes.Add("colspan", "2");
                    personHeader[12].Text = "9日";

                    personHeader.Add(new TableHeaderCell());
                    personHeader[13].Attributes.Add("colspan", "2");
                    personHeader[13].Text = "10日";

                    personHeader.Add(new TableHeaderCell());
                    personHeader[14].Attributes.Add("colspan", "2");
                    personHeader[14].Text = "11日";

                    personHeader.Add(new TableHeaderCell());
                    personHeader[15].Attributes.Add("colspan", "2");
                    personHeader[15].Text = "12日";

                    personHeader.Add(new TableHeaderCell());
                    personHeader[16].Attributes.Add("colspan", "2");
                    personHeader[16].Text = "13日";

                    personHeader.Add(new TableHeaderCell());
                    personHeader[17].Attributes.Add("colspan", "2");
                    personHeader[17].Text = "14日";

                    personHeader.Add(new TableHeaderCell());
                    personHeader[18].Attributes.Add("colspan", "2");
                    personHeader[18].Text = "15日";

                    personHeader.Add(new TableHeaderCell());
                    personHeader[19].Attributes.Add("colspan", "2");
                    personHeader[19].Text = "16日";

                    personHeader.Add(new TableHeaderCell());
                    personHeader[20].Attributes.Add("colspan", "2");
                    personHeader[20].Text = "17日";

                    personHeader.Add(new TableHeaderCell());
                    personHeader[21].Attributes.Add("colspan", "2");
                    personHeader[21].Text = "18日";

                    personHeader.Add(new TableHeaderCell());
                    personHeader[22].Attributes.Add("colspan", "2");
                    personHeader[22].Text = "19日";

                    personHeader.Add(new TableHeaderCell());
                    personHeader[23].Attributes.Add("colspan", "2");
                    personHeader[23].Text = "20日";

                    personHeader.Add(new TableHeaderCell());
                    personHeader[24].Attributes.Add("colspan", "2");
                    personHeader[24].Text = "21日";

                    personHeader.Add(new TableHeaderCell());
                    personHeader[25].Attributes.Add("colspan", "2");
                    personHeader[25].Text = "22日";

                    personHeader.Add(new TableHeaderCell());
                    personHeader[26].Attributes.Add("colspan", "2");
                    personHeader[26].Text = "23日";

                    personHeader.Add(new TableHeaderCell());
                    personHeader[27].Attributes.Add("colspan", "2");
                    personHeader[27].Text = "24日";

                    personHeader.Add(new TableHeaderCell());
                    personHeader[28].Attributes.Add("colspan", "2");
                    personHeader[28].Text = "25日";

                    personHeader.Add(new TableHeaderCell());
                    personHeader[29].Attributes.Add("colspan", "2");
                    personHeader[29].Text = "26日";

                    personHeader.Add(new TableHeaderCell());
                    personHeader[30].Attributes.Add("colspan", "2");
                    personHeader[30].Text = "27日";

                    personHeader.Add(new TableHeaderCell());
                    personHeader[31].Attributes.Add("colspan", "2");
                    personHeader[31].Text = "28日";

                    personHeader.Add(new TableHeaderCell());
                    personHeader[32].Attributes.Add("colspan", "2");
                    personHeader[32].Text = "29日";

                    personHeader.Add(new TableHeaderCell());
                    personHeader[33].Attributes.Add("colspan", "2");
                    personHeader[33].Text = "30日";

                    personHeader.Add(new TableHeaderCell());
                    personHeader[34].Attributes.Add("colspan", "2");
                    personHeader[34].Text = "31日</th></tr><tr>";

                    personHeader.Add(new TableHeaderCell());
                    personHeader[35].Text = "数量";

                    personHeader.Add(new TableHeaderCell());
                    personHeader[36].Text = "金额";

                    personHeader.Add(new TableHeaderCell());
                    personHeader[37].Text = "单价";

                    personHeader.Add(new TableHeaderCell());
                    personHeader[38].Text = "数量";

                    personHeader.Add(new TableHeaderCell());
                    personHeader[39].Text = "金额";

                    personHeader.Add(new TableHeaderCell());
                    personHeader[40].Text = "单价";



                    //日期
                    int count = 1;
                    for (int i = 0; i < 31; i++)
                    {
                        personHeader.Add(new TableHeaderCell());
                        personHeader[40 + count].Text = "数量";
                        count = count + 1;
                        personHeader.Add(new TableHeaderCell());
                        personHeader[40 + count].Text = "金额";
                        count = count + 1;
                    }
                    //personHeader.Add(new TableHeaderCell());
                    //personHeader[2].Attributes.Add("colspan", "3"); //跨3列
                    //personHeader[2].Text = "上期合计</th></tr><tr>";
                    break;
            }
        }

        protected void gdRKBB_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow) //判断是否是DataRow，以防止鼠标经过Header也有效果   
            {
                e.Row.Attributes.Add("onmouseover", "e=this.style.backgroundColor; this.style.backgroundColor='#CCCCCC'");
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=e");
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
                    if (j < 2)
                    {
                        string DgvValue = dt.Rows[I][j].ToString();
                        row2.CreateCell(j).SetCellValue(DgvValue);
                        sheet.SetColumnWidth(j, 20 * 150);
                    }
                    else
                    {
                        if (dt.Rows[I][j].ToString().Trim() != "")
                        {
                            double DgvValue = Convert.ToDouble(dt.Rows[I][j].ToString());
                            row2.CreateCell(j).SetCellValue(DgvValue);
                            sheet.SetColumnWidth(j, 20 * 150);
                        }
                        else
                        {
                            string DgvValue = dt.Rows[I][j].ToString();
                            row2.CreateCell(j).SetCellValue(DgvValue);
                            sheet.SetColumnWidth(j, 20 * 150);
                        }
                    }
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
            DataTable dt = new DataTable();
            if (cb1.Checked == false)
            {
                DateTime dtime = new DateTime(Convert.ToInt32(txtYEAR.Text), Convert.ToInt32(txtMOUTH.Text), 1);
                dt = fpService.SELECT_RKBB(dtime.ToString("yyyy-MM-dd"), dtime.AddMonths(1).ToString("yyyy-MM-dd"), ddlLBZL.SelectedValue, ddlCKBH.SelectedValue, ddlLB.SelectedValue);
            }
            else
            {
                DateTime dtime = new DateTime(Convert.ToInt32(txtYEAR.Text), Convert.ToInt32(txtMOUTH.Text), 1);
                dt = fpService.SELECT_RKBB_1(dtime.ToString("yyyy-MM-dd"), dtime.AddMonths(1).ToString("yyyy-MM-dd"), dtime.AddMonths(-1).ToString("yyyy-MM-dd"), ddlLBZL.SelectedValue, ddlCKBH.SelectedValue, ddlLB.SelectedValue);
            }

            if (dt.Rows.Count > 0)
            {
                string[] s = new string[68];
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < 68; i++)
                    {
                        double ss = 0;
                        for (int j = 0; j < dt.Rows.Count; j++)
                        {
                            if (dt.Rows[j][i].ToString() != "")
                            {
                                if (Convert.ToDouble(dt.Rows[j][i].ToString()) != 0)
                                {
                                    ss = ss + Convert.ToDouble(dt.Rows[j][i].ToString());
                                }
                            }
                        }
                        if (ss != 0)
                        {
                            s[i] = ss.ToString();
                        }
                        else
                        {
                            s[i] = "";
                        }
                    }
                }
                GridViewRow foot = gdRKBB.FooterRow;

                for (int i = 0; i < 68; i++)
                {
                    foot.Cells[i + 2].HorizontalAlign = HorizontalAlign.Right;
                    foot.Cells[i + 2].Text = s[i];
                }
            }
        }
    }
}