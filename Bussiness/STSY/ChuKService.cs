using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using Bussiness.SqlHelper;

namespace Bussiness.STSY
{
    public class ChuKService
    {
        public DataTable SELECTCKDBYID(string ChuKDID)
        {
            string sql = "SELECT * FROM MSChuKD WHERE ChuKDID=@ChuKDID";
            SqlParameter[] paras = {
                                     new SqlParameter("@ChuKDID",ChuKDID)
                                 };
            DataTable dt = DBHelper.GetDataSet(CommandType.Text, sql, paras);
            return dt;
        }

        public DataTable SELECTCKDMXBYID(string ChuKDID)
        {
            string sql = "SELECT * FROM MSChuKMX WHERE ChuKDID=@ChuKDID";
            SqlParameter[] paras = {
                                     new SqlParameter("@ChuKDID",ChuKDID)
                                 };
            DataTable dt = DBHelper.GetDataSet(CommandType.Text, sql, paras);
            return dt;
        }

        public DataTable SELECTCKDMXBYID1(string ChuKDID)
        {
            string sql = "SELECT MSChuKMX.*,MSMaterial.mtSN,MSMaterial.mtName,MSMaterial.mtSpec FROM MSChuKMX,MSMaterial WHERE ChuKDID=@ChuKDID and MSChuKMX.materialID=MSMaterial.materialID";
            SqlParameter[] paras = {
                                     new SqlParameter("@ChuKDID",ChuKDID)
                                 };
            DataTable dt = DBHelper.GetDataSet(CommandType.Text, sql, paras);
            return dt;
        }

        public DataTable GETKHINFO()
        {
            string str = "select ClientID,ClSN,ClName from MSClient where isSale=1 and isDelete=0 order by ClSN";
            DataTable dt = DBHelper.GetDataSet(CommandType.Text, str);
            return dt;
        }

        public DataTable SELECTCKD(string nStatus, string StockID, string ClientID, string ChuKDates, string ChuKDatee, string TypeID, string ChuKDNO, string fillID, string IS)
        {
            string sql = "select x.*,b.ClSN,b.ClName from(select a.ChuKDID,a.ChuKDNO,a.StockID,a.SMonY,a.ClientID, ";
            sql = sql + "a.ChuKDate,a.ChuKMem,a.fillID,a.fillName,a.fillTime,a.isAudi,a.nStatus, c.StockSN,c.StockName from MSChuKD a,MSStock c where a.StockID=c.StockID ";
            if (StockID != "0")
            {
                sql = sql + " AND a.StockID=@StockID";
            }
            if (ClientID != "0")
            {
                sql = sql + " AND a.ClientID=@ClientID";
            }
            if (ChuKDates != "")
            {
                sql = sql + " AND a.ChuKDate>=@ChuKDates";
            }
            if (ChuKDatee != "")
            {
                sql = sql + " AND a.ChuKDate<=@ChuKDatee";
            }
            if (TypeID != "-1")
            {
                sql = sql + " AND a.TypeID=@TypeID";
            }
            if (ChuKDNO != "")
            {
                sql = sql + " AND a.ChuKDNO=@ChuKDNO";
            }
            if (IS == "TRUE")
            {
                sql = sql + " and a.fillID=@fillID ";
            }
            if (nStatus == "0")
            {
                sql = sql + " and a.nStatus=0 and a.fillID=@fillID ";
            }
            if (nStatus == "1")
            {
                sql = sql + " and a.nStatus=1 ";
            }
            if (nStatus == "2")
            {
                sql = sql + " and a.isAudi=1 ";
            }
            if (nStatus == "3")
            {
                sql = sql + " and (a.nStatus>0 or a.fillID=@fillID) ";
            }
            sql = sql + " )x left join MSClient b on x.ClientID=b.ClientID";
            SqlParameter[] paras = {
                                     new SqlParameter("@StockID",StockID),
                                     new SqlParameter("@ClientID",ClientID),
                                     new SqlParameter("@ChuKDates",ChuKDates),
                                     new SqlParameter("@ChuKDatee",ChuKDatee),
                                     new SqlParameter("@TypeID",TypeID),
                                     new SqlParameter("@ChuKDNO",ChuKDNO),
                                     new SqlParameter("@fillID",fillID)
                                 };
            DataTable dt = DBHelper.GetDataSet(CommandType.Text, sql, paras);
            return dt;
        }

        public void DELETECKDMX(string ChuKDID)
        {
            IDAL objDAL = DALFactory.GetDBO();
            objDAL.Open();
            string sql = "DELETE FROM MSChuKMX WHERE ChuKDID=@ChuKDID";
            Parameter[] paras = {
                                    new Parameter("@ChuKDID",ChuKDID)
                                };
            objDAL.ExecuteNonQuery(CommandType.Text, sql, paras);
            objDAL.Close();
        }

