using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Bussiness.SqlHelper;
using System.Data.SqlClient;

namespace Bussiness.STSY
{
    public class FPService
    {
        public DataTable GETGYSINFO()
        {
            string str = "select ClientID,ClSN,ClName from MSClient where isBuy=1 and isDelete=0 order by ClSN";
            DataTable dt = DBHelper.GetDataSet(CommandType.Text, str);
            return dt;
        }

        public DataTable GETGYSINFOALL()
        {
            string str = "select ClientID,ClSN,ClName from MSClient where isBuy=1 order by ClSN";
            DataTable dt = DBHelper.GetDataSet(CommandType.Text, str);
            return dt;
        }

        public DataTable GETLBINFO(string LB, string StockID, string ClientID, string RuKDateS, string RuKDateE, string TypeID, string RuKDNO)
        {
            string sql = "select x.*,b.ClSN,b.ClName from(select ";
            sql = sql + "a.RuKDID,a.RuKDNO,a.StockID,a.SMonY,a.ClientID,a.RuKTotal, ";
            sql = sql + "a.RuKDate,a.RuKMem,a.fillID,a.fillName,a.fillTime,a.isAudi,a.nStatus, c.StockSN,c.StockName from MSRuKD a,MSStock ";
            sql = sql + "c where a.StockID=c.StockID ";

            if (StockID != "")
            {
                sql = sql + "and a.StockID=@StockID ";
            }
            if (ClientID != "0")
            {
                sql = sql + "and a.ClientID=@ClientID ";
            }
            if (RuKDateS != "")
            {
                sql = sql + "and a.RuKDate>=@RuKDateS ";
            }
            if (RuKDateE != "")
            {
                sql = sql + "and a.RuKDate<=@RuKDateE ";
            }
            if (TypeID != "-1")
            {
                sql = sql + "and a.TypeID=@TypeID ";
            }
            if (RuKDNO != "")
            {
                sql = sql + "and a.RuKDNO=@RuKDNO ";
            }
            if (LB == "0")
            {
                sql = sql + "and a.isAudi='1' and not exists(select RuKDID from FaPRuKD where FaPRuKD.RuKDID=a.RuKDID) ";
            }
            if (LB == "1")
            {
                sql = sql + "and a.isAudi='1' and a.nStatus<>3 ";
            }
            if (LB == "2")
            {
                sql = sql + "and a.nStatus=3 ";
            }
            if (LB == "3")
            {
                sql = sql + "and (a.isAudi='1')";
            }
            sql = sql + ")x left join MSClient b on x.ClientID=b.ClientID order by x.RuKDNO";
            SqlParameter[] paras = {
                                     new SqlParameter("@StockID",StockID),
                                     new SqlParameter("@ClientID",ClientID),
                                     new SqlParameter("@RuKDateS",RuKDateS),
                                     new SqlParameter("@RuKDateE",RuKDateE),
                                     new SqlParameter("@TypeID",TypeID),
                                     new SqlParameter("@RuKDNO",RuKDNO)
                                 };
            DataTable dt = DBHelper.GetDataSet(CommandType.Text, sql, paras);
            return dt;
        }

        public void UPDATE_CaiGFPYZ_RKBD(string RKBD, string RI)
        {
            IDAL objDAL = DALFactory.GetDBO();
            objDAL.Open();
            string sql = "UPDATE CaiGFPYZ SET RKBD = RKBD +' '+@RKBD WHERE RI = @RI";
            Parameter[] paras = {
                                    new Parameter("@RKBD",RKBD),
                                    new Parameter("@RI",RI)
                                };
            objDAL.ExecuteNonQuery(CommandType.Text, sql, paras);
            objDAL.Close();
        }

        public DataTable GETFPLBINFO(string fillID, string ClientID, string ACDateS, string ACDateE, string FaPDateS, string FaPDateE, string CaiGFPNO, string XZ, string ACType)
        {
            string sql = "select x.*,b.ClSN,b.ClName from(SELECT RuKTotal,CaiGFPID, CaiGFPNO, FaPDate, ClientID, FaPMem, FaPTotal, AudiID, AudiName, isAudi, PayID, PayName,";
            sql = sql + " isPay,ACDate,ACType, PayTotal, nStatus, fillID, fillName, fillTime,RePayID,RePayName,isRePay,RePayMemo,AudiTime,PayTime,RePayTime from CaiGFP";
            sql = sql + " WHERE";
            sql = sql + " 1=1";
            if (fillID != "" && fillID != "0")
            {
                sql = sql + " AND fillID=@fillID";
            }
            if (ClientID != "0")
            {
                sql = sql + " AND ClientID=@ClientID";
            }
            if (ACDateS != "")
            {
                sql = sql + " AND ACDate>=@ACDateS";
            }
            if (ACDateE != "")
            {
                sql = sql + " AND ACDate<=@ACDateE";
            }
            if (ACType != "0")
            {
                sql = sql + " AND ACType=@ACType";
            }
            if (FaPDateS != "")
            {
                sql = sql + " AND FaPDate>=@FaPDateS";
            }
            if (FaPDateE != "")
            {
                sql = sql + " AND FaPDate<=@FaPDateE";
            }
            if (CaiGFPNO != "")
            {
                sql = sql + " AND CaiGFPNO=@CaiGFPNO";
            }
            if (XZ == "0")
            {
                sql = sql + " AND nStatus=0 ";
            }
            else if (XZ == "1")
            {
                sql = sql + " and nStatus=1";
            }
            else if (XZ == "2")
            {
                sql = sql + " and isAudi=1";
            }
            else if (XZ == "3")
            {
                sql = sql + " and isPay=1";
            }
            else if (XZ == "4")
            {
                sql = sql + " and isRePay=1";
            }
            sql = sql + "  )x left join MSClient b on x.ClientID=b.ClientID  order by x.fillTime";
            SqlParameter[] paras = {
                                     new SqlParameter("@fillID",fillID),
                                     new SqlParameter("@ClientID",ClientID),
                                     new SqlParameter("@ACDateS",ACDateS),
                                     new SqlParameter("@ACDateE",ACDateE),
                                     new SqlParameter("@FaPDateS",FaPDateS),
                                     new SqlParameter("@FaPDateE",FaPDateE),
                                     new SqlParameter("@CaiGFPNO",CaiGFPNO),
                                     new SqlParameter("@ACType",ACType)
                                 };
            DataTable dt = DBHelper.GetDataSet(CommandType.Text, sql, paras);
            return dt;
        }

