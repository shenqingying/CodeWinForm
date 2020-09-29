using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bussiness.STSY;
using System.Data;
using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;
using System.IO;
using STSY.KQInfoService;

namespace STSY.Index
{
    public partial class RSTJ : System.Web.UI.Page
    {
        CKService ckService = new CKService();
        RKService rkService = new RKService();
        WLService wlService = new WLService();
        Mainservice mainService = new Mainservice();
        KQinfo kq = new KQinfo();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LOGINTIMEOUT();
                txtCXRQS.Text = DateTime.Now.ToString("yyyy-MM-dd");
                string date = txtCXRQS.Text;
                lbshangwu.Text = wufan(date);
                lbxiawu.Text = wanfan(date);
            }
        }

        private void LOGINTIMEOUT()
        {
            if (Session["STAFFID"] == null)
            {
                Response.Redirect("../login.aspx?RI=1");
            }
        }

      

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow) //判断是否是DataRow，以防止鼠标经过Header也有效果   
            {
                e.Row.Attributes.Add("onmouseover", "e=this.style.backgroundColor; this.style.backgroundColor='#BBFFFF'");
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=e");
            }
        }


        protected void btnCXSX_Click(object sender, EventArgs e)           //查询刷新
        {
            string date = txtCXRQS.Text;
            lbshangwu.Text = wufan(date);
            lbxiawu.Text = wanfan(date);
        }

      

        protected void gdRKDLB_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow) //判断是否是DataRow，以防止鼠标经过Header也有效果   
            {
                e.Row.Attributes.Add("onmouseover", "e=this.style.backgroundColor; this.style.backgroundColor='#CCCCCC'");
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=e");
            }
        }

        public string wufan(string d)
        {
            //int num = Convert.ToInt16(kq.getKQINFO(d).Count1);
            string num = kq.getKQINFO(d).Count1;
            return num;

        }


        public string wanfan(string d)
        {
            //int num = Convert.ToInt16(kq.getKQINFO(d).Count1) - Convert.ToInt16(kq.getKQINFO(d).Count2);
            string num = (Convert.ToInt16(kq.getKQINFO(d).Count2) - Convert.ToInt16(kq.getKQINFO(d).Count1)).ToString();
            return num;
        }

    }
}