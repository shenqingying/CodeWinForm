using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Bussiness.SqlHelper;
using System.Data.SqlClient;

namespace Bussiness.STSY
{
    public class RKService
    {
        public DataTable GETGYSINFO()
        {
            string str = "select ClientID,ClSN,ClName from MSClient where isBuy=1 and isDelete=0 order by ClSN";
            DataTable dt = DBHelper.GetDataSet(CommandType.Text, str);
            return dt;
        }

        public DataTable GETGYSINFOCX()
        {
            string str = "select ClientID,ClSN,ClName from MSClient where isBuy=1 order by ClSN";
            DataTable dt = DBHelper.GetDataSet(CommandType.Text, str);
            return dt;
        }

        public DataTable GETLBINFO(string StockID, string ClientID, string RuKDateS, string RuKDateE, string TypeID, string RuKDNO,string WLLB)     //!!!列表页面gridview
        {
            string sql = "select x.*,b.ClSN,b.ClName from(select ";
            sql = sql + "a.RuKDID,a.RuKDNO,a.StockID,a.SMonY,a.ClientID,a.RuKTotal, ";
            sql = sql + "a.RuKDate,a.RuKMem,a.fillID,a.fillName,a.fillTime,a.isAudi,a.nStatus, c.StockSN,c.StockName from MSRuKD a,MSStock ";
            sql = sql + "c where a.StockID=c.StockID ";

            if (StockID != "0" && StockID != "")
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
            if(WLLB != "" && WLLB !="0" )
            {
                sql = sql + "and a.WLid=@WLLB ";
            }
            //if (LB == "0")
            //{
                sql = sql + "and a.isAudi='1' and not exists(select RuKDID from FaPRuKD where FaPRuKD.RuKDID=a.RuKDID) ";      //已审核而未关联发票
            //}
            //if (LB == "1")
            //{
            //    sql = sql + "and a.isAudi='1' and a.nStatus<>3 ";
            //}
            //if (LB == "2")
            //{
            //    sql = sql + "and a.nStatus=3 ";
            //}
            //if (LB == "3")
            //{
            //    sql = sql + "and (a.isAudi='1')";
            //}
            
            sql = sql + ")x left join MSClient b on x.ClientID=b.ClientID order by x.RuKDNO";
            SqlParameter[] paras = {
                                     new SqlParameter("@StockID",StockID),
                                     new SqlParameter("@ClientID",ClientID),
                                     new SqlParameter("@RuKDateS",RuKDateS),
                                     new SqlParameter("@RuKDateE",RuKDateE),
                                     new SqlParameter("@TypeID",TypeID),
                                     new SqlParameter("@RuKDNO",RuKDNO),
                                     new SqlParameter("@WLLB",WLLB)
                                 };
            DataTable dt = DBHelper.GetDataSet(CommandType.Text, sql, paras);
            return dt;
        }

        public string insertRKDH(string RuKDNO, string StockID, string ClientID, string RuKDate, string RuKMem, string fillID, string fillName, string fillTime, string isAudi, string nStatus, string TypeID, string RuKTotal,string WLLB)
        {
            string str = "SELECT SYSINT FROM HG_SYSINS where SYSNAME = 'MSRuKD'";
            DataTable dt = DBHelper.GetDataSet(CommandType.Text, str);
            IDAL objDAL = DALFactory.GetDBO();
            objDAL.Open();

            string sql = "insert into MSRuKD(RuKDID,RuKDNO,StockID,SMonY,ClientID, RuKDate,RuKMem,fillID,fillName,fillTime,isAudi,nStatus,TypeID,RuKTotal,WLid) values (@RuKDID,@RuKDNO,@StockID,0,@ClientID, @RuKDate,@RuKMem,@fillID,@fillName,@fillTime,@isAudi,@nStatus,@TypeID,@RuKTotal,@WLLB)";
            Parameter[] paras = {
                                    new Parameter("@RuKDID",(Convert.ToInt32(dt.Rows[0][0].ToString())+1).ToString()),
                                    new Parameter("@RuKDNO",RuKDNO),
                                    new Parameter("@StockID",StockID),
                                    new Parameter("@ClientID",ClientID),
                                    new Parameter("@RuKDate",RuKDate),
                                    new Parameter("@RuKMem",RuKMem),
                                    new Parameter("@fillID",fillID),
                                    new Parameter("@fillName",fillName),
                                    new Parameter("@fillTime",fillTime),
                                    new Parameter("@isAudi",isAudi),
                                    new Parameter("@nStatus",nStatus),
                                    new Parameter("@TypeID",TypeID),
                                    new Parameter("@RuKTotal",RuKTotal),
                                    new Parameter("@WLLB",WLLB)
                                };
            objDAL.ExecuteNonQuery(CommandType.Text, sql, paras);

            string sql1 = "UPDATE HG_SYSINS SET SYSINT=@SYSINT WHERE upper(SYSNAME)='MSRuKD'";
            Parameter[] paras1 =
            {
                new Parameter("@SYSINT",(Convert.ToInt32(dt.Rows[0][0].ToString())+1).ToString())
            };
            objDAL.ExecuteNonQuery(CommandType.Text, sql1, paras1);

            objDAL.Close();
            return (Convert.ToInt32(dt.Rows[0][0].ToString()) + 1).ToString();
        }

