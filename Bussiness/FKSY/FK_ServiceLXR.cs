using Bussiness.SqlHelper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Bussiness.FKSY
{
    public class FK_ServiceLXR
    {
        public DataTable Select_FK_GSDMQX(string GSDMRI)
        {
            string sql = "select * from FK_GSDMQX where GSDMRI=@GSDMRI";
            SqlParameter[] paras = {
                                       new SqlParameter("@GSDMRI",GSDMRI)
                                   };
            DataTable dt = DBHelper.GetDataSet(CommandType.Text, sql, paras);
            return dt;
        }

        public DataTable Select_FK_DEPT(string JGBM)
        {
            string sql = "select * from FK_DEPT where JGBM like @JGBM and JGBM<>@JGBM1";
            SqlParameter[] paras = {
                                       new SqlParameter("@JGBM",JGBM+"%"),
                                       new SqlParameter("@JGBM1",JGBM)
                                   };
            DataTable dt = DBHelper.GetDataSet(CommandType.Text, sql, paras);
            return dt;
        }
        
        public DataTable Select_FK_LXRINFO(string DEPTNO)
        {
            string sql = "select *,(select GSJC from FK_GSDMQX where GSDEPT = SUBSTRING(DEPTNO,1,7)) as GSMC from FK_LXRINFO where DEPTNO like @DEPTNO";
            SqlParameter[] paras = {
                                       new SqlParameter("@DEPTNO",DEPTNO+"%")
                                   };
            DataTable dt = DBHelper.GetDataSet(CommandType.Text, sql, paras);
            return dt;
        }
    }
}