        public void DELETECKD(string ChuKDID)
        {
            IDAL objDAL = DALFactory.GetDBO();
            objDAL.Open();
            string sql = "DELETE FROM MSChuKD WHERE ChuKDID=@ChuKDID";
            Parameter[] paras = {
                                    new Parameter("@ChuKDID",ChuKDID)
                                };
            objDAL.ExecuteNonQuery(CommandType.Text, sql, paras);
            objDAL.Close();
        }

        public string insertCKDH(string ChuKDNO, string StockID, string ClientID, string ChuKDate, string ChuKMem, string fillID, string fillName, string fillTime, string isAudi, string nStatus, string TypeID)
        {
            string str = "SELECT SYSINT FROM HG_SYSINS where SYSNAME = 'MSChuKD'";
            DataTable dt = DBHelper.GetDataSet(CommandType.Text, str);
            IDAL objDAL = DALFactory.GetDBO();
            objDAL.Open();
            string sql = "insert into MSChuKD(ChuKDID,ChuKDNO,StockID,SMonY,ClientID,ChuKDate,ChuKMem,fillID,fillName,fillTime,isAudi,nStatus,TypeID) values (@ChuKDID,@ChuKDNO,@StockID,0,@ClientID,@ChuKDate,@ChuKMem,@fillID,@fillName,@fillTime,@isAudi,@nStatus,@TypeID)";
            Parameter[] paras = {
                                    new Parameter("@ChuKDID",(Convert.ToInt32(dt.Rows[0][0].ToString())+1).ToString()),
                                    new Parameter("@ChuKDNO",ChuKDNO),
                                    new Parameter("@StockID",StockID),
                                    new Parameter("@ClientID",ClientID),
                                    new Parameter("@ChuKDate",ChuKDate),
                                    new Parameter("@ChuKMem",ChuKMem),
                                    new Parameter("@fillID",fillID),
                                    new Parameter("@fillName",fillName),
                                    new Parameter("@fillTime",fillTime),
                                    new Parameter("@isAudi",isAudi),
                                    new Parameter("@nStatus",nStatus),
                                    new Parameter("@TypeID",TypeID),
                                };
            objDAL.ExecuteNonQuery(CommandType.Text, sql, paras);

            string sql1 = "UPDATE HG_SYSINS SET SYSINT=@SYSINT WHERE upper(SYSNAME)='MSChuKD'";
            Parameter[] paras1 = {
                                    new Parameter("@SYSINT",(Convert.ToInt32(dt.Rows[0][0].ToString())+1).ToString())
                                };
            objDAL.ExecuteNonQuery(CommandType.Text, sql1, paras1);

            objDAL.Close();
            return (Convert.ToInt32(dt.Rows[0][0].ToString()) + 1).ToString();
        }

        public void insertCKDMX(string ChuKDID, string materialID, string mtNumber, string mtUnit, string mtPrice, string mtTotal, string PlaceID, string MXMemo, string mtBuyPrice, string mtBuyTotal)
        {
            string str = "SELECT SYSINT FROM HG_SYSINS where SYSNAME = 'MSChuKMX'";
            DataTable dt = DBHelper.GetDataSet(CommandType.Text, str);
            IDAL objDAL = DALFactory.GetDBO();
            objDAL.Open();
            string sql = " insert into MSChuKMX(ChuKMXID,ChuKDID,materialID,mtNumber,mtUnit ,mtPrice,mtTotal,PlaceID,MXMemo,mtBuyPrice,mtBuyTotal) values (@ChuKMXID,@ChuKDID,@materialID,@mtNumber,@mtUnit, @mtPrice,@mtTotal,@PlaceID,@MXMemo,@mtBuyPrice,@mtBuyTotal)";
            Parameter[] paras = {
                                    new Parameter("@ChuKMXID",(Convert.ToInt32(dt.Rows[0][0].ToString())+1).ToString()),
                                    new Parameter("@ChuKDID",ChuKDID),
                                    new Parameter("@materialID",materialID),
                                    new Parameter("@mtNumber",mtNumber),
                                    new Parameter("@mtUnit",mtUnit),
                                    new Parameter("@mtPrice",mtPrice),
                                    new Parameter("@mtTotal",mtTotal),
                                    new Parameter("@PlaceID",PlaceID),
                                    new Parameter("@MXMemo",MXMemo),
                                    new Parameter("@mtBuyPrice",mtBuyPrice),
                                    new Parameter("@mtBuyTotal",mtBuyTotal)
                                };
            objDAL.ExecuteNonQuery(CommandType.Text, sql, paras);

            string sql1 = "UPDATE HG_SYSINS SET SYSINT=@SYSINT WHERE upper(SYSNAME)='MSChuKMX'";
            Parameter[] paras1 = {
                                    new Parameter("@SYSINT",(Convert.ToInt32(dt.Rows[0][0].ToString())+1).ToString()),
                                };
            objDAL.ExecuteNonQuery(CommandType.Text, sql1, paras1);

            objDAL.Close();
        }

