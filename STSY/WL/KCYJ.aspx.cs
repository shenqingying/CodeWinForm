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

namespace STSY.WL
{
    public partial class KCYJ : System.Web.UI.Page
    {
        WLService wlService = new WLService();
        Mainservice mainService = new Mainservice();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LOGINTIMEOUT();
                STAFFQX();
                BANDddlLBBH();
                BAND();
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
            bool s = mainService.SELECT_HG_RIGHT(Session["STAFFID"].ToString(), "216");
            if (s == false)
            {
                Response.Redirect("../Index/Index.aspx?RI=1");
            }
        }
        protected void btnFIND_Click(object sender, EventArgs e)
        {
            BAND();
        }

        protected void btnDC_Click(object sender, EventArgs e)
        {
            string wlbh = "";
            if (ddlLBBH.SelectedValue == "0")
            {
                wlbh = "0";
            }
            else
            {
                wlbh = ddlWLBH.SelectedValue;
            }
            DataTable dt = wlService.GETKC(ddlLBBH.SelectedValue, txtWLTM.Text.Trim(), txtWLGG.Text.Trim(), wlbh);
            DataTable dtDC = new DataTable();
            dtDC.Columns.Add("类别", typeof(string));
            dtDC.Columns.Add("类别名称", typeof(string));
            dtDC.Columns.Add("条码", typeof(string));
            dtDC.Columns.Add("物料编号", typeof(string));
            dtDC.Columns.Add("物料名称", typeof(string));
            dtDC.Columns.Add("规格", typeof(string));
            dtDC.Columns.Add("单位", typeof(string));
            dtDC.Columns.Add("最小库存", typeof(string));
            dtDC.Columns.Add("最大库存", typeof(string));
            dtDC.Columns.Add("库存", typeof(string));
            dtDC.Columns.Add("库存金额", typeof(string));
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
                    dtDC.Rows[i][7] = dt.Rows[i]["MinNum"].ToString();
                    dtDC.Rows[i][8] = dt.Rows[i]["MaxNum"].ToString();
                    dtDC.Rows[i][9] = dt.Rows[i]["smtNumber"].ToString();
                    dtDC.Rows[i][10] = dt.Rows[i]["smtTotal"].ToString();
                }
                NpoiExcel(dtDC, "库存预警");
            }
        }


        private void BANDddlLBBH()
        {
            DataTable dt = wlService.GETWLTYPE();
            ddlLBBH.DataSource = dt;
            dt.Columns.Add("NAME", typeof(string));
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dt.Rows[i]["NAME"] = dt.Rows[i]["typeSN"].ToString() + " " + dt.Rows[i]["typeName"].ToString();
                }
            }
            ddlLBBH.DataTextField = "NAME";
            ddlLBBH.DataValueField = "typeID";
            ddlLBBH.DataBind();
            ListItem item = new ListItem();
            item.Text = "=请选择=";
            item.Value = "0";
            ddlLBBH.Items.Insert(0, item);
        }

        private void BAND()
        {
            string wlbh = "";
            if (ddlLBBH.SelectedValue == "0")
            {
                wlbh = "0";
            }
            else
            {
                wlbh = ddlWLBH.SelectedValue;
            }
            DataTable dt = wlService.GETKC(ddlLBBH.SelectedValue, txtWLTM.Text.Trim(), txtWLGG.Text.Trim(), wlbh);
            gdYJKC.DataSource = dt;
            gdYJKC.DataBind();
        }

        protected void gdYJKC_RowDataBound(object sender, GridViewRowEventArgs e)
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

        protected void ddlLBBH_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlLBBH.SelectedValue != "0")
            {
                DataTable dt = wlService.GETWLTYPE(ddlLBBH.SelectedValue);
                ddlWLBH.DataSource = dt;
                dt.Columns.Add("NAME", typeof(string));
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        dt.Rows[i]["NAME"] = dt.Rows[i]["mtSN"].ToString() + " " + dt.Rows[i]["mtName"].ToString();
                    }
                }
                ddlWLBH.DataTextField = "NAME";
                ddlWLBH.DataValueField = "materialID";
                ddlWLBH.DataBind();
                ListItem item = new ListItem();
                item.Text = "=请选择=";
                item.Value = "0";
                ddlWLBH.Items.Insert(0, item);
            }
        }

        protected void gdYJKC_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gdYJKC.PageIndex = e.NewPageIndex;
            BAND();
        }
    }
}