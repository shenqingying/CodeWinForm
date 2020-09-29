using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bussiness.STSY;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System.IO;

namespace STSY.WL
{
    public partial class WLKC : System.Web.UI.Page
    {
        WLService wlService = new WLService();
        CKService ckService = new CKService();
        Mainservice mainService = new Mainservice();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LOGINTIMEOUT();
                STAFFQX();
                BANDddlLBBH();
                BANDddlCKBH();

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
            bool s = mainService.SELECT_HG_RIGHT(Session["STAFFID"].ToString(), "215");
            if (s == false)
            {
                Response.Redirect("../Index/Index.aspx?RI=1");
            }
        }
        protected void btnDC_Click(object sender, EventArgs e)
        {
            DataTable dt = wlService.GETKC(ddlCKBH.SelectedValue, ddlLBBH.SelectedValue, txtWLBH.Text.Trim(), txtWLMC.Text.Trim(), txtWLTM.Text.Trim(), txtWLGG.Text.Trim());
            dt.Columns.Add("PJJ", typeof(string));
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    double pjj = Convert.ToDouble(dt.Rows[i]["mtTotal"].ToString()) / Convert.ToDouble(dt.Rows[i]["mtNumber"].ToString());
                    dt.Rows[i]["PJJ"] = Math.Round((decimal)pjj, 2, MidpointRounding.AwayFromZero);
                }
            }
            DataTable dtDC = new DataTable();
            dtDC.Columns.Add("编号", typeof(string));
            dtDC.Columns.Add("类别名称", typeof(string));
            dtDC.Columns.Add("编码", typeof(string));
            dtDC.Columns.Add("物料编号", typeof(string));
            dtDC.Columns.Add("物料名称", typeof(string));
            dtDC.Columns.Add("规格型号", typeof(string));
            dtDC.Columns.Add("单位", typeof(string));
            dtDC.Columns.Add("库位编号", typeof(string));
            dtDC.Columns.Add("库位名称", typeof(string));
            dtDC.Columns.Add("数量", typeof(string));
            dtDC.Columns.Add("总金额", typeof(string));
            dtDC.Columns.Add("平均价", typeof(string));
            dtDC.Columns.Add("卖出价", typeof(string));
            dtDC.Columns.Add("买入价", typeof(string));
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dtDC.Rows.Add();
                    dtDC.Rows[i][0] = dt.Rows[i]["typeSN"].ToString();
                    dtDC.Rows[i][1] = dt.Rows[i]["typeName"].ToString();
                    dtDC.Rows[i][2] = dt.Rows[i]["mtNO"].ToString();
                    dtDC.Rows[i][3] = dt.Rows[i]["mtSN"].ToString();
                    dtDC.Rows[i][4] = dt.Rows[i]["mtName"].ToString();
                    dtDC.Rows[i][5] = dt.Rows[i]["mtSpec"].ToString();
                    dtDC.Rows[i][6] = dt.Rows[i]["mtUnit"].ToString();
                    dtDC.Rows[i][7] = dt.Rows[i]["PlaceSN"].ToString();
                    dtDC.Rows[i][8] = dt.Rows[i]["PlaceName"].ToString();
                    dtDC.Rows[i][9] = dt.Rows[i]["mtNumber"].ToString();
                    dtDC.Rows[i][10] = dt.Rows[i]["mtTotal"].ToString();
                    dtDC.Rows[i][11] = dt.Rows[i]["PJJ"].ToString();
                    dtDC.Rows[i][12] = dt.Rows[i]["mtSalePrice"].ToString();
                    dtDC.Rows[i][13] = dt.Rows[i]["mtBuyPrice"].ToString();
                }
                NpoiExcel(dtDC, "库存");
            }
        }

        private void BANDddlLBBH()
        {
            DataTable dt = wlService.GETWLTYPELB();
            ddlLBBH.DataSource = dt;
            ddlLBBH.DataTextField = "NAME";
            ddlLBBH.DataValueField = "typeID";
            ddlLBBH.DataBind();
            ListItem item = new ListItem();
            item.Text = "=请选择=";
            item.Value = "0";
            ddlLBBH.Items.Insert(0, item);
        }

        private void BANDddlCKBH()
        {
            DataTable dt = ckService.GETCKINFOISUSERLB();
            ddlCKBH.DataSource = dt;
            ddlCKBH.DataTextField = "NAME";
            ddlCKBH.DataValueField = "StockID";
            ddlCKBH.DataBind();
        }

        protected void btnFIND_Click(object sender, EventArgs e)
        {
            DataTable dt = wlService.GETKC(ddlCKBH.SelectedValue, ddlLBBH.SelectedValue, txtWLBH.Text.Trim(), txtWLMC.Text.Trim(), txtWLTM.Text.Trim(), txtWLGG.Text.Trim());
            dt.Columns.Add("PJJ", typeof(string));
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    double pjj = Convert.ToDouble(dt.Rows[i]["mtTotal"].ToString()) / Convert.ToDouble(dt.Rows[i]["mtNumber"].ToString());
                    dt.Rows[i]["PJJ"] = Math.Round((decimal)pjj, 2, MidpointRounding.AwayFromZero);
                }
            }
            gdKC.DataSource = dt;
            gdKC.DataBind();
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

        protected void gdKC_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow) //判断是否是DataRow，以防止鼠标经过Header也有效果   
            {
                e.Row.Attributes.Add("onmouseover", "e=this.style.backgroundColor; this.style.backgroundColor='#CCCCCC'");
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=e");
            }
        }

        protected void gdKC_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gdKC.PageIndex = e.NewPageIndex;
            DataTable dt = wlService.GETKC(ddlCKBH.SelectedValue, ddlLBBH.SelectedValue, txtWLBH.Text.Trim(), txtWLMC.Text.Trim(), txtWLTM.Text.Trim(), txtWLGG.Text.Trim());
            dt.Columns.Add("PJJ", typeof(string));
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    double pjj = Convert.ToDouble(dt.Rows[i]["mtTotal"].ToString()) / Convert.ToDouble(dt.Rows[i]["mtNumber"].ToString());
                    dt.Rows[i]["PJJ"] = Math.Round((decimal)pjj, 2, MidpointRounding.AwayFromZero);
                }
            }
            gdKC.DataSource = dt;
            gdKC.DataBind();
        }
    }
}