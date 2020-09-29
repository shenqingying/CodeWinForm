using STSY.Modles;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace STSY.Index
{
    public partial class ChangePSW : System.Web.UI.Page
    {
        PUBLICModels publicmodels = new PUBLICModels();
        protected void Page_Load(object sender, EventArgs e)
        {
            string url = Request.Url.ToString();
            string SSOURL = "";
            if (url.IndexOf("192.168") != -1 || url.IndexOf("localhost") != -1)
            {
                SSOURL = ConfigurationManager.AppSettings["SSOURLIN"];
            }
            else
            {
                SSOURL = ConfigurationManager.AppSettings["SSOURLOUT"];
            }
            url = publicmodels.PUBLICFUN.get_my_jm(url);


            //url = url + "&LOGINTYPE=1";
            SSOURL = SSOURL + "/SSO/Public/password";
            //Response.Redirect(SSOURL + "?URL=" + url);
            Response.Write("<script>window.parent.location.href='" + SSOURL + "?URL=" + url + "';</script>");
            return;
        }
    }
}