using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace STSY.Index
{
    public partial class Index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["STAFFID"] == null)
                {
                    this.Page.RegisterStartupScript("onclick", "<script>alert('登录超时！')</script>");
                    Response.Redirect("../login.aspx");
                }
                if (Request.QueryString["RI"] != null)
                {
                    band();
                }
            }
        }
        private void band()
        {
            if (Request.QueryString["RI"].ToString() == "1")
            {
                this.Page.RegisterStartupScript("onclick", "<script>alert('对不起，您无该权限！')</script>");
            }
        }
    }
}