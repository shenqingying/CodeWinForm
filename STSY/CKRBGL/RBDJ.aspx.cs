using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bussiness.STSY;
using System.Data;

namespace STSY.CKRBGL
{
    public partial class RBDJ : System.Web.UI.Page
    {
        CKRBService ckrbService = new CKRBService();
        CKService ckService = new CKService();
        Mainservice mainService = new Mainservice();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LOGINTIMEOUT();
                STAFFQX();
                txtYEAR.Text = DateTime.Now.Year.ToString();
                txtMOUTH.Text = DateTime.Now.Month.ToString();
                BANDTAIT();
                BANDDDLCK();
                CLEAR();
                BANDBOTTON("1,2");
                ISEnabled(false);
            }
        }
        private void STAFFQX()
        {
            if (Session["STAFFID"] == null)
            {
                Response.Redirect("../login.aspx?RI=1");
            }
            bool s = mainService.SELECT_HG_RIGHT(Session["STAFFID"].ToString(), "226");
            if (s == false)
            {
                Response.Redirect("../Index/Index.aspx?RI=1");
            }
        }
        protected void gdRBDJ_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow) //判断是否是DataRow，以防止鼠标经过Header也有效果   
            {
                e.Row.Attributes.Add("onmouseover", "e=this.style.backgroundColor; this.style.backgroundColor='#CCCCCC'");
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=e");
            }
        }

        protected void btnSX_Click(object sender, EventArgs e)
        {
            BANDTAIT();
            CLEAR();
            lbRI.Text = "0";
            BANDBOTTON("1,2");
            ISEnabled(false);
        }

        protected void btnXZJL_Click(object sender, EventArgs e)
        {
            lbRI.Text = "0";
            CLEAR();
            ISEnabled(true);
            BANDBOTTON("1,5,6");
        }

        protected void btnSCJL_Click(object sender, EventArgs e)
        {
            ckrbService.DELETE_MSDayUseItem(lbRI.Text);
            ckrbService.DELETE_MSDayUse(lbRI.Text);
            lbRI.Text = "0";
            BANDTAIT();
            CLEAR();
            ISEnabled(false);
            BANDBOTTON("1,2");
            this.Page.RegisterStartupScript("onclick", "<script>alert('删除成功!')</script>");
        }

        protected void btnXG_Click(object sender, EventArgs e)
        {
            ISEnabled(true);
            BANDBOTTON("1,5,6");
        }

        protected void btnSAVE_Click(object sender, EventArgs e)
        {
            if (lbRI.Text == "0")
            {
                DataTable dt = ckrbService.SELECT_MSDayUse_COUNT(txtFSRQ.Text.Trim(), lbRI.Text, ddlCKBH.SelectedValue);
                if (dt.Rows.Count > 0)
                {
                    this.Page.RegisterStartupScript("onclick", "<script>alert('系统当中已存在当天记录，无法重复保存!')</script>");
                    return;
                }
                string s = ckrbService.insert_MSDayUse(ddlCKBH.SelectedValue, txtFSRQ.Text, txtSM.Text, "1", "0", Session["STAFFID"].ToString(), txtTXR.Text, txtTXSJ.Text);
                for (int i = 0; i < gdRBXM.Rows.Count; i++)
                {
                    Label ItemID = (Label)gdRBXM.Rows[i].FindControl("ItemID");
                    Label ItemName = (Label)gdRBXM.Rows[i].FindControl("ItemName");
                    TextBox userNumber = (TextBox)gdRBXM.Rows[i].FindControl("userNumber");
                    ckrbService.insert_MSDayUseItem(s, ItemID.Text, ItemName.Text, userNumber.Text);
                }
                this.Page.RegisterStartupScript("onclick", "<script>alert('保存成功!')</script>");
                ISEnabled(false);
                lbRI.Text = s;
                BANDBOTTON("1,2,3,4");
                BANDTAIT();
            }
            else
            {
                DataTable dt = ckrbService.SELECT_MSDayUse_COUNT(txtFSRQ.Text.Trim(), lbRI.Text, ddlCKBH.SelectedValue);
                if (dt.Rows.Count > 0)
                {
                    this.Page.RegisterStartupScript("onclick", "<script>alert('系统当中已存在当天记录，无法重复保存!')</script>");
                    return;
                }
                ckrbService.DELETE_MSDayUseItem(lbRI.Text);
                ckrbService.DELETE_MSDayUse(lbRI.Text);
                string s = ckrbService.insert_MSDayUse(ddlCKBH.SelectedValue, txtFSRQ.Text, txtSM.Text, "1", "0", Session["STAFFID"].ToString(), txtTXR.Text, txtTXSJ.Text);
                for (int i = 0; i < gdRBXM.Rows.Count; i++)
                {
                    Label ItemID = (Label)gdRBXM.Rows[i].FindControl("ItemID");
                    Label ItemName = (Label)gdRBXM.Rows[i].FindControl("ItemName");
                    TextBox userNumber = (TextBox)gdRBXM.Rows[i].FindControl("userNumber");
                    ckrbService.insert_MSDayUseItem(s, ItemID.Text, ItemName.Text, userNumber.Text);
                }
                this.Page.RegisterStartupScript("onclick", "<script>alert('修改成功!')</script>");
                ISEnabled(false);
                lbRI.Text = s;
                BANDBOTTON("1,2,3,4");
                BANDTAIT();
            }
        }

        protected void btnQX_Click(object sender, EventArgs e)
        {
            if (lbRI.Text == "0")
            {
                ISEnabled(false);
                CLEAR();
                BANDBOTTON("1,2");
            }
            else
            {
                ISEnabled(false);
                CLEAR();
                BANDMX();
                BANDBOTTON("1,2,3,4");
            }
        }


        protected void lbtnFIND_OnClick(object sender, EventArgs e)
        {
            LinkButton t = (LinkButton)sender;
            GridViewRow drv = (GridViewRow)t.NamingContainer;
            int rowIndex = drv.RowIndex;
            Label DayUseID = ((Label)gdRBDJ.Rows[drv.RowIndex].FindControl("DayUseID"));
            lbRI.Text = DayUseID.Text;
            BANDMX();
            for (int i = 0; i < gdRBDJ.Rows.Count; i++)
            {
                if (i != rowIndex)
                {
                    gdRBDJ.Rows[i].BackColor = System.Drawing.Color.Empty;
                }
                else
                {
                    gdRBDJ.Rows[i].BackColor = System.Drawing.Color.DarkGray;
                }
            }
            BANDBOTTON("1,2,3,4");
            ISEnabled(false);
        }

        private void BANDTAIT()
        {
            DateTime dtime = new DateTime(Convert.ToInt32(txtYEAR.Text), Convert.ToInt32(txtMOUTH.Text), 1);
            DataTable dt = ckrbService.SELECT_MSDayUse(dtime.ToString("yyyy-MM-dd"), dtime.AddMonths(1).ToString("yyyy-MM-dd"));
            dt.Columns.Add("XH", typeof(string));
            dt.Columns.Add("UseDate1", typeof(string));
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dt.Rows[i]["XH"] = (i + 1).ToString();
                    dt.Rows[i]["UseDate1"] = Convert.ToDateTime(dt.Rows[i]["UseDate"].ToString()).ToString("yyyy-MM-dd");
                }
                Label10.Text = "";
            }
            else
            {
                Label10.Text = "没有数据！";
            }
            gdRBDJ.DataSource = dt;
            gdRBDJ.DataBind();
        }

        private void BANDITEM()
        {
            DataTable dt = ckrbService.SELECT_MSRptItem();
            dt.Columns.Add("userNumber", typeof(string));
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dt.Rows[i]["userNumber"] = "";
            }
            gdRBXM.DataSource = dt;
            gdRBXM.DataBind();
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

        protected void gdRBXM_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow) //判断是否是DataRow，以防止鼠标经过Header也有效果   
            {
                e.Row.Attributes.Add("onmouseover", "e=this.style.backgroundColor; this.style.backgroundColor='#CCCCCC'");
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=e");
            }
        }

        private void BANDMX()
        {
            DataTable dt = ckrbService.SELECT_MSDayUse(lbRI.Text);
            if (dt.Rows.Count > 0)
            {
                txtFSRQ.Text = Convert.ToDateTime(dt.Rows[0]["UseDate"].ToString()).ToString("yyyy-MM-dd");
                txtTXR.Text = dt.Rows[0]["fillName"].ToString();
                txtTXSJ.Text = Convert.ToDateTime(dt.Rows[0]["fillTime"].ToString()).ToString("yyyy-MM-dd HH:mm:ss");
                txtSM.Text = dt.Rows[0]["UseMem"].ToString();
            }
            DataTable dtMX = ckrbService.SELECT_MSDayUseItem(lbRI.Text);
            gdRBXM.DataSource = dtMX;
            gdRBXM.DataBind();
        }

        private void CLEAR()
        {
            txtTXR.Text = Session["STAFFNAME"].ToString();
            txtTXSJ.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            txtSM.Text = "";
            BANDITEM();
        }

        private void LOGINTIMEOUT()
        {
            if (Session["STAFFID"] == null)
            {
                Response.Redirect("../login.aspx?RI=1");
            }
        }

        private void BANDBOTTON(string str)
        {
            string[] str1 = str.Split(',');
            btnSX.Enabled = false;
            btnSX.BackColor = System.Drawing.Color.Gray;
            btnXZJL.Enabled = false;
            btnXZJL.BackColor = System.Drawing.Color.Gray;
            btnSCJL.Enabled = false;
            btnSCJL.BackColor = System.Drawing.Color.Gray;
            btnXG.Enabled = false;
            btnXG.BackColor = System.Drawing.Color.Gray;
            btnSAVE.Enabled = false;
            btnSAVE.BackColor = System.Drawing.Color.Gray;
            btnQX.Enabled = false;
            btnQX.BackColor = System.Drawing.Color.Gray;
            if (str1.Length > 0)
            {
                for (int i = 0; i < str1.Length; i++)
                {
                    if (str1[i] == "1")
                    {
                        btnSX.Enabled = true;
                        btnSX.BackColor = System.Drawing.Color.FromName("#3498db");
                    }
                    else if (str1[i] == "2")
                    {
                        btnXZJL.Enabled = true;
                        btnXZJL.BackColor = System.Drawing.Color.FromName("#3498db");
                    }
                    else if (str1[i] == "3")
                    {
                        btnSCJL.Enabled = true;
                        btnSCJL.BackColor = System.Drawing.Color.FromName("#3498db");
                    }
                    else if (str1[i] == "4")
                    {
                        btnXG.Enabled = true;
                        btnXG.BackColor = System.Drawing.Color.FromName("#3498db");
                    }
                    else if (str1[i] == "5")
                    {
                        btnSAVE.Enabled = true;
                        btnSAVE.BackColor = System.Drawing.Color.FromName("#3498db");
                    }
                    else if (str1[i] == "6")
                    {
                        btnQX.Enabled = true;
                        btnQX.BackColor = System.Drawing.Color.FromName("#3498db");
                    }
                }
            }
        }

        private void ISEnabled(bool s)
        {
            ddlCKBH.Enabled = s;
            txtFSRQ.Enabled = s;
            txtTXR.Enabled = s;
            txtTXSJ.Enabled = s;
            txtSM.Enabled = s;
            gdRBXM.Enabled = s;
        }
    }
}