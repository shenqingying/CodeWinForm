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

namespace STSY.RKGL
{
    public partial class RSTJ : System.Web.UI.Page
    {
        CKService ckService = new CKService();
        RKService rkService = new RKService();
        WLService wlService = new WLService();
        Mainservice mainService = new Mainservice();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LOGINTIMEOUT();
                STAFFQX();
                BANDDDLCK();         //仓库编号下拉框
                BANDddlGYSBH();      //供应商编号下拉框
                BANDddlLBCKBH();     //列表仓库编号下拉框
                BANDddlLBGYSBH();    //列表供应商编号下拉框
                BANDddlWLLB();       //物料类别下拉框
                BANDddlLBWLLB();     //列表物料类别下拉框
                //BAND();
                divRKDTX.Visible = false;
                divRKDLB.Visible = true;
                //rbl.SelectedValue = "1";
                //txtZDSJ.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");             原制单时间
                //txtZDR.Text = Session["STAFFNAME"].ToString();                           原制单人
                txtLBRKRQE.Text = DateTime.Now.ToString("yyyy-MM-dd");
                DateTime now = DateTime.Now;
                DateTime d1 = new DateTime(now.Year, now.Month, 1);
                txtLBRKRQS.Text = d1.ToString("yyyy-MM-dd");
                BANDBOTTON("1,4,5,6,7,9");
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
            bool s = mainService.SELECT_HG_RIGHT(Session["STAFFID"].ToString(), "202");
            if (s == false)
            {
                Response.Redirect("../Index/Index.aspx?RI=1");
            }
        }
        private void BANDBOTTON(string str)
        {
            string[] str1 = str.Split(',');
            btnCXSX.Enabled = false;
            btnCXSX.BackColor = System.Drawing.Color.Gray;
            btnDC.Enabled = false;
            btnDC.BackColor = System.Drawing.Color.Gray;
            btnRKDHJ.Enabled = false;
            btnRKDHJ.BackColor = System.Drawing.Color.Gray;
            if (str1.Length > 0)
            {
                for (int i = 0; i < str1.Length; i++)
                {
                    if (str1[i] == "1")
                    {
                        btnCXSX.Enabled = true;
                        btnCXSX.BackColor = System.Drawing.Color.FromName("#3498db");
                    }
                    else if (str1[i] == "8")
                    {
                        btnDC.Enabled = true;
                        btnDC.BackColor = System.Drawing.Color.FromName("#3498db");
                    }
                    else if (str1[i] == "9")
                    {
                        btnRKDHJ.Enabled = true;
                        btnRKDHJ.BackColor = System.Drawing.Color.FromName("#3498db");
                    }
                }
            }
        }

        private void BANDddlWLLB()             //物料类别下拉框
        {
            DataTable dt = wlService.GETWLTYPELB();
            ddlWLLB.DataSource = dt;
            ddlWLLB.DataTextField = "NAME";
            ddlWLLB.DataValueField = "typeID";
            ddlWLLB.DataBind();
            ListItem item = new ListItem();
            item.Text = "=请选择=";
            item.Value = "0";
            ddlWLLB.Items.Insert(0, item);
        }

        private void BANDddlLBWLLB()             //列表物料类别下拉框
        {
            DataTable dt = wlService.GETWLTYPELB();
            ddlLBWLLB.DataSource = dt;
            ddlLBWLLB.DataTextField = "NAME";
            ddlLBWLLB.DataValueField = "typeID";
            ddlLBWLLB.DataBind();
            ListItem item = new ListItem();
            item.Text = "=请选择=";
            item.Value = "0";
            ddlLBWLLB.Items.Insert(0, item);
        }

