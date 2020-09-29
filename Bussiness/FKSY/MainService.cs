using Bussiness.SqlHelper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Bussiness.Modles;
namespace Bussiness.FKSY
{
    public class MainService
    {
        public int LOGIN(string name, string password, string MWRI)
        {
            byte[] result = Encoding.Default.GetBytes(password);    //tbPass为输入密码的文本框
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] output = md5.ComputeHash(result);
            password = BitConverter.ToString(output).Replace("-", "");  //tbMd5pass为输出加密文本的文本框
            //password = UserMd5(password);
            string sql = "SELECT * FROM HG_STAFF WHERE STAFFUSER=@STAFFUSER AND PWMD5=@PWMD5 AND ISACTIVENEW = '1'";
            SqlParameter[] paras = {
                                       new SqlParameter("@STAFFUSER",name),
                                       new SqlParameter("@PWMD5",password)
                                   };
            DataTable dt = DBHelper.GetDataSet(CommandType.Text, sql, paras);
            if (dt.Rows.Count == 0)
            {
                return 1;
            }
            sql = "select * from FK_MWQX where MWRI=@MWRI and STAFFID=@STAFFID";
            SqlParameter[] paras1 = {
                                        new SqlParameter("@MWRI",MWRI),
                                        new SqlParameter("@STAFFID",dt.Rows[0]["STAFFID"].ToString())
                                    };
            DataTable dt1 = DBHelper.GetDataSet(CommandType.Text, sql, paras1);
            if (dt1.Rows.Count > 0)
            {
                return 0;
            }
            else
            {
                return 2;
            }
        }

        public int CheckMWQX(int STAFFID, string MWRI)
        {
            string sql = "select * from FK_MWQX where MWRI=@MWRI and STAFFID=@STAFFID";
            SqlParameter[] paras = {
                                        new SqlParameter("@MWRI",MWRI),
                                        new SqlParameter("@STAFFID",STAFFID)
                                    };
            DataTable dt = DBHelper.GetDataSet(CommandType.Text, sql, paras);
            if (dt.Rows.Count > 0)
            {
                return 0;
            }
            else
            {
                return 2;
            }
        }

