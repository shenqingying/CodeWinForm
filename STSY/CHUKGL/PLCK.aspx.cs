using Bussiness.STSY;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace STSY.CHUKGL
{
    public partial class PLCK : System.Web.UI.Page
    {
        WLService wlService = new WLService();
        ChuKService chukService = new ChuKService();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BANDddlWLLB();
                BAND();
            }
        }

        protected void btnCXSX_Click(object sender, EventArgs e)
        {
            BAND();
        }

        protected void btnQRTJ_Click(object sender, EventArgs e)
        {
            if (gdKC.Rows.Count > 0)
            {
                for (int i = 0; i < gdKC.Rows.Count; i++)
                {
                    CheckBox cbXZ = ((CheckBox)gdKC.Rows[i].FindControl("cbXZ"));
                    if (cbXZ.Checked == true)
                    {
                        Label materialID = ((Label)gdKC.Rows[i].FindControl("materialID"));
                        Label mtSpec = ((Label)gdKC.Rows[i].FindControl("mtSpec"));
                        Label mtUnit = ((Label)gdKC.Rows[i].FindControl("mtUnit"));
                        Label mtNumber = ((Label)gdKC.Rows[i].FindControl("mtNumber"));
                        Label PJJ = ((Label)gdKC.Rows[i].FindControl("PJJ"));
                        Label mtTotal = ((Label)gdKC.Rows[i].FindControl("mtTotal"));
                        Label PlaceSN = ((Label)gdKC.Rows[i].FindControl("PlaceSN"));
                        Label PlaceName = ((Label)gdKC.Rows[i].FindControl("PlaceName"));
                        if (PlaceSN.Text == "")
                        {
                            PlaceSN.Text = "0";
                        }
                        chukService.insert_CHUKYZMX(Request.QueryString["RI"], materialID.Text, mtSpec.Text, mtUnit.Text, mtNumber.Text, PJJ.Text, mtTotal.Text, PlaceSN.Text, PlaceName.Text, "");
                    }
                }
            }
            Response.Redirect("WLCK.aspx?RI=" + Request.QueryString["RI"]);
        }

        protected void btnTC_Click(object sender, EventArgs e)
        {
            Response.Redirect("WLCK.aspx?RI=" + Request.QueryString["RI"]);
        }

        private void BANDddlWLLB()
        {
            DataTable dt = wlService.GETWLTYPEKC();
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dt.Rows[i]["typeSN"] = dt.Rows[i]["typeSN"].ToString() + "  " + dt.Rows[i]["typeName"].ToString();
                }
            }
            ddlWLLB.DataSource = dt;
            ddlWLLB.DataTextField = "typeSN";
            ddlWLLB.DataValueField = "typeID";
            ddlWLLB.DataBind();
            ListItem item = new ListItem();
            item.Text = "=请选择=";
            item.Value = "0";
            ddlWLLB.Items.Insert(0, item);
        }

        protected void ddlWLLB_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = wlService.GETWLTYPEKC(ddlWLLB.SelectedValue);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dt.Rows[i]["mtName"] = dt.Rows[i]["mtSN"].ToString() + "  " + dt.Rows[i]["mtName"].ToString();
                }
            }
            ddlWL.DataSource = dt;
            ddlWL.DataTextField = "mtName";
            ddlWL.DataValueField = "materialID";
            ddlWL.DataBind();
            ListItem item = new ListItem();
            item.Text = "=请选择=";
            item.Value = "0";
            ddlWL.Items.Insert(0, item);
        }

        private void BAND()
        {
            string WLLB = "0";
            string WL = "0";
            if (ddlWLLB.SelectedValue != "0")
            {
                WLLB = ddlWLLB.SelectedValue;
                WL = ddlWL.SelectedValue;
            }
            DataTable dt = wlService.SELECT_KC(WLLB, WL);
            gdKC.DataSource = dt;
            gdKC.DataBind();
        }

        protected void gdKC_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow) //判断是否是DataRow，以防止鼠标经过Header也有效果   
            {
                e.Row.Attributes.Add("onmouseover", "e=this.style.backgroundColor; this.style.backgroundColor='#CCCCCC'");
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=e");
            }
        }

        protected void btnALL_Click(object sender, EventArgs e)
        {
            if (gdKC.Rows.Count > 0)
            {
                for (int i = 0; i < gdKC.Rows.Count; i++)
                {
                    CheckBox cbXZ = ((CheckBox)gdKC.Rows[i].FindControl("cbXZ"));
                    if (lbQX.Text == "FALSE")
                    {
                        cbXZ.Checked = true;
                    }
                    else
                    {
                        cbXZ.Checked = false;
                    }
                }
                if (lbQX.Text == "FALSE")
                {
                    lbQX.Text = "TRUE";
                }
                else
                {
                    lbQX.Text = "FALSE";
                }
            }
        }
    }
}