using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Bussiness.SqlHelper;
using System.Data.SqlClient;

namespace Bussiness.STSY
{
    public class CKService
    {
        public DataTable GETCKINFO()
        {
            string str = "SELECT StockID,StockSN,StockName,isUse from MSStock where isDelete='0' order by StockSN,StockName";
            DataTable dt = DBHelper.GetDataSet(CommandType.Text, str);
            return dt;
        }
        public DataTable GETCKINFOISUSER()
        {
            string str = "SELECT StockID,StockSN,StockName,isUse from MSStock where isDelete='0' and isUse='1' order by StockSN,StockName";
            DataTable dt = DBHelper.GetDataSet(CommandType.Text, str);
            return dt;
        }

        public DataTable GETCKINFOISUSERLB()
        {
            string str = "SELECT StockID,StockSN+' '+StockName as NAME from MSStock where isDelete='0' and isUse='1' order by StockSN,StockName";
            DataTable dt = DBHelper.GetDataSet(CommandType.Text, str);
            return dt;
        }

        public DataTable GETKWINFO(string StockID)
        {
            string str = "SELECT * from MSPlace where isDelete='0' AND StockID=@StockID order by PlaceSN,PlaceName ";
            SqlParameter[] para = {
                                       new SqlParameter("@StockID",StockID)
                                   };
            DataTable dt = DBHelper.GetDataSet(CommandType.Text, str, para);
            return dt;
        }

        public DataTable GETKWINFO()
        {
            string str = "SELECT * from MSPlace where isDelete='0'";
            DataTable dt = DBHelper.GetDataSet(CommandType.Text, str);
            return dt;
        }

        public DataTable GETCKINFO(string StockID)
        {
            string str = "SELECT * from MSStock where StockID=@StockID";
            SqlParameter[] para = {
                                      new SqlParameter("@StockID",StockID)
                                  };
            DataTable dt = DBHelper.GetDataSet(CommandType.Text, str, para);
            return dt;
        }

        public DataTable GETKWINFOBYKWID(string PlaceID)
        {
            string str = "SELECT * from MSPlace where PlaceID=@PlaceID";
            SqlParameter[] para = {
                                       new SqlParameter("@PlaceID",PlaceID)
                                   };
            DataTable dt = DBHelper.GetDataSet(CommandType.Text, str, para);
            return dt;
        }

        public void DELETECK(string StockID)
        {
            IDAL objDAL = DALFactory.GetDBO();
            objDAL.Open();
            string sql = "UPDATE MSStock SET isDelete='1' WHERE StockID=@StockID";
            Parameter[] paras = {
                                    new Parameter("@StockID",StockID)
                                };
            objDAL.ExecuteNonQuery(CommandType.Text, sql, paras);
            objDAL.Close();
        }

        public DataTable SELECT_MSClient(string isDelete)
        {
            string str = "select ClientID,ClSN,ClName,isBuy,isSale,isDelete from MSClient where isDelete=@isDelete order by ClSN,ClName";
            SqlParameter[] para = {
                                       new SqlParameter("@isDelete",isDelete)
                                   };
            DataTable dt = DBHelper.GetDataSet(CommandType.Text, str, para);
            return dt;
        }

        public DataTable SELECT_MSClient_ID(string ClientID)
        {
            string str = "select ClientID,ClSN,ClName,ClMan,ClAddress,CBANK,CBANKNO, ClPost,ClTel,ClFax,ClEMail,ClMemo,isBuy,isSale,isDelete from MSClient where ClientID=@ClientID";
            SqlParameter[] para = {
                                       new SqlParameter("@ClientID",ClientID)
                                   };
            DataTable dt = DBHelper.GetDataSet(CommandType.Text, str, para);
            return dt;
        }

        public DataTable SELECT_MSClient_COUNT(string ClSN, string ClientID)
        {
            string str = "select ClSN from MSClient where ClSN=@ClSN and ClientID<>@ClientID";
            SqlParameter[] para = {
                                       new SqlParameter("@ClSN",ClSN),
                                       new SqlParameter("@ClientID",ClientID)
                                   };
            DataTable dt = DBHelper.GetDataSet(CommandType.Text, str, para);
            return dt;
        }

