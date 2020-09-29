using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bussiness.STSY;

namespace STSY.JDGL
{
    public partial class JDSZ : System.Web.UI.Page
    {
        ZDService zdService = new ZDService();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BAND();
                BANDBOTTON("1,2");
            }
        }

        protected void btnQX_Click(object sender, EventArgs e)
        {
            BANDCB();
            BANDBOTTON("1,2");
        }

        protected void btnSX_Click(object sender, EventArgs e)
        {
            BAND();
            ISSY(false);
            cbGD.Checked = false;
            cbKF.Checked = false;
            cbSG.Checked = false;
            BANDBOTTON("1,2");
        }

        protected void btnSAVE_Click(object sender, EventArgs e)
        {
            zdService.DELETE_CRM_ZDRIGHT(ddlRY.SelectedValue);
            if (cbKF.Checked == true)
            {
                zdService.INSERT_CRM_ZDRIGHT(ddlRY.SelectedValue, "1");
            }
            if (cbSG.Checked == true)
            {
                zdService.INSERT_CRM_ZDRIGHT(ddlRY.SelectedValue, "2");
            }
            if (cbGD.Checked == true)
            {
                zdService.INSERT_CRM_ZDRIGHT(ddlRY.SelectedValue, "3");
            }
            ISSY(false);
            BANDBOTTON("1,2");
        }

        private void BAND()
        {
            DataTable dt = zdService.SELECT_HG_RIGHT();
            dt.Columns.Add("NAME", typeof(string));
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dt.Rows[i]["NAME"] = dt.Rows[i]["STAFFUSER"].ToString() + "  " + dt.Rows[i]["STAFFNAME"].ToString();
                }
            }
            ddlRY.DataSource = dt;
            ddlRY.DataTextField = "NAME";
            ddlRY.DataValueField = "STAFFID";
            ddlRY.DataBind();
            ListItem item = new ListItem();
            item.Text = "=请选择=";
            item.Value = "0";
            ddlRY.Items.Insert(0, item);
        }

        protected void ddlRY_SelectedIndexChanged(object sender, EventArgs e)
        {
            BANDCB();
        }

        protected void btnXG_Click(object sender, EventArgs e)
        {
            ISSY(true);
            BANDBOTTON("1,3,4");
        }

        private void ISSY(bool S)
        {
            cbGD.Enabled = S;
            cbKF.Enabled = S;
            cbSG.Enabled = S;
        }

        private void BANDBOTTON(string str)
        {
            string[] str1 = str.Split(',');
            btnSX.Enabled = false;
            btnSX.BackColor = System.Drawing.Color.Gray;
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
                        btnXG.Enabled = true;
                        btnXG.BackColor = System.Drawing.Color.FromName("#3498db");
                    }
                    else if (str1[i] == "3")
                    {
                        btnSAVE.Enabled = true;
                        btnSAVE.BackColor = System.Drawing.Color.FromName("#3498db");
                    }
                    else if (str1[i] == "4")
                    {
                        btnQX.Enabled = true;
                        btnQX.BackColor = System.Drawing.Color.FromName("#3498db");
                    }
                }
            }
        }


        private void BANDCB()
        {
            if (ddlRY.SelectedValue != "0")
            {
                ISSY(false);
                cbGD.Checked = false;
                cbKF.Checked = false;
                cbSG.Checked = false;
                DataTable dt = zdService.SELECT_HG_RIGHT(ddlRY.SelectedValue);
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (dt.Rows[i]["XMTPYEID"].ToString() == "1")
                        {
                            cbKF.Checked = true;
                        }
                        if (dt.Rows[i]["XMTPYEID"].ToString() == "2")
                        {
                            cbSG.Checked = true;
                        }
                        if (dt.Rows[i]["XMTPYEID"].ToString() == "3")
                        {
                            cbGD.Checked = true;
                        }
                    }
                }
            }
            else
            {
                ISSY(false);
            }
        }
    }
}