        public DataTable GETWLTYPEADD()
        {
            string str = "select distinct MSType.typeID,MSType.typeSN,MSType.typeName from MSRealStock,MSMaterial,MSType where MSRealStock.mtNumber > 0 and MSMaterial.materialID = MSRealStock.materialID and MSType.typeID = MSMaterial.typeID order by MSType.typeID";
            DataTable dt = DBHelper.GetDataSet(CommandType.Text, str);
            return dt;
        }

        public DataTable GETWLTYPEADD(string typeID)
        {
            string str = "SELECT MSMaterial.materialID,MSMaterial.mtName,MSMaterial.mtSN FROM MSMaterial,MSRealStock ";
            str = str + "WHERE MSMaterial.typeID=@typeID AND MSMaterial.isDelete = '0' AND MSRealStock.mtNumber>0 AND MSMaterial.materialID = MSRealStock.materialID ";
            str = str + "ORDER BY MSMaterial.mtSN ";
            SqlParameter[] paras = {
                                       new SqlParameter("@typeID",typeID)
                                   };
            DataTable dt = DBHelper.GetDataSet(CommandType.Text, str, paras);
            return dt;
        }

        public DataTable GETWLKCINFO(string materialID)
        {
            string str = "SELECT mtPrice,mtNumber,mtTotal FROM MSRealStock WHERE materialID = @materialID";
            SqlParameter[] paras = {
                                       new SqlParameter("@materialID",materialID)
                                   };
            DataTable dt = DBHelper.GetDataSet(CommandType.Text, str, paras);
            return dt;
        }

        public void UPDATETJ(string SMonY, string nStatus, string ChuKDID)
        {
            IDAL objDAL = DALFactory.GetDBO();
            objDAL.Open();
            string sql = "update MSChuKD set SMonY=@SMonY,nStatus=@nStatus where ChuKDID=@ChuKDID";
            Parameter[] paras = {
                                    new Parameter("@SMonY",SMonY),
                                    new Parameter("@nStatus",nStatus),
                                    new Parameter("@ChuKDID",ChuKDID)
                                };
            objDAL.ExecuteNonQuery(CommandType.Text, sql, paras);
            objDAL.Close();
        }

        public void UPDATETJTH(string nStatus, string ChuKDID)
        {
            IDAL objDAL = DALFactory.GetDBO();
            objDAL.Open();
            string sql = "update MSChuKD set nStatus=@nStatus where ChuKDID=@ChuKDID";
            Parameter[] paras = {
                                    new Parameter("@nStatus",nStatus),
                                    new Parameter("@ChuKDID",ChuKDID)
                                };
            objDAL.ExecuteNonQuery(CommandType.Text, sql, paras);
            objDAL.Close();
        }