        public void insertRKDMX(string RuKDID, string materialID, string mtNumber, string mtUnit, string mtPrice, string mtTotal, string PlaceID, string MXMemo)
        {
            string str = "SELECT SYSINT FROM HG_SYSINS where SYSNAME = 'MSRuKMX'";
            DataTable dt = DBHelper.GetDataSet(CommandType.Text, str);
            IDAL objDAL = DALFactory.GetDBO();
            objDAL.Open();
            string sql = " insert into MSRuKMX(RuKMXID,RuKDID,materialID,mtNumber,mtUnit ,mtPrice,mtTotal,PlaceID,MXMemo) values (@RuKMXID,@RuKDID,@materialID,@mtNumber,@mtUnit, @mtPrice,@mtTotal,@PlaceID,@MXMemo)";
            Parameter[] paras = {
                                    new Parameter("@RuKMXID",(Convert.ToInt32(dt.Rows[0][0].ToString())+1).ToString()),
                                    new Parameter("@RuKDID",RuKDID),
                                    new Parameter("@materialID",materialID),
                                    new Parameter("@mtNumber",mtNumber),
                                    new Parameter("@mtUnit",mtUnit),
                                    new Parameter("@mtPrice",mtPrice),
                                    new Parameter("@mtTotal",mtTotal),
                                    new Parameter("@PlaceID",PlaceID),
                                    new Parameter("@MXMemo",MXMemo)
                                };
            objDAL.ExecuteNonQuery(CommandType.Text, sql, paras);

            string sql1 = "UPDATE HG_SYSINS SET SYSINT=@SYSINT WHERE upper(SYSNAME)='MSRuKMX'";
            Parameter[] paras1 =
            {
                new Parameter("@SYSINT",(Convert.ToInt32(dt.Rows[0][0].ToString())+1).ToString())
            };
            objDAL.ExecuteNonQuery(CommandType.Text, sql1, paras1);

            objDAL.Close();
        }

