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

namespace STSY.JDGL
{
    public partial class JDCX : System.Web.UI.Page
    {
        ZDService zdService = new ZDService();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LOGINTIMEOUT();
                lbtnJDDJ.BackColor = System.Drawing.Color.White;
                lbtnJDDJLB.BackColor = System.Drawing.Color.DarkGray;
                divJDDJ.Visible = false;
                divJDDJLB.Visible = true;
                BANDddlLBTZBM();
                BANDddlLBLXR();
                BANDddlLBDD();
                BANDddlLBLX();
                cbLBXQRQ.Checked = true;
                txtLBXQRQS.Enabled = true;
                txtLBXQRQE.Enabled = true;
                DateTime now = DateTime.Now;
                DateTime d1 = new DateTime(now.Year, now.Month, 1);
                txtLBXQRQS.Text = d1.ToString("yyyy-MM-dd");
                txtLBXQRQE.Text = now.ToString("yyyy-MM-dd");
                rbLB.SelectedValue = "0";
            }
        }

        protected void btnCXSX_Click(object sender, EventArgs e)
        {
            if (lbISLB.Text.ToUpper() == "TRUE")
            {
                BANDLB();
            }
        }

        protected void btnDC_Click(object sender, EventArgs e)
        {
            if (lbISLB.Text.ToUpper() == "TRUE")
            {
                if (gdZDDJLB.Rows.Count > 0)
                {
                    int[] numbers = new int[4] { 0, 0, 0, 0 };
                    DataTable dt = new DataTable();
                    dt.Columns.Add("招待ID", typeof(string));
                    dt.Columns.Add("ID", typeof(string));
                    dt.Columns.Add("通知部门", typeof(string));
                    dt.Columns.Add("联系人", typeof(string));
                    dt.Columns.Add("联系电话", typeof(string));
                    dt.Columns.Add("类型名称", typeof(string));
                    dt.Columns.Add("需求日期", typeof(string));
                    dt.Columns.Add("需求时间", typeof(string));
                    dt.Columns.Add("地点", typeof(string));
                    dt.Columns.Add("数量", typeof(string));
                    dt.Columns.Add("蛋糕", typeof(string));
                    dt.Columns.Add("饼干", typeof(string));
                    dt.Columns.Add("其他", typeof(string));
                    dt.Columns.Add("已确认", typeof(string));
                    dt.Columns.Add("已结束", typeof(string));
                    dt.Columns.Add("需求说明", typeof(string));
                    dt.Columns.Add("制单人", typeof(string));
                    dt.Columns.Add("制单时间", typeof(string));
                    dt.Columns.Add("备注", typeof(string));
                    for (int i = 0; i < gdZDDJLB.Rows.Count; i++)
                    {
                        dt.Rows.Add();
                        dt.Rows[i]["招待ID"] = ((Label)gdZDDJLB.Rows[i].FindControl("ZDID")).Text;
                        dt.Rows[i]["ID"] = ((Label)gdZDDJLB.Rows[i].FindControl("ID")).Text;
                        dt.Rows[i]["通知部门"] = ((Label)gdZDDJLB.Rows[i].FindControl("TZBM")).Text;
                        dt.Rows[i]["联系人"] = ((Label)gdZDDJLB.Rows[i].FindControl("LXR")).Text;
                        dt.Rows[i]["联系电话"] = ((Label)gdZDDJLB.Rows[i].FindControl("LXDH")).Text;
                        dt.Rows[i]["类型名称"] = ((Label)gdZDDJLB.Rows[i].FindControl("LXMC")).Text;
                        dt.Rows[i]["需求日期"] = ((Label)gdZDDJLB.Rows[i].FindControl("XQRQ")).Text;
                        dt.Rows[i]["需求时间"] = ((Label)gdZDDJLB.Rows[i].FindControl("XQSJ")).Text;
                        dt.Rows[i]["地点"] = ((Label)gdZDDJLB.Rows[i].FindControl("DD")).Text;
                        dt.Rows[i]["数量"] = ((Label)gdZDDJLB.Rows[i].FindControl("SL")).Text;
                        if (((Label)gdZDDJLB.Rows[i].FindControl("SL")).Text != "")
                        {
                            numbers[0] = numbers[0] + Convert.ToInt32(((Label)gdZDDJLB.Rows[i].FindControl("SL")).Text);
                        }
                        dt.Rows[i]["蛋糕"] = ((Label)gdZDDJLB.Rows[i].FindControl("DG")).Text;
                        if (((Label)gdZDDJLB.Rows[i].FindControl("DG")).Text != "")
                        {
                            numbers[1] = numbers[1] + Convert.ToInt32(((Label)gdZDDJLB.Rows[i].FindControl("DG")).Text);
                        }
                        dt.Rows[i]["饼干"] = ((Label)gdZDDJLB.Rows[i].FindControl("BG")).Text;
                        if (((Label)gdZDDJLB.Rows[i].FindControl("BG")).Text != "")
                        {
                            numbers[2] = numbers[2] + Convert.ToInt32(((Label)gdZDDJLB.Rows[i].FindControl("BG")).Text);
                        }
                        dt.Rows[i]["其他"] = ((Label)gdZDDJLB.Rows[i].FindControl("qt")).Text;
                        if (((Label)gdZDDJLB.Rows[i].FindControl("qt")).Text != "")
                        {
                            numbers[3] = numbers[3] + Convert.ToInt32(((Label)gdZDDJLB.Rows[i].FindControl("qt")).Text);
                        }
                        dt.Rows[i]["已确认"] = ((CheckBox)gdZDDJLB.Rows[i].FindControl("YQR")).Checked.ToString();
                        dt.Rows[i]["已结束"] = ((CheckBox)gdZDDJLB.Rows[i].FindControl("YJS")).Checked.ToString();
                        dt.Rows[i]["需求说明"] = ((Label)gdZDDJLB.Rows[i].FindControl("XQSM")).Text;
                        dt.Rows[i]["制单人"] = ((Label)gdZDDJLB.Rows[i].FindControl("ZDR")).Text;
                        dt.Rows[i]["制单时间"] = ((Label)gdZDDJLB.Rows[i].FindControl("ZDSJ")).Text;
                        dt.Rows[i]["备注"] = ((Label)gdZDDJLB.Rows[i].FindControl("BZ")).Text;
                    }

                    dt.Rows.Add();
                    dt.Rows[dt.Rows.Count - 1][0] = "合计：";
                    dt.Rows[dt.Rows.Count - 1][9] = numbers[0].ToString();
                    dt.Rows[dt.Rows.Count - 1][10] = numbers[1].ToString();
                    dt.Rows[dt.Rows.Count - 1][11] = numbers[2].ToString();
                    dt.Rows[dt.Rows.Count - 1][12] = numbers[3].ToString();
                    NpoiExcel(dt, "接待审核数据");
                }
                else
                {
                    this.Page.RegisterStartupScript("onclick", "<script>alert('无导出数据!')</script>");
                }
            }
        }

        protected void lbtnJDDJLB_Click(object sender, EventArgs e)
        {
            lbtnJDDJ.BackColor = System.Drawing.Color.White;
            lbtnJDDJLB.BackColor = System.Drawing.Color.DarkGray;
            divJDDJ.Visible = false;
            divJDDJLB.Visible = true;
            lbISLB.Text = "TRUE";
        }

        protected void lbtnJDDJ_Click(object sender, EventArgs e)
        {
            lbtnJDDJ.BackColor = System.Drawing.Color.DarkGray;
            lbtnJDDJLB.BackColor = System.Drawing.Color.White;
            divJDDJ.Visible = true;
            divJDDJLB.Visible = false;
            lbISLB.Text = "FALSE";
        }

        protected void gdZDDJ_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow) //判断是否是DataRow，以防止鼠标经过Header也有效果   
            {
                e.Row.Attributes.Add("onmouseover", "e=this.style.backgroundColor; this.style.backgroundColor='#CCCCCC'");
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=e");
            }
        }

        protected void cbLBXQRQ_CheckedChanged(object sender, EventArgs e)
        {
            if (cbLBXQRQ.Checked == true)
            {
                txtLBXQRQS.Enabled = true;
                txtLBXQRQE.Enabled = true;
            }
            else
            {
                txtLBXQRQS.Enabled = false;
                txtLBXQRQE.Enabled = false;
            }
        }

        protected void gdZDDJLB_RowDataBound(object sender, GridViewRowEventArgs e)
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
            Label ZDID = ((Label)gdZDDJLB.Rows[drv.RowIndex].FindControl("ZDID"));
            CheckBox YQR = ((CheckBox)gdZDDJLB.Rows[drv.RowIndex].FindControl("YQR"));
            CheckBox YJS = ((CheckBox)gdZDDJLB.Rows[drv.RowIndex].FindControl("YJS"));
            lbRI.Text = ZDID.Text;
            lbtnJDDJ.BackColor = System.Drawing.Color.DarkGray;
            lbtnJDDJLB.BackColor = System.Drawing.Color.White;
            divJDDJ.Visible = true;
            divJDDJLB.Visible = false;
            lbISLB.Text = "FALSE";
            BAND();
        }

        private void LOGINTIMEOUT()
        {
            if (Session["STAFFID"] == null)
            {
                Response.Redirect("../login.aspx?RI=1");
            }
        }

        private void BANDddlLBTZBM()
        {
            DataTable dt = zdService.SELECT_CRM_DEPT();
            ddlLBTZBM.DataSource = dt;
            ddlLBTZBM.DataTextField = "DEPTNAME";
            ddlLBTZBM.DataValueField = "DEPTID";
            ddlLBTZBM.DataBind();
            ListItem item = new ListItem();
            item.Text = "=请选择=";
            item.Value = "0";
            ddlLBTZBM.Items.Insert(0, item);
        }

        private void BANDddlLBLXR()
        {
            DataTable dt = zdService.SELECT_CRM_ZDLXR();
            ddlLBLXR.DataSource = dt;
            ddlLBLXR.DataTextField = "LXR";
            ddlLBLXR.DataValueField = "LXRID";
            ddlLBLXR.DataBind();
            ListItem item = new ListItem();
            item.Text = "=请选择=";
            item.Value = "0";
            ddlLBLXR.Items.Insert(0, item);
        }

        private void BANDddlLBDD()
        {
            DataTable dt = zdService.SELECT_CRM_ZDADD();
            ddlLBDD.DataSource = dt;
            ddlLBDD.DataTextField = "ADDNAME";
            ddlLBDD.DataValueField = "ADDID";
            ddlLBDD.DataBind();
            ListItem item = new ListItem();
            item.Text = "=请选择=";
            item.Value = "0";
            ddlLBDD.Items.Insert(0, item);
        }

        private void BANDddlLBLX()
        {
            DataTable dt = zdService.SELECT_QX(Session["STAFFID"].ToString());
            ddlLBLX.DataSource = dt;
            ddlLBLX.DataTextField = "XMTYPE";
            ddlLBLX.DataValueField = "XMTPYEID";
            ddlLBLX.DataBind();
            ListItem item = new ListItem();
            item.Text = "=请选择=";
            item.Value = "0";
            ddlLBLX.Items.Insert(0, item);
        }

        private void BANDLB()
        {
            string TZBM = "";
            if (ddlLBTZBM.SelectedValue != "0")
            {
                TZBM = ddlLBTZBM.SelectedItem.Text;
            }
            string LXR = "";
            if (ddlLBLXR.SelectedValue != "0")
            {
                LXR = ddlLBLXR.SelectedItem.Text;
            }
            string XQRQS = "";
            string XQRQE = "";
            if (cbLBXQRQ.Checked == true)
            {
                XQRQS = txtLBXQRQS.Text;
                XQRQE = txtLBXQRQE.Text;
            }
            string DD = "";
            if (ddlLBDD.SelectedValue != "0")
            {
                DD = ddlLBDD.SelectedItem.Text;
            }
            DataTable dt = zdService.SELECT_ZDXM1(Session["STAFFID"].ToString(), rbLB.SelectedValue, TZBM, LXR, txtLBBZ.Text, XQRQS, XQRQE, DD, ddlLBLX.SelectedValue, txtLBXQSM.Text);
            dt.Columns.Add("XMDATE1", typeof(string));
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dt.Rows[i]["XMDATE1"] = Convert.ToDateTime(dt.Rows[i]["XMDATE"].ToString()).ToString("yyyy-MM-dd");
                }
            }
            gdZDDJLB.DataSource = dt;
            gdZDDJLB.DataBind();
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    CheckBox YQR = (CheckBox)gdZDDJLB.Rows[i].FindControl("YQR");
                    YQR.Checked = Convert.ToBoolean(dt.Rows[i]["ISCONFIG"].ToString());
                    CheckBox YJS = (CheckBox)gdZDDJLB.Rows[i].FindControl("YJS");
                    YJS.Checked = Convert.ToBoolean(dt.Rows[i]["ISEND"].ToString());
                }
            }
        }

        private void BAND()
        {
            DataTable dtZD = zdService.SELECT_CRM_ZD(lbRI.Text);
            if (dtZD.Rows.Count > 0)
            {
                txtTZBM.Text = dtZD.Rows[0]["DEPTNAME"].ToString();
                txtLXR.Text = dtZD.Rows[0]["STAFFNAME"].ToString();
                txtLXDH.Text = dtZD.Rows[0]["STAFFMOBLE"].ToString();
                txtBZ.Text = dtZD.Rows[0]["ZDMEMO"].ToString();
                txtZDR.Text = dtZD.Rows[0]["fillName"].ToString();
                txtZDSJ.Text = dtZD.Rows[0]["fillTime"].ToString();
            }
            zdService.UPDATE_CRM_CRM_ZDXM_QR("1", lbRI.Text, Session["STAFFID"].ToString());
            DataTable dtZDXM = zdService.SELECT_CRM_ZDXM_QX(lbRI.Text, Session["STAFFID"].ToString());
            dtZDXM.Columns.Add("XMDATE1", typeof(string));
            if (dtZDXM.Rows.Count > 0)
            {
                for (int i = 0; i < dtZDXM.Rows.Count; i++)
                {
                    dtZDXM.Rows[i]["XMDATE1"] = Convert.ToDateTime(dtZDXM.Rows[i]["XMDATE"].ToString()).ToString("yyyy-MM-dd");
                }
            }
            gdZDDJ.DataSource = dtZDXM;
            gdZDDJ.DataBind();
            for (int i = 0; i < dtZDXM.Rows.Count; i++)
            {
                CheckBox cbYQR = (CheckBox)gdZDDJ.Rows[i].FindControl("cbYQR");
                cbYQR.Checked = Convert.ToBoolean(dtZDXM.Rows[i]["ISCONFIG"]);
                CheckBox cbYJS = (CheckBox)gdZDDJ.Rows[i].FindControl("cbYJS");
                cbYJS.Checked = Convert.ToBoolean(dtZDXM.Rows[i]["ISEND"]);
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

            int s1 = 0;
            int s2 = 0;
            int s3 = 0;
            int s4 = 0;
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
                    if (j != 9 && j != 10 && j != 11 && j != 12)
                    {
                        string DgvValue = dt.Rows[I][j].ToString();
                        row2.CreateCell(j).SetCellValue(DgvValue);
                        sheet.SetColumnWidth(j, 20 * 150);
                    }
                    else
                    {
                        if (dt.Rows[I][j].ToString() != "")
                        {
                            int DgvValue = Convert.ToInt32(dt.Rows[I][j].ToString());
                            row2.CreateCell(j).SetCellValue(DgvValue);
                            //if (j == 9)
                            //{
                            //    s1 = s1 + DgvValue;
                            //}
                            //else if (j == 10)
                            //{
                            //    s2 = s2 + DgvValue;
                            //}
                            //else if (j == 11)
                            //{
                            //    s3 = s3 + DgvValue;
                            //}
                            //else if (j == 12)
                            //{
                            //    s4 = s4 + DgvValue;
                            //}
                        }
                        else
                        {
                            row2.CreateCell(j).SetCellValue("");
                        }
                        sheet.SetColumnWidth(j, 20 * 150);
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

    }
}