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
using System.Drawing;

namespace STSY.RKGL
{
    public partial class JGCX : System.Web.UI.Page
    {
        WLService wlService = new WLService();
        RKService rkService = new RKService();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LOGINTIMEOUT();
                BANDLOAD();
                lbtnWLCGQD.BackColor = System.Drawing.Color.DarkGray;
                lbtnWLCGJGQST.BackColor = System.Drawing.Color.White;
                divBT.Visible = true;
                divQST.Visible = true;
                DateTime now = DateTime.Now;
                DateTime d1 = new DateTime(now.Year, 1, 1);
                txtRKDATES.Text = d1.ToString("yyyy-MM-dd");
                txtRKDATEE.Text = now.ToString("yyyy-MM-dd");
                cbRKRQ.Checked = true;
                txtRKDATES.Enabled = true;
                txtRKDATEE.Enabled = true;
            }
        }
        private void LOGINTIMEOUT()
        {
            if (Session["STAFFID"] == null)
            {
                Response.Redirect("../login.aspx?RI=1");
            }
        }
        private void BANDLOAD()
        {
            TreeView1.Nodes.Clear();
            DataTable dt = wlService.GETWLTYPE();
            BANDTREEFATHER(dt);
        }

        protected void TreeView1_SelectedNodeChanged(object sender, EventArgs e)
        {
            string RUKDATES = "";
            string RUKDATEE = "";
            if (cbRKRQ.Checked == true)
            {
                RUKDATES = txtRKDATES.Text;
                RUKDATEE = txtRKDATEE.Text;
            }
            TreeView1.SelectedNodeStyle.BackColor = System.Drawing.Color.BurlyWood;
            string[] JD = TreeView1.SelectedNode.Value.Split(',');
            if (JD.Length == 2)
            {
                if (JD[0] == "1")
                {
                    string materialID = JD[1];
                    DataTable dt = rkService.SELECRKWK(materialID, "1", txtRKDATES.Text, txtRKDATEE.Text);
                    dt.Columns.Add("RuKDate1", typeof(string));
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            dt.Rows[i]["RuKDate1"] = Convert.ToDateTime(dt.Rows[i]["RuKDate"].ToString()).ToString("yyyy-MM-dd");
                        }
                    }
                    gdWLRK.DataSource = dt;
                    gdWLRK.DataBind();

                    DataTable dtQST = new DataTable();
                    dtQST.Columns.Add("RuKDate", typeof(string));
                    dtQST.Columns.Add("mtPrice", typeof(string));
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            dtQST.Rows.Add();
                        }
                        for (int i = dt.Rows.Count - 1; i >= 0; i--)
                        {
                            dtQST.Rows[i][0] = dt.Rows[i]["RuKDate1"].ToString();
                            dtQST.Rows[i][1] = dt.Rows[i]["mtPrice"].ToString();
                            if (i > 20)
                            {
                                break;
                            }
                        }
                    }
                    BANDGST(dtQST);
                }
            }
        }

        private void BANDTREEFATHER(DataTable dt)
        {
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    TreeNode father = new TreeNode();
                    father.Text = dt.Rows[i]["typeSN"].ToString() + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + dt.Rows[i]["typeName"].ToString();
                    father.Value = "0," + dt.Rows[i]["typeID"].ToString();
                    this.TreeView1.Nodes.Add(father);
                    DataTable dtChild = wlService.GETWLTYPE(dt.Rows[i]["typeID"].ToString());
                    if (dtChild.Rows.Count > 0)
                    {
                        for (int j = 0; j < dtChild.Rows.Count; j++)
                        {
                            TreeNode child = new TreeNode();
                            child.Text = dtChild.Rows[j]["mtSN"].ToString() + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + dtChild.Rows[j]["mtName"].ToString();
                            child.Value = "1," + dtChild.Rows[j]["materialID"].ToString();
                            father.ChildNodes.Add(child);
                        }
                    }
                }
            }
        }

        protected void btnCXSX_Click(object sender, EventArgs e)
        {
            BANDLOAD();
            gdWLRK.DataSource = null;
            gdWLRK.DataBind();
        }

        protected void btnDC_Click(object sender, EventArgs e)
        {
            if (gdWLRK.Rows.Count > 0)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("物料编号", typeof(string));
                dt.Columns.Add("单位", typeof(string));
                dt.Columns.Add("入库日期", typeof(string));
                dt.Columns.Add("数量", typeof(string));
                dt.Columns.Add("单价", typeof(string));
                dt.Columns.Add("总金额", typeof(string));
                dt.Columns.Add("供应商名称", typeof(string));
                dt.Columns.Add("说明", typeof(string));
                dt.Columns.Add("入库单号", typeof(string));
                for (int i = 0; i < gdWLRK.Rows.Count; i++)
                {
                    dt.Rows.Add();
                    dt.Rows[i][0] = ((Label)gdWLRK.Rows[i].FindControl("WLBH")).Text;
                    dt.Rows[i][1] = ((Label)gdWLRK.Rows[i].FindControl("DW")).Text;
                    dt.Rows[i][2] = ((Label)gdWLRK.Rows[i].FindControl("RKRQ")).Text;
                    dt.Rows[i][3] = ((Label)gdWLRK.Rows[i].FindControl("SL")).Text;
                    dt.Rows[i][4] = ((Label)gdWLRK.Rows[i].FindControl("DJ")).Text;
                    dt.Rows[i][5] = ((Label)gdWLRK.Rows[i].FindControl("ZJE")).Text;
                    dt.Rows[i][6] = ((Label)gdWLRK.Rows[i].FindControl("GYSMC")).Text;
                    dt.Rows[i][7] = ((Label)gdWLRK.Rows[i].FindControl("SM")).Text;
                    dt.Rows[i][8] = ((Label)gdWLRK.Rows[i].FindControl("RKDH")).Text;
                }
                NpoiExcel(dt, "价格导出");
            }
            else
            {
                this.Page.RegisterStartupScript("onclick", "<script>alert('无数据，无法导出！')</script>");
            }
        }

        protected void gdWLRK_RowDataBound(object sender, GridViewRowEventArgs e)
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

        DataTable CreatData()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("RuKDate", System.Type.GetType("System.String"));
            dt.Columns.Add("mtPrice", System.Type.GetType("System.String"));

            string[] n = new string[] { "2016-01-29", "2016-01-28", "2016-01-27", "2016-01-23", "2016-01-18", "2016-01-15", "2016-01-29", "2016-01-28", "2016-01-27", "2016-01-23", "2016-01-18", "2016-01-15" };
            string[] c = new string[] { "155", "155", "155", "155", "140", "140", "155", "155", "155", "155", "140", "140" };
            for (int i = 0; i < n.Length; i++)
            {
                DataRow dr = dt.NewRow();
                dr["RuKDate"] = n[i];
                dr["mtPrice"] = c[i];
                dt.Rows.Add(dr);
            }
            return dt;
        }


        protected void lbtnWLCGQD_Click(object sender, EventArgs e)
        {
            lbtnWLCGQD.BackColor = System.Drawing.Color.DarkGray;
            lbtnWLCGJGQST.BackColor = System.Drawing.Color.White;
            divBT.Visible = true;
            divQST.Visible = false;
        }
        protected void lbtnWLCGJGQST_Click(object sender, EventArgs e)
        {
            lbtnWLCGQD.BackColor = System.Drawing.Color.White;
            lbtnWLCGJGQST.BackColor = System.Drawing.Color.DarkGray;
            divBT.Visible = false;
            divQST.Visible = true;
        }

        private void BANDGST(DataTable dt)
        {
            Chart1.DataSource = dt;//绑定数据
            Chart1.Series["Series1"].ChartType = System.Web.UI.DataVisualization.Charting.SeriesChartType.Line;//设置图表类型
            Chart1.Series[0].XValueMember = "RuKDate";//X轴数据成员列
            Chart1.Series[0].YValueMembers = "mtPrice";//Y轴数据成员列
            Chart1.ChartAreas["ChartArea1"].AxisX.Title = "日期";//X轴标题
            Chart1.ChartAreas["ChartArea1"].AxisX.TitleAlignment = StringAlignment.Far;//设置Y轴标题的名称所在位置位远
            Chart1.ChartAreas["ChartArea1"].AxisY.Title = "价格";//X轴标题
            Chart1.ChartAreas["ChartArea1"].AxisY.TitleAlignment = StringAlignment.Far;//设置Y轴标题的名称所在位置位远
            Chart1.ChartAreas["ChartArea1"].AxisX.Interval = 1;//X轴数据的间距
            Chart1.ChartAreas["ChartArea1"].AxisX.MajorGrid.Enabled = false;//不显示竖着的分割线
            Chart1.Series[0].IsValueShownAsLabel = true;//显示坐标值
        }

        protected void cbRKRQ_CheckedChanged(object sender, EventArgs e)
        {
            if (cbRKRQ.Checked == true)
            {
                txtRKDATEE.Enabled = true;
                txtRKDATES.Enabled = true;
            }
            else
            {
                txtRKDATEE.Enabled = false;
                txtRKDATES.Enabled = false;
            }
        }

        protected void gdWLRK_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gdWLRK.PageIndex = e.NewPageIndex;
            string RUKDATES = "";
            string RUKDATEE = "";
            if (cbRKRQ.Checked == true)
            {
                RUKDATES = txtRKDATES.Text;
                RUKDATEE = txtRKDATEE.Text;
            }
            TreeView1.SelectedNodeStyle.BackColor = System.Drawing.Color.BurlyWood;
            string[] JD = TreeView1.SelectedNode.Value.Split(',');
            if (JD.Length == 2)
            {
                if (JD[0] == "1")
                {
                    string materialID = JD[1];
                    DataTable dt = rkService.SELECRKWK(materialID, "1", txtRKDATES.Text, txtRKDATEE.Text);
                    dt.Columns.Add("RuKDate1", typeof(string));
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            dt.Rows[i]["RuKDate1"] = Convert.ToDateTime(dt.Rows[i]["RuKDate"].ToString()).ToString("yyyy-MM-dd");
                        }
                    }
                    gdWLRK.DataSource = dt;
                    gdWLRK.DataBind();
                }
            }
        }
    }
}