        public DataTable INSERT_CaiGFPYZ(string GYS, string RZRQ, string RZLX, string FPRQ, string FPJE, string FKJE, string FPHM, string RKJE, string JKK, string FPSM, string HDQRSM, string RKBD)
        {
            string sql = "INSERT INTO CaiGFPYZ(GYS,RZRQ,RZLX,FPRQ,FPJE,FKJE,FPHM,RKJE,JKK,FPSM,HDQRSM,RKBD) VALUES(@GYS,@RZRQ,@RZLX,@FPRQ,@FPJE,@FKJE,@FPHM,@RKJE,@JKK,@FPSM,@HDQRSM,@RKBD)";
            sql = sql + " select @@identity as RI";
            SqlParameter[] paras = {
                                    new SqlParameter("@GYS",GYS),
                                    new SqlParameter("@RZRQ",RZRQ),
                                    new SqlParameter("@RZLX",RZLX),
                                    new SqlParameter("@FPRQ",FPRQ),
                                    new SqlParameter("@FPJE",FPJE),
                                    new SqlParameter("@FKJE",FKJE),
                                    new SqlParameter("@FPHM",FPHM),
                                    new SqlParameter("@RKJE",RKJE),
                                    new SqlParameter("@JKK",JKK),
                                    new SqlParameter("@FPSM",FPSM),
                                    new SqlParameter("@HDQRSM",HDQRSM),
                                    new SqlParameter("@RKBD",RKBD)
                                };
            DataTable dt = DBHelper.GetDataSet(CommandType.Text, sql, paras);
            return dt;
        }

        public DataTable GETCaiGFPYZ(string RI)
        {
            string sql = "select * from CaiGFPYZ where RI=@RI";
            SqlParameter[] paras = {
                                       new SqlParameter("@RI",RI)
                                   };
            DataTable dt = DBHelper.GetDataSet(CommandType.Text, sql, paras);
            return dt;
        }

        public DataTable GETrukd(string RuKDID)
        {
            string sql = "select * from MSRuKD where RuKDID=@RuKDID";
            SqlParameter[] paras = {
                                       new SqlParameter("@RuKDID",RuKDID)
                                   };
            DataTable dt = DBHelper.GetDataSet(CommandType.Text, sql, paras);
            return dt;
        }

        public DataTable GETrukd1(string RuKDID)
        {
            string sql = "select x.*,b.ClSN,b.ClName from (select a.RuKDID,a.RuKDNO,a.StockID,a.SMonY,a.ClientID, a.RuKDate,a.RuKMem,a.fillID,a.fillName,a.fillTime, a.isAudi,a.nStatus,a.TypeID,a.AudiName, c.StockSN,c.StockName from MSRuKD a,MSStock c where a.RuKDID=@RuKDID and a.StockID=c.StockID)x left join MSClient b on x.ClientID=b.ClientID";
            SqlParameter[] paras = {
                                       new SqlParameter("@RuKDID",RuKDID)
                                   };
            DataTable dt = DBHelper.GetDataSet(CommandType.Text, sql, paras);
            return dt;
        }

        public DataTable GETrukd2(string RuKDID)
        {
            string sql = "select x.*,c.PlaceName,c.PlaceSN from( select a.RuKMXID,a.RuKDID,a.materialID,a.mtUnit, a.mtPrice,a.mtNumber,a.mtTotal,a.PlaceID,a.MXMemo, b.mtName,b.mtSpec,b.mtNO,b.mtSN from MSRuKMX a ,MSMaterial b where a.materialID=b.materialID and a.RuKDID=@RuKDID)x left join MSPlace c on x.PlaceID=c.PlaceID ";
            SqlParameter[] paras = {
                                       new SqlParameter("@RuKDID",RuKDID)
                                   };
            DataTable dt = DBHelper.GetDataSet(CommandType.Text, sql, paras);
            return dt;
        }

