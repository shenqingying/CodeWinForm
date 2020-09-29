using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bussiness.STSY;

namespace STSY.CKGL
{
    public partial class KHGYS : System.Web.UI.Page
    {
        CKService ckService = new CKService();
        Mainservice mainService = new Mainservice();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LOGINTIMEOUT();
                STAFFQX();
                BAND();
                BANDBOTTON("1,2");
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
            bool s = mainService.SELECT_HG_RIGHT(Session["STAFFID"].ToString(), "291");
            if (s == false)
            {
                Response.Redirect("../Index/Index.aspx?RI=1");
            }
        }
        protected void btnSX_Click(object sender, EventArgs e)
        {
            BAND();
            CLEAR();
            ISEnabled(false);
            lbRI.Text = "0";
        }

        protected void btnXZJL_Click(object sender, EventArgs e)
        {
            CLEAR();
            ISEnabled(true);
            lbRI.Text = "0";
            BANDBOTTON("1,5,6");
        }

        protected void btnSCJL_Click(object sender, EventArgs e)
        {
            CLEAR();
            ckService.DELETE_MSCLIENT(lbRI.Text);
            this.Page.RegisterStartupScript("onclick", "<script>alert('删除成功！')</script>");
            ISEnabled(false);
            lbRI.Text = "0";
            BANDBOTTON("1,2");
        }

        protected void btnXG_Click(object sender, EventArgs e)
        {
            ISEnabled(true);
            BANDBOTTON("1,5,6");
        }

        protected void btnBC_Click(object sender, EventArgs e)
        {
            if (txtBH.Text == "")
            {
                this.Page.RegisterStartupScript("onclick", "<script>alert('编号不能为空！')</script>");
                return;
            }
            if (txtMC.Text == "")
            {
                this.Page.RegisterStartupScript("onclick", "<script>alert('名称不能为空！')</script>");
                return;
            }
            if (ckService.SELECT_MSClient_COUNT(txtBH.Text, lbRI.Text).Rows.Count > 0)
            {
                this.Page.RegisterStartupScript("onclick", "<script>alert('该编号已存在！')</script>");
                return;
            }
            if (lbRI.Text == "0")
            {
                lbRI.Text = ckService.insert_MSCLIENT(txtBH.Text, txtMC.Text, txtLXR.Text, txtDZ.Text, txtYB.Text, txtDH.Text, txtCZ.Text, txtEMAIL.Text, txtBZ.Text, cbGYS.Checked.ToString(), cbKH.Checked.ToString(), "0", txtKFYH.Text, txtZH.Text);
                this.Page.RegisterStartupScript("onclick", "<script>alert('新增成功！')</script>");
                ISEnabled(false);
                BANDBOTTON("1,2,3,4");
            }
            else
            {
                ckService.UPDATE_MSCLIENT(txtBH.Text, txtMC.Text, txtLXR.Text, txtDZ.Text, txtYB.Text, txtDH.Text, txtCZ.Text, txtEMAIL.Text, txtBZ.Text, cbGYS.Checked.ToString(), cbKH.Checked.ToString(), txtKFYH.Text, txtZH.Text, lbRI.Text);
                this.Page.RegisterStartupScript("onclick", "<script>alert('修改成功！')</script>");
                ISEnabled(false);
                BANDBOTTON("1,2,3,4");
            }
        }

        protected void btnQX_Click(object sender, EventArgs e)
        {
            BANDXX();
        }

