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
    public partial class CKRBB : System.Web.UI.Page
    {
        CKService ckService = new CKService();
        WLService wlService = new WLService();
        CKRBService ckrbService = new CKRBService();
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
            bool s = mainService.SELECT_HG_RIGHT(Session["STAFFID"].ToString(), "227");
            if (s == false)
            {
                Response.Redirect("../Index/Index.aspx?RI=1");
            }
        }
        protected void btnSCBB_Click(object sender, EventArgs e)
        {

            string typeType = "";
            if (ddlLBZL.SelectedValue != "-1")
            {
                typeType = ddlLBZL.SelectedValue;
            }
            string typeID = "";
            if (ddlLB.SelectedValue != "0")
            {
                typeID = ddlLB.SelectedValue;
            }
            DataTable dt = ckrbService.SELECT_MSMaterial(typeType, typeID);
            if (dt.Rows.Count > 0)
            {
                DataTable dtMX = new DataTable();
                DateTime dtime = new DateTime(Convert.ToInt32(txtYEAR.Text), Convert.ToInt32(txtMOUTH.Text), 1);
                dtMX = ckrbService.SCCRBB(typeType, dtime.ToString("yyyy-MM-dd"), dtime.AddMonths(1).ToString("yyyy-MM-dd"), ddlCKBH.SelectedValue, typeID, rbl1.SelectedValue);

                //GridView1.DataSource = dtMX;
                //GridView1.DataBind();
                //BANDHJ(dtMX);




                DataTable dtmxs = new DataTable();
                for (int i = 0; i < dtMX.Columns.Count; i++)
                {
                    dtmxs.Columns.Add(i.ToString(), typeof(string));
                }
                if (dtMX.Rows.Count > 0)
                {
                    for (int i = 0; i < dtMX.Rows.Count; i++)
                    {
                        if (Convert.ToDouble(dtMX.Rows[i][1].ToString()) != 0 || Convert.ToDouble(dtMX.Rows[i][64].ToString()) != 0 || Convert.ToDouble(dtMX.Rows[i][65].ToString()) != 0 || Convert.ToDouble(dtMX.Rows[i][66].ToString()) != 0 || Convert.ToDouble(dtMX.Rows[i][67].ToString()) != 0)
                        {
                            dtmxs.Rows.Add();
                            for (int j = 0; j < dtMX.Columns.Count; j++)
                            {

                                if (dtMX.Rows[i][j].ToString() == "")
                                {
                                    dtmxs.Rows[dtmxs.Rows.Count - 1][j] = "";
                                }
                                else
                                {
                                    dtmxs.Rows[dtmxs.Rows.Count - 1][j] = dtMX.Rows[i][j].ToString();
                                }

                            }
                        }

                    }

                }
                if (dtmxs.Rows.Count > 0)
                {
                    Label6.Text = "";
                }
                else
                {
                    Label6.Text = "没有数据！";
                }
                GridView1.DataSource = dtmxs;
                GridView1.DataBind();
                BANDHJ(dtmxs);


            }
        }

        protected void btnDC_Click(object sender, EventArgs e)
        {
            string typeType = "";
            if (ddlLBZL.SelectedValue != "-1")
            {
                typeType = ddlLBZL.SelectedValue;
            }
            string typeID = "";
            if (ddlLB.SelectedValue != "0")
            {
                typeID = ddlLB.SelectedValue;
            }
            DataTable dt = ckrbService.SELECT_MSMaterial(typeType, typeID);
            if (dt.Rows.Count > 0)
            {
                DataTable dtMX = new DataTable();
                DateTime dtime = new DateTime(Convert.ToInt32(txtYEAR.Text), Convert.ToInt32(txtMOUTH.Text), 1);
                this.Page.RegisterStartupScript("onclick", "<script>alert('根据品名生成的报表已大于50列，改用类别汇总报表!')</script>");
                dtMX = ckrbService.SCCRBB(typeType, dtime.ToString("yyyy-MM-dd"), dtime.AddMonths(1).ToString("yyyy-MM-dd"), ddlCKBH.SelectedValue, typeID, rbl1.SelectedValue);

                if (dtMX.Rows.Count > 0)
                {
                    DataTable dtDC = new DataTable();
                    dtDC.Columns.Add("名称", typeof(string));
                    dtDC.Columns.Add("上期库存", typeof(string));
                    for (int i = 0; i < 31; i++)
                    {
                        dtDC.Columns.Add("" + (i + 1) + "日|增", typeof(string));
                        dtDC.Columns.Add("" + (i + 1) + "日|耗", typeof(string));
                    }
                    dtDC.Columns.Add("当期合计|增", typeof(string));
                    dtDC.Columns.Add("当期合计|耗", typeof(string));
                    dtDC.Columns.Add("当期合计|小计", typeof(string));
                    dtDC.Columns.Add("本期库存", typeof(string));
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
                    NpoiExcel(dtDC, "耗用日报表");
                }
            }
        }


        protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
        {
            switch (e.Row.RowType)
            {
                case DataControlRowType.Header://行是标题行
                    TableCellCollection personHeader = e.Row.Cells;//标题行的单元格集合
                    personHeader.Clear();//清空

                    personHeader.Add(new TableHeaderCell());
                    personHeader[0].Attributes.Add("rowspan", "2"); //跨2行
                    personHeader[0].Attributes.Add("colspan", "1"); //跨1列
                    personHeader[0].Text = "名称";

                    personHeader.Add(new TableHeaderCell());
                    personHeader[1].Attributes.Add("rowspan", "2"); //跨2行
                    personHeader[1].Attributes.Add("colspan", "1"); //跨1列
                    personHeader[1].Text = "上期库存";

                    int s = 2;
                    for (int i = 0; i < 31; i++)
                    {
                        personHeader.Add(new TableHeaderCell());
                        personHeader[s].Attributes.Add("colspan", "2");
                        personHeader[s].Text = "" + (i + 1) + "日";
                        s = s + 1;
                    }

                    personHeader.Add(new TableHeaderCell());
                    personHeader[s].Attributes.Add("colspan", "3"); //跨3列
                    personHeader[s].Text = "当期合计";
                    s = s + 1;

                    personHeader.Add(new TableHeaderCell());
                    personHeader[s].Attributes.Add("rowspan", "2"); //跨2行
                    personHeader[s].Attributes.Add("colspan", "1"); //跨1列
                    personHeader[s].Text = "本期库存</th></tr><tr>";
                    s = s + 1;

                    for (int i = 0; i < 32; i++)
                    {
                        personHeader.Add(new TableHeaderCell());
                        personHeader[s].Text = "增";
                        s = s + 1;
                        personHeader.Add(new TableHeaderCell());
                        personHeader[s].Text = "耗";
                        s = s + 1;
                    }

                    personHeader.Add(new TableHeaderCell());
                    personHeader[s].Text = "小计";
                    break;
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