        private void BAND()                     //入库单信息页面的gridview数据绑定
        {
            if (lbRI.Text == "0")
            {
                DataTable dt = new DataTable();
                dt.Columns.Add(new DataColumn("LBBH", typeof(string)));
                dt.Columns.Add(new DataColumn("LBMC", typeof(string)));
                dt.Columns.Add(new DataColumn("WLGG", typeof(string)));
                dt.Columns.Add(new DataColumn("DW", typeof(string)));
                dt.Columns.Add(new DataColumn("SL", typeof(string)));
                dt.Columns.Add(new DataColumn("DJ", typeof(string)));
                dt.Columns.Add(new DataColumn("ZJE", typeof(string)));
                dt.Columns.Add(new DataColumn("KWMC", typeof(string)));
                dt.Columns.Add(new DataColumn("SM", typeof(string)));
                dt.Columns.Add(new DataColumn("XZ", typeof(bool)));
                dt.Columns.Add(new DataColumn("RuKMXID", typeof(string)));
                for (int i = 0; i < 5; i++)
                {
                    dt.Rows.Add();
                    dt.Rows[i][0] = "";
                    dt.Rows[i][1] = "";
                    dt.Rows[i][2] = "";
                    dt.Rows[i][3] = "";
                    dt.Rows[i][4] = "";
                    dt.Rows[i][5] = "";
                    dt.Rows[i][6] = "";
                    dt.Rows[i][7] = "";
                    dt.Rows[i][8] = "";
                    dt.Rows[i][9] = false;
                    dt.Rows[i][10] = "0";
                }
                gdRKDH.DataSource = dt;
                gdRKDH.DataBind();
            }
            else
            {
                DataTable dt = rkService.SELECTRKDBYID(lbRI.Text);
                if (dt.Rows.Count > 0)
                {
                    ddlCKBH.SelectedValue = dt.Rows[0]["StockID"].ToString();
                    ddlGYSBH.SelectedValue = dt.Rows[0]["ClientID"].ToString();
                    //txtRKRQ.Text = Convert.ToDateTime(dt.Rows[0]["RuKDate"].ToString()).ToString("yyyy-MM-dd");

                    ddlRKLX.SelectedValue = dt.Rows[0]["TypeID"].ToString();
                    txtRKDH.Text = dt.Rows[0]["RuKDNO"].ToString();
                    //txtRKBZ.Text = dt.Rows[0]["RuKMem"].ToString();
                    //txtZDR.Text = dt.Rows[0]["fillName"].ToString();
                    //txtZDSJ.Text = Convert.ToDateTime(dt.Rows[0]["fillTime"].ToString()).ToString("yyyy-MM-dd HH:mm:ss");
                }
                DataTable dtMX = rkService.SELECTRKDMXBYID(lbRI.Text);
                if (dtMX.Rows.Count > 0)
                {
                    DataTable dtband = new DataTable();
                    dtband.Columns.Add(new DataColumn("LBBH", typeof(string)));
                    dtband.Columns.Add(new DataColumn("LBMC", typeof(string)));
                    dtband.Columns.Add(new DataColumn("WLGG", typeof(string)));
                    dtband.Columns.Add(new DataColumn("DW", typeof(string)));
                    dtband.Columns.Add(new DataColumn("SL", typeof(string)));
                    dtband.Columns.Add(new DataColumn("DJ", typeof(string)));
                    dtband.Columns.Add(new DataColumn("ZJE", typeof(string)));
                    dtband.Columns.Add(new DataColumn("KWMC", typeof(string)));
                    dtband.Columns.Add(new DataColumn("SM", typeof(string)));
                    dtband.Columns.Add(new DataColumn("XZ", typeof(bool)));
                    dtband.Columns.Add(new DataColumn("RuKMXID", typeof(string)));
                    for (int i = 0; i < dtMX.Rows.Count; i++)
                    {
                        dtband.Rows.Add();
                        DataTable dtlbbh = wlService.GETWLTYPEbymaterialID(dtMX.Rows[i]["materialID"].ToString());
                        dtband.Rows[i][0] = dtlbbh.Rows[0]["typeID"].ToString();
                        dtband.Rows[i][1] = dtMX.Rows[i]["materialID"].ToString();
                        dtband.Rows[i][2] = "";
                        dtband.Rows[i][3] = dtMX.Rows[i]["mtUnit"].ToString();
                        dtband.Rows[i][4] = dtMX.Rows[i]["mtNumber"].ToString();
                        dtband.Rows[i][5] = dtMX.Rows[i]["mtPrice"].ToString();
                        dtband.Rows[i][6] = dtMX.Rows[i]["mtTotal"].ToString();
                        dtband.Rows[i][7] = dtMX.Rows[i]["PlaceID"].ToString();
                        dtband.Rows[i][8] = dtMX.Rows[i]["MXMemo"].ToString();
                        dtband.Rows[i][9] = false;
                        dtband.Rows[i][10] = dtMX.Rows[i]["RuKMXID"].ToString();
                    }
                    gdRKDH.DataSource = dtband;
                    gdRKDH.DataBind();
                    for (int i = 0; i < dtband.Rows.Count; i++)
                    {
                        if (dtband.Rows[i][0].ToString() == "")
                        {
                            dtband.Rows[i][0] = "0";
                        }
                        DropDownList ddlLBBH = (DropDownList)gdRKDH.Rows[i].FindControl("ddlLBBH");
                        ddlLBBH.SelectedValue = dtband.Rows[i][0].ToString();
                        if (dtband.Rows[i][1].ToString() == "")
                        {
                            dtband.Rows[i][1] = "0";
                        }
                        DataTable dtWL = wlService.GETWLTYPE(dtband.Rows[i][0].ToString());
                        if (dtWL.Rows.Count > 0)
                        {
                            for (int j = 0; j < dtWL.Rows.Count; j++)
                            {
                                dtWL.Rows[j]["mtName"] = dtWL.Rows[j]["mtSN"].ToString() + "  " + dtWL.Rows[j]["mtName"].ToString();
                            }
                        }
                        DropDownList ddlLBMC = ((DropDownList)gdRKDH.Rows[i].FindControl("ddlLBMC"));
                        ddlLBMC.DataSource = dtWL;
                        ddlLBMC.DataTextField = "mtName";
                        ddlLBMC.DataValueField = "materialID";
                        ddlLBMC.DataBind();
                        ListItem item = new ListItem();
                        item.Text = "=请选择=";
                        item.Value = "0";
                        ddlLBMC.Items.Insert(0, item);
                        ddlLBMC.SelectedValue = dtband.Rows[i][1].ToString();
                        if (dtband.Rows[i][7].ToString() == "")
                        {
                            dtband.Rows[i][7] = "0";
                        }
                        DropDownList ddlKWMC = (DropDownList)gdRKDH.Rows[i].FindControl("ddlKWMC");
                        ddlKWMC.SelectedValue = dtband.Rows[i][7].ToString();

                        CheckBox cbXZ = (CheckBox)gdRKDH.Rows[i].FindControl("cbXZ");
                        cbXZ.Checked = Convert.ToBoolean(dtband.Rows[i][9].ToString());
                    }
                }
            }
            BANDHJ();
        }