        public int INSERT_CaiGFP(string CaiGFPNO, string FaPDate, string ClientID, string FaPMem, string FaPTotal, string isAudi, string isPay, string PayTotal, string nStatus, string fillID, string fillName, string fillTime, string isRePay, string ACDate, string ACType, string RuKTotal)
        {
            string sql1 = "SELECT SYSNAME,SYSINT,SYSEXP FROM HG_SYSINS WHERE upper(SYSNAME)='CAIGFP'";
            DataTable dt = DBHelper.GetDataSet(CommandType.Text, sql1);
            int s = Convert.ToInt32(dt.Rows[0]["SYSINT"].ToString());
            s = s + 1;
            string sql2 = "UPDATE HG_SYSINS SET SYSINT=@SYSINT WHERE upper(SYSNAME)='CAIGFP'";
            Parameter[] paras2 = {
                                    new Parameter("@SYSINT",s)
                                };
            IDAL objDAL = DALFactory.GetDBO();
            objDAL.Open();
            string sql = "insert into CaiGFP(CaiGFPID,CaiGFPNO,FaPDate,ClientID,FaPMem,FaPTotal,isAudi,isPay,PayTotal,nStatus,fillID,fillName,fillTime,isRePay,ACDate,ACType,RuKTotal) ";
            sql = sql + " values (@CaiGFPID,@CaiGFPNO,@FaPDate,@ClientID,@FaPMem,@FaPTotal,@isAudi,@isPay,@PayTotal,@nStatus,@fillID,@fillName,@fillTime,@isRePay,@ACDate,@ACType,@RuKTotal) ";
            Parameter[] paras = {
                                    new Parameter("@CaiGFPID",s),
                                    new Parameter("@CaiGFPNO",CaiGFPNO),
                                    new Parameter("@FaPDate",FaPDate),
                                    new Parameter("@ClientID",ClientID),
                                    new Parameter("@FaPMem",FaPMem),
                                    new Parameter("@FaPTotal",FaPTotal),
                                    new Parameter("@isAudi",isAudi),
                                    new Parameter("@isPay",isPay),
                                    new Parameter("@PayTotal",PayTotal),
                                    new Parameter("@nStatus",nStatus),
                                    new Parameter("@fillID",fillID),
                                    new Parameter("@fillName",fillName),
                                    new Parameter("@fillTime",fillTime),
                                    new Parameter("@isRePay",isRePay),
                                    new Parameter("@ACDate",ACDate),
                                    new Parameter("@ACType",ACType),
                                    new Parameter("@RuKTotal",RuKTotal)
                                };
            objDAL.ExecuteNonQuery(CommandType.Text, sql2, paras2);
            objDAL.ExecuteNonQuery(CommandType.Text, sql, paras);
            objDAL.Close();
            return s;
        }

        public void DELETEFaPRuKD(string CaiGFPID)
        {
            IDAL objDAL = DALFactory.GetDBO();
            objDAL.Open();
            string sql = "DELETE FROM FaPRuKD WHERE CaiGFPID=@CaiGFPID";
            Parameter[] paras = {
                                    new Parameter("@CaiGFPID",CaiGFPID)
                                };
            objDAL.ExecuteNonQuery(CommandType.Text, sql, paras);
            objDAL.Close();
        }

        public void INSERTFaPRuKD(string CaiGFPID, string RuKDID)
        {
            IDAL objDAL = DALFactory.GetDBO();
            objDAL.Open();
            string sql = "insert into FaPRuKD(CaiGFPID,RuKDID) values(@CaiGFPID,@RuKDID)";
            Parameter[] paras = {
                                    new Parameter("@CaiGFPID",CaiGFPID),
                                    new Parameter("@RuKDID",RuKDID)
                                };
            objDAL.ExecuteNonQuery(CommandType.Text, sql, paras);
            objDAL.Close();
        }

        public DataTable GETCaiGFP(string CaiGFPID)
        {
            string sql = "SELECT * FROM CaiGFP WHERE CaiGFPID=@CaiGFPID";
            SqlParameter[] paras = {
                                       new SqlParameter("@CaiGFPID",CaiGFPID)
                                   };
            DataTable dt = DBHelper.GetDataSet(CommandType.Text, sql, paras);
            return dt;
        }

        public DataTable GETFaPRuKD(string CaiGFPID)
        {
            string sql = "SELECT * FROM FaPRuKD WHERE CaiGFPID=@CaiGFPID";
            SqlParameter[] paras = {
                                       new SqlParameter("@CaiGFPID",CaiGFPID)
                                   };
            DataTable dt = DBHelper.GetDataSet(CommandType.Text, sql, paras);
            return dt;
        }

        public void DELETE_CaiGFP(string CaiGFPID)
        {
            IDAL objDAL = DALFactory.GetDBO();
            objDAL.Open();
            string sql = "DELETE FROM CaiGFP WHERE CaiGFPID=@CaiGFPID";
            Parameter[] paras = {
                                    new Parameter("@CaiGFPID",CaiGFPID)
                                };
            objDAL.ExecuteNonQuery(CommandType.Text, sql, paras);
            objDAL.Close();
        }

