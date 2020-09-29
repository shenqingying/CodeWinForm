using Bussiness.STSY;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace STSY.RKGL
{
    public partial class RKTJ : System.Web.UI.Page
    {
        WLService wlService = new WLService();
        RKService rkService = new RKService();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BANDDDLLB();
                BAND();
            }
        }

        protected void btnCXWL_Click(object sender, EventArgs e)
        {
            BAND();
        }

        protected void btnQX_Click(object sender, EventArgs e)
        {
            Response.Redirect("RKDJ.aspx?RI=" + Request.QueryString["RI"]);
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

        protected void lbtnFIND_OnClick(object sender, EventArgs e)
        {
            LinkButton t = (LinkButton)sender;
            GridViewRow drv = (GridViewRow)t.NamingContainer;
            int rowIndex = drv.RowIndex;
            Label materialID = ((Label)gdRK.Rows[drv.RowIndex].FindControl("materialID"));
            Label mtUnit = ((Label)gdRK.Rows[drv.RowIndex].FindControl("mtUnit"));
            Label mtBuyPrice = ((Label)gdRK.Rows[drv.RowIndex].FindControl("mtBuyPrice"));
            rkService.INSERT_RUKYZMX(Request.QueryString["RI"], Request.QueryString["MXID"], materialID.Text, mtUnit.Text, "", mtBuyPrice.Text, "", "0", "");
            Response.Redirect("RKDJ.aspx?RI=" + Request.QueryString["RI"]);
        }

        private void BAND()
        {
            DataTable dt = wlService.SELECT_RUKWK(ddlLB.SelectedValue, txtWLBH.Text, txtWLMC.Text, txtWLTM.Text, txtWLGG.Text);
            gdRK.DataSource = dt;
            gdRK.DataBind();
        }

        protected void gdRKDLB_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow) //判断是否是DataRow，以防止鼠标经过Header也有效果   
            {
                e.Row.Attributes.Add("onmouseover", "e=this.style.backgroundColor; this.style.backgroundColor='#CCCCCC'");
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=e");
            }
        }

        protected void gdRK_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gdRK.PageIndex = e.NewPageIndex;
            BAND();
        }
    }
}