        private void BANDDDLCK()
        {
            DataTable dt = ckService.GETCKINFOISUSER();
            dt.Columns.Add("NAME", typeof(string));
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dt.Rows[i]["NAME"] = dt.Rows[i]["StockSN"].ToString() + "  " + dt.Rows[i]["StockName"].ToString();
                }
            }
            ddlCKBH.DataSource = dt;
            ddlCKBH.DataTextField = "NAME";
            ddlCKBH.DataValueField = "StockID";
            ddlCKBH.DataBind();
            ListItem item = new ListItem();
            item.Text = "=请选择=";
            item.Value = "0";
            ddlCKBH.Items.Insert(0, item);
        }

        private void BANDddlGYSBH()
        {
            DataTable dt = rkService.GETGYSINFO();
            dt.Columns.Add("NAME", typeof(string));
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dt.Rows[i]["NAME"] = dt.Rows[i]["ClSN"].ToString() + "  " + dt.Rows[i]["ClName"].ToString();
                }
            }
            ddlGYSBH.DataSource = dt;
            ddlGYSBH.DataTextField = "NAME";
            ddlGYSBH.DataValueField = "ClientID";
            ddlGYSBH.DataBind();
            ListItem item = new ListItem();
            item.Text = "=请选择=";
            item.Value = "0";
            ddlGYSBH.Items.Insert(0, item);
        }

        private void BANDddlLBCKBH()
        {
            DataTable dt = ckService.GETCKINFOISUSER();
            dt.Columns.Add("NAME", typeof(string));
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dt.Rows[i]["NAME"] = dt.Rows[i]["StockSN"].ToString() + "  " + dt.Rows[i]["StockName"].ToString();
                }
            }
            ddlLBCKBH.DataSource = dt;
            ddlLBCKBH.DataTextField = "NAME";
            ddlLBCKBH.DataValueField = "StockID";
            ddlLBCKBH.DataBind();
            ListItem item = new ListItem();
            item.Text = "=请选择=";
            item.Value = "0";
            ddlLBCKBH.Items.Insert(0, item);
            ddlLBCKBH.SelectedValue = "1";
        }

        private void BANDddlLBGYSBH()
        {
            DataTable dt = rkService.GETGYSINFO();
            dt.Columns.Add("NAME", typeof(string));
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dt.Rows[i]["NAME"] = dt.Rows[i]["ClSN"].ToString() + "  " + dt.Rows[i]["ClName"].ToString();
                }
            }
            ddlLBGYSBH.DataSource = dt;
            ddlLBGYSBH.DataTextField = "NAME";
            ddlLBGYSBH.DataValueField = "ClientID";
            ddlLBGYSBH.DataBind();
            ListItem item = new ListItem();
            item.Text = "=请选择=";
            item.Value = "0";
            ddlLBGYSBH.Items.Insert(0, item);
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow) //判断是否是DataRow，以防止鼠标经过Header也有效果   
            {
                e.Row.Attributes.Add("onmouseover", "e=this.style.backgroundColor; this.style.backgroundColor='#BBFFFF'");
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=e");
            }
        }

        protected void ddlLBBH_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList t = (DropDownList)sender;
            GridViewRow drv = (GridViewRow)t.NamingContainer;
            int rowIndex = drv.RowIndex;
            string coid = ((DropDownList)gdRKDH.Rows[drv.RowIndex].FindControl("ddlLBBH")).SelectedValue;
            DataTable dt = wlService.GETWLTYPE(coid);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dt.Rows[i]["mtName"] = dt.Rows[i]["mtSN"].ToString() + "  " + dt.Rows[i]["mtName"].ToString();
                }
            }
            DropDownList ddlLBMC = ((DropDownList)gdRKDH.Rows[drv.RowIndex].FindControl("ddlLBMC"));
            ddlLBMC.DataSource = dt;
            ddlLBMC.DataTextField = "mtName";
            ddlLBMC.DataValueField = "materialID";
            ddlLBMC.DataBind();
            ListItem item = new ListItem();
            item.Text = "=请选择=";
            item.Value = "0";
            ddlLBMC.Items.Insert(0, item);
        }

        protected void ddlLBMC_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList t = (DropDownList)sender;
            GridViewRow drv = (GridViewRow)t.NamingContainer;
            int rowIndex = drv.RowIndex;
            string coid = ((DropDownList)gdRKDH.Rows[drv.RowIndex].FindControl("ddlLBMC")).SelectedValue;
            DataTable dt = wlService.GETWLTYPEbymaterialID(coid);
            TextBox txtDW = ((TextBox)gdRKDH.Rows[drv.RowIndex].FindControl("txtDW"));
            txtDW.Text = dt.Rows[0]["mtUnit"].ToString();
            TextBox txtDJ = ((TextBox)gdRKDH.Rows[drv.RowIndex].FindControl("txtDJ"));
            txtDJ.Text = dt.Rows[0]["mtBuyPrice"].ToString();
            TextBox txtWLGG = ((TextBox)gdRKDH.Rows[drv.RowIndex].FindControl("txtWLGG"));
            txtWLGG.Text = dt.Rows[0]["mtSpec"].ToString();
        }

        protected void txtSL_TextChanged(object sender, EventArgs e)
        {
            TextBox t = (TextBox)sender;
            GridViewRow drv = (GridViewRow)t.NamingContainer;
            int rowIndex = drv.RowIndex;
            TextBox txtSL = ((TextBox)gdRKDH.Rows[drv.RowIndex].FindControl("txtSL"));
            TextBox txtDJ = ((TextBox)gdRKDH.Rows[drv.RowIndex].FindControl("txtDJ"));
            TextBox txtZJE = ((TextBox)gdRKDH.Rows[drv.RowIndex].FindControl("txtZJE"));
            if (txtSL.Text == "")
            {
                txtSL.Text = "0";
            }
            if (txtDJ.Text == "")
            {
                txtDJ.Text = "0";
            }
            txtZJE.Text = (Convert.ToDouble(txtSL.Text) * Convert.ToDouble(txtDJ.Text)).ToString();
            BANDHJ();
        }

        protected void txtDJ_TextChanged(object sender, EventArgs e)
        {
            TextBox t = (TextBox)sender;
            GridViewRow drv = (GridViewRow)t.NamingContainer;
            int rowIndex = drv.RowIndex;
            TextBox txtSL = ((TextBox)gdRKDH.Rows[drv.RowIndex].FindControl("txtSL"));
            TextBox txtDJ = ((TextBox)gdRKDH.Rows[drv.RowIndex].FindControl("txtDJ"));
            TextBox txtZJE = ((TextBox)gdRKDH.Rows[drv.RowIndex].FindControl("txtZJE"));
            if (txtSL.Text == "")
            {
                txtSL.Text = "0";
            }
            if (txtDJ.Text == "")
            {
                txtDJ.Text = "0";
            }
            txtZJE.Text = (Convert.ToDouble(txtSL.Text) * Convert.ToDouble(txtDJ.Text)).ToString();
            BANDHJ();
        }

        private void BANDRuKLLB()             //入库单列表的gridview数据刷新
        {
            LOGINTIMEOUT();
            DataTable dt = new DataTable();
            //if (rbl.SelectedValue == "0")
            //{
            //    dt = rkService.SELECTRKD(rbl.SelectedValue, ddlLBCKBH.SelectedValue, ddlLBGYSBH.SelectedValue, txtLBRKRQS.Text.Trim(), txtLBRKRQE.Text.Trim(), ddlLBRKLX.SelectedValue, txtLBRKDH.Text, Session["STAFFID"].ToString());
            //}
            //else
            //{
            //    dt = rkService.SELECTRKD(rbl.SelectedValue, ddlLBCKBH.SelectedValue, ddlLBGYSBH.SelectedValue, txtLBRKRQS.Text.Trim(), txtLBRKRQE.Text.Trim(), ddlLBRKLX.SelectedValue, txtLBRKDH.Text, "");
            //}
            dt.Columns.Add("RuKDate1", typeof(string));
            dt.Columns.Add("ISSH", typeof(bool));
            dt.Columns.Add("fillTime1", typeof(string));
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dt.Rows[i]["RuKDate1"] = Convert.ToDateTime(dt.Rows[i]["RuKDate"].ToString()).ToString("yyyy-MM-dd");
                    if (dt.Rows[i]["nStatus"].ToString() == "0" || dt.Rows[i]["nStatus"].ToString() == "1")
                    {
                        dt.Rows[i]["ISSH"] = false;
                    }
                    else if (dt.Rows[i]["nStatus"].ToString() == "2")
                    {
                        dt.Rows[i]["ISSH"] = true;
                    }
                    dt.Rows[i]["fillTime1"] = Convert.ToDateTime(dt.Rows[i]["fillTime"].ToString()).ToString("yyyy-MM-dd HH:mm:ss");
                }
            }
            gdRKDLB.DataSource = dt;
            gdRKDLB.DataBind();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                CheckBox cbSH = ((CheckBox)gdRKDLB.Rows[i].FindControl("cbSH"));
                bool z = Convert.ToBoolean(dt.Rows[i]["ISSH"].ToString());
                cbSH.Checked = z;
            }
        }

        protected void btnCXSX_Click(object sender, EventArgs e)           //查询刷新
        {
            if (divRKDLB.Visible == true)
            {
                //BANDRuKLLB();
                BANDLB();
                cbALL.Visible = true;
                label233.Visible = true;
            }
        }

        private void BANDLB()                       //点查询后的列表gridview数据更新
        {
            string RukDATAs = "";
            string RukDATAe = "";
            //if (cbRKRQ.Checked == true)
            //{
                RukDATAs = txtLBRKRQS.Text.Trim();
                RukDATAe = txtLBRKRQE.Text.Trim();
            //}
            DataTable dt = rkService.GETLBINFO(ddlLBCKBH.SelectedValue, ddlLBGYSBH.SelectedValue, RukDATAs, RukDATAe, ddlLBRKLX.SelectedValue, txtLBRKDH.Text.Trim(),ddlLBWLLB.SelectedValue);
            //dt.Columns.Add("RuKDate1", typeof(string));
            //if (dt.Rows.Count > 0)
            //{
            //    for (int i = 0; i < dt.Rows.Count; i++)
            //    {
            //        DateTime dtime = Convert.ToDateTime(dt.Rows[i]["RuKDate"].ToString());
            //        dt.Rows[i]["RuKDate1"] = dtime.ToString("yyyy-MM-dd");
            //    }
            //}
            gdRKDLB.DataSource = dt;
            gdRKDLB.DataBind();
            //if (dt.Rows.Count > 0)
            //{
            //    for (int i = 0; i < dt.Rows.Count; i++)
            //    {
            //        CheckBox cbSH = (CheckBox)gdRKDLB.Rows[i].FindControl("cbSH");
            //        cbSH.Checked = Convert.ToBoolean(dt.Rows[i]["isAudi"].ToString());
            //    }
            //}
        }

        protected void btnXZ_Click(object sender, EventArgs e)
        {
            BAND();
            BANDBOTTON("1,4,5,6,7");
            lbRI.Text = "0";
            lbISADD.Text = "true";
            lbISED.Text = "false";
            lbnStatus.Text = "0";
        }

        protected void btnBJ_Click(object sender, EventArgs e)
        {
            if (lbnStatus.Text == "0")
            {
                lbISED.Text = "true";
                BANDBOTTON("1,4,5,6,7");
                lbISADD.Text = "false";
                lbISED.Text = "true";
            }
            else
            {
                //Response.Write("<script>alert('已提交，无法修改!')</script>");
                this.Page.RegisterStartupScript("onclick", "<script>alert('已提交，无法修改!')</script>");
            }
        }

        protected void btnaddrow_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("LBBH", typeof(string)));
            dt.Columns.Add(new DataColumn("LBMC", typeof(string)));
            dt.Columns.Add(new DataColumn("WLGG", typeof(string)));
            dt.Columns.Add(new DataColumn("DW", typeof(string)));
            dt.Columns.Add(new DataColumn("SL", typeof(string)));
            dt.Columns.Add(new DataColumn("DJ", typeof(string)));
            dt.Columns.Add(new DataColumn("ZJE", typeof(string)));
            dt.Columns.Add(new DataColumn("KWMC", typeof(string)));
            dt.Columns.Add(new DataColumn("SM", typeof(string)));
            dt.Columns.Add(new DataColumn("XZ", typeof(bool)));
            dt.Columns.Add(new DataColumn("RuKMXID", typeof(string)));
            for (int i = 0; i < gdRKDH.Rows.Count; i++)
            {
                dt.Rows.Add();
                dt.Rows[i][0] = ((DropDownList)gdRKDH.Rows[i].FindControl("ddlLBBH")).SelectedValue;
                dt.Rows[i][1] = ((DropDownList)gdRKDH.Rows[i].FindControl("ddlLBMC")).SelectedValue;
                dt.Rows[i][2] = ((TextBox)gdRKDH.Rows[i].FindControl("txtWLGG")).Text;
                dt.Rows[i][3] = ((TextBox)gdRKDH.Rows[i].FindControl("txtDW")).Text;
                dt.Rows[i][4] = ((TextBox)gdRKDH.Rows[i].FindControl("txtSL")).Text;
                dt.Rows[i][5] = ((TextBox)gdRKDH.Rows[i].FindControl("txtDJ")).Text;
                dt.Rows[i][6] = ((TextBox)gdRKDH.Rows[i].FindControl("txtZJE")).Text;
                dt.Rows[i][7] = ((DropDownList)gdRKDH.Rows[i].FindControl("ddlKWMC")).SelectedValue;
                dt.Rows[i][8] = ((TextBox)gdRKDH.Rows[i].FindControl("txtSM")).Text;
                dt.Rows[i][9] = ((CheckBox)gdRKDH.Rows[i].FindControl("cbXZ")).Checked;
                dt.Rows[i][10] = ((Label)gdRKDH.Rows[i].FindControl("RuKMXID")).Text;
            }
            for (int i = gdRKDH.Rows.Count; i < 5 + gdRKDH.Rows.Count; i++)
            {
                dt.Rows.Add();
                dt.Rows[i][0] = "";
                dt.Rows[i][1] = "";
                dt.Rows[i][2] = "";
                dt.Rows[i][3] = "";
                dt.Rows[i][4] = "";
                dt.Rows[i][5] = "";
                dt.Rows[i][6] = "";
                dt.Rows[i][7] = "";
                dt.Rows[i][8] = "";
                dt.Rows[i][9] = false;
                dt.Rows[i][10] = "0";
            }
            gdRKDH.DataSource = dt;
            gdRKDH.DataBind();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i][0].ToString() == "")
                {
                    dt.Rows[i][0] = "0";
                }
                DropDownList ddlLBBH = (DropDownList)gdRKDH.Rows[i].FindControl("ddlLBBH");
                ddlLBBH.SelectedValue = dt.Rows[i][0].ToString();
                if (dt.Rows[i][1].ToString() == "")
                {
                    dt.Rows[i][1] = "0";
                }
                DataTable dtWL = wlService.GETWLTYPE(dt.Rows[i][0].ToString());
                if (dtWL.Rows.Count > 0)
                {
                    for (int j = 0; j < dtWL.Rows.Count; j++)
                    {
                        dtWL.Rows[j]["mtName"] = dtWL.Rows[j]["mtSN"].ToString() + "  " + dtWL.Rows[j]["mtName"].ToString();
                    }
                }
                DropDownList ddlLBMC = ((DropDownList)gdRKDH.Rows[i].FindControl("ddlLBMC"));
                ddlLBMC.DataSource = dtWL;
                ddlLBMC.DataTextField = "mtName";
                ddlLBMC.DataValueField = "materialID";
                ddlLBMC.DataBind();
                ListItem item = new ListItem();
                item.Text = "=请选择=";
                item.Value = "0";
                ddlLBMC.Items.Insert(0, item);
                ddlLBMC.SelectedValue = dt.Rows[i][1].ToString();
                if (dt.Rows[i][7].ToString() == "")
                {
                    dt.Rows[i][7] = "0";
                }
                DropDownList ddlKWMC = (DropDownList)gdRKDH.Rows[i].FindControl("ddlKWMC");
                ddlKWMC.SelectedValue = dt.Rows[i][7].ToString();

                CheckBox cbXZ = (CheckBox)gdRKDH.Rows[i].FindControl("cbXZ");
                cbXZ.Checked = Convert.ToBoolean(dt.Rows[i][9].ToString());
            }
        }

        protected void btndeleterow_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("LBBH", typeof(string)));
            dt.Columns.Add(new DataColumn("LBMC", typeof(string)));
            dt.Columns.Add(new DataColumn("WLGG", typeof(string)));
            dt.Columns.Add(new DataColumn("DW", typeof(string)));
            dt.Columns.Add(new DataColumn("SL", typeof(string)));
            dt.Columns.Add(new DataColumn("DJ", typeof(string)));
            dt.Columns.Add(new DataColumn("ZJE", typeof(string)));
            dt.Columns.Add(new DataColumn("KWMC", typeof(string)));
            dt.Columns.Add(new DataColumn("SM", typeof(string)));
            dt.Columns.Add(new DataColumn("XZ", typeof(bool)));
            dt.Columns.Add(new DataColumn("RuKMXID", typeof(string)));
            if (gdRKDH.Rows.Count > 0)
            {
                int s = 0;
                for (int i = 0; i < gdRKDH.Rows.Count; i++)
                {
                    if (((CheckBox)gdRKDH.Rows[i].FindControl("cbXZ")).Checked == false)
                    {
                        dt.Rows.Add();
                        dt.Rows[s][0] = ((DropDownList)gdRKDH.Rows[i].FindControl("ddlLBBH")).SelectedValue;
                        dt.Rows[s][1] = ((DropDownList)gdRKDH.Rows[i].FindControl("ddlLBMC")).SelectedValue;
                        dt.Rows[s][2] = ((TextBox)gdRKDH.Rows[i].FindControl("txtWLGG")).Text;
                        dt.Rows[s][3] = ((TextBox)gdRKDH.Rows[i].FindControl("txtDW")).Text;
                        dt.Rows[s][4] = ((TextBox)gdRKDH.Rows[i].FindControl("txtSL")).Text;
                        dt.Rows[s][5] = ((TextBox)gdRKDH.Rows[i].FindControl("txtDJ")).Text;
                        dt.Rows[s][6] = ((TextBox)gdRKDH.Rows[i].FindControl("txtZJE")).Text;
                        dt.Rows[s][7] = ((DropDownList)gdRKDH.Rows[i].FindControl("ddlKWMC")).SelectedValue;
                        dt.Rows[s][8] = ((TextBox)gdRKDH.Rows[i].FindControl("txtSM")).Text;
                        dt.Rows[s][9] = ((CheckBox)gdRKDH.Rows[i].FindControl("cbXZ")).Checked;
                        dt.Rows[s][10] = ((Label)gdRKDH.Rows[i].FindControl("RuKMXID")).Text;
                        s = s + 1;
                    }
                }
                gdRKDH.DataSource = dt;
                gdRKDH.DataBind();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i][0].ToString() == "")
                    {
                        dt.Rows[i][0] = "0";
                    }
                    DropDownList ddlLBBH = (DropDownList)gdRKDH.Rows[i].FindControl("ddlLBBH");
                    ddlLBBH.SelectedValue = dt.Rows[i][0].ToString();
                    if (dt.Rows[i][1].ToString() == "")
                    {
                        dt.Rows[i][1] = "0";
                    }
                    DataTable dtWL = wlService.GETWLTYPE(dt.Rows[i][0].ToString());
                    if (dtWL.Rows.Count > 0)
                    {
                        for (int j = 0; j < dtWL.Rows.Count; j++)
                        {
                            dtWL.Rows[j]["mtName"] = dtWL.Rows[j]["mtSN"].ToString() + "  " + dtWL.Rows[j]["mtName"].ToString();
                        }
                    }
                    DropDownList ddlLBMC = ((DropDownList)gdRKDH.Rows[i].FindControl("ddlLBMC"));
                    ddlLBMC.DataSource = dtWL;
                    ddlLBMC.DataTextField = "mtName";
                    ddlLBMC.DataValueField = "materialID";
                    ddlLBMC.DataBind();
                    ListItem item = new ListItem();
                    item.Text = "=请选择=";
                    item.Value = "0";
                    ddlLBMC.Items.Insert(0, item);
                    ddlLBMC.SelectedValue = dt.Rows[i][1].ToString();
                    if (dt.Rows[i][7].ToString() == "")
                    {
                        dt.Rows[i][7] = "0";
                    }
                    DropDownList ddlKWMC = ((DropDownList)gdRKDH.Rows[i].FindControl("ddlKWMC"));
                    ddlKWMC.SelectedValue = dt.Rows[i][7].ToString();

                    CheckBox cbXZ = (CheckBox)gdRKDH.Rows[i].FindControl("cbXZ");
                    cbXZ.Checked = Convert.ToBoolean(dt.Rows[i][9].ToString());
                }
            }
        }

        //protected void btnSAVE_Click(object sender, EventArgs e)
        //{
        //    bool isnew = false;
        //    LOGINTIMEOUT();
        //    if (lbISED.Text == "true" && lbRI.Text != "0")
        //    {
        //        rkService.DELETERKDMX(lbRI.Text);
        //        rkService.DELETERKD(lbRI.Text);
        //        isnew = true;
        //    }
        //    if ((lbISADD.Text == "true" && lbRI.Text == "0") || (lbISED.Text == "true" && lbRI.Text != "0"))
        //    {
        //        string s = rkService.insertRKDH(txtRKDH.Text.Trim(), ddlCKBH.SelectedValue, ddlGYSBH.SelectedValue, txtRKRQ.Text.Trim(), txtRKBZ.Text.Trim(), Session["STAFFID"].ToString(), txtZDR.Text.Trim(), txtZDSJ.Text.Trim(), "0", "0", ddlRKLX.SelectedValue, "0");
        //        for (int i = 0; i < gdRKDH.Rows.Count; i++)
        //        {
        //            string[] str = new string[10];
        //            str[0] = ((DropDownList)gdRKDH.Rows[i].FindControl("ddlLBBH")).SelectedValue;
        //            str[1] = ((DropDownList)gdRKDH.Rows[i].FindControl("ddlLBMC")).SelectedValue;
        //            str[2] = ((TextBox)gdRKDH.Rows[i].FindControl("txtWLGG")).Text;
        //            str[3] = ((TextBox)gdRKDH.Rows[i].FindControl("txtDW")).Text;
        //            str[4] = ((TextBox)gdRKDH.Rows[i].FindControl("txtSL")).Text;
        //            str[5] = ((TextBox)gdRKDH.Rows[i].FindControl("txtDJ")).Text;
        //            str[6] = ((TextBox)gdRKDH.Rows[i].FindControl("txtZJE")).Text;
        //            str[7] = ((DropDownList)gdRKDH.Rows[i].FindControl("ddlKWMC")).SelectedValue;
        //            str[8] = ((TextBox)gdRKDH.Rows[i].FindControl("txtSM")).Text;
        //            if (str[0] != "0" && str[0] != "" && str[1] != "0" && str[1] != "")
        //            {
        //                rkService.insertRKDMX(s, str[1], str[4], str[3], str[5], str[6], str[7], str[8]);
        //            }
        //            else
        //            {
        //                break;
        //            }
        //        }
        //        lbRI.Text = s;
        //    }
        //    if (isnew == false)
        //    {
        //        //Response.Write("<script>alert('添加成功!')</script>");
        //        this.Page.RegisterStartupScript("onclick", "<script>alert('添加成功!')</script>");
        //    }
        //    else
        //    {
        //        //Response.Write("<script>alert('修改成功!')</script>");
        //        this.Page.RegisterStartupScript("onclick", "<script>alert('修改成功!')</script>");

        //    }
        //    lbISADD.Text = "false";
        //    lbISED.Text = "false";
        //    BANDBOTTON("1,2,3,8,9");
        //}

        protected void btnQX_Click(object sender, EventArgs e)
        {
            lbISADD.Text = "false";
            lbISED.Text = "false";
            BANDBOTTON("1,2,3,8,9");
            BAND();
        }

        //protected void btnTJ_Click(object sender, EventArgs e)
        //{
        //    if (lbRI.Text != "0" && lbRI.Text != "")
        //    {
        //        DateTime dtime = Convert.ToDateTime(txtRKRQ.Text);
        //        string s = dtime.ToString("yyyyMM");
        //        rkService.RuKDTJ(s, "1", lbRI.Text);
        //        BANDRuKLLB();
        //        //Response.Write("<script>alert('提交成功，单据无法修改!')</script>");
        //        this.Page.RegisterStartupScript("onclick", "<script>alert('提交成功，单据无法修改!')</script>");
        //    }
        //}

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            LinkButton1.BackColor = System.Drawing.Color.DarkGray;
            LinkButton2.BackColor = System.Drawing.Color.White;
            divRKDTX.Visible = true;
            divRKDLB.Visible = false;
        }

        protected void LinkButton2_Click(object sender, EventArgs e)
        {
            LinkButton1.BackColor = System.Drawing.Color.White;
            LinkButton2.BackColor = System.Drawing.Color.DarkGray;
            divRKDTX.Visible = false;
            divRKDLB.Visible = true;
            BANDBOTTON("1,9");
        }

        protected void gdRKDLB_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow) //判断是否是DataRow，以防止鼠标经过Header也有效果   
            {
                e.Row.Attributes.Add("onmouseover", "e=this.style.backgroundColor; this.style.backgroundColor='#CCCCCC'");
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=e");
            }
        }

        //protected void lbtnFIND_OnClick(object sender, EventArgs e)              原查看按钮，现暂无用
        //{
        //    LinkButton t = (LinkButton)sender;
        //    GridViewRow drv = (GridViewRow)t.NamingContainer;
        //    int rowIndex = drv.RowIndex;
        //    Label RuKDID = ((Label)gdRKDLB.Rows[drv.RowIndex].FindControl("RuKDID"));
        //    Label nStatus = ((Label)gdRKDLB.Rows[drv.RowIndex].FindControl("nStatus"));
        //    lbRI.Text = RuKDID.Text;
        //    BAND();
        //    LinkButton1.BackColor = System.Drawing.Color.DarkGray;
        //    LinkButton2.BackColor = System.Drawing.Color.White;
        //    divRKDTX.Visible = true;
        //    divRKDLB.Visible = false;
        //    BANDBOTTON("1,2,3,8,9");
        //    lbISADD.Text = "false";
        //    lbISED.Text = "false";
        //    lbnStatus.Text = nStatus.Text;
        //}

        //protected void btnDJTH_Click(object sender, EventArgs e)
        //{
        //    if (lbRI.Text != "" && lbRI.Text != "0")
        //    {
        //        if (lbnStatus.Text == "0")
        //        {
        //            this.Page.RegisterStartupScript("onclick", "<script>alert('该入库单未提交，无需退回!')</script>");
        //        }
        //        else if (lbnStatus.Text == "2")
        //        {
        //            this.Page.RegisterStartupScript("onclick", "<script>alert('该入库单已审核，请先审核退回!')</script>");
        //        }
        //        else if (lbnStatus.Text == "1")
        //        {
        //            rkService.RuKDTJ("", "0", lbRI.Text);
        //            this.Page.RegisterStartupScript("onclick", "<script>alert('单价退回成功!')</script>");
        //        }
        //    }
        //    else
        //    {
        //        this.Page.RegisterStartupScript("onclick", "<script>alert('请先选择入库单!')</script>");
        //    }
        //}


        protected void btnRKDHJ_Click(object sender, EventArgs e)            //入库单合计按钮
        {
            LOGINTIMEOUT();
            string s = "";

            for (int i = 0; i < gdRKDLB.Rows.Count; i++)
            {
                CheckBox cbXZ = (CheckBox)gdRKDLB.Rows[i].FindControl("cbXZ");
                Label RuKDID = (Label)gdRKDLB.Rows[i].FindControl("RuKDID");
                if (cbXZ.Checked == true)
                {
                    if (s == "")
                    {
                        s = RuKDID.Text;
                    }
                    else
                    {
                        s = s + "," + RuKDID.Text;
                    }
                }
            }
            if (s == "")
            {
                this.Page.RegisterStartupScript("onclick", "<script>alert('请选择入库单据！')</script>");
                return;
            }

            DataTable dt = rkService.selectHJ(s);
            if(dt.Rows.Count > 0)
            {
                DataTable dthj = new DataTable();
                dthj.Columns.Add(new DataColumn("WLLBid", typeof(string)));
                dthj.Columns.Add(new DataColumn("WLLB", typeof(string)));
                dthj.Columns.Add(new DataColumn("WLMCid", typeof(string)));
                dthj.Columns.Add(new DataColumn("WLMC", typeof(string)));
                dthj.Columns.Add(new DataColumn("DW", typeof(string)));
                dthj.Columns.Add(new DataColumn("SL", typeof(string)));
                dthj.Columns.Add(new DataColumn("DJ", typeof(string)));
                dthj.Columns.Add(new DataColumn("ZJE", typeof(string)));
                for(int i = 0; i < dt.Rows.Count; i++)
                {
                    dthj.Rows.Add();
                    dthj.Rows[i][0] = dt.Rows[i]["typeID"].ToString();
                    dthj.Rows[i][1] = dt.Rows[i]["typeName"].ToString();
                    dthj.Rows[i][2] = dt.Rows[i]["materialID"].ToString();
                    dthj.Rows[i][3] = dt.Rows[i]["mtName"].ToString();
                    dthj.Rows[i][4] = dt.Rows[i]["mtUnit"].ToString();
                    dthj.Rows[i][5] = dt.Rows[i]["mtNumber"].ToString();
                    dthj.Rows[i][5] = Math.Round(Convert.ToDouble(dthj.Rows[i][5]), 2);
                    dthj.Rows[i][7] = dt.Rows[i]["mtTotal"].ToString();
                    dthj.Rows[i][7] = Math.Round(Convert.ToDouble(dthj.Rows[i][7]), 2);
                    if (dthj.Rows[i][5].ToString() == "0" || dthj.Rows[i][5].ToString() == "")
                    {
                        dthj.Rows[i][6] = "0";
                    }
                    else
                    {
                        dthj.Rows[i][6] = (Convert.ToDouble(dthj.Rows[i][7]) / Convert.ToDouble(dthj.Rows[i][5])).ToString();
                        dthj.Rows[i][6] = Math.Round(Convert.ToDouble(dthj.Rows[i][6]), 4);
                        if (dthj.Rows[i][6].ToString() == "非数字")
                        {
                            dthj.Rows[i][6] = "0";
                        }
                    }
                    
                }
                
                if (dt.Rows.Count > 0)              //把id和name合并
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        dthj.Rows[i]["WLLB"] = dthj.Rows[i]["WLLBid"].ToString() + "  " + dthj.Rows[i]["WLLB"].ToString();
                        dthj.Rows[i]["WLMC"] = dthj.Rows[i]["WLMCid"].ToString() + "  " + dthj.Rows[i]["WLMC"].ToString();  
                    }
                }

                
                gdRKDH.DataSource = dthj;
                gdRKDH.DataBind();
                BANDHJ();

                LinkButton1.BackColor = System.Drawing.Color.DarkGray;
                LinkButton2.BackColor = System.Drawing.Color.White;
                divRKDTX.Visible = true;
                divRKDLB.Visible = false;
                ddlCKBH.SelectedValue = ddlLBCKBH.SelectedValue;
                ddlGYSBH.SelectedValue = ddlLBGYSBH.SelectedValue;
                txtRKRQS.Text = txtLBRKRQS.Text;//入库日期
                txtRKRQE.Text = txtLBRKRQE.Text;
                ddlRKLX.SelectedValue = ddlLBRKLX.SelectedValue;
                txtRKDH.Text = txtLBRKDH.Text;
                ddlWLLB.SelectedValue = ddlLBWLLB.SelectedValue;

                ddlCKBH.Enabled = false;
                ddlGYSBH.Enabled = false;
                txtRKRQS.Enabled = false;
                txtRKRQE.Enabled = false;
                ddlRKLX.Enabled = false;
                txtRKDH.Enabled = false;
                ddlWLLB.Enabled = false;

                for (int i = 0; i < gdRKDLB.Rows.Count; i++)
                {
                    CheckBox cbxz = (CheckBox)gdRKDLB.Rows[i].FindControl("cbXZ");
                    cbxz.Checked = false;
                }
                cbALL.Checked = false;
            }

            BANDBOTTON("8");


            
            //if (lbRI.Text != "" && lbRI.Text != "0")
            //{
            //    if (gdRKDH.Rows.Count > 0)
            //    {
            //        for (int i = 0; i < gdRKDH.Rows.Count; i++)
            //        {
            //            string mtNumber = "";
            //            string mtTotal = "";
            //            DropDownList ddlLBMC = (DropDownList)gdRKDH.Rows[i].FindControl("ddlLBMC");
            //            DropDownList ddlKWMC = (DropDownList)gdRKDH.Rows[i].FindControl("ddlKWMC");
            //            TextBox txtSL = (TextBox)gdRKDH.Rows[i].FindControl("txtSL");
            //            TextBox txtZJE = (TextBox)gdRKDH.Rows[i].FindControl("txtZJE");
            //            TextBox txtDW = (TextBox)gdRKDH.Rows[i].FindControl("txtDW");
            //            TextBox txtDJ = (TextBox)gdRKDH.Rows[i].FindControl("txtDJ");
            //            DataTable dtRealStock = rkService.SELECTMSRealStock(ddlCKBH.SelectedValue, ddlLBMC.SelectedValue, ddlKWMC.SelectedValue);
            //            if (dtRealStock.Rows.Count > 0)
            //            {
            //                mtNumber = (Convert.ToDouble(dtRealStock.Rows[0]["mtNumber"].ToString()) + Convert.ToDouble(txtSL.Text)).ToString();
            //                mtTotal = (Convert.ToDouble(dtRealStock.Rows[0]["mtTotal"].ToString()) + Convert.ToDouble(txtZJE.Text)).ToString();
            //                rkService.UPDATEMSRealStock(mtNumber, mtTotal, ddlCKBH.SelectedValue, ddlLBMC.SelectedValue, ddlKWMC.SelectedValue);
            //            }
            //            else
            //            {
            //                rkService.INSERT_MSRealStock(ddlCKBH.SelectedValue, ddlLBMC.SelectedValue, ddlKWMC.SelectedValue, txtDW.Text, txtDJ.Text, txtSL.Text, txtZJE.Text);
            //            }
            //        }
            //        DateTime dttime = Convert.ToDateTime(txtRKRQ.Text);

            //        rkService.UPDATEMSRuKD(dttime.ToString("yyyyMM"), "2", "1", Session["STAFFNAME"].ToString(), Session["STAFFID"].ToString(), lbRI.Text);
            //        rkService.UPDATEMSRuKD(lbRI.Text);
            //        DataTable dtStock = rkService.SELECTStock(lbRI.Text);
            //        string RuKNO = (Convert.ToInt32(dtStock.Rows[0]["RuKNO"].ToString()) + 1).ToString();
            //        rkService.UPDATEMSStock(RuKNO, dtStock.Rows[0]["StockID"].ToString());
            //        rkService.UPDATEMSMaterial(lbRI.Text);
            //        lbnStatus.Text = "2";
            //        this.Page.RegisterStartupScript("onclick", "<script>alert('审核入库，无法进行修改!')</script>");
            //    }
            //}
            //else
            //{
            //    this.Page.RegisterStartupScript("onclick", "<script>alert('请先选择入库单!')</script>");
            //}
        }

        private void BANDHJ()
        {
            //double sumSL = 0;
            double sumZJE = 0;
            if (gdRKDH.Rows.Count > 0)
            {
                for (int i = 0; i < gdRKDH.Rows.Count; i++)
                {
                    //TextBox txtSL = ((TextBox)gdRKDH.Rows[i].FindControl("txtSL"));
                    TextBox txtZJE = ((TextBox)gdRKDH.Rows[i].FindControl("txtZJE"));
                    //try
                    //{
                    //    sumSL = sumSL + Convert.ToDouble(txtSL.Text);
                    //}
                    //catch
                    //{
                    //    sumSL = sumSL + 0;
                    //}
                    try
                    {
                        sumZJE = sumZJE + Convert.ToDouble(txtZJE.Text);
                    }
                    catch
                    {
                        sumZJE = sumZJE + 0;
                    }
                }
            }
            GridViewRow foot = gdRKDH.FooterRow;
            //foot.Cells[5].HorizontalAlign = HorizontalAlign.Right;
            //foot.Cells[5].Text = sumSL.ToString();
            foot.Cells[5].HorizontalAlign = HorizontalAlign.Right;
            foot.Cells[5].Text = sumZJE.ToString();
        }

        protected void btnDC_Click(object sender, EventArgs e)            //导出按钮
        {
            DataTable dtDC = new DataTable();
            dtDC.Columns.Add("物料类别", typeof(string));
            dtDC.Columns.Add("物料名称", typeof(string));
            dtDC.Columns.Add("单位", typeof(string));
            dtDC.Columns.Add("数量", typeof(double));
            dtDC.Columns.Add("单价", typeof(double));
            dtDC.Columns.Add("总金额", typeof(double));
            
            for (int i = 0; i < gdRKDH.Rows.Count; i++)
            {
                dtDC.Rows.Add();
                dtDC.Rows[i][0] = getname(((TextBox)gdRKDH.Rows[i].FindControl("txtWLLB")).Text);
                //dtDC.Rows[i][0] = ((TextBox)gdRKDH.Rows[i].FindControl("txtWLLB")).Text;
                dtDC.Rows[i][1] = getname(((TextBox)gdRKDH.Rows[i].FindControl("txtWLMC")).Text);
                //dtDC.Rows[i][1] = ((TextBox)gdRKDH.Rows[i].FindControl("txtWLMC")).Text;
                dtDC.Rows[i][2] = ((TextBox)gdRKDH.Rows[i].FindControl("txtDW")).Text;
                dtDC.Rows[i][3] = ((TextBox)gdRKDH.Rows[i].FindControl("txtSL")).Text;
                dtDC.Rows[i][4] = ((TextBox)gdRKDH.Rows[i].FindControl("txtDJ")).Text;
                dtDC.Rows[i][5] = ((TextBox)gdRKDH.Rows[i].FindControl("txtZJE")).Text;
                
            }
            NpoiExcel(dtDC, "表单合计");
        }

        private string getname(string a)
        {
            
            string[] name = a.Split(' ');
            return name[2];
            
            
        }


        public void NpoiExcel(DataTable dt, string title)
        {
            NPOI.HSSF.UserModel.HSSFWorkbook book = new NPOI.HSSF.UserModel.HSSFWorkbook();
            NPOI.SS.UserModel.ISheet sheet = book.CreateSheet("Sheet1");
            NPOI.SS.UserModel.IRow headerrow = sheet.CreateRow(0);
            ICellStyle style = book.CreateCellStyle();
            style.Alignment = HorizontalAlignment.CENTER;
            style.VerticalAlignment = VerticalAlignment.CENTER;
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                ICell cell = headerrow.CreateCell(i);
                cell.CellStyle = style;
                cell.SetCellValue(dt.Columns[i].ColumnName);

            }
            for (int I = 0; I <= dt.Rows.Count - 1; I++)
            {
                HSSFRow row2 = (HSSFRow)sheet.CreateRow(I + 1);
                for (int j = 0; j < 3; j++)
                {
                    string DgvValue = dt.Rows[I][j].ToString();              //!!!
                    row2.CreateCell(j).SetCellValue(DgvValue);
                    sheet.SetColumnWidth(j, 20 * 150);
                }
                for (int j = 3; j < 6; j++)
                {
                    double DgvValue = Convert.ToDouble((dt.Rows[I][j]).ToString());              //!!!
                    Math.Round(DgvValue, 2);
                    row2.CreateCell(j).SetCellValue(DgvValue);
                    sheet.SetColumnWidth(j, 20 * 150);
                }
            }
            MemoryStream ms = new MemoryStream();
            book.Write(ms);
            Response.AddHeader("Content-Disposition", string.Format("attachment; filename={0}.xls", HttpUtility.UrlEncode(title + "_" + DateTime.Now.ToString("yyyy-MM-dd"), System.Text.Encoding.UTF8)));
            Response.BinaryWrite(ms.ToArray());
            Response.End();
            book = null;
            ms.Close();
            ms.Dispose();
        }

        protected void cbALL_CheckedChanged(object sender, EventArgs e)
        {

                for (int i = 0; i < gdRKDLB.Rows.Count; i++)
                {
                    CheckBox cbxz = (CheckBox)gdRKDLB.Rows[i].FindControl("cbXZ");

                    if (cbALL.Checked == true)
                    {
                        cbxz.Checked = true;

                    }
                    else
                    {
                        cbxz.Checked = false;
                    }
                    

                }




        }

        protected void btnckall_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < gdRKDLB.Rows.Count; i++)
            {
                CheckBox cbxz = (CheckBox)gdRKDLB.Rows[i].FindControl("cbXZ");


            }

        }

    }
}