        public void UPDATE_CaiGFP(string CaiGFPID)
        {
            IDAL objDAL = DALFactory.GetDBO();
            objDAL.Open();
            string sql = "UPDATE CaiGFP SET nStatus='1' WHERE CaiGFPID=@CaiGFPID";
            Parameter[] paras = {
                                    new Parameter("@CaiGFPID",CaiGFPID)
                                };
            objDAL.ExecuteNonQuery(CommandType.Text, sql, paras);
            objDAL.Close();
        }

        public void UPDATE_CaiGFP_DJTH(string CaiGFPID)
        {
            IDAL objDAL = DALFactory.GetDBO();
            objDAL.Open();
            string sql = "update CaiGFP set nStatus='0',AudiID='-1',AudiTime=getdate() where CaiGFPID=@CaiGFPID";
            Parameter[] paras = {
                                    new Parameter("@CaiGFPID",CaiGFPID)
                                };
            objDAL.ExecuteNonQuery(CommandType.Text, sql, paras);
            objDAL.Close();
        }

        public void UPDATE_CaiGFP_TJFK(string CaiGFPID, string AudiID, string AudiName)
        {
            IDAL objDAL = DALFactory.GetDBO();
            objDAL.Open();
            string sql = "update CaiGFP set nStatus='2',isAudi='1',AudiID=@AudiID,AudiName=@AudiName,AudiTime=getdate() where CaiGFPID=@CaiGFPID";
            Parameter[] paras = {
                                    new Parameter("@CaiGFPID",CaiGFPID),
                                    new Parameter("@AudiID",AudiID),
                                    new Parameter("@AudiName",AudiName)
                                };
            objDAL.ExecuteNonQuery(CommandType.Text, sql, paras);
            objDAL.Close();
        }

        public DataTable GETFPLBINFO_FKQR(string fillID, string ClientID, string ACDateS, string ACDateE, string FaPDateS, string FaPDateE, string CaiGFPNO, string XZ)
        {
            string sql = "select x.*,b.ClSN,b.ClName from(SELECT RuKTotal,CaiGFPID, CaiGFPNO, FaPDate, ClientID, FaPMem, FaPTotal, AudiID, AudiName, isAudi, PayID, PayName,";
            sql = sql + " isPay,ACDate,ACType, PayTotal, nStatus, fillID, fillName, fillTime,RePayID,RePayName,isnull(isRePay,0) as isRePay,RePayMemo,AudiTime,PayTime,RePayTime,isnull(PayCash,0) as PayCash,isnull(PayType,0) as PayType from CaiGFP";
            sql = sql + " WHERE";
            sql = sql + " 1=1";
            if (fillID != "" && fillID != "0")
            {
                sql = sql + " AND fillID=@fillID";
            }
            if (ClientID != "0")
            {
                sql = sql + " AND ClientID=@ClientID";
            }
            if (ACDateS != "")
            {
                sql = sql + " AND ACDate>=@ACDateS";
            }
            if (ACDateE != "")
            {
                sql = sql + " AND ACDate<=@ACDateE";
            }
            if (FaPDateS != "")
            {
                sql = sql + " AND FaPDate>=@FaPDateS";
            }
            if (FaPDateE != "")
            {
                sql = sql + " AND FaPDate<=@FaPDateE";
            }
            if (CaiGFPNO != "")
            {
                sql = sql + " AND CaiGFPNO=@CaiGFPNO";
            }
            if (XZ == "0")
            {
                sql = sql + " AND isAudi=1 AND isPay=0 ";
            }
            else if (XZ == "1")
            {
                sql = sql + " AND isPay=1 ";
            }
            else if (XZ == "2")
            {
                sql = sql + " and isRePay=1 AND PayType=1";
            }
            else if (XZ == "3")
            {
                sql = sql + " AND isAudi=1 ";
            }
            sql = sql + "  )x left join MSClient b on x.ClientID=b.ClientID";
            SqlParameter[] paras = {
                                     new SqlParameter("@fillID",fillID),
                                     new SqlParameter("@ClientID",ClientID),
                                     new SqlParameter("@ACDateS",ACDateS),
                                     new SqlParameter("@ACDateE",ACDateE),
                                     new SqlParameter("@FaPDateS",FaPDateS),
                                     new SqlParameter("@FaPDateE",FaPDateE),
                                     new SqlParameter("@CaiGFPNO",CaiGFPNO)
                                 };
            DataTable dt = DBHelper.GetDataSet(CommandType.Text, sql, paras);
            return dt;
        }

        public void UPDATE_CaiGFP_FPTH(string CaiGFPID)
        {
            IDAL objDAL = DALFactory.GetDBO();
            objDAL.Open();
            string sql = "update CaiGFP set nStatus='1',isAudi='0',AudiID='-1',AudiName='',PayTime=getdate(),PayID='-1',isPay='0' where CaiGFPID=@CaiGFPID";
            Parameter[] paras = {
                                    new Parameter("@CaiGFPID",CaiGFPID)
                                };
            objDAL.ExecuteNonQuery(CommandType.Text, sql, paras);
            objDAL.Close();
        }