        public void Update_ChangePassWord(string name, string password)
        {
            byte[] result = Encoding.Default.GetBytes(password);    //tbPass为输入密码的文本框
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] output = md5.ComputeHash(result);
            password = BitConverter.ToString(output).Replace("-", "");  //tbMd5pass为输出加密文本的文本框
            IDAL objDAL = DALFactory.GetDBO();
            objDAL.Open();
            string sql = "update HG_STAFF set PWMD5=@PWMD5 where STAFFUSER=@STAFFUSER";
            Parameter[] paras = {
                                    new Parameter("@STAFFUSER",name),
                                    new Parameter("PWMD5",password)
                                };
            objDAL.ExecuteNonQuery(CommandType.Text, sql, paras);
            objDAL.Close();
        }

        public DataTable Select_FK_MWINFO(string RI)
        {
            string sql = "SELECT * FROM FK_MWINFO";
            if (RI != "")
            {
                sql = sql + " where RI=@RI";
            }
            SqlParameter[] paras = {
                                       new SqlParameter("@RI",RI)
                                   };
            DataTable dt = DBHelper.GetDataSet(CommandType.Text, sql, paras);
            return dt;
        }

        public DataTable Select_FK_MWINFO_GSDMRI(string GSDMRI)
        {
            string sql = "SELECT * FROM FK_MWINFO where GSDMRI=@GSDMRI and ISDELETE=0";
            SqlParameter[] paras = {
                                       new SqlParameter("@GSDMRI",GSDMRI)
                                   };
            DataTable dt = DBHelper.GetDataSet(CommandType.Text, sql, paras);
            return dt;
        }

        public modleMW Select_FK_MWINFO_modle(string RI)
        {
            string sql = "SELECT * FROM FK_MWINFO where RI=@RI and ISDELETE=0";
            SqlParameter[] paras = {
                                       new SqlParameter("@RI",RI)
                                   };
            DataTable dt = DBHelper.GetDataSet(CommandType.Text, sql, paras);
            modleMW modlemw = new modleMW();
            modlemw.RI = RI;
            modlemw.MWNO = dt.Rows[0]["MWNO"].ToString();
            modlemw.MWMS = dt.Rows[0]["MWMS"].ToString();
            modlemw.GSDM = dt.Rows[0]["GSDM"].ToString();
            modlemw.GSDMMS = dt.Rows[0]["GSDMMS"].ToString();
            modlemw.GSDMRI = dt.Rows[0]["GSDMRI"].ToString();
            return modlemw;
        }

        public DataTable Select_FK_DEPT()
        {
            string sql = "select * from FK_DEPT";
            SqlParameter[] paras = {
                                   };
            DataTable dt = DBHelper.GetDataSet(CommandType.Text, sql, paras);
            return dt;
        }

        public DataTable Select_FK_LXRINFO(string BFNAME, string DEPTNO)
        {
            string sql = "select * from FK_LXRINFO where 1=1";
            if (BFNAME != "")
            {
                sql = sql + " and BFNAME like @BFNAME";
            }
            if (DEPTNO != "A")
            {
                sql = sql + " and DEPTNO=@DEPTNO";
            }
            SqlParameter[] paras ={
                                      new SqlParameter("@BFNAME","%"+BFNAME+"%"),
                                      new SqlParameter("@DEPTNO",DEPTNO)
                                 };
            DataTable dt = DBHelper.GetDataSet(CommandType.Text, sql, paras);
            return dt;
        }

        public DataTable Select_FK_LXRINFO(string GH, string BFNAME, string DEPTNO)
        {
            string sql = "select *,(select GSJC from FK_GSDMQX where GSDEPT = SUBSTRING(DEPTNO,1,7)) as GSMC from FK_LXRINFO where 1=1";
            if (GH != "")
            {
                sql = sql + " and GH like @GH";
            }
            if (BFNAME != "")
            {
                sql = sql + " and BFNAME like @BFNAME";
            }
            if (DEPTNO != "A")
            {
                sql = sql + " and DEPTNO=@DEPTNO";
            }
            SqlParameter[] paras ={
                                      new SqlParameter("@GH","%"+GH+"%"),
                                      new SqlParameter("@BFNAME","%"+BFNAME+"%"),
                                      new SqlParameter("@DEPTNO",DEPTNO)
                                 };
            DataTable dt = DBHelper.GetDataSet(CommandType.Text, sql, paras);
            return dt;
        }

        public DataTable Select_FK_FKINFO(string GSDMRI)
        {
            string sql = "select top(1) * from FK_FKINFO where GSDMRI=@GSDMRI order by FKNO desc";
            SqlParameter[] paras = {
                                       new SqlParameter("@GSDMRI",GSDMRI)
                                   };
            DataTable dt = DBHelper.GetDataSet(CommandType.Text, sql, paras);
            return dt;
        }

        public DataTable Select_FK_FKINFO()
        {
            string sql = "select top(1) RI from FK_FKINFO order by RI desc";
            SqlParameter[] paras = {
                                   };
            DataTable dt = DBHelper.GetDataSet(CommandType.Text, sql, paras);
            return dt;
        }

        public DataTable Select_FK_ZJLX()
        {
            string sql = "select * from FK_ZJLX where ISDELETE=0";
            SqlParameter[] paras = {
                                   };
            DataTable dt = DBHelper.GetDataSet(CommandType.Text, sql, paras);
            return dt;
        }

        public DataTable Select_FK_sysinfo(string GSDM, string DATE)
        {
            string sql = "select * from FK_sysinfo where GSDM=@GSDM and DATE=@DATE";
            SqlParameter[] paras = {
                                       new SqlParameter("@GSDM",GSDM),
                                       new SqlParameter("@DATE",DATE)
                                   };
            DataTable dt = DBHelper.GetDataSet(CommandType.Text, sql, paras);
            return dt;
        }

        public void UPDATE_FK_sysinfo(string GSDM, string DATE, int NUM)
        {
            IDAL objDAL = DALFactory.GetDBO();
            objDAL.Open();
            string sql = "update FK_sysinfo set NUM=@NUM where GSDM=@GSDM and DATE=@DATE";
            Parameter[] paras = {
                                    new Parameter("@GSDM",GSDM),
                                    new Parameter("@DATE",DATE),
                                    new Parameter("@NUM",NUM)
                                };
            objDAL.ExecuteNonQuery(CommandType.Text, sql, paras);
            objDAL.Close();
        }

        public void INSERT_FK_sysinfo(string GSDM, string DATE, int NUM)
        {
            IDAL objDAL = DALFactory.GetDBO();
            objDAL.Open();
            string sql = "insert into FK_sysinfo(GSDM,DATE,NUM) values(@GSDM,@DATE,@NUM)";
            Parameter[] paras = {
                                    new Parameter("@GSDM",GSDM),
                                    new Parameter("@DATE",DATE),
                                    new Parameter("@NUM",NUM)
                                };
            objDAL.ExecuteNonQuery(CommandType.Text, sql, paras);
            objDAL.Close();
        }

        public MODLESTAFF Select_HG_STAFF(string STAFFUSER)
        {
            string sql = "select * from HG_STAFF where STAFFUSER=@STAFFUSER";
            SqlParameter[] paras = {
                                       new SqlParameter("@STAFFUSER",STAFFUSER)
                                   };
            DataTable dt = DBHelper.GetDataSet(CommandType.Text, sql, paras);
            MODLESTAFF modleSTAFF = new MODLESTAFF();
            modleSTAFF.STAFFID = dt.Rows[0]["STAFFID"].ToString();
            modleSTAFF.STAFFNAME = dt.Rows[0]["STAFFNAME"].ToString();
            modleSTAFF.STAFFNO = dt.Rows[0]["STAFFNO"].ToString();
            modleSTAFF.STAFFUSER = dt.Rows[0]["STAFFUSER"].ToString();
            return modleSTAFF;
        }

        public void INSERT_FK_FKINFO(MODLEFKINFO modleFKINFO)
        {
            IDAL objDAL = DALFactory.GetDBO();
            objDAL.Open();
            string sql = "insert into FK_FKINFO(FKNO,TM,FKNAME,FKSEX,FKMZ,FKCSRQ,FKZJLXRI,FKZJLX,FKZJHM,FKADD,FKDW,FKCPH,FKZH,FKZLX,FKZLXRI,XDH,FXH,SFBM,SFNAME,SFGH,SFRYDH,FKLFSY,FKLFSYMX,FKSUM,FKISHAVESSWP,FKSSWP,JRMWRI,JRMW,LFTIME,ISLK,GSDMRI,GSDM,GSDMMS,JRDJR,JRDJRRI,ISDELETE,FRI,BZ,LFRQ)";
            sql = sql + " values(@FKNO,@TM,@FKNAME,@FKSEX,@FKMZ,@FKCSRQ,@FKZJLXRI,@FKZJLX,@FKZJHM,@FKADD,@FKDW,@FKCPH,@FKZH,@FKZLX,@FKZLXRI,@XDH,@FXH,@SFBM,@SFNAME,@SFGH,@SFRYDH,@FKLFSY,@FKLFSYMX,@FKSUM,@FKISHAVESSWP,@FKSSWP,@JRMWRI,@JRMW,GETDATE(),@ISLK,@GSDMRI,@GSDM,@GSDMMS,@JRDJR,@JRDJRRI,@ISDELETE,@FRI,@BZ,CONVERT(varchar(100), GETDATE(), 23))";
            Parameter[] paras = {
                                    new Parameter("@FKNO",modleFKINFO.FKNO),
                                    new Parameter("@TM",modleFKINFO.TM),
                                    new Parameter("@FKNAME",modleFKINFO.FKNAME),
                                    new Parameter("@FKSEX",modleFKINFO.FKSEX),
                                    new Parameter("@FKMZ",modleFKINFO.FKMZ),
                                    new Parameter("@FKCSRQ",modleFKINFO.FKCSRQ),
                                    new Parameter("@FKZJLXRI",modleFKINFO.FKZJLXRI),
                                    new Parameter("@FKZJLX",modleFKINFO.FKZJLX),
                                    new Parameter("@FKZJHM",modleFKINFO.FKZJHM),
                                    new Parameter("@FKADD",modleFKINFO.FKADD),
                                    new Parameter("@FKDW",modleFKINFO.FKDW),
                                    new Parameter("@FKCPH",modleFKINFO.FKCPH),
                                    new Parameter("@FKZH",modleFKINFO.FKZH),
                                    new Parameter("@FKZLX",modleFKINFO.FKZLX),
                                    new Parameter("@FKZLXRI",modleFKINFO.FKZLXRI),
                                    new Parameter("@XDH",modleFKINFO.XDH),
                                    new Parameter("@FXH",modleFKINFO.FXH),
                                    new Parameter("@SFBM",modleFKINFO.SFBM),
                                    new Parameter("@SFNAME",modleFKINFO.SFNAME),
                                    new Parameter("@SFGH",modleFKINFO.SFGH),
                                    new Parameter("@SFRYDH",modleFKINFO.SFRYDH),
                                    new Parameter("@FKLFSY",modleFKINFO.FKLFSY),
                                    new Parameter("@FKLFSYMX",modleFKINFO.FKLFSYMX),
                                    new Parameter("@FKSUM",modleFKINFO.FKSUM),
                                    new Parameter("@FKISHAVESSWP",modleFKINFO.FKISHAVESSWP),
                                    new Parameter("@FKSSWP",modleFKINFO.FKSSWP),
                                    new Parameter("@JRMWRI",modleFKINFO.JRMWRI),
                                    new Parameter("@JRMW",modleFKINFO.JRMW),
                                    new Parameter("@LFTIME",modleFKINFO.LFTIME),
                                    new Parameter("@ISLK",modleFKINFO.ISLK),
                                    new Parameter("@GSDMRI",modleFKINFO.GSDMRI),
                                    new Parameter("@GSDM",modleFKINFO.GSDM),
                                    new Parameter("@GSDMMS",modleFKINFO.GSDMMS),
                                    new Parameter("@JRDJR",modleFKINFO.JRDJR),
                                    new Parameter("@JRDJRRI",modleFKINFO.JRDJRRI),
                                    new Parameter("@ISDELETE",modleFKINFO.ISDELETE),
                                    new Parameter("@FRI",modleFKINFO.FRI),
                                    new Parameter("@BZ",modleFKINFO.BZ),
                                    new Parameter("@LFRQ",modleFKINFO.LFRQ)
                                };
            objDAL.ExecuteNonQuery(CommandType.Text, sql, paras);
            objDAL.Close();
        }


        public DataTable Select_FK_FKINFO_TM(string TM)
        {
            string sql = "select * from FK_FKINFO where TM=@TM and ISDELETE=0";
            SqlParameter[] paras = {
                                       new SqlParameter("@TM",TM)
                                   };
            DataTable dt = DBHelper.GetDataSet(CommandType.Text, sql, paras);
            return dt;
        }

        public void Update_FK_FKINFO(string LKMWRI, string LKMW, string LKTIME, string LKDJR, string LKDJRRI, string RI)
        {
            IDAL objDAL = DALFactory.GetDBO();
            objDAL.Open();
            string sql = "update FK_FKINFO set ISLK=1,LKMWRI=@LKMWRI,LKMW=@LKMW,LKTIME=GETDATE(),LKDJR=@LKDJR,LKDJRRI=@LKDJRRI where RI=@RI";
            Parameter[] paras = {
                                    new Parameter("@LKMWRI",LKMWRI),
                                    new Parameter("@LKMW",LKMW),
                                    new Parameter("@LKTIME",LKTIME),
                                    new Parameter("@LKDJR",LKDJR),
                                    new Parameter("@LKDJRRI",LKDJRRI),
                                    new Parameter("@RI",RI)
                                };
            objDAL.ExecuteNonQuery(CommandType.Text, sql, paras);
            objDAL.Close();
        }

        public DataTable Select_FK_FKINFO_ZFRY(string GSDMRI, string JRMWRI)
        {
            string sql = "select * from FK_FKINFO where ISLK=0 and ISDELETE=0 ";
            if (JRMWRI == "0")
            {
                sql = sql + " and GSDMRI=@GSDMRI ";
            }
            else
            {
                sql = sql + " and JRMWRI=@JRMWRI ";
            }
            sql = sql + " order by RI DESC";
            SqlParameter[] paras = {
                                       new SqlParameter("@GSDMRI",GSDMRI),
                                       new SqlParameter("@JRMWRI",JRMWRI)
                                   };
            DataTable dt = DBHelper.GetDataSet(CommandType.Text, sql, paras);
            return dt;
        }

        public void DELETE_FK_FKINFO(string RI)
        {
            IDAL objDAL = DALFactory.GetDBO();
            objDAL.Open();
            string sql = "update FK_FKINFO set ISDELETE=1 where RI=@RI";
            Parameter[] paras = {
                                    new Parameter("@RI",RI)
                                };
            objDAL.ExecuteNonQuery(CommandType.Text, sql, paras);
            objDAL.Close();
        }

        public void Insert_FK_IMAGE(string FKRI, byte[] FKIMAGE, byte[] FKIMAGEB)
        {
            IDAL objDAL = DALFactory.GetDBO();
            objDAL.Open();
            string sql = "insert into FK_IMAGE (FKRI,FKIMAGE,FKIMAGEB) values(@FKRI,@FKIMAGE,@FKIMAGEB)";
            Parameter[] paras = {
                                    new Parameter("@FKRI",FKRI),
                                    new Parameter("@FKIMAGE",FKIMAGE),
                                    new Parameter("@FKIMAGEB",FKIMAGEB)
                                };
            objDAL.ExecuteNonQuery(CommandType.Text, sql, paras);
            objDAL.Close();
        }
        public void Insert_FK_IMAGE(string FKRI, byte[] FKIMAGE)
        {
            IDAL objDAL = DALFactory.GetDBO();
            objDAL.Open();
            string sql = "insert into FK_IMAGE (FKRI,FKIMAGE) values(@FKRI,@FKIMAGE)";
            Parameter[] paras = {
                                    new Parameter("@FKRI",FKRI),
                                    new Parameter("@FKIMAGE",FKIMAGE)
                                };
            objDAL.ExecuteNonQuery(CommandType.Text, sql, paras);
            objDAL.Close();
        }

        public DataTable Select_FK_FKINFO_FKZJHM(string FKZJHM)
        {
            string sql = "select top(1) * from FK_FKINFO where FKZJHM=@FKZJHM order by FKNO desc";
            SqlParameter[] paras = {
                                       new SqlParameter("@FKZJHM",FKZJHM)
                                   };
            DataTable dt = DBHelper.GetDataSet(CommandType.Text, sql, paras);
            return dt;
        }

        public DataTable Select_FK_BL(string BLNAME)
        {
            string sql = "select * from FK_BL where BLNAME=@BLNAME";
            SqlParameter[] paras = {
                                       new SqlParameter("@BLNAME",BLNAME)
                                   };
            DataTable dt = DBHelper.GetDataSet(CommandType.Text, sql, paras);
            return dt;
        }

        public DataTable Select_FK_LXRINFO_BFNAME()
        {
            string sql = "select RI,BFNAME,DEPT,(select GSJC from FK_GSDMQX where GSDEPT = SUBSTRING(DEPTNO,1,7)) as GSMC from FK_LXRINFO where 1=1 order by BFNAME";
            SqlParameter[] paras = {
                                   };
            DataTable dt = DBHelper.GetDataSet(CommandType.Text, sql, paras);
            return dt;
        }

        public DataTable Select_FK_LXRINFO_RI(string RI)
        {
            string sql = "select *,(select GSJC from FK_GSDMQX where GSDEPT = SUBSTRING(DEPTNO,1,7)) as GSMC from FK_LXRINFO where RI=@RI";
            SqlParameter[] paras = {
                                       new SqlParameter("@RI",RI)
                                   };
            DataTable dt = DBHelper.GetDataSet(CommandType.Text, sql, paras);
            return dt;
        }

        public DataTable Select_FK_INFO_ISLK()
        {
            string sql = "select * from FK_FKINFO where LFTIME < '2017-01-01' and ISLK = 0";
            DataTable dt = DBHelper.GetDataSet(CommandType.Text, sql);
            return dt;
        }
    }
}
