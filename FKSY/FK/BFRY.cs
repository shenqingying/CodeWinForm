using Bussiness.FKSY;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FKSY.FK
{
    public partial class BFRY : Form
    {
        public string DEPT = "";
        public string BFNAME = "";
        public string GH = "";
        MainService mainService = new MainService();
        public BFRY()
        {
            InitializeComponent();
        }

        private void BFRY_Load(object sender, EventArgs e)
        {
            BANDddlDEPT();
            GVLXR.AutoGenerateColumns = false;
            DataTable dt = mainService.Select_FK_LXRINFO(txtBFNAME.Text.Trim(), ddlDEPT.SelectedValue.ToString());
            GVLXR.DataSource = dt;
        }

        private void btnFIND_Click(object sender, EventArgs e)
        {
            DataTable dt = mainService.Select_FK_LXRINFO(txtBFNAME.Text.Trim(), ddlDEPT.SelectedValue.ToString());
            GVLXR.DataSource = dt;
        }

        private void BANDddlDEPT()
        {
            DataTable dt = mainService.Select_FK_DEPT();
            DataRow dr = dt.NewRow();
            dr["JGBM"] = "A";
            dr["MC"] = "=请选择=";
            dt.Rows.Add(dr);
            DataTable dtCopy = dt.Copy();
            DataView dv = dt.DefaultView;
            dv.Sort = "JGBM";
            dtCopy = dv.ToTable();
            ddlDEPT.DataSource = dtCopy;
            ddlDEPT.ValueMember = "JGBM";
            ddlDEPT.DisplayMember = "MC";
        }

        private void GVLXR_DoubleClick(object sender, EventArgs e)
        {
            DEPT = GVLXR.CurrentRow.Cells[3].Value.ToString();
            BFNAME = GVLXR.CurrentRow.Cells[1].Value.ToString();
            GH = GVLXR.CurrentRow.Cells[0].Value.ToString();
            this.DialogResult = DialogResult.OK;
        }
    }
}