        public void UPDATE_CaiGFP_QRFK(string CaiGFPID, string PayID, string PayName, string PayTotal, string PayCash, string PayType, string ACDate, string ACType)
        {
            IDAL objDAL = DALFactory.GetDBO();
            objDAL.Open();
            string sql = "update CaiGFP set nStatus='3',isPay='1',PayID=@PayID,PayName=@PayName,PayTotal=@PayTotal,PayTime=getdate(),PayCash=@PayCash,PayType=@PayType,ACDate=@ACDate,ACType=@ACType where CaiGFPID=@CaiGFPID";
            Parameter[] paras = {
                                    new Parameter("@CaiGFPID",CaiGFPID),
                                    new Parameter("@PayID",PayID),
                                    new Parameter("@PayName",PayName),
                                    new Parameter("@PayTotal",PayTotal),
                                    new Parameter("@PayCash",PayCash),
                                    new Parameter("@PayType",PayType),
                                    new Parameter("@ACDate",ACDate),
                                    new Parameter("@ACType",ACType)
                                };
            objDAL.ExecuteNonQuery(CommandType.Text, sql, paras);
            objDAL.Close();
        }

        public DataTable GETFPLBINFO_HDQR(string fillID, string ClientID, string ACDateS, string ACDateE, string FaPDateS, string FaPDateE, string CaiGFPNO, string XZ)
        {
            string sql = "select TOP(200) x.*,b.ClSN,b.ClName from(SELECT RuKTotal,CaiGFPID, CaiGFPNO, FaPDate, ClientID, FaPMem, FaPTotal, AudiID, AudiName, isAudi, PayID, PayName,";
            sql = sql + " isPay,ACDate,ACType, PayTotal, nStatus, fillID, fillName, fillTime,RePayID,RePayName,isnull(isRePay,0) as isRePay,RePayMemo,AudiTime,PayTime,RePayTime,isnull(PayCash,0) as PayCash,isnull(PayType,0) as PayType from CaiGFP";
            sql = sql + " WHERE";
            sql = sql + " 1=1 and PayType=1";
            if (fillID != "" && fillID != "0")
            {
                sql = sql + " AND fillID=@fillID";
            }
            if (ClientID != "0")
            {
                sql = sql + " AND ClientID=@ClientID";
            }
            if (ACDateS != "")
            {
                sql = sql + " AND ACDate>=@ACDateS";
            }
            if (ACDateE != "")
            {
                sql = sql + " AND ACDate<=@ACDateE";
            }
            if (FaPDateS != "")
            {
                sql = sql + " AND FaPDate>=@FaPDateS";
            }
            if (FaPDateE != "")
            {
                sql = sql + " AND FaPDate<=@FaPDateE";
            }
            if (CaiGFPNO != "")
            {
                sql = sql + " AND CaiGFPNO=@CaiGFPNO";
            }
            if (XZ == "0")
            {
                sql = sql + " and isPay=1 and(isRePay = 0 or isRePay is null)";
            }
            else if (XZ == "1")
            {
                sql = sql + " and isRePay=1";
            }
            else if (XZ == "2")
            {
                sql = sql + " and isPay=1";
            }
            sql = sql + "  )x left join MSClient b on x.ClientID=b.ClientID";
            SqlParameter[] paras = {
                                     new SqlParameter("@fillID",fillID),
                                     new SqlParameter("@ClientID",ClientID),
                                     new SqlParameter("@ACDateS",ACDateS),
                                     new SqlParameter("@ACDateE",ACDateE),
                                     new SqlParameter("@FaPDateS",FaPDateS),
                                     new SqlParameter("@FaPDateE",FaPDateE),
                                     new SqlParameter("@CaiGFPNO",CaiGFPNO)
                                 };
            DataTable dt = DBHelper.GetDataSet(CommandType.Text, sql, paras);
            return dt;
        }
        public void UPDATE_CaiGFP_HDTH(string CaiGFPID)
        {
            IDAL objDAL = DALFactory.GetDBO();
            objDAL.Open();
            string sql = "update CaiGFP set nStatus='2',PayID='-1',isPay='0',isRePay='0',RePayID='-1',RePayTime=getdate() where CaiGFPID=@CaiGFPID";
            Parameter[] paras = {
                                    new Parameter("@CaiGFPID",CaiGFPID)
                                };
            objDAL.ExecuteNonQuery(CommandType.Text, sql, paras);
            objDAL.Close();
        }

        public void UPDATE_CaiGFP_HDQR(string CaiGFPID, string RePayID, string RePayName, string RePayMemo, string PayCash, string ACDate, string ACType)
        {
            IDAL objDAL = DALFactory.GetDBO();
            objDAL.Open();
            string sql = "update CaiGFP set nStatus='4',isRePay='1',RePayID=@RePayID,RePayName=@RePayName, RePayMemo=@RePayMemo,RePayTime=getdate(),PayCash=@PayCash,ACDate=@ACDate,ACType=@ACType where CaiGFPID=@CaiGFPID";
            Parameter[] paras = {
                                    new Parameter("@CaiGFPID",CaiGFPID),
                                    new Parameter("@RePayID",RePayID),
                                    new Parameter("@RePayName",RePayName),
                                    new Parameter("@RePayMemo",RePayMemo),
                                    new Parameter("@PayCash",PayCash),
                                    new Parameter("@ACDate",ACDate),
                                    new Parameter("@ACType",ACType)
                                };
            objDAL.ExecuteNonQuery(CommandType.Text, sql, paras);
            objDAL.Close();
        }