        public DataTable SELECTRKD(string nStatus, string StockID, string ClientID, string RuKDates, string RuKDatee, string TypeID, string RuKDNO, string fillID)
        {
            string sql = "SELECT MSRuKD.*,MSClient.ClSN,MSClient.ClName,MSStock.StockSN,MSStock.StockNAME,MSRuKD.WLid FROM MSRuKD,MSClient,MSStock";
            string str = " where MSRuKD.ClientID=MSClient.ClientID AND MSStock.StockID=MSRuKD.StockID ";
            if (nStatus != "3" && nStatus != "4")
            {
                str = str + " AND MSRuKD.nStatus=@nStatus";
            }
            if (nStatus == "4")
            {
                str = str + " AND not exists(select RuKDID from FaPRuKD where FaPRuKD.RuKDID=MSRuKD.RuKDID) AND MSRuKD.nStatus=2";
            }
            if (StockID != "0")
            {
                str = str + " AND MSRuKD.StockID=@StockID";
            }
            if (ClientID != "0")
            {
                str = str + " AND MSRuKD.ClientID=@ClientID";
            }
            if (RuKDates != "")
            {
                str = str + " AND MSRuKD.RuKDate>=@RuKDates";
            }
            if (RuKDatee != "")
            {
                str = str + " AND MSRuKD.RuKDate<=@RuKDatee";
            }
            if (TypeID != "-1")
            {
                str = str + " AND MSRuKD.TypeID=@TypeID";
            }
            if (RuKDNO != "")
            {
                str = str + " AND MSRuKD.RuKDNO=@RuKDNO";
            }
            if (fillID != "")
            {
                str = str + " AND MSRuKD.fillID=@fillID";
            }
            sql = sql + str;
            SqlParameter[] paras = {
                                     new SqlParameter("@nStatus",nStatus),
                                     new SqlParameter("@StockID",StockID),
                                     new SqlParameter("@ClientID",ClientID),
                                     new SqlParameter("@RuKDates",RuKDates),
                                     new SqlParameter("@RuKDatee",RuKDatee),
                                     new SqlParameter("@TypeID",TypeID),
                                     new SqlParameter("@RuKDNO",RuKDNO),
                                     new SqlParameter("@fillID",fillID)
                                 };
            DataTable dt = DBHelper.GetDataSet(CommandType.Text, sql, paras);
            return dt;
        }

        public DataTable SELECTRKDBYID(string RuKDID)
        {
            string sql = "SELECT * FROM MSRuKD WHERE RuKDID=@RuKDID";
            SqlParameter[] paras = {
                                     new SqlParameter("@RuKDID",RuKDID)
                                 };
            DataTable dt = DBHelper.GetDataSet(CommandType.Text, sql, paras);
            return dt;
        }

        public DataTable SELECTRKDBYIDCX(string RuKDID)
        {
            string sql = "SELECT MSStock.StockSN+' '+MSStock.StockName as Stock,MSClient.ClSN+' '+MSClient.ClName as Client,MSRuKD.RuKDate,MSRuKD.TypeID,MSRuKD.RuKDNO,MSRuKD.RuKMem,MSRuKD.fillName,MSRuKD.fillTime FROM MSRuKD,MSStock,MSClient where MSRuKD.StockID=MSStock.StockID AND MSRuKD.ClientID=MSClient.ClientID AND RuKDID=@RuKDID";
            SqlParameter[] paras = {
                                     new SqlParameter("@RuKDID",RuKDID)
                                 };
            DataTable dt = DBHelper.GetDataSet(CommandType.Text, sql, paras);
            return dt;
        }

        public DataTable SELECTRKDMXBYID(string RuKDID)
        {
            string sql = "SELECT * FROM MSRuKMX WHERE RuKDID=@RuKDID";
            SqlParameter[] paras = {
                                     new SqlParameter("@RuKDID",RuKDID)
                                 };
            DataTable dt = DBHelper.GetDataSet(CommandType.Text, sql, paras);
            return dt;
        }

        public DataTable SELECTRKDMXBYIDCX(string RuKDID)
        {
            string sql = "SELECT MSMaterial.mtSN,MSMaterial.mtName,MSMaterial.mtSpec,MSRuKMX.mtUnit,MSRuKMX.mtNumber,MSRuKMX.mtPrice,MSRuKMX.mtTotal,MSRuKMX.PlaceID,MSRuKMX.MXMemo FROM MSRuKMX,MSMaterial where MSRuKMX.materialID=MSMaterial.materialID AND RuKDID=@RuKDID";
            SqlParameter[] paras = {
                                     new SqlParameter("@RuKDID",RuKDID)
                                 };
            DataTable dt = DBHelper.GetDataSet(CommandType.Text, sql, paras);
            return dt;
        }

        public void DELETERKDMX(string RuKDID)
        {
            IDAL objDAL = DALFactory.GetDBO();
            objDAL.Open();
            string sql = "DELETE FROM MSRuKMX WHERE RuKDID=@RuKDID";
            Parameter[] paras = {
                                    new Parameter("@RuKDID",RuKDID)
                                };
            objDAL.ExecuteNonQuery(CommandType.Text, sql, paras);
            objDAL.Close();
        }

