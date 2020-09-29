using Bussiness.SqlHelper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Bussiness.STSY
{
    public class ZDService
    {
        public DataTable SELECT_CRM_ZDRIGHT_STAFFID(string STAFFID)
        {
            string sql = "SELECT A.XMTPYEID, A.XMTYPE FROM CRM_XMTYPE A,CRM_ZDRIGHT B WHERE A.XMTPYEID=B.XMTPYEID AND B.STAFFID=@STAFFID order by A.XMTPYEID";
            SqlParameter[] paras = {
                                     new SqlParameter("@STAFFID",STAFFID)
                                 };
            DataTable dt = DBHelper.GetDataSet(CommandType.Text, sql, paras);
            return dt;
        }

        /// <summary>
        /// 获取部门
        /// </summary>
        /// <returns></returns>
        public DataTable SELECT_CRM_DEPT()
        {
            string sql = " SELECT DEPTID,DEPTNAME FROM CRM_DEPT ORDER BY DEPTID";
            DataTable dt = DBHelper.GetDataSet(CommandType.Text, sql);
            return dt;
        }

        public DataTable SELECT_QX(string STAFFID)
        {
            string sql = "SELECT A.XMTPYEID, A.XMTYPE FROM CRM_XMTYPE A,CRM_ZDRIGHT B WHERE A.XMTPYEID=B.XMTPYEID AND B.STAFFID=@STAFFID order by A.XMTPYEID";
            SqlParameter[] paras = {
                                     new SqlParameter("@STAFFID",STAFFID)
                                 };
            DataTable dt = DBHelper.GetDataSet(CommandType.Text, sql, paras);
            return dt;
        }
        /// <summary>
        /// 获取联系人员
        /// </summary>
        /// <returns></returns>
        public DataTable SELECT_CRM_ZDLXR()
        {
            string sql = "SELECT LXRID, LXR, LXDH, LXDES FROM CRM_ZDLXR ORDER BY LXR";
            DataTable dt = DBHelper.GetDataSet(CommandType.Text, sql);
            return dt;
        }

        public DataTable SELECT_ZDXM(string STAFFID, string LB, string DEPTNAME, string STAFFNAME, string ZDMEMO, string XMDATES, string XMDATEE, string XMADD, string XMTPYEID, string XMMEMO)
        {
            string sql = "SELECT A.ZDID, A.ZDVER, A.ZDSTATUS, A.ISACTIVE, A.ISUPDATE, A.DEPTNAME, A.STAFFNAME, A.STAFFMOBLE, A.fillID, A.fillName, A.fillTime,A.ZDMEMO,B.ZDXMID, ";
            sql = sql + "B.XMTPYEID,B.XMTYPE,B.XMDATE,B.XMTIME,B.XMADD,B.XMNUM,B.ISCONFIG,B.ISDELETE,B.ISEND,B.XMMEMO FROM CRM_ZD A,CRM_ZDXM B,CRM_ZDRIGHT C WHERE A.ZDID=B.ZDID AND ";
            sql = sql + "A.ZDVER=B.ZDVER AND B.XMTPYEID=C.XMTPYEID ";
            if (STAFFID != "")
            {
                sql = sql + "AND C.STAFFID=@STAFFID ";
            }
            if (LB == "0")
            {
                sql = sql + "and A.ZDSTATUS=0 ";
            }
            else if (LB == "1")
            {
                sql = sql + "and A.ZDSTATUS=1 and B.ISCONFIG=0 ";
            }
            else if (LB == "2")
            {
                sql = sql + "and A.ZDSTATUS=1 and B.ISEND=0 and B.ISCONFIG=1 ";
            }
            else if (LB == "3")
            {
                sql = sql + "and A.ZDSTATUS=1 and B.ISEND=1 and B.ISCONFIG=1 ";
            }
            //else if (LB == "4")
            //{
            //    sql = sql + "and A.ZDSTATUS=1 and B.ISEND=0 and B.ISCONFIG=1 ";
            //}
            if (DEPTNAME != "")
            {
                sql = sql + "AND A.DEPTNAME=@DEPTNAME ";
            }
            if (STAFFNAME != "")
            {
                sql = sql + "AND A.STAFFNAME=@STAFFNAME ";
            }
            if (ZDMEMO != "")
            {
                sql = sql + "AND A.ZDMEMO LIKE @ZDMEMO ";
            }
            if (XMDATES != "")
            {
                sql = sql + "AND B.XMDATE>=@XMDATES ";
            }
            if (XMDATEE != "")
            {
                sql = sql + "AND B.XMDATE<=@XMDATEE ";
            }
            if (XMADD != "")
            {
                sql = sql + "AND B.XMADD=@XMADD ";
            }
            if (XMTPYEID != "0")
            {
                sql = sql + "AND B.XMTPYEID=@XMTPYEID";
            }
            if (XMMEMO != "")
            {
                sql = sql + "AND B.XMMEMO=@XMMEMO";
            }
            SqlParameter[] paras =
            {
                new SqlParameter("@STAFFID",STAFFID),
                new SqlParameter("@DEPTNAME",DEPTNAME),
                new SqlParameter("@STAFFNAME",STAFFNAME),
                new SqlParameter("@ZDMEMO","%"+ZDMEMO+"%"),
                new SqlParameter("@XMDATES",XMDATES),
                new SqlParameter("@XMDATEE",XMDATEE),
                new SqlParameter("@XMADD",XMADD),
                new SqlParameter("@XMTPYEID",XMTPYEID),
                new SqlParameter("@XMMEMO",XMMEMO)
            };
            DataTable dt = DBHelper.GetDataSet(CommandType.Text, sql, paras);
            return dt;
        }

        /// <summary>
        /// 地点
        /// </summary>
        /// <returns></returns>
        public DataTable SELECT_CRM_ZDADD()
        {
            string sql = "SELECT ADDID, ADDNAME, ADDMEMO FROM CRM_ZDADD ORDER BY ADDNAME";
            DataTable dt = DBHelper.GetDataSet(CommandType.Text, sql);
            return dt;
        }

        public DataTable SELECT_CRM_ZD_RI()
        {
            string sql = "SELECT MAX(ZDID) ZDID FROM CRM_ZD";
            DataTable dt = DBHelper.GetDataSet(CommandType.Text, sql);
            return dt;
        }

        public void insertCRM_ZD(string ZDID, string ZDVER, string ZDSTATUS, string ISACTIVE, string ISUPDATE, string DEPTNAME, string STAFFNAME, string STAFFMOBLE, string fillID, string fillName, string fillTime, string ZDMEMO)
        {
            IDAL objDAL = DALFactory.GetDBO();
            objDAL.Open();
            string sql = "insert into CRM_ZD(ZDID,ZDVER,ZDSTATUS,ISACTIVE,ISUPDATE,DEPTNAME,STAFFNAME,STAFFMOBLE,fillID,fillName,fillTime,ZDMEMO) values (@ZDID,@ZDVER,@ZDSTATUS,@ISACTIVE,@ISUPDATE,@DEPTNAME,@STAFFNAME,@STAFFMOBLE,@fillID,@fillName,@fillTime,@ZDMEMO)";
            Parameter[] paras = {
                                    new Parameter("@ZDID",ZDID),
                                    new Parameter("@ZDVER",ZDVER),
                                    new Parameter("@ZDSTATUS",ZDSTATUS),
                                    new Parameter("@ISACTIVE",ISACTIVE),
                                    new Parameter("@ISUPDATE",ISUPDATE),
                                    new Parameter("@DEPTNAME",DEPTNAME),
                                    new Parameter("@STAFFNAME",STAFFNAME),
                                    new Parameter("@STAFFMOBLE",STAFFMOBLE),
                                    new Parameter("@fillID",fillID),
                                    new Parameter("@fillName",fillName),
                                    new Parameter("@fillTime",fillTime),
                                    new Parameter("@ZDMEMO",ZDMEMO)
                                };
            objDAL.ExecuteNonQuery(CommandType.Text, sql, paras);
            objDAL.Close();
        }

        public void DELETE_CRM_ZDXM(string ZDID)
        {
            IDAL objDAL = DALFactory.GetDBO();
            objDAL.Open();
            string sql = "DELETE FROM CRM_ZDXM WHERE ZDID=@ZDID";
            Parameter[] paras = {
                                    new Parameter("@ZDID",ZDID)
                                };
            objDAL.ExecuteNonQuery(CommandType.Text, sql, paras);
            objDAL.Close();
        }

        public void DELETE_CRM_ZD(string ZDID)
        {
            IDAL objDAL = DALFactory.GetDBO();
            objDAL.Open();
            string sql = "DELETE FROM CRM_ZD WHERE ZDID=@ZDID";
            Parameter[] paras = {
                                    new Parameter("@ZDID",ZDID)
                                };
            objDAL.ExecuteNonQuery(CommandType.Text, sql, paras);
            objDAL.Close();
        }

        public void insertCRM_ZDXM(string ZDXMID, string ZDID, string ZDVER, string XMTPYEID, string XMTYPE, string XMDATE, string XMTIME, string XMADD, string XMNUM, string ISCONFIG, string ISDELETE, string ISEND, string XMMEMO)
        {
            IDAL objDAL = DALFactory.GetDBO();
            objDAL.Open();
            string sql = "insert into CRM_ZDXM(ZDXMID,ZDID,ZDVER,XMTPYEID,XMTYPE,XMDATE,XMTIME,XMADD,XMNUM,ISCONFIG,ISDELETE,ISEND,XMMEMO) values (@ZDXMID,@ZDID,@ZDVER,@XMTPYEID,@XMTYPE,@XMDATE,@XMTIME,@XMADD,@XMNUM,@ISCONFIG,@ISDELETE,@ISEND,@XMMEMO)";
            Parameter[] paras = {
                                    new Parameter("@ZDXMID",ZDXMID),
                                    new Parameter("@ZDID",ZDID),
                                    new Parameter("@ZDVER",ZDVER),
                                    new Parameter("@XMTPYEID",XMTPYEID),
                                    new Parameter("@XMTYPE",XMTYPE),
                                    new Parameter("@XMDATE",XMDATE),
                                    new Parameter("@XMTIME",XMTIME),
                                    new Parameter("@XMADD",XMADD),
                                    new Parameter("@XMNUM",XMNUM),
                                    new Parameter("@ISCONFIG",ISCONFIG),
                                    new Parameter("@ISDELETE",ISDELETE),
                                    new Parameter("@ISEND",ISEND),
                                    new Parameter("@XMMEMO",XMMEMO)
                                };
            objDAL.ExecuteNonQuery(CommandType.Text, sql, paras);
            objDAL.Close();
        }

        public DataTable SELECT_CRM_ZDXM_QX(string ZDID, string STAFFID)
        {
            string sql = "SELECT A.ZDXMID,A.ZDID,A.ZDVER,A.XMTPYEID,A.XMTYPE,A.XMDATE,A.XMTIME,A.XMADD,A.XMNUM,A.ISCONFIG,A.ISDELETE,A.ISEND,A.XMMEMO,A.XMD,A.XMB,A.XMQ FROM CRM_ZDXM A,CRM_ZDRIGHT B WHERE A.XMTPYEID=B.XMTPYEID AND A.ZDID=@ZDID AND B.STAFFID=@STAFFID ORDER BY A.ZDXMID";
            SqlParameter[] paras = {
                                    new SqlParameter("@ZDID",ZDID),
                                    new SqlParameter("@STAFFID",STAFFID)
                                };
            DataTable dt = DBHelper.GetDataSet(CommandType.Text, sql, paras);
            return dt;
        }

        public DataTable SELECT_CRM_ZD(string ZDID)
        {
            string sql = "SELECT * FROM CRM_ZD WHERE ZDID=@ZDID";
            SqlParameter[] paras = {
                                    new SqlParameter("@ZDID",ZDID)
                                };
            DataTable dt = DBHelper.GetDataSet(CommandType.Text, sql, paras);
            return dt;
        }

        public void UPDATE_CRM_ZD(string ZDSTATUS, string ZDID)
        {
            IDAL objDAL = DALFactory.GetDBO();
            objDAL.Open();
            string sql = "update CRM_ZD set ZDSTATUS=@ZDSTATUS WHERE ZDID=@ZDID";
            Parameter[] paras = {
                                    new Parameter("@ZDSTATUS",ZDSTATUS),
                                    new Parameter("@ZDID",ZDID)
                                };
            objDAL.ExecuteNonQuery(CommandType.Text, sql, paras);
            objDAL.Close();
        }

        /// <summary>
        /// 招待审核
        /// </summary>
        /// <param name="STAFFID"></param>
        /// <param name="LB"></param>
        /// <param name="DEPTNAME"></param>
        /// <param name="STAFFNAME"></param>
        /// <param name="ZDMEMO"></param>
        /// <param name="XMDATES"></param>
        /// <param name="XMDATEE"></param>
        /// <param name="XMADD"></param>
        /// <param name="XMTPYEID"></param>
        /// <param name="XMMEMO"></param>
        /// <returns></returns>
        public DataTable SELECT_ZDXM1(string STAFFID, string LB, string DEPTNAME, string STAFFNAME, string ZDMEMO, string XMDATES, string XMDATEE, string XMADD, string XMTPYEID, string XMMEMO)
        {
            string sql = "SELECT A.ZDID,A.ZDVER,A.ZDSTATUS,A.ISACTIVE,A.ISUPDATE,A.DEPTNAME,A.STAFFNAME,A.STAFFMOBLE,A.fillID,A.fillName,A.fillTime,A.ZDMEMO,B.ZDXMID, ";
            sql = sql + "B.XMTPYEID,B.XMTYPE,B.XMDATE,B.XMTIME,B.XMADD,B.XMNUM,B.ISCONFIG,B.ISDELETE,B.ISEND,B.XMMEMO,B.XMD,B.XMB,B.XMQ FROM CRM_ZD A,CRM_ZDXM B,CRM_ZDRIGHT C WHERE A.ZDID=B.ZDID AND ";
            sql = sql + "A.ZDVER=B.ZDVER AND B.XMTPYEID=C.XMTPYEID ";
            if (STAFFID != "")
            {
                sql = sql + "AND C.STAFFID=@STAFFID ";
            }
            if (LB == "0")
            {
                sql = sql + "and A.ZDSTATUS=1 and B.ISCONFIG=0 ";
            }
            else if (LB == "1")
            {
                sql = sql + "and A.ZDSTATUS=1 and B.ISEND=0 and B.ISCONFIG=1 ";
            }
            else if (LB == "2")
            {
                sql = sql + "and A.ZDSTATUS=1 and B.ISEND=1 and B.ISCONFIG=1 ";
            }
            else if (LB == "3")
            {
                sql = sql + "and A.ZDSTATUS=1 ";
            }
            if (DEPTNAME != "")
            {
                sql = sql + "AND A.DEPTNAME=@DEPTNAME ";
            }
            if (STAFFNAME != "")
            {
                sql = sql + "AND A.STAFFNAME=@STAFFNAME ";
            }
            if (ZDMEMO != "")
            {
                sql = sql + "AND A.ZDMEMO LIKE @ZDMEMO ";
            }
            if (XMDATES != "")
            {
                sql = sql + "AND B.XMDATE>=@XMDATES ";
            }
            if (XMDATEE != "")
            {
                sql = sql + "AND B.XMDATE<=@XMDATEE ";
            }
            if (XMADD != "")
            {
                sql = sql + "AND B.XMADD=@XMADD ";
            }
            if (XMTPYEID != "0")
            {
                sql = sql + "AND B.XMTPYEID=@XMTPYEID ";
            }
            if (XMMEMO != "")
            {
                sql = sql + "AND B.XMMEMO=@XMMEMO ";
            }
            sql = sql + "order by A.ZDID,B.ZDXMID ";
            SqlParameter[] paras =
            {
                new SqlParameter("@STAFFID",STAFFID),
                new SqlParameter("@DEPTNAME",DEPTNAME),
                new SqlParameter("@STAFFNAME",STAFFNAME),
                new SqlParameter("@ZDMEMO","%"+ZDMEMO+"%"),
                new SqlParameter("@XMDATES",XMDATES),
                new SqlParameter("@XMDATEE",XMDATEE),
                new SqlParameter("@XMADD",XMADD),
                new SqlParameter("@XMTPYEID",XMTPYEID),
                new SqlParameter("@XMMEMO",XMMEMO)
            };
            DataTable dt = DBHelper.GetDataSet(CommandType.Text, sql, paras);
            return dt;
        }

        public void UPDATE_CRM_CRM_ZDXM_QR(string ISCONFIG, string ZDID, string STAFFID)
        {
            IDAL objDAL = DALFactory.GetDBO();
            objDAL.Open();
            string sql = "UPDATE CRM_ZDXM SET ISCONFIG=@ISCONFIG WHERE ZDID=@ZDID AND XMTPYEID IN (SELECT XMTPYEID FROM CRM_ZDRIGHT WHERE STAFFID=@STAFFID)";
            Parameter[] paras = {
                                    new Parameter("@ISCONFIG",ISCONFIG),
                                    new Parameter("@ZDID",ZDID),
                                    new Parameter("@STAFFID",STAFFID)
                                };
            objDAL.ExecuteNonQuery(CommandType.Text, sql, paras);
            objDAL.Close();
        }

        public void UPDATE_CRM_CRM_ZD_TH(string ZDSTATUS, string ISCONFIG, string ZDID)
        {
            IDAL objDAL = DALFactory.GetDBO();
            objDAL.Open();
            string sql = "update CRM_ZD set ZDSTATUS=@ZDSTATUS,ISCONFIG=@ISCONFIG WHERE ZDID=@ZDID";
            Parameter[] paras = {
                                    new Parameter("@ZDSTATUS",ZDSTATUS),
                                    new Parameter("@ISCONFIG",ISCONFIG),
                                    new Parameter("@ZDID",ZDID)
                                };
            objDAL.ExecuteNonQuery(CommandType.Text, sql, paras);
            objDAL.Close();
        }

        public void UPDATE_CRM_CRM_ZDXM_SAVE(string XMD, string XMB, string XMQ, string ZDID, string ZDXMID)
        {
            IDAL objDAL = DALFactory.GetDBO();
            objDAL.Open();
            string sql = "UPDATE CRM_ZDXM SET XMD=@XMD,XMB=@XMB,XMQ=@XMQ WHERE ZDID=@ZDID AND ZDXMID=@ZDXMID";
            Parameter[] paras = {
                                    new Parameter("@XMD",XMD),
                                    new Parameter("@XMB",XMB),
                                    new Parameter("@XMQ",XMQ),
                                    new Parameter("@ZDID",ZDID),
                                    new Parameter("@ZDXMID",ZDXMID)
                                };
            objDAL.ExecuteNonQuery(CommandType.Text, sql, paras);
            objDAL.Close();
        }

        public void UPDATE_CRM_CRM_ZDXM_WC(string ISEND, string ZDID, string STAFFID)
        {
            IDAL objDAL = DALFactory.GetDBO();
            objDAL.Open();
            string sql = "UPDATE CRM_ZDXM SET ISEND=@ISEND  WHERE ZDID=@ZDID AND XMTPYEID IN (SELECT XMTPYEID FROM CRM_ZDRIGHT WHERE STAFFID=@STAFFID)";
            Parameter[] paras = {
                                    new Parameter("@ISEND",ISEND),
                                    new Parameter("@ZDID",ZDID),
                                    new Parameter("@STAFFID",STAFFID)
                                };
            objDAL.ExecuteNonQuery(CommandType.Text, sql, paras);
            objDAL.Close();
        }


        public DataTable SELECT_HG_RIGHT()
        {
            string sql = " SELECT C.STAFFID,C.STAFFNAME,C.STAFFUSER  FROM HG_RIGHT A,HG_STAFFRIGHT B,HG_STAFF C WHERE A.RIGHTID=B.RIGHTID AND B.STAFFID=C.STAFFID AND A.RIGHTTAG IN('231','232','233') UNION SELECT C.STAFFID,C.STAFFNAME,C.STAFFUSER  FROM HG_RIGHT A,HG_DEPTRIGHT B,HG_STAFF C WHERE A.RIGHTID=B.RIGHTID AND B.DEPTID=C.DEPTID AND A.RIGHTTAG IN('231','232','233') UNION SELECT D.STAFFID,D.STAFFNAME,D.STAFFUSER  FROM HG_RIGHT A,HG_ROLERIGHT B,HG_STAFFROLE C,HG_STAFF D WHERE A.RIGHTID=B.RIGHTID AND B.ROLEID=C.ROLEID AND C.STAFFID=D.STAFFID AND A.RIGHTTAG IN('231','232','233')";
            DataTable dt = DBHelper.GetDataSet(CommandType.Text, sql);
            return dt;
        }

        public DataTable SELECT_HG_RIGHT(string STAFFID)
        {
            string sql = "SELECT * FROM CRM_ZDRIGHT where STAFFID=@STAFFID";
            SqlParameter[] paras =
            {
                new SqlParameter("@STAFFID",STAFFID)
            };
            DataTable dt = DBHelper.GetDataSet(CommandType.Text, sql, paras);
            return dt;
        }

        public void DELETE_CRM_ZDRIGHT(string STAFFID)
        {
            IDAL objDAL = DALFactory.GetDBO();
            objDAL.Open();
            string sql = "delete from CRM_ZDRIGHT where STAFFID=@STAFFID";
            Parameter[] paras = {
                                    new Parameter("@STAFFID",STAFFID)
                                };
            objDAL.ExecuteNonQuery(CommandType.Text, sql, paras);
            objDAL.Close();
        }

        public void INSERT_CRM_ZDRIGHT(string STAFFID, string XMTPYEID)
        {
            IDAL objDAL = DALFactory.GetDBO();
            objDAL.Open();
            string sql = "INSERT INTO CRM_ZDRIGHT(STAFFID,XMTPYEID) VALUES (@STAFFID,@XMTPYEID)";
            Parameter[] paras = {
                                    new Parameter("@STAFFID",STAFFID),
                                    new Parameter("@XMTPYEID",XMTPYEID)
                                };
            objDAL.ExecuteNonQuery(CommandType.Text, sql, paras);
            objDAL.Close();
        }

        public DataTable SELECT_CRM_DEPT_ED()
        {
            string sql = " SELECT * FROM CRM_DEPT ORDER BY DEPTID";
            DataTable dt = DBHelper.GetDataSet(CommandType.Text, sql);
            return dt;
        }

        public void INSERT_CRM_DEPT(string DEPTNAME, string DEPTMEMO)
        {
            string sql1 = "SELECT SYSINT FROM HG_SYSINS where SYSNAME = 'CRM_DEPT'";
            DataTable dt = DBHelper.GetDataSet(CommandType.Text, sql1);
            int s = Convert.ToInt32(dt.Rows[0][0].ToString()) + 1;

            IDAL objDAL = DALFactory.GetDBO();
            objDAL.Open();
            string sql = "INSERT INTO CRM_DEPT(DEPTID,DEPTNAME,PDEPTID,DEPTMEMO) VALUES (@DEPTID,@DEPTNAME,'0',@DEPTMEMO)";
            Parameter[] paras = {
                                    new Parameter("@DEPTID",s.ToString()),
                                    new Parameter("@DEPTNAME",DEPTNAME),
                                    new Parameter("@DEPTMEMO",DEPTMEMO)
                                };
            objDAL.ExecuteNonQuery(CommandType.Text, sql, paras);

            string sql2 = "UPDATE HG_SYSINS SET SYSINT=@SYSINT WHERE upper(SYSNAME)='CRM_DEPT'";
            Parameter[] paras1 = {
                                    new Parameter("@SYSINT",s.ToString())
                                };
            objDAL.ExecuteNonQuery(CommandType.Text, sql2, paras1);
            objDAL.Close();
        }

        public void UPDATE_CRM_DEPT(string DEPTID, string DEPTNAME, string DEPTMEMO)
        {
            IDAL objDAL = DALFactory.GetDBO();
            objDAL.Open();
            string sql = "UPDATE CRM_DEPT set DEPTNAME=@DEPTNAME,DEPTMEMO=@DEPTMEMO where DEPTID=@DEPTID";
            Parameter[] paras = {
                                    new Parameter("@DEPTNAME",DEPTNAME),
                                    new Parameter("@DEPTMEMO",DEPTMEMO),
                                    new Parameter("@DEPTID",DEPTID)
                                };
            objDAL.ExecuteNonQuery(CommandType.Text, sql, paras);
            objDAL.Close();
        }

        public void DELETE_CRM_DEPT(string DEPTID)
        {
            IDAL objDAL = DALFactory.GetDBO();
            objDAL.Open();
            string sql = "DELETE from CRM_DEPT where DEPTID=@DEPTID";
            Parameter[] paras = {
                                    new Parameter("@DEPTID",DEPTID)
                                };
            objDAL.ExecuteNonQuery(CommandType.Text, sql, paras);
            objDAL.Close();
        }

        public DataTable SELECT_CRM_ZDLXR_ED()
        {
            string sql = " SELECT * FROM CRM_ZDLXR ORDER BY LXRID";
            DataTable dt = DBHelper.GetDataSet(CommandType.Text, sql);
            return dt;
        }

        public void INSERT_CRM_ZDLXR(string LXR, string LXDH, string LXDES)
        {
            string sql1 = "SELECT SYSINT FROM HG_SYSINS where SYSNAME = 'CRM_ZDLXR'";
            DataTable dt = DBHelper.GetDataSet(CommandType.Text, sql1);
            int s = Convert.ToInt32(dt.Rows[0][0].ToString()) + 1;

            IDAL objDAL = DALFactory.GetDBO();
            objDAL.Open();
            string sql = "INSERT INTO CRM_ZDLXR(LXRID,LXR,LXDH,LXDES) VALUES (@LXRID,@LXR,@LXDH,@LXDES)";
            Parameter[] paras = {
                                    new Parameter("@LXRID",s.ToString()),
                                    new Parameter("@LXR",LXR),
                                    new Parameter("@LXDH",LXDH),
                                    new Parameter("@LXDES",LXDES)
                                };
            objDAL.ExecuteNonQuery(CommandType.Text, sql, paras);

            string sql2 = "UPDATE HG_SYSINS SET SYSINT=@SYSINT WHERE upper(SYSNAME)='CRM_ZDLXR'";
            Parameter[] paras1 = {
                                    new Parameter("@SYSINT",s.ToString())
                                };
            objDAL.ExecuteNonQuery(CommandType.Text, sql2, paras1);
            objDAL.Close();
        }

        public void UPDATE_CRM_ZDLXR(string LXRID, string LXR, string LXDH, string LXDES)
        {
            IDAL objDAL = DALFactory.GetDBO();
            objDAL.Open();
            string sql = "UPDATE CRM_ZDLXR set LXR=@LXR,LXDH=@LXDH,LXDES=@LXDES where LXRID=@LXRID";
            Parameter[] paras = {
                                    new Parameter("@LXR",LXR),
                                    new Parameter("@LXDH",LXDH),
                                    new Parameter("@LXDES",LXDES),
                                    new Parameter("@LXRID",LXRID)
                                };
            objDAL.ExecuteNonQuery(CommandType.Text, sql, paras);
            objDAL.Close();
        }

        public void DELETE_CRM_ZDLXR(string LXRID)
        {
            IDAL objDAL = DALFactory.GetDBO();
            objDAL.Open();
            string sql = "DELETE from CRM_ZDLXR where LXRID=@LXRID";
            Parameter[] paras = {
                                    new Parameter("@LXRID",LXRID)
                                };
            objDAL.ExecuteNonQuery(CommandType.Text, sql, paras);
            objDAL.Close();
        }



        public DataTable SELECT_CRM_ZDADD_ED()
        {
            string sql = " SELECT * FROM CRM_ZDADD ORDER BY ADDID";
            DataTable dt = DBHelper.GetDataSet(CommandType.Text, sql);
            return dt;
        }

        public void INSERT_CRM_ZDADD(string ADDNAME, string ADDMEMO)
        {
            string sql1 = "SELECT SYSINT FROM HG_SYSINS where SYSNAME = 'CRM_ZDADD'";
            DataTable dt = DBHelper.GetDataSet(CommandType.Text, sql1);
            int s = Convert.ToInt32(dt.Rows[0][0].ToString()) + 1;

            IDAL objDAL = DALFactory.GetDBO();
            objDAL.Open();
            string sql = "INSERT INTO CRM_ZDADD(ADDID,ADDNAME,ADDMEMO) VALUES (@ADDID,@ADDNAME,@ADDMEMO)";
            Parameter[] paras = {
                                    new Parameter("@ADDID",s.ToString()),
                                    new Parameter("@ADDNAME",ADDNAME),
                                    new Parameter("@ADDMEMO",ADDMEMO)
                                };
            objDAL.ExecuteNonQuery(CommandType.Text, sql, paras);

            string sql2 = "UPDATE HG_SYSINS SET SYSINT=@SYSINT WHERE upper(SYSNAME)='CRM_ZDADD'";
            Parameter[] paras1 = {
                                    new Parameter("@SYSINT",s.ToString())
                                };
            objDAL.ExecuteNonQuery(CommandType.Text, sql2, paras1);
            objDAL.Close();
        }

        public void UPDATE_CRM_ZDADD(string ADDID, string ADDNAME, string ADDMEMO)
        {
            IDAL objDAL = DALFactory.GetDBO();
            objDAL.Open();
            string sql = "UPDATE CRM_ZDADD set ADDNAME=@ADDNAME,ADDMEMO=@ADDMEMO where ADDID=@ADDID";
            Parameter[] paras = {
                                    new Parameter("@ADDNAME",ADDNAME),
                                    new Parameter("@ADDMEMO",ADDMEMO),
                                    new Parameter("@ADDID",ADDID)
                                };
            objDAL.ExecuteNonQuery(CommandType.Text, sql, paras);
            objDAL.Close();
        }

        public void DELETE_CRM_ZDADD(string ADDID)
        {
            IDAL objDAL = DALFactory.GetDBO();
            objDAL.Open();
            string sql = "DELETE from CRM_ZDADD where ADDID=@ADDID";
            Parameter[] paras = {
                                    new Parameter("@ADDID",ADDID)
                                };
            objDAL.ExecuteNonQuery(CommandType.Text, sql, paras);
            objDAL.Close();
        }
    }
}