        public DataTable SELECT_RKBB(string times, string timee, string typeType, string StockID, string typeid)
        {
            string sql = "SELECT sum(mtNumber) mtNumber,sum(mtTotal) mtTotal ,case when sum(mtNumber)>0 then sum(mtTotal)/sum(mtNumber) else 0 end mtPrice ,sum(pmtNumber) pmtNumber,sum(pmtTotal) pmtTotal ,case when sum(pmtNumber)>0 then sum(pmtTotal)/sum(pmtNumber) else 0 end pmtPrice,sum(s1) s1, sum(j1) j1, sum(s2) s2, sum(j2) j2, sum(s3) s3, sum(j3) j3, sum(s4) s4, sum(j4) j4, sum(s5) s5, sum(j5) j5, sum(s6) s6, sum(j6) j6, sum(s7) s7, sum(j7) j7, sum(s8) s8, sum(j8) j8, sum(s9) s9, sum(j9) j9, sum(s10) s10, sum(j10) j10, sum(s11) s11, sum(j11) j11, sum(s12) s12, sum(j12) j12, sum(s13) s13, sum(j13) j13, sum(s14) s14, sum(j14) j14, sum(s15) s15, sum(j15) j15, sum(s16) s16, sum(j16) j16, sum(s17) s17, sum(j17) j17, sum(s18) s18, sum(j18) j18, sum(s19) s19, sum(j19) j19, sum(s20) s20, sum(j20) j20, sum(s21) s21, sum(j21) j21, sum(s22) s22, sum(j22) j22, sum(s23) s23, sum(j23) j23, sum(s24) s24, sum(j24) j24, sum(s25) s25, sum(j25) j25, sum(s26) s26, sum(j26) j26, sum(s27) s27, sum(j27) j27, sum(s28) s28, sum(j28) j28, sum(s29) s29, sum(j29) j29, sum(s30) s30, sum(j30) j30, sum(s31) s31, sum(j31) j31 ,typeName,typeID,mtName,materialID ";
            sql = sql + "FROM (SELECT case dday when 'x1' then mtNumber end s1, case dday when 'x1' then mtTotal end j1, case dday when 'x2' then mtNumber end s2, case dday when 'x2' then mtTotal end j2, case dday when 'x3' then mtNumber end s3, case dday when ";
            sql = sql + "'x3' then mtTotal end j3, case dday when 'x4' then mtNumber end s4, case dday when 'x4' then mtTotal end j4, case dday when 'x5' then mtNumber end s5, case dday when 'x5' then mtTotal end j5, case dday when 'x6' then ";
            sql = sql + "mtNumber end s6, case dday when 'x6' then mtTotal end j6, case dday when 'x7' then mtNumber end s7, case dday when 'x7' then mtTotal end j7, case dday when 'x8' then mtNumber end s8, case dday when 'x8' then mtTotal end j8, ";
            sql = sql + "case dday when 'x9' then mtNumber end s9, case dday when 'x9' then mtTotal end j9, case dday when 'x10' then mtNumber end s10, case dday when 'x10' then mtTotal end j10, case dday when 'x11' then mtNumber end s11, case dday ";
            sql = sql + "when 'x11' then mtTotal end j11, case dday when 'x12' then mtNumber end s12, case dday when 'x12' then mtTotal end j12, case dday when 'x13' then mtNumber end s13, case dday when 'x13' then mtTotal end j13, case dday when 'x14' then mtNumber end s14, case dday when 'x14' then mtTotal end j14, case dday when 'x15' then mtNumber end s15, case dday when 'x15' then mtTotal end j15, case dday when 'x16' then mtNumber end s16, case dday when 'x16' then mtTotal end j16, case dday when 'x17' then mtNumber end s17, case dday when 'x17' then mtTotal end j17, case dday when 'x18' then mtNumber end s18, case dday when 'x18' then mtTotal end j18, case dday when 'x19' then mtNumber end s19, case dday when 'x19' then mtTotal end j19, case dday when 'x20' then mtNumber end s20, case dday when 'x20' then mtTotal end j20, case dday when 'x21' then mtNumber end s21, case dday when 'x21' then mtTotal end j21, case dday when 'x22' then mtNumber end s22, case dday when 'x22' then mtTotal end j22, case dday when 'x23' then mtNumber end s23, case dday when 'x23' then mtTotal end j23, case dday when 'x24' then mtNumber end s24, case dday when 'x24' then mtTotal end j24, case dday when 'x25' then mtNumber end s25, case dday when 'x25' then mtTotal end j25, case dday when 'x26' then mtNumber end s26, case dday when 'x26' then mtTotal end j26, case dday when 'x27' then mtNumber end s27, case dday when 'x27' then mtTotal end j27, case dday when 'x28' then mtNumber end s28, case dday when 'x28' then mtTotal end j28, case dday when 'x29' then mtNumber end s29, case dday when 'x29' then mtTotal end j29, case dday when 'x30' then mtNumber end s30, case dday when 'x30' then mtTotal end j30, case dday when 'x31' then mtNumber end s31, case dday when 'x31' then mtTotal end j31,typeName,typeID,mtName,materialID,mtNumber,mtTotal,pmtNumber,pmtTotal ";
            sql = sql + " FROM (select b.materialID,b.mtNumber,b.mtTotal,'x'+cast(day(e.ACDate)as varchar) dday,c.mtName,d.typeName,d.typeID ";
            sql = sql + " ,0 pmtNumber,0 pmtTotal from MSRuKD a,MSRuKMX b,MSMaterial c,MSType d ,CaiGFP e,FaPRuKD f ";
            sql = sql + " where a.RuKDID=b.RuKDID and e.isAudi=1 and b.materialID=c.materialID and f.RuKDID=a.RuKDID and e.CaiGFPID=f.CaiGFPID and c.typeID=d.typeID and e.ACDate>=@times and e.ACDate<@timee ";
            if (typeType != "-1")
            {
                sql = sql + " and d.typeType=@typeType ";
            }
            if (StockID != "0")
            {
                sql = sql + " and a.StockID=@StockID ";
            }
            if (typeid != "0")
            {
                sql = sql + " and c.typeid=@typeid ";
            }
            sql = sql + " )x)y group by typeName,typeID,mtName,materialID ";
            SqlParameter[] paras = {
                                       new SqlParameter("@times",times),
                                       new SqlParameter("@timee",timee),
                                       new SqlParameter("@typeType",typeType),
                                       new SqlParameter("@StockID",StockID),
                                       new SqlParameter("@typeid",typeid)
                                   };
            DataTable dt = DBHelper.GetDataSet(CommandType.Text, sql, paras);
            return dt;
        }
        public DataTable SELECT_RKBB_1(string times, string timee, string time, string typeType, string StockID, string typeid)
        {
            string sql = "SELECT sum(mtNumber) mtNumber,sum(mtTotal) mtTotal ,case when sum(mtNumber)>0 then sum(mtTotal)/sum(mtNumber) else 0 end mtPrice ,sum(pmtNumber) pmtNumber,sum(pmtTotal) pmtTotal ,case when sum(pmtNumber)>0 then sum(pmtTotal)/sum(pmtNumber) else 0 end pmtPrice ,sum(s1) s1, sum(j1) j1, sum(s2) s2, sum(j2) j2, sum(s3) s3, sum(j3) j3, sum(s4) s4, sum(j4) j4, sum(s5) s5, sum(j5) j5, sum(s6) s6, sum(j6) j6, sum(s7) s7, sum(j7) j7, sum(s8) s8, sum(j8) j8, sum(s9) s9, sum(j9) j9, sum(s10) s10, sum(j10) j10, sum(s11) s11, sum(j11) j11, sum(s12) s12, sum(j12) j12, sum(s13) s13, sum(j13) j13, sum(s14) s14, sum(j14) j14, sum(s15) s15, sum(j15) j15, sum(s16) s16, sum(j16) j16, sum(s17) s17, sum(j17) j17, sum(s18) s18, sum(j18) j18, sum(s19) s19, sum(j19) j19, sum(s20) s20, sum(j20) j20, sum(s21) s21, sum(j21) j21, sum(s22) s22, sum(j22) j22, sum(s23) s23, sum(j23) j23, sum(s24) s24, sum(j24) j24, sum(s25) s25, sum(j25) j25, sum(s26) s26, sum(j26) j26, sum(s27) s27, sum(j27) j27, sum(s28) s28, sum(j28) j28, sum(s29) s29, sum(j29) j29, sum(s30) s30, sum(j30) j30, sum(s31) s31, sum(j31) j31,typeName,typeID,mtName,materialID ";
            sql = sql + " FROM (SELECT case dday when 'x1' then mtNumber end s1, case dday when 'x1' then mtTotal end j1, case dday when 'x2' then mtNumber end s2, case dday when 'x2' then mtTotal end j2, case dday when 'x3' then mtNumber end s3, case dday when ";
            sql = sql + "'x3' then mtTotal end j3, case dday when 'x4' then mtNumber end s4, case dday when 'x4' then mtTotal end j4, case dday when 'x5' then mtNumber end s5, case dday when 'x5' then mtTotal end j5, case dday when 'x6' then ";
            sql = sql + "mtNumber end s6, case dday when 'x6' then mtTotal end j6, case dday when 'x7' then mtNumber end s7, case dday when 'x7' then mtTotal end j7, case dday when 'x8' then mtNumber end s8, case dday when 'x8' then mtTotal end j8, ";
            sql = sql + "case dday when 'x9' then mtNumber end s9, case dday when 'x9' then mtTotal end j9, case dday when 'x10' then mtNumber end s10, case dday when 'x10' then mtTotal end j10, case dday when 'x11' then mtNumber end s11, case dday ";
            sql = sql + "when 'x11' then mtTotal end j11, case dday when 'x12' then mtNumber end s12, case dday when 'x12' then mtTotal end j12, case dday when 'x13' then mtNumber end s13, case dday when 'x13' then mtTotal end j13, case dday when 'x14' then mtNumber end s14, case dday when 'x14' then mtTotal end j14, case dday when 'x15' then mtNumber end s15, case dday when 'x15' then mtTotal end j15, case dday when 'x16' then mtNumber end s16, case dday when 'x16' then mtTotal end j16, case dday when 'x17' then mtNumber end s17, case dday when 'x17' then mtTotal end j17, case dday when 'x18' then mtNumber end s18, case dday when 'x18' then mtTotal end j18, case dday when 'x19' then mtNumber end s19, case dday when 'x19' then mtTotal end j19, case dday when 'x20' then mtNumber end s20, case dday when 'x20' then mtTotal end j20, case dday when 'x21' then mtNumber end s21, case dday when 'x21' then mtTotal end j21, case dday when 'x22' then mtNumber end s22, case dday when 'x22' then mtTotal end j22, case dday when 'x23' then mtNumber end s23, case dday when 'x23' then mtTotal end j23, case dday when 'x24' then mtNumber end s24, case dday when 'x24' then mtTotal end j24, case dday when 'x25' then mtNumber end s25, case dday when 'x25' then mtTotal end j25, case dday when 'x26' then mtNumber end s26, case dday when 'x26' then mtTotal end j26, case dday when 'x27' then mtNumber end s27, case dday when 'x27' then mtTotal end j27, case dday when 'x28' then mtNumber end s28, case dday when 'x28' then mtTotal end j28, case dday when 'x29' then mtNumber end s29, case dday when 'x29' then mtTotal end j29, case dday when 'x30' then mtNumber end s30, case dday when 'x30' then mtTotal end j30, case dday when 'x31' then mtNumber end s31, case dday when 'x31' then mtTotal end j31,typeName,typeID,mtName,materialID,mtNumber,mtTotal,pmtNumber,pmtTotal ";
            sql = sql + " FROM (select b.materialID,b.mtNumber,b.mtTotal,'x'+cast(day(e.ACDate)as varchar) dday,c.mtName,d.typeName,d.typeID ";
            sql = sql + " ,0 pmtNumber,0 pmtTotal from MSRuKD a,MSRuKMX b,MSMaterial c,MSType d ,CaiGFP e,FaPRuKD f where a.RuKDID=b.RuKDID and e.isAudi=1 and b.materialID=c.materialID and f.RuKDID=a.RuKDID and e.CaiGFPID=f.CaiGFPID and c.typeID=d.typeID and e.ACDate>=@times and e.ACDate<@timee ";
            if (typeType != "-1")
            {
                sql = sql + " and d.typeType=@typeType ";
            }
            if (StockID != "0")
            {
                sql = sql + " and a.StockID=@StockID ";
            }
            if (typeid != "0")
            {
                sql = sql + " and c.typeid=@typeid ";
            }
            sql = sql + " union all select b.materialID,0,0,'xx' dday,c.mtName,d.typeName,d.typeID ";
            sql = sql + " ,b.mtNumber,b.mtTotal from MSRuKD a,MSRuKMX b,MSMaterial c,MSType d ,CaiGFP e,FaPRuKD f where a.RuKDID=b.RuKDID and e.isAudi=1 and b.materialID=c.materialID and f.RuKDID=a.RuKDID and e.CaiGFPID=f.CaiGFPID and c.typeID=d.typeID and e.ACDate>=@time and e.ACDate<@times ";
            if (typeType != "-1")
            {
                sql = sql + " and d.typeType=@typeType ";
            }
            if (StockID != "0")
            {
                sql = sql + " and a.StockID=@StockID ";
            }
            if (typeid != "0")
            {
                sql = sql + " and c.typeid=@typeid ";
            }
            sql = sql + " )x)y group by typeName,typeID,mtName,materialID ";

            SqlParameter[] paras = {
                                       new SqlParameter("@times",times),
                                       new SqlParameter("@timee",timee),
                                       new SqlParameter("@time",time),
                                       new SqlParameter("@typeType",typeType),
                                       new SqlParameter("@StockID",StockID),
                                       new SqlParameter("@typeid",typeid)
                                   };
            DataTable dt = DBHelper.GetDataSet(CommandType.Text, sql, paras);
            return dt;
        }