        public void DELETERKD(string RuKDID)
        {
            IDAL objDAL = DALFactory.GetDBO();
            objDAL.Open();
            string sql = "DELETE FROM MSRuKD WHERE RuKDID=@RuKDID";
            Parameter[] paras = {
                                    new Parameter("@RuKDID",RuKDID)
                                };
            objDAL.ExecuteNonQuery(CommandType.Text, sql, paras);
            objDAL.Close();
        }

        public void RuKDTJ(string SMonY, string nStatus, string RuKDID)
        {
            IDAL objDAL = DALFactory.GetDBO();
            objDAL.Open();
            string sql = "update MSRuKD set SMonY=@SMonY,nStatus=@nStatus,RuKTotal=isnull((select sum(mtTotal) from MSRuKMX where RuKDID=@RuKDID),0) where RuKDID=@RuKDID";
            Parameter[] paras = {
                                    new Parameter("@SMonY",SMonY),
                                    new Parameter("@nStatus",nStatus),
                                    new Parameter("@RuKDID",RuKDID)
                                };
            objDAL.ExecuteNonQuery(CommandType.Text, sql, paras);
            objDAL.Close();
        }

        public DataTable SELECTStock(string RuKDID)
        {
            string sql = "select a.RuKDID,a.StockID,b.SMonY,b.RuKNO,b.ChuKNO from MSRuKD a,MSStock b where a.StockID=b.StockID and a.RuKDID=@RuKDID";
            SqlParameter[] paras = {
                                     new SqlParameter("@RuKDID",RuKDID)
                                 };
            DataTable dt = DBHelper.GetDataSet(CommandType.Text, sql, paras);
            return dt;
        }

        public DataTable SELECTMSRealStock(string StockID, string materialID, string PlaceID)
        {
            string sql = "select StockID,mtTotal,mtNumber from MSRealStock where StockID=@StockID and materialID=@materialID and PlaceID=@PlaceID";
            SqlParameter[] paras = {
                                     new SqlParameter("@StockID",StockID),
                                     new SqlParameter("@materialID",materialID),
                                     new SqlParameter("@PlaceID",PlaceID)
                                 };
            DataTable dt = DBHelper.GetDataSet(CommandType.Text, sql, paras);
            return dt;
        }

        public void UPDATEMSRealStock(string mtNumber, string mtTotal, string StockID, string materialID, string PlaceID)
        {
            IDAL objDAL = DALFactory.GetDBO();
            objDAL.Open();
            string sql = "update MSRealStock set mtNumber=@mtNumber, mtTotal=@mtTotal where StockID=@StockID and materialID=@materialID and PlaceID=@PlaceID";
            Parameter[] paras = {
                                     new Parameter("@mtNumber",mtNumber),
                                     new Parameter("@mtTotal",mtTotal),
                                     new Parameter("@StockID",StockID),
                                     new Parameter("@materialID",materialID),
                                     new Parameter("@PlaceID",PlaceID)
                                 };
            objDAL.ExecuteNonQuery(CommandType.Text, sql, paras);
            objDAL.Close();
        }

        public void INSERT_MSRealStock(string StockID, string materialID, string PlaceID, string mtUnit, string mtPrice, string mtNumber, string mtTotal)
        {
            IDAL objDAL = DALFactory.GetDBO();
            objDAL.Open();
            string sql = "INSERT INTO MSRealStock(StockID,materialID,PlaceID,mtUnit,mtPrice,mtNumber,mtTotal) VALUES(@StockID,@materialID,@PlaceID,@mtUnit,@mtPrice,@mtNumber,@mtTotal)";
            Parameter[] paras = {
                                     new Parameter("@StockID",StockID),
                                     new Parameter("@materialID",materialID),
                                     new Parameter("@PlaceID",PlaceID),
                                     new Parameter("@mtUnit",mtUnit),
                                     new Parameter("@mtPrice",mtPrice),
                                     new Parameter("@mtNumber",mtNumber),
                                     new Parameter("@mtTotal",mtTotal)
                                 };
            objDAL.ExecuteNonQuery(CommandType.Text, sql, paras);
            objDAL.Close();
        }

