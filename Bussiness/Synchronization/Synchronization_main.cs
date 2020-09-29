using Bussiness.SqlHelper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Bussiness.Synchronization
{
    public class Synchronization_main
    {
        public DataTable Select_Lala_Employee()
        {
            string sql = "select * from Lala_Employee where 在职状态='在职在岗' order by id";
            SqlParameter[] paras = {
                                       
                                   };
            DataTable dt = DBHelper.GetDataSet("83", CommandType.Text, sql, paras);
            return dt;
        }

        public void Insert_FK_LXRINFO(string GH, string BFNAME, string BFNAMEPY, string SEX, string DEPT, string DEPTNO, string DH, string ZZZT)
        {
            IDAL objDAL = DALFactory.GetDBO();
            objDAL.Open();
            string sql = "insert into FK_LXRINFO(GH,BFNAME,BFNAMEPY,SEX,DEPT,DEPTNO,DH,ZZZT,GSDMID,GSDM,GSDMMS) values (@GH,@BFNAME,@BFNAMEPY,@SEX,@DEPT,@DEPTNO,@DH,@ZZZT,'1','1000','中银（宁波）电池有限公司')";
            Parameter[] paras = {
                                       new Parameter("@GH",GH),
                                       new Parameter("@BFNAME",BFNAME),
                                       new Parameter("@BFNAMEPY",BFNAMEPY),
                                       new Parameter("@SEX",SEX),
                                       new Parameter("@DEPT",DEPT),
                                       new Parameter("@DEPTNO",DEPTNO),
                                       new Parameter("@DH",DH),
                                       new Parameter("@ZZZT",ZZZT)
                                };
            objDAL.ExecuteNonQuery(CommandType.Text, sql, paras);
            objDAL.Close();
        }

        public void DELETE_FK_LXRINFO()
        {
            IDAL objDAL = DALFactory.GetDBO();
            objDAL.Open();
            string sql = "delete from FK_LXRINFO";
            Parameter[] paras = {
                                };
            objDAL.ExecuteNonQuery(CommandType.Text, sql, paras);
            objDAL.Close();
        }
    }
}
