using Bussiness.SqlHelper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Bussiness.FKSY
{
    public class ServiceFKINFO
    {
        public DataTable Select_FKINFO(string STAFFID, string FKNO, string FKNAME, string LFRQS, string LFRQE, string ISLK, string GSDMRI, string JRMWRI)
        {
            string sql = "SELECT FK_FKINFO.* FROM FK_FKINFO,FK_MWCXQX where FK_FKINFO.ISDELETE=0 and FK_MWCXQX.STAFFID=@STAFFID and FK_FKINFO.JRMWRI=FK_MWCXQX.MWRI ";
            if (FKNO != "")
            {
                sql = sql + " and FK_FKINFO.FKNO like @FKNO ";
            }
            if (FKNAME != "")
            {
                sql = sql + " and FK_FKINFO.FKNAME like @FKNAME ";
            }
            if (LFRQS != "")
            {
                sql = sql + " and FK_FKINFO.LFRQ>=@LFRQS ";
            }
            if (LFRQE != "")
            {
                sql = sql + " and FK_FKINFO.LFRQ<=@LFRQE ";
            }
            if (ISLK != "-1")
            {
                sql = sql + " and FK_FKINFO.ISLK=@ISLK";
            }
            if (GSDMRI != "0")
            {
                sql = sql + " and FK_FKINFO.GSDMRI=@GSDMRI";
            }
            if (JRMWRI != "0")
            {
                sql = sql + " and FK_FKINFO.JRMWRI=@JRMWRI";
            }
            sql = sql + " order by FKNO";
            SqlParameter[] paras = {
                                       new SqlParameter("@STAFFID",STAFFID),
                                       new SqlParameter("@FKNO","%"+FKNO+"%"),
                                       new SqlParameter("@FKNAME","%"+FKNAME+"%"),
                                       new SqlParameter("@LFRQS",LFRQS),
                                       new SqlParameter("@LFRQE",LFRQE),
                                       new SqlParameter("@ISLK",ISLK),
                                       new SqlParameter("@GSDMRI",GSDMRI),
                                       new SqlParameter("@JRMWRI",JRMWRI)
                                   };
            DataTable dt = DBHelper.GetDataSet(CommandType.Text, sql, paras);
            return dt;
        }
        public DataTable Select_FKINFO_QX_GSDM(string STAFFID)
        {
            string sql = "select distinct FK_MWINFO.GSDMRI,FK_MWINFO.GSDM,FK_MWINFO.GSDMMS from FK_MWCXQX,FK_MWINFO where FK_MWCXQX.MWRI=FK_MWINFO.RI and FK_MWCXQX.STAFFID=@STAFFID and FK_MWINFO.ISDELETE=0";
            SqlParameter[] paras = {
                                       new SqlParameter("@STAFFID",STAFFID)
                                   };
            DataTable dt = DBHelper.GetDataSet(CommandType.Text, sql, paras);
            return dt;
        }

        public DataTable Select_FKINFO_QX_MW(string STAFFID, string GSDMRI)
        {
            string sql = "select distinct FK_MWINFO.RI,FK_MWINFO.MWNO,FK_MWINFO.MWMS from FK_MWCXQX,FK_MWINFO where FK_MWCXQX.MWRI=FK_MWINFO.RI and FK_MWCXQX.STAFFID=@STAFFID and FK_MWINFO.ISDELETE=0 and FK_MWINFO.GSDMRI=@GSDMRI";
            SqlParameter[] paras = {
                                       new SqlParameter("@STAFFID",STAFFID),
                                       new SqlParameter("@GSDMRI",GSDMRI)
                                   };
            DataTable dt = DBHelper.GetDataSet(CommandType.Text, sql, paras);
            return dt;
        }
    }
}