        public void UPDATEMSRuKD(string SMonY, string nStatus, string isAudi, string AudiName, string AudiID, string RuKDID)
        {
            IDAL objDAL = DALFactory.GetDBO();
            objDAL.Open();
            string sql = "update MSRuKD set SMonY=@SMonY,nStatus=@nStatus,isAudi=@isAudi,AudiName=@AudiName,AudiID=@AudiID where RuKDID=@RuKDID";
            Parameter[] paras = {
                                     new Parameter("@SMonY",SMonY),
                                     new Parameter("@nStatus",nStatus),
                                     new Parameter("@isAudi",isAudi),
                                     new Parameter("@AudiName",AudiName),
                                     new Parameter("@AudiID",AudiID),
                                     new Parameter("@RuKDID",RuKDID)
                                 };
            objDAL.ExecuteNonQuery(CommandType.Text, sql, paras);
            objDAL.Close();
        }

        public void UPDATEMSRuKD(string RuKDID)
        {
            IDAL objDAL = DALFactory.GetDBO();
            objDAL.Open();
            string sql = "update MSRuKD set  RuKTotal=(select sum(mtTotal) from MSRuKMX where RuKDID=@RuKDID) where RuKDID=@RuKDID";
            Parameter[] paras = {
                                     new Parameter("@RuKDID",RuKDID)
                                 };
            objDAL.ExecuteNonQuery(CommandType.Text, sql, paras);
            objDAL.Close();
        }

        public void UPDATEMSStock(string RuKNO, string StockID)
        {
            IDAL objDAL = DALFactory.GetDBO();
            objDAL.Open();
            string sql = "update MSStock set RuKNO=@RuKNO where StockID=@StockID";
            Parameter[] paras = {
                                     new Parameter("@RuKNO",RuKNO),
                                     new Parameter("@StockID",StockID)
                                 };
            objDAL.ExecuteNonQuery(CommandType.Text, sql, paras);
            objDAL.Close();
        }

        public void UPDATEMSMaterial(string RuKDID)
        {
            IDAL objDAL = DALFactory.GetDBO();
            objDAL.Open();
            string sql = "UPDATE MSMaterial SET mtBuyPrice=MSRuKMX.mtPrice FROM MSRuKMX WHERE MSMaterial.materialID=MSRuKMX.materialID AND MSRuKMX.RuKDID=@RuKDID";
            Parameter[] paras = {
                                     new Parameter("@RuKDID",RuKDID)
                                 };
            objDAL.ExecuteNonQuery(CommandType.Text, sql, paras);
            objDAL.Close();
        }

        public DataTable SELECTISHAVEFP(string RuKDID)
        {
            string sql = "select RuKDID from FaPRuKD where RuKDID=@RuKDID";
            SqlParameter[] paras = {
                                     new SqlParameter("@RuKDID",RuKDID)
                                 };
            DataTable dt = DBHelper.GetDataSet(CommandType.Text, sql, paras);
            return dt;
        }

        public DataTable SELECRKWK(string materialID, string isAudi, string RuKDates, string RuKDatee)
        {
            string sql = "SELECT X.*,Y.ClName FROM(select a.RuKMXID,a.RuKDID,a.materialID,a.mtUnit, a.mtPrice,a.mtNumber,a.mtTotal,a.PlaceID,a.MXMemo, b.mtName,b.mtSpec,b.mtNO,b.mtSN,c.RuKDate,c.RuKDNO,c.ClientID from MSRuKMX a ,MSMaterial b,MSRuKD c where a.materialID=b.materialID and a.RuKDID=c.RuKDID and a.materialID=@materialID and c.isAudi=@isAudi";
            if (RuKDates != "")
            {
                sql = sql + " and c.RuKDate>=@RuKDates";
            }
            if (RuKDatee != "")
            {
                sql = sql + " and c.RuKDate<=@RuKDatee";
            }
            sql = sql + ")X LEFT JOIN MSClient Y ON X.ClientID=Y.ClientID order by X.RuKDate desc";
            SqlParameter[] paras = {
                                     new SqlParameter("@materialID",materialID),
                                     new SqlParameter("@isAudi",isAudi),
                                     new SqlParameter("@RuKDates",RuKDates),
                                     new SqlParameter("@RuKDatee",RuKDatee)
                                 };
            DataTable dt = DBHelper.GetDataSet(CommandType.Text, sql, paras);
            return dt;
        }

