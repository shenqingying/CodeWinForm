using Bussiness.STSY;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace STSY.JDGL
{
    public partial class JDRYGL : System.Web.UI.Page
    {
        ZDService zdService = new ZDService();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lbtnTZBM.BackColor = System.Drawing.Color.DarkGray;
                lbtnLXR.BackColor = System.Drawing.Color.White;
                BANDBM();
                BANDLXR();
                BANDDD();

                BMADD.Visible = false;
                LXRADD.Visible = false;
                divDDADD.Visible = false;

                divTZBM.Visible = true;
                divLXR.Visible = false;
                divDD.Visible = false;
                lbBMRI.Text = "0";
                lbLXRRI.Text = "0";
                lbDDRI.Text = "";
            }
        }


        protected void lbtnTZBM_Click(object sender, EventArgs e)
        {
            lbtnTZBM.BackColor = System.Drawing.Color.DarkGray;
            lbtnLXR.BackColor = System.Drawing.Color.White;
            lbtnDD.BackColor = System.Drawing.Color.White;
            divTZBM.Visible = true;
            divLXR.Visible = false;
            divDD.Visible = false;
        }
        protected void lbtnLXR_Click(object sender, EventArgs e)
        {
            lbtnTZBM.BackColor = System.Drawing.Color.White;
            lbtnLXR.BackColor = System.Drawing.Color.DarkGray;
            lbtnDD.BackColor = System.Drawing.Color.White;
            divTZBM.Visible = false;
            divLXR.Visible = true;
            divDD.Visible = false;
        }
        protected void lbtnDD_Click(object sender, EventArgs e)
        {
            lbtnTZBM.BackColor = System.Drawing.Color.White;
            lbtnLXR.BackColor = System.Drawing.Color.White;
            lbtnDD.BackColor = System.Drawing.Color.DarkGray;
            divTZBM.Visible = false;
            divLXR.Visible = false;
            divDD.Visible = true;
        }

        protected void GDBM_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow) //判断是否是DataRow，以防止鼠标经过Header也有效果   
            {
                e.Row.Attributes.Add("onmouseover", "e=this.style.backgroundColor; this.style.backgroundColor='#CCCCCC'");
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=e");
            }
        }

        protected void lbtnBMMODEFY_OnClick(object sender, EventArgs e)
        {
            LinkButton t = (LinkButton)sender;
            GridViewRow drv = (GridViewRow)t.NamingContainer;
            Label DEPTID = ((Label)GDBM.Rows[drv.RowIndex].FindControl("DEPTID"));
            Label DEPTNAME = ((Label)GDBM.Rows[drv.RowIndex].FindControl("DEPTNAME"));
            Label DEPTMEMO = ((Label)GDBM.Rows[drv.RowIndex].FindControl("DEPTMEMO"));
            lbBMRI.Text = DEPTID.Text;
            txtBMMC.Text = DEPTNAME.Text;
            txtBMBZ.Text = DEPTMEMO.Text;
            BMADD.Visible = true;
        }

        protected void lbtnBMDELETE_OnClick(object sender, EventArgs e)
        {
            LinkButton t = (LinkButton)sender;
            GridViewRow drv = (GridViewRow)t.NamingContainer;
            Label DEPTID = ((Label)GDBM.Rows[drv.RowIndex].FindControl("DEPTID"));
            zdService.DELETE_CRM_DEPT(DEPTID.Text);
            this.Page.RegisterStartupScript("onclick", "<script>alert('删除成功!')</script>");
            BANDBM();
        }

        protected void btnBMADD_Click(object sender, EventArgs e)
        {
            BMADD.Visible = true;
            lbBMRI.Text = "0";
        }

        protected void btnBMFIND_Click(object sender, EventArgs e)
        {
            BANDBM();
        }

        protected void btnBMSAVE_Click(object sender, EventArgs e)
        {
            if (lbBMRI.Text == "0")
            {
                if (txtBMMC.Text != "")
                {
                    zdService.INSERT_CRM_DEPT(txtBMMC.Text, txtBMBZ.Text);
                }
                else
                {
                    this.Page.RegisterStartupScript("onclick", "<script>alert('部门名称不能为空!')</script>");
                    return;
                }
                this.Page.RegisterStartupScript("onclick", "<script>alert('保存成功!')</script>");
                BANDBM();
                txtBMMC.Text = "";
                txtBMBZ.Text = "";
                lbBMRI.Text = "0";
                BMADD.Visible = false;
            }
            else
            {
                if (txtBMMC.Text != "")
                {
                    zdService.UPDATE_CRM_DEPT(lbBMRI.Text, txtBMMC.Text, txtBMBZ.Text);
                }
                else
                {
                    this.Page.RegisterStartupScript("onclick", "<script>alert('部门名称不能为空!')</script>");
                    return;
                }
                this.Page.RegisterStartupScript("onclick", "<script>alert('修改成功!')</script>");
                BANDBM();
                txtBMMC.Text = "";
                txtBMBZ.Text = "";
                lbBMRI.Text = "0";
                BMADD.Visible = false;
            }
        }

        protected void btnBMCLEAR_Click(object sender, EventArgs e)
        {
            txtBMMC.Text = "";
            txtBMBZ.Text = "";
            lbBMRI.Text = "0";
            BMADD.Visible = false;
        }

        private void BANDBM()
        {
            DataTable dt = zdService.SELECT_CRM_DEPT_ED();
            GDBM.DataSource = dt;
            GDBM.DataBind();
        }




        private void BANDLXR()
        {
            DataTable dt = zdService.SELECT_CRM_ZDLXR_ED();
            GVLXR.DataSource = dt;
            GVLXR.DataBind();
        }
        protected void btnLXRADD_Click(object sender, EventArgs e)
        {
            LXRADD.Visible = true;
            lbLXRRI.Text = "0";
        }

        protected void btnLXRFIND_Click(object sender, EventArgs e)
        {
            BANDLXR();
        }

        protected void btnLXRCLEAR_Click(object sender, EventArgs e)
        {
            lbLXRRI.Text = "0";
            txtLXR.Text = "";
            txtLXRLXDH.Text = "";
            txtLXRBZ.Text = "";
            LXRADD.Visible = false;
        }

        protected void lbtnLXRMODEFY_OnClick(object sender, EventArgs e)
        {
            LinkButton t = (LinkButton)sender;
            GridViewRow drv = (GridViewRow)t.NamingContainer;
            Label LXRID = ((Label)GVLXR.Rows[drv.RowIndex].FindControl("LXRID"));
            Label LXR = ((Label)GVLXR.Rows[drv.RowIndex].FindControl("LXR"));
            Label LXDH = ((Label)GVLXR.Rows[drv.RowIndex].FindControl("LXDH"));
            Label LXDES = ((Label)GVLXR.Rows[drv.RowIndex].FindControl("LXDES"));
            lbLXRRI.Text = LXRID.Text;
            txtLXR.Text = LXR.Text;
            txtLXRLXDH.Text = LXDH.Text;
            txtLXRBZ.Text = LXDES.Text;
            LXRADD.Visible = true;
        }

        protected void lbtnLXRDELETE_OnClick(object sender, EventArgs e)
        {
            LinkButton t = (LinkButton)sender;
            GridViewRow drv = (GridViewRow)t.NamingContainer;
            Label LXRID = ((Label)GDBM.Rows[drv.RowIndex].FindControl("LXRID"));
            zdService.DELETE_CRM_ZDLXR(LXRID.Text);
            this.Page.RegisterStartupScript("onclick", "<script>alert('删除成功!')</script>");
            BANDLXR();
        }

        protected void btnLXRSAVE_Click(object sender, EventArgs e)
        {
            if (lbLXRRI.Text == "0")
            {
                if (txtLXR.Text != "")
                {
                    zdService.INSERT_CRM_ZDLXR(txtLXR.Text, txtLXRLXDH.Text, txtLXRBZ.Text);
                }
                else
                {
                    this.Page.RegisterStartupScript("onclick", "<script>alert('联系人不能为空!')</script>");
                    return;
                }
                this.Page.RegisterStartupScript("onclick", "<script>alert('保存成功!')</script>");
            }
            else
            {
                if (txtLXR.Text != "")
                {
                    zdService.UPDATE_CRM_ZDLXR(lbLXRRI.Text, txtLXR.Text, txtLXRLXDH.Text, txtLXRBZ.Text);
                }
                else
                {
                    this.Page.RegisterStartupScript("onclick", "<script>alert('联系人不能为空!')</script>");
                    return;
                }
                this.Page.RegisterStartupScript("onclick", "<script>alert('修改成功!')</script>");
            }


            BANDLXR();
            lbLXRRI.Text = "0";
            txtLXR.Text = "";
            txtLXRLXDH.Text = "";
            txtLXRBZ.Text = "";
            LXRADD.Visible = false;
        }



        private void BANDDD()
        {
            DataTable dt = zdService.SELECT_CRM_ZDADD_ED();
            GVDD.DataSource = dt;
            GVDD.DataBind();
        }
        protected void btnDDADD_Click(object sender, EventArgs e)
        {
            divDDADD.Visible = true;
            lbDDRI.Text = "0";
        }

        protected void btnDDFIND_Click(object sender, EventArgs e)
        {
            BANDDD();
        }

        protected void btnDDSAVE_Click(object sender, EventArgs e)
        {
            if (lbDDRI.Text == "0")
            {
                if (txtDDDD.Text != "")
                {
                    zdService.INSERT_CRM_ZDADD(txtDDDD.Text, txtDDSM.Text);
                }
                else
                {
                    this.Page.RegisterStartupScript("onclick", "<script>alert('地点不能为空!')</script>");
                    return;
                }
                this.Page.RegisterStartupScript("onclick", "<script>alert('保存成功!')</script>");
            }
            else
            {
                if (txtDDDD.Text != "")
                {
                    zdService.UPDATE_CRM_ZDADD(lbDDRI.Text, txtDDDD.Text, txtDDSM.Text);
                }
                else
                {
                    this.Page.RegisterStartupScript("onclick", "<script>alert('地点不能为空!')</script>");
                    return;
                }
                this.Page.RegisterStartupScript("onclick", "<script>alert('修改成功!')</script>");
            }


            divDDADD.Visible = false;
            lbDDRI.Text = "0";
            txtDDDD.Text = "";
            txtDDSM.Text = "";
        }

        protected void btnDDCLEAR_Click(object sender, EventArgs e)
        {
            divDDADD.Visible = false;
            lbDDRI.Text = "0";
            txtDDDD.Text = "";
            txtDDSM.Text = "";
        }

        protected void lbtnDDMODEFY_OnClick(object sender, EventArgs e)
        {
            LinkButton t = (LinkButton)sender;
            GridViewRow drv = (GridViewRow)t.NamingContainer;
            Label ADDID = ((Label)GVDD.Rows[drv.RowIndex].FindControl("ADDID"));
            Label ADDNAME = ((Label)GVDD.Rows[drv.RowIndex].FindControl("ADDNAME"));
            Label ADDMEMO = ((Label)GVDD.Rows[drv.RowIndex].FindControl("ADDMEMO"));
            lbDDRI.Text = ADDID.Text;
            txtDDDD.Text = ADDNAME.Text;
            txtDDSM.Text = ADDMEMO.Text;
            divDDADD.Visible = true;
        }

        protected void lbtnDDDELETE_OnClick(object sender, EventArgs e)
        {
            LinkButton t = (LinkButton)sender;
            GridViewRow drv = (GridViewRow)t.NamingContainer;
            Label ADDID = ((Label)GVDD.Rows[drv.RowIndex].FindControl("ADDID"));
            zdService.DELETE_CRM_ZDADD(ADDID.Text);
            this.Page.RegisterStartupScript("onclick", "<script>alert('删除成功!')</script>");
            BANDLXR();
        }
    }
}