        public DataTable GETFaPRuKDALL(string CaiGFPID)
        {
            string sql = "select MSRuKD.* from MSRuKD,FaPRuKD where FaPRuKD.RuKDID=MSRuKD.RuKDID and FaPRuKD.CaiGFPID=@CaiGFPID";
            SqlParameter[] paras = {
                                       new SqlParameter("@CaiGFPID",CaiGFPID)
                                   };
            DataTable dt = DBHelper.GetDataSet(CommandType.Text, sql, paras);
            return dt;
        }

        public DataTable GETFaPRuKDstring(string RuKDID)
        {
            string sql = "select * from MSRuKD where RuKDID in (" + RuKDID + ")";
            SqlParameter[] paras = {
                                       new SqlParameter("@RuKDID",RuKDID)
                                   };
            DataTable dt = DBHelper.GetDataSet(CommandType.Text, sql, paras);
            return dt;
        }

        public int GETjkkhj(string times,string timee)
        {
            string sql = "select sum(FaPTotal-RuKTotal) as JKK from CaiGFP where ACDate > @times and ACDate < @timee ";
            SqlParameter[] paras = {
                                       new SqlParameter ("@times",times),
                                       new SqlParameter ("@timee",timee)
                                   };
            DataTable dt = DBHelper.GetDataSet(CommandType.Text, sql, paras);
            int a = Convert.ToInt16(dt.Rows[0][0]);
            return a;
        }

    }
}
