using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bussiness.FKSY;
using System.Data;
using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;
using System.IO;
using Bussiness.STSY;

namespace STSY.FKSY
{
    public partial class SelectFKINFO : System.Web.UI.Page
    {
        ServiceFKINFO serviceFINFO = new ServiceFKINFO();
        Mainservice mainService = new Mainservice();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["ID"] != null)
                {
                    Session["STAFFID"] = Request.QueryString["ID"];
                }
                lbJLMW.Visible = false;
                ddlJLMW.Visible = false;
                LOGINTIMEOUT();
                STAFFQX();
                BANDddlGSDM();
                gvFKINFO.Attributes.Add("style", "word-break:keep-all;word-wrap:normal");
            }
        }

        private void LOGINTIMEOUT()
        {
            if (Session["STAFFID"] == null)
            {
                Response.Redirect("../login.aspx?RI=1");
            }
        }
        protected void btnCXSX_Click(object sender, EventArgs e)
        {
            BANDgvFKINFO();
        }

        private void BANDgvFKINFO()
        {
            string GSDMRI = "0";
            string JRMWRI = "0";
            GSDMRI = ddlGSDM.SelectedValue;
            if (GSDMRI == "0")
            {
                JRMWRI = "0";
            }
            else
            {
                JRMWRI = ddlJLMW.SelectedValue;
            }
            DataTable dtFKINFO = serviceFINFO.Select_FKINFO(Session["STAFFID"].ToString(), txtFKZH.Text, txtFKXM.Text, txtFKLFSJS.Text, txtFKLFSJE.Text, ddlFKZT.SelectedValue, GSDMRI, JRMWRI);
            gvFKINFO.DataSource = dtFKINFO;
            gvFKINFO.DataBind();
        }

        protected void gvFKINFO_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow) //判断是否是DataRow，以防止鼠标经过Header也有效果   
            {
                e.Row.Attributes.Add("onmouseover", "e=this.style.backgroundColor; this.style.backgroundColor='#CCCCCC'");
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=e");
            }
        }

        private void BANDddlGSDM()
        {
            DataTable dt = serviceFINFO.Select_FKINFO_QX_GSDM(Session["STAFFID"].ToString());
            ddlGSDM.DataSource = dt;
            ddlGSDM.DataTextField = "GSDMMS";
            ddlGSDM.DataValueField = "GSDMRI";
            ddlGSDM.DataBind();
            ListItem item = new ListItem();
            item.Text = "=请选择=";
            item.Value = "0";
            ddlGSDM.Items.Insert(0, item);
        }

        private void BANDddlJLMW()
        {
            DataTable dt = serviceFINFO.Select_FKINFO_QX_MW(Session["STAFFID"].ToString(), ddlGSDM.SelectedValue);
            ddlJLMW.DataSource = dt;
            ddlJLMW.DataTextField = "MWMS";
            ddlJLMW.DataValueField = "RI";
            ddlJLMW.DataBind();
            ListItem item = new ListItem();
            item.Text = "=请选择=";
            item.Value = "0";
            ddlJLMW.Items.Insert(0, item);
        }

        protected void ddlGSDM_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlGSDM.SelectedValue != "0")
            {
                lbJLMW.Visible = true;
                ddlJLMW.Visible = true;
                BANDddlJLMW();
            }
            else
            {
                lbJLMW.Visible = false;
                ddlJLMW.Visible = false;
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

        protected void btnDC_Click(object sender, EventArgs e)
        {
            LOGINTIMEOUT();
            try
            {
                string GSDMRI = "0";
                string JRMWRI = "0";
                GSDMRI = ddlGSDM.SelectedValue;
                if (GSDMRI == "0")
                {
                    JRMWRI = "0";
                }
                else
                {
                    JRMWRI = ddlJLMW.SelectedValue;
                }
                DataTable dtFKINFO = serviceFINFO.Select_FKINFO(Session["STAFFID"].ToString(), txtFKZH.Text, txtFKXM.Text, txtFKLFSJS.Text, txtFKLFSJE.Text, ddlFKZT.SelectedValue, GSDMRI, JRMWRI);
                DataTable dtDC = new DataTable();
                dtDC.Columns.Add("访客编号", typeof(string));
                dtDC.Columns.Add("访客姓名", typeof(string));
                dtDC.Columns.Add("性别", typeof(string));
                dtDC.Columns.Add("名族", typeof(string));
                dtDC.Columns.Add("出生日期", typeof(string));
                dtDC.Columns.Add("证件类型描述", typeof(string));
                dtDC.Columns.Add("证件号码", typeof(string));
                dtDC.Columns.Add("住址", typeof(string));
                dtDC.Columns.Add("单位", typeof(string));
                dtDC.Columns.Add("车牌号", typeof(string));
                dtDC.Columns.Add("访客证号", typeof(string));
                dtDC.Columns.Add("箱单号", typeof(string));
                dtDC.Columns.Add("封箱号", typeof(string));
                dtDC.Columns.Add("受访部门", typeof(string));
                dtDC.Columns.Add("受访人员名称", typeof(string));
                dtDC.Columns.Add("受访人工号", typeof(string));
                dtDC.Columns.Add("访客来访事由", typeof(string));
                dtDC.Columns.Add("事由明细", typeof(string));
                dtDC.Columns.Add("访客人数", typeof(string));
                dtDC.Columns.Add("随身物品", typeof(string));
                dtDC.Columns.Add("进入门卫", typeof(string));
                dtDC.Columns.Add("来访时间", typeof(string));
                dtDC.Columns.Add("是否离开", typeof(string));
                dtDC.Columns.Add("离开门卫", typeof(string));
                dtDC.Columns.Add("离开时间", typeof(string));
                dtDC.Columns.Add("备注", typeof(string));
                if (dtFKINFO.Rows.Count > 0)
                {
                    for (int i = 0; i < dtFKINFO.Rows.Count; i++)
                    {
                        DataRow dr = dtDC.NewRow();
                        dr[0] = dtFKINFO.Rows[i]["FKNO"].ToString();
                        dr[1] = dtFKINFO.Rows[i]["FKNAME"].ToString();
                        dr[2] = dtFKINFO.Rows[i]["FKSEX"].ToString();
                        dr[3] = dtFKINFO.Rows[i]["FKMZ"].ToString();
                        dr[4] = dtFKINFO.Rows[i]["FKCSRQ"].ToString();
                        dr[5] = dtFKINFO.Rows[i]["FKZJLX"].ToString();
                        dr[6] = dtFKINFO.Rows[i]["FKZJHM"].ToString();
                        dr[7] = dtFKINFO.Rows[i]["FKADD"].ToString();
                        dr[8] = dtFKINFO.Rows[i]["FKDW"].ToString();
                        dr[9] = dtFKINFO.Rows[i]["FKCPH"].ToString();
                        dr[10] = dtFKINFO.Rows[i]["FKZH"].ToString();
                        dr[11] = dtFKINFO.Rows[i]["XDH"].ToString();
                        dr[12] = dtFKINFO.Rows[i]["FXH"].ToString();
                        dr[13] = dtFKINFO.Rows[i]["SFBM"].ToString();
                        dr[14] = dtFKINFO.Rows[i]["SFNAME"].ToString();
                        dr[15] = dtFKINFO.Rows[i]["SFGH"].ToString();
                        dr[16] = dtFKINFO.Rows[i]["FKLFSY"].ToString();
                        dr[17] = dtFKINFO.Rows[i]["FKLFSYMX"].ToString();
                        dr[18] = dtFKINFO.Rows[i]["FKSUM"].ToString();
                        dr[19] = dtFKINFO.Rows[i]["FKSSWP"].ToString();
                        dr[20] = dtFKINFO.Rows[i]["JRMW"].ToString();
                        dr[21] = dtFKINFO.Rows[i]["LFTIME"].ToString();
                        dr[22] = dtFKINFO.Rows[i]["ISLK"].ToString();
                        dr[23] = dtFKINFO.Rows[i]["LKMW"].ToString();
                        dr[24] = dtFKINFO.Rows[i]["LKTIME"].ToString();
                        dr[25] = dtFKINFO.Rows[i]["BZ"].ToString();
                        dtDC.Rows.Add(dr);
                    }
                }
                NpoiExcel(dtDC, "访客查询");
                this.Page.RegisterStartupScript("onclick", "<script>alert('导出成功！')</script>");
            }
            catch
            {
                this.Page.RegisterStartupScript("onclick", "<script>alert('导出失败,请联系管理员！')</script>");
            }
        }

        private void STAFFQX()
        {
            LOGINTIMEOUT();
            bool s = mainService.SELECT_HG_RIGHT(Session["STAFFID"].ToString(), "501");
            if (s == false)
            {
                Response.Redirect("../Index/Index.aspx?RI=1");
            }
        }
    }
}