        protected void gdKHGYS_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow) //判断是否是DataRow，以防止鼠标经过Header也有效果   
            {
                e.Row.Attributes.Add("onmouseover", "e=this.style.backgroundColor; this.style.backgroundColor='#CCCCCC'");
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=e");
            }
        }

        private void BAND()
        {
            DataTable dt = ckService.SELECT_MSClient("0");
            gdKHGYS.DataSource = dt;
            gdKHGYS.DataBind();
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    CheckBox cbKH = (CheckBox)gdKHGYS.Rows[i].FindControl("cbKH");
                    cbKH.Checked = Convert.ToBoolean(dt.Rows[i]["isSale"].ToString());
                    CheckBox cbGYS = (CheckBox)gdKHGYS.Rows[i].FindControl("cbGYS");
                    cbGYS.Checked = Convert.ToBoolean(dt.Rows[i]["isBuy"].ToString());
                }
            }
        }

        protected void lbtnFIND_OnClick(object sender, EventArgs e)
        {
            LinkButton t = (LinkButton)sender;
            GridViewRow drv = (GridViewRow)t.NamingContainer;
            int rowIndex = drv.RowIndex;
            Label ClientID = ((Label)gdKHGYS.Rows[drv.RowIndex].FindControl("ClientID"));
            lbRI.Text = ClientID.Text;
            CLEAR();
            BANDXX();
            for (int i = 0; i < gdKHGYS.Rows.Count; i++)
            {
                if (i != rowIndex)
                {
                    gdKHGYS.Rows[i].BackColor = System.Drawing.Color.Empty;
                }
                else
                {
                    gdKHGYS.Rows[i].BackColor = System.Drawing.Color.DarkGray;
                }
            }
        }

        private void BANDXX()
        {
            if (lbRI.Text != "0")
            {
                DataTable dt = ckService.SELECT_MSClient_ID(lbRI.Text);
                if (dt.Rows.Count > 0)
                {
                    txtBH.Text = dt.Rows[0]["ClSN"].ToString();
                    txtMC.Text = dt.Rows[0]["ClName"].ToString();
                    txtKFYH.Text = dt.Rows[0]["CBANK"].ToString();
                    txtZH.Text = dt.Rows[0]["CBANKNO"].ToString();
                    txtLXR.Text = dt.Rows[0]["ClMan"].ToString();
                    txtDZ.Text = dt.Rows[0]["ClAddress"].ToString();
                    txtYB.Text = dt.Rows[0]["ClPost"].ToString();
                    txtDH.Text = dt.Rows[0]["ClTel"].ToString();
                    txtCZ.Text = dt.Rows[0]["ClFax"].ToString();
                    txtEMAIL.Text = dt.Rows[0]["ClEMail"].ToString();
                    txtBZ.Text = dt.Rows[0]["ClMemo"].ToString();
                    cbGYS.Checked = Convert.ToBoolean(dt.Rows[0]["isBuy"].ToString());
                    cbKH.Checked = Convert.ToBoolean(dt.Rows[0]["isSale"].ToString());
                }
                BANDBOTTON("1,2,3,4");
            }
            else
            {
                CLEAR();
                BANDBOTTON("1,2");
            }
            ISEnabled(false);
        }

        private void CLEAR()
        {
            txtBH.Text = "";
            txtMC.Text = "";
            txtKFYH.Text = "";
            txtZH.Text = "";
            txtLXR.Text = "";
            txtDZ.Text = "";
            txtYB.Text = "";
            txtDH.Text = "";
            txtCZ.Text = "";
            txtEMAIL.Text = "";
            txtBZ.Text = "";
            cbGYS.Checked = false;
            cbKH.Checked = false;
        }


        private void ISEnabled(bool s)
        {
            txtBH.Enabled = s;
            txtMC.Enabled = s;
            txtKFYH.Enabled = s;
            txtZH.Enabled = s;
            txtLXR.Enabled = s;
            txtDZ.Enabled = s;
            txtYB.Enabled = s;
            txtDH.Enabled = s;
            txtCZ.Enabled = s;
            txtEMAIL.Enabled = s;
            txtBZ.Enabled = s;
            cbGYS.Enabled = s;
            cbKH.Enabled = s;
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
            btnBC.Enabled = false;
            btnBC.BackColor = System.Drawing.Color.Gray;
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
                        btnBC.Enabled = true;
                        btnBC.BackColor = System.Drawing.Color.FromName("#3498db");
                    }
                    else if (str1[i] == "6")
                    {
                        btnQX.Enabled = true;
                        btnQX.BackColor = System.Drawing.Color.FromName("#3498db");
                    }
                }
            }
        }
    }
}