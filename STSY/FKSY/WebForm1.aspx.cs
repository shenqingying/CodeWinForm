using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace STSY.FKSY
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BAND();
            }
        }

        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {

        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }

        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {

        }

        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {

        }

        protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
        {

        }

        private void BAND()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("身份证号码", typeof(string));
            dt.Columns.Add("邮政编码", typeof(string));
            dt.Columns.Add("家庭住址", typeof(string));
            dt.Columns.Add("姓名", typeof(string));
            for (int i = 0; i < 100; i++)
            {
                DataRow dr = dt.NewRow();
                dr[0] = "123456";
                dr[1] = "123456";
                dr[2] = "123456";
                dr[3] = "123456";
                dt.Rows.Add(dr);
            }
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }
    }
}