        public DataTable SELECRKWK1(string materialID, string isAudi, string RuKDates, string RuKDatee)
        {
            string sql = "SELECT X.*,Y.ClName FROM(select a.RuKMXID,a.RuKDID,a.materialID,a.mtUnit, a.mtPrice,a.mtNumber,a.mtTotal,a.PlaceID,a.MXMemo, b.mtName,b.mtSpec,b.mtNO,b.mtSN,c.RuKDate,c.RuKDNO,c.ClientID from MSRuKMX a ,MSMaterial b,MSRuKD c where a.materialID=b.materialID and a.RuKDID=c.RuKDID and a.materialID=@materialID and c.isAudi=@isAudi";
            if (RuKDates != "")
            {
                sql = sql + " and c.RuKDate>=@RuKDates";
            }
            if (RuKDatee != "")
            {
                sql = sql + " and c.RuKDate<=@RuKDatee";
            }
            sql = sql + ")X LEFT JOIN MSClient Y ON X.ClientID=Y.ClientID order by X.RuKDate";
            SqlParameter[] paras = {
                                     new SqlParameter("@materialID",materialID),
                                     new SqlParameter("@isAudi",isAudi),
                                     new SqlParameter("@RuKDates",RuKDates),
                                     new SqlParameter("@RuKDatee",RuKDatee)
                                 };
            DataTable dt = DBHelper.GetDataSet(CommandType.Text, sql, paras);
            return dt;
        }

        public DataTable SELECTcount_RuKDNO(string RuKDNO, string RuKDID, string ClientID)
        {
            string sql = "select count(*) as NO from MSRukd where RuKDNO=@RuKDNO and RuKDID<>@RuKDID and ClientID=@ClientID";
            SqlParameter[] paras = {
                                     new SqlParameter("@RuKDNO",RuKDNO),
                                     new SqlParameter("@RuKDID",RuKDID),
                                     new SqlParameter("@ClientID",ClientID)
                                 };
            DataTable dt = DBHelper.GetDataSet(CommandType.Text, sql, paras);
            return dt;
        }

        public DataTable SELECT_MSPlace(string PlaceID)
        {
            string sql = "select PlaceSN+' '+PlaceName as Place from MSPlace where PlaceID=@PlaceID";
            SqlParameter[] paras = {
                                     new SqlParameter("@PlaceID",PlaceID)
                                 };
            DataTable dt = DBHelper.GetDataSet(CommandType.Text, sql, paras);
            return dt;
        }

        public DataTable INSERT_RUKYZ(string CK, string GYS, string RKRQ, string RKLX, string RKDH, string RKBZ, string ZDR, string ZDSJ, string RUKDID)
        {
            string sql = "insert into RUKYZ(CK,GYS,RKRQ,RKLX,RKDH,RKBZ,ZDR,ZDSJ,RUKDID) values(@CK,@GYS,@RKRQ,@RKLX,@RKDH,@RKBZ,@ZDR,@ZDSJ,@RUKDID)";
            sql = sql + " select @@identity as RI";
            SqlParameter[] paras = {
                                     new SqlParameter("@CK",CK),
                                     new SqlParameter("@GYS",GYS),
                                     new SqlParameter("@RKRQ",RKRQ),
                                     new SqlParameter("@RKLX",RKLX),
                                     new SqlParameter("@RKDH",RKDH),
                                     new SqlParameter("@RKBZ",RKBZ),
                                     new SqlParameter("@ZDR",ZDR),
                                     new SqlParameter("@ZDSJ",ZDSJ),
                                     new SqlParameter("@RUKDID",RUKDID)
                                 };
            DataTable dt = DBHelper.GetDataSet(CommandType.Text, sql, paras);
            return dt;
        }