        public DataTable GETWLKC(string StockID, string materialID, string PlaceID)
        {
            string str = "select StockID,mtTotal,mtNumber from MSRealStock where StockID=@StockID and materialID=@materialID and PlaceID=@PlaceID";
            SqlParameter[] paras = {
                                       new SqlParameter("@StockID",StockID),
                                       new SqlParameter("@materialID",materialID),
                                       new SqlParameter("@PlaceID",PlaceID)
                                   };
            DataTable dt = DBHelper.GetDataSet(CommandType.Text, str, paras);
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

        public void UPDATEMSChuKD(string SMonY, string nStatus, string isAudi, string AudiID, string AudiName, string ChuKDID)
        {
            IDAL objDAL = DALFactory.GetDBO();
            objDAL.Open();
            string sql = "update MSChuKD set SMonY=@SMonY,nStatus=@nStatus,isAudi=@isAudi,AudiID=@AudiID,AudiName=@AudiName where ChuKDID=@ChuKDID";
            Parameter[] paras = {
                                    new Parameter("@SMonY",SMonY),
                                    new Parameter("@nStatus",nStatus),
                                    new Parameter("@isAudi",isAudi),
                                    new Parameter("@AudiID",AudiID),
                                    new Parameter("@AudiName",AudiName),
                                    new Parameter("@ChuKDID",ChuKDID)
                                };
            objDAL.ExecuteNonQuery(CommandType.Text, sql, paras);
            objDAL.Close();
        }

        public void UPDATEMSMaterial(string ChuKDID)
        {
            IDAL objDAL = DALFactory.GetDBO();
            objDAL.Open();
            string sql = "UPDATE MSMaterial SET mtSalePrice=MSChuKMX.mtPrice FROM MSChuKMX WHERE MSMaterial.materialID = MSChuKMX.materialID AND MSChuKMX.ChuKDID = @ChuKDID";
            Parameter[] paras = {
                                    new Parameter("@ChuKDID",ChuKDID)
                                };
            objDAL.ExecuteNonQuery(CommandType.Text, sql, paras);
            objDAL.Close();
        }

        public DataTable insert_CHUKYZ(string CK, string KH, string CKRQ, string CKLX, string CKDH, string CKBZ, string ZDR, string ZDSJ)
        {
            string sql = "insert into CHUKYZ(CK,KH,CKRQ,CKLX,CKDH,CKBZ,ZDR,ZDSJ) values (@CK,@KH,@CKRQ,@CKLX,@CKDH,@CKBZ,@ZDR,@ZDSJ)";
            sql = sql + " select @@identity as RI";
            SqlParameter[] paras = {
                                    new SqlParameter("@CK",CK),
                                    new SqlParameter("@KH",KH),
                                    new SqlParameter("@CKRQ",CKRQ),
                                    new SqlParameter("@CKLX",CKLX),
                                    new SqlParameter("@CKDH",CKDH),
                                    new SqlParameter("@CKBZ",CKBZ),
                                    new SqlParameter("@ZDR",ZDR),
                                    new SqlParameter("@ZDSJ",ZDSJ)
                                };
            DataTable dt = DBHelper.GetDataSet(CommandType.Text, sql, paras);
            return dt;
        }

        public DataTable insert_CHUKYZMX(string CKRI, string materialID, string mtSpec, string mtUnit, string mtNumber, string PJJ, string mtTotal, string KW, string KwMC, string SM)
        {
            string sql = "insert into CHUKYZMX(CKRI,materialID,mtSpec,mtUnit,mtNumber,PJJ,mtTotal,KW,KwMC,SM) values (@CKRI,@materialID,@mtSpec,@mtUnit,@mtNumber,@PJJ,@mtTotal,@KW,@KwMC,@SM)";
            sql = sql + " select @@identity as RI";
            SqlParameter[] paras = {
                                    new SqlParameter("@CKRI",CKRI),
                                    new SqlParameter("@materialID",materialID),
                                    new SqlParameter("@mtSpec",mtSpec),
                                    new SqlParameter("@mtUnit",mtUnit),
                                    new SqlParameter("@mtNumber",mtNumber),
                                    new SqlParameter("@PJJ",PJJ),
                                    new SqlParameter("@mtTotal",mtTotal),
                                    new SqlParameter("@KW",KW),
                                    new SqlParameter("@KwMC",KwMC),
                                    new SqlParameter("@SM",SM)
                                };
            DataTable dt = DBHelper.GetDataSet(CommandType.Text, sql, paras);
            return dt;
        }

        public DataTable SELECT_CHUKYZ(string RI)
        {
            string sql = "SELECT * FROM CHUKYZ WHERE RI=@RI";
            SqlParameter[] paras = {
                                     new SqlParameter("@RI",RI)
                                 };
            DataTable dt = DBHelper.GetDataSet(CommandType.Text, sql, paras);
            return dt;
        }

        public DataTable SELECT_CHUKYZMX(string CKRI)
        {
            string sql = "SELECT * FROM CHUKYZMX WHERE CKRI=@CKRI";
            SqlParameter[] paras = {
                                     new SqlParameter("@CKRI",CKRI)
                                 };
            DataTable dt = DBHelper.GetDataSet(CommandType.Text, sql, paras);
            return dt;
        }

        public DataTable ssss(string ChuKDID)
        {
            string sql = "SELECT * FROM MSChuKMX WHERE ChuKDID>=@ChuKDID";
            SqlParameter[] paras = {
                                     new SqlParameter("@ChuKDID",ChuKDID)
                                 };
            DataTable dt = DBHelper.GetDataSet(CommandType.Text, sql, paras);
            return dt;
        }

        public void ssss(string ChuKMXID, string mtBuyTotal, string mtTotal)
        {
            IDAL objDAL = DALFactory.GetDBO();
            objDAL.Open();
            string sql = "UPDATE MSChuKMX SET mtBuyTotal=@mtBuyTotal,mtTotal=@mtTotal where ChuKMXID=@ChuKMXID";
            Parameter[] paras = {
                                    new Parameter("@ChuKMXID",ChuKMXID),
                                    new Parameter("@mtBuyTotal",mtBuyTotal),
                                    new Parameter("@mtTotal",mtTotal)
                                };
            objDAL.ExecuteNonQuery(CommandType.Text, sql, paras);
            objDAL.Close();
        }
    }
}
