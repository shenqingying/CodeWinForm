using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bussiness.STSY;
using System.Data;
using System.Configuration;
using Bussiness.Modles;
using System.Text;
using Bussiness.SSO.TOKEN_TOKENIDINFOService;
using Bussiness.SSO.UserTokenService;
using Bussiness.CRM.CRM_LoginService;
using STSY.Modles;

namespace STSY
{
    public partial class login : System.Web.UI.Page
    {
        Mainservice mainService = new Mainservice();
        SSOModels ssomodels = new SSOModels();
        PUBLICModels publicmodels = new PUBLICModels();
        CRMModels crmModels = new CRMModels();
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
            string LOGINTYPE = Request.QueryString["LOGINTYPE"];
            if (string.IsNullOrEmpty(LOGINTYPE))
            {
                LOGINTYPE = "";
            }
            if (LOGINTYPE == "1")
            {
                Response.Cookies["TokenID"].Value = null;
                url = url + "&LOGINTYPE=1";
                //Response.Redirect(SSOURL + "?URL=" + url);
                Response.Write("<script>window.parent.location.href='" + SSOURL + "?URL=" + url + "';</script>");
                return;
            }






            string TOKENID = Request.QueryString["TOKENID"];
            if (string.IsNullOrEmpty(TOKENID))
            {
                TOKENID = "";
            }
            if (TOKENID != "")
            {
                //第二次运行时执行
                MES_RETURN_UI rst_MES_RETURN_UI = ssomodels.TOKEN_TOKENIDINFO.SELECT(TOKENID);
                if (rst_MES_RETURN_UI.TYPE == "S")
                {
                    Response.Cookies["token"].Value = rst_MES_RETURN_UI.MESSAGE;
                    Response.Cookies["token"].Expires = DateTime.Now.AddDays(10);
                    CRM_LoginInfo tokeninfo = crmModels.CRM_Login.Login_SSO_TOKEN(TOKENID, 1, 0);
                    if (tokeninfo.TokenInfo.access_token != "")
                    {
                        if (Session["token"] == null)
                        {
                            Session["MYPW"] = null;
                        }
                        getlogininfo(tokeninfo, SSOURL, url);
                        Response.Cookies["TokenID"].Value = TOKENID;
                        Response.Redirect("Index/Index.aspx");
                    }
                    else
                    {
                        Response.Redirect(SSOURL + "?URL=" + url);
                    }
                }
                else
                {
                    Response.Redirect(SSOURL + "?URL=" + url);
                }
            }
            else
            {
                //第一次执行时运行
                if (Request.Cookies["TokenID"] == null)
                {
                    Response.Redirect(SSOURL + "?URL=" + url);
                }
                else
                {
                    if (string.IsNullOrEmpty(Request.Cookies["TokenID"].Value))
                    {
                        Response.Redirect(SSOURL + "?URL=" + url);
                    }
                    else
                    {
                        MES_RETURN_UI rst_MES_RETURN_UI = ssomodels.TOKEN_TOKENIDINFO.SELECT(Request.Cookies["TokenID"].Value);
                        if (rst_MES_RETURN_UI.TYPE == "S")
                        {
                            Response.Cookies["token"].Value = rst_MES_RETURN_UI.MESSAGE;
                            Response.Cookies["token"].Expires = DateTime.Now.AddDays(10);
                            CRM_LoginInfo tokeninfo = crmModels.CRM_Login.Login_SSO_TOKEN(Request.Cookies["TokenID"].Value, 1, 0);
                            if (tokeninfo.TokenInfo.access_token != "")
                            {
                                if (Session["token"] == null)
                                {
                                    Session["MYPW"] = null;
                                }
                                getlogininfo(tokeninfo, SSOURL, url);
                                Response.Redirect("Index/Index.aspx");
                            }
                            else
                            {
                                Response.Redirect(SSOURL + "?URL=" + url);
                            }
                        }
                        else
                        {
                            Response.Redirect(SSOURL + "?URL=" + url);
                        }
                    }
                }
            }



            if (!IsPostBack)
            {
                if (Request.QueryString["RI"] != null)
                {
                    band();
                }
            }
        }

        public void getlogininfo(CRM_LoginInfo tokeninfo, string SSOURL, string url)
        {
            //Session["MENUINFO"] = tokeninfo.JURISDICTION_GROUP;
            Session["token"] = tokeninfo.TokenInfo.access_token;
            //Session["STAFFID"] = tokeninfo.TokenInfo.STAFFID;
            //Session["NAME"] = crmModels.HG_STAFF.ReadBySTAFFID(tokeninfo.TokenInfo.STAFFID, tokeninfo.TokenInfo.access_token).STAFFNAME;
            //Response.Cookies["token"].Value = tokeninfo.TokenInfo.access_token;
            //Response.Cookies["token"].Expires = DateTime.Now.AddDays(10);

            SSO_TOKEN_USERNAMEDY cxmodel = new SSO_TOKEN_USERNAMEDY();
            cxmodel.STAFFID = tokeninfo.TokenInfo.STAFFID;
            cxmodel.ZHLB = 6;
            SSO_TOKEN_USERNAMEDY_SELECT data = ssomodels.TOKEN_TOKENIDINFO.USERNAMEDY_SELECT(cxmodel, tokeninfo.TokenInfo.access_token);
            if (data.MES_RETURN.TYPE == "S")
            {
                if (data.SSO_TOKEN_USERNAMEDY.Length > 0)
                {
                    DataTable dt = mainService.GETSTAFFID(data.SSO_TOKEN_USERNAMEDY[0].ZHUSERNAME);
                    Session["STAFFID"] = dt.Rows[0][0].ToString();
                    Session["STAFFNAME"] = dt.Rows[0][1].ToString();

                }
                else
                {
                    url = url + "&LOGINTYPE=2";
                    Response.Redirect(SSOURL + "?URL=" + url);
                }
            }
            else
            {
                url = url + "&LOGINTYPE=2";
                Response.Redirect(SSOURL + "?URL=" + url);
            }


        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            if (txtname.Text.Trim() == "" || txtpassword.Text.Trim() == "")
            {
                this.Page.RegisterStartupScript("onclick", "<script>alert('用户名或密码不能为空！')</script>");
                return;
            }
            bool ISLOHIN = mainService.LOGIN(txtname.Text.Trim(), txtpassword.Text.Trim());
            if (ISLOHIN == false)
            {
                this.Page.RegisterStartupScript("onclick", "<script>alert('用户名或密码错误！')</script>");
                return;
            }
            else
            {
                //Session["STAFFUSER"] = txtname.Text.Trim();
                DataTable dt = mainService.GETSTAFFID(txtname.Text.Trim());
                Session["STAFFID"] = dt.Rows[0][0].ToString();
                Session["STAFFNAME"] = dt.Rows[0][1].ToString();
                Response.Redirect("Index/Index.aspx");
            }
        }

        private void band()
        {
            if (Request.QueryString["RI"].ToString() == "1")
            {
                this.Page.RegisterStartupScript("onclick", "<script>alert('登录超时，请重新登录！')</script>");
            }
        }
    }
}