        public void INSERT_RUKYZMX(string RKRI, string RKMXID, string materialID, string mtUnit, string mtNumber, string mtPrice, string mtTotal, string PlaceID, string MXMemo)
        {
            IDAL objDAL = DALFactory.GetDBO();
            objDAL.Open();
            string sql = "insert into RUKYZMX(RKRI,RKMXID,materialID,mtUnit,mtNumber,mtPrice,mtTotal,PlaceID,MXMemo) values(@RKRI,@RKMXID,@materialID,@mtUnit,@mtNumber,@mtPrice,@mtTotal,@PlaceID,@MXMemo)";
            Parameter[] paras = {
                                     new Parameter("@RKRI",RKRI),
                                     new Parameter("@RKMXID",RKMXID),
                                     new Parameter("@materialID",materialID),
                                     new Parameter("@mtUnit",mtUnit),
                                     new Parameter("@mtNumber",mtNumber),
                                     new Parameter("@mtPrice",mtPrice),
                                     new Parameter("@mtTotal",mtTotal),
                                     new Parameter("@PlaceID",PlaceID),
                                     new Parameter("@MXMemo",MXMemo)
                                 };
            objDAL.ExecuteNonQuery(CommandType.Text, sql, paras);
            objDAL.Close();
        }

        public DataTable SELECT_RUKYZ(string RI)
        {
            string sql = "select * from RUKYZ where RI=@RI";
            SqlParameter[] paras ={
                                    new SqlParameter("@RI",RI)  
                                  };
            DataTable dt = DBHelper.GetDataSet(CommandType.Text, sql, paras);
            return dt;
        }

        public DataTable SELECT_RUKYZMX(string RKRI)
        {
            string sql = "select * from RUKYZMX where RKRI=@RKRI ORDER BY RKMXID";
            SqlParameter[] paras ={
                                    new SqlParameter("@RKRI",RKRI)  
                                  };
            DataTable dt = DBHelper.GetDataSet(CommandType.Text, sql, paras);
            return dt;
        }

        public DataTable selectHJ(string s)
        {
            string sql = "exec('select MSType.typeID,MSType.typeName,MSMaterial.materialID,MSMaterial.mtName,MSMaterial.mtUnit,MSMaterial.mtBuyPrice,SUM(MSRuKMX.mtNumber) as mtNumber,SUM(MSRuKMX.mtTotal) as mtTotal ";
            sql = sql + "from MSType,MSMaterial,MSRuKMX ";
            sql = sql + "where MSMaterial.typeID = MSType.typeID ";
            sql = sql + "and MSMaterial.materialID = MSRuKMX.materialID ";
            sql = sql + "and MSRuKMX.RuKDID in (";

            sql = sql + "'+@s+') ";

            sql = sql + "group by MSMaterial.mtName,MSType.typeID,MSType.typeName,MSMaterial.mtUnit,MSMaterial.mtBuyPrice,MSMaterial.materialID')";

            SqlParameter[] paras ={
                                    new SqlParameter("@s",s)
                                  };
            DataTable dt = DBHelper.GetDataSet(CommandType.Text, sql, paras);
            return dt;
        }

        public void save_record(string type,string danhao,string time)
        {
            IDAL objDAL = DALFactory.GetDBO();
            objDAL.Open();
            string sql = "insert into OperationRecord(type,danhao,time) values(@type,@danhao,@time)";
            Parameter[] paras = {
                                     new Parameter("@type",type),
                                     new Parameter("@danhao",danhao),
                                     new Parameter("@time",time)
                                 };
            objDAL.ExecuteNonQuery(CommandType.Text, sql, paras);
            objDAL.Close();
        }

        public DataTable ReadRuKList(string begindate, string enddate)
        {
            string str = "RUKU_ReadRKList";
            SqlParameter[] paras ={
                                    new SqlParameter("@begindate",begindate),
                                    new SqlParameter("@enddate",enddate)
                                  };
            DataTable dt = DBHelper.GetDataSet(CommandType.StoredProcedure, str, paras);
            return dt;
        }

    }
}