        public string insert_MSCLIENT(string ClSN, string ClName, string ClMan, string ClAddress, string ClPost, string ClTel, string ClFax, string ClEMail, string ClMemo, string isBuy, string isSale, string isDelete, string CBANK, string CBANKNO)
        {
            string str = "SELECT SYSINT FROM HG_SYSINS WHERE upper(SYSNAME)='MSCLIENT'";
            DataTable dt = DBHelper.GetDataSet(CommandType.Text, str);
            IDAL objDAL = DALFactory.GetDBO();
            objDAL.Open();
            string sql = "insert into MSClient(ClientID,ClSN,ClName,ClMan,ClAddress,ClPost,ClTel,ClFax,ClEMail,ClMemo,isBuy,isSale,isDelete,CBANK,CBANKNO) values (@ClientID,@ClSN,@ClName,@ClMan,@ClAddress,@ClPost,@ClTel,@ClFax,@ClEMail,@ClMemo,@isBuy,@isSale,@isDelete,@CBANK,@CBANKNO)";
            Parameter[] paras = {
                                    new Parameter("@ClientID",(Convert.ToInt32(dt.Rows[0][0].ToString())+1).ToString()),
                                    new Parameter("@ClSN",ClSN),
                                    new Parameter("@ClName",ClName),
                                    new Parameter("@ClMan",ClMan),
                                    new Parameter("@ClAddress",ClAddress),
                                    new Parameter("@ClPost",ClPost),
                                    new Parameter("@ClTel",ClTel),
                                    new Parameter("@ClFax",ClFax),
                                    new Parameter("@ClEMail",ClEMail),
                                    new Parameter("@ClMemo",ClMemo),
                                    new Parameter("@isBuy",isBuy),
                                    new Parameter("@isSale",isSale),
                                    new Parameter("@isDelete",isDelete),
                                    new Parameter("@CBANK",CBANK),
                                    new Parameter("@CBANKNO",CBANKNO)
                                };
            objDAL.ExecuteNonQuery(CommandType.Text, sql, paras);

            string sql1 = "UPDATE HG_SYSINS SET SYSINT=@SYSINT WHERE upper(SYSNAME)='MSCLIENT'";
            Parameter[] paras1 = {
                                    new Parameter("@SYSINT",(Convert.ToInt32(dt.Rows[0][0].ToString())+1).ToString()),
                                };
            objDAL.ExecuteNonQuery(CommandType.Text, sql1, paras1);

            objDAL.Close();
            return (Convert.ToInt32(dt.Rows[0][0].ToString()) + 1).ToString();
        }

        public void DELETE_MSCLIENT(string ClientID)
        {
            IDAL objDAL = DALFactory.GetDBO();
            objDAL.Open();
            string sql = "UPDATE MSClient SET isDelete=1 WHERE ClientID=@ClientID";
            Parameter[] paras = {
                                    new Parameter("@ClientID",ClientID)
                                };
            objDAL.ExecuteNonQuery(CommandType.Text, sql, paras);
            objDAL.Close();
        }

        public void UPDATE_MSCLIENT(string ClSN, string ClName, string ClMan, string ClAddress, string ClPost, string ClTel, string ClFax, string ClEMail, string ClMemo, string isBuy, string isSale, string CBANK, string CBANKNO, string ClientID)
        {
            IDAL objDAL = DALFactory.GetDBO();
            objDAL.Open();
            string sql = "UPDATE MSClient SET ClSN=@ClSN,ClName=@ClName,ClMan=@ClMan,ClAddress=@ClAddress,ClPost=@ClPost,ClTel=@ClTel,ClFax=@ClFax,ClEMail=@ClEMail,ClMemo=@ClMemo,isBuy=@isBuy,isSale=@isSale,CBANK=@CBANK,CBANKNO=@CBANKNO where ClientID=@ClientID";
            Parameter[] paras = {
                                    new Parameter("@ClientID",ClientID),
                                    new Parameter("@ClSN",ClSN),
                                    new Parameter("@ClName",ClName),
                                    new Parameter("@ClMan",ClMan),
                                    new Parameter("@ClAddress",ClAddress),
                                    new Parameter("@ClPost",ClPost),
                                    new Parameter("@ClTel",ClTel),
                                    new Parameter("@ClFax",ClFax),
                                    new Parameter("@ClEMail",ClEMail),
                                    new Parameter("@ClMemo",ClMemo),
                                    new Parameter("@isBuy",isBuy),
                                    new Parameter("@isSale",isSale),
                                    new Parameter("@CBANK",CBANK),
                                    new Parameter("@CBANKNO",CBANKNO)
                                };
            objDAL.ExecuteNonQuery(CommandType.Text, sql, paras);
            objDAL.Close();
        }
    }
}
