using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Bussiness.SqlHelper;
using System.Data.SqlClient;

namespace Bussiness.STSY
{
    public class WLService
    {
        public DataTable GETWLTYPE()
        {
            string str = "SELECT typeID,typeSN,typeName FROM MSType WHERE isDelete = '0' ORDER BY typeSN";
            DataTable dt = DBHelper.GetDataSet(CommandType.Text, str);
            return dt;
        }

        public DataTable GETWLTYPEKC()
        {
            string str = "SELECT distinct MSType.* from MSMaterial,MSRealStock,MSType where MSMaterial.materialID=MSRealStock.materialID and MSRealStock.mtNumber<>0 and MSMaterial.typeID=MSType.typeID order by MSType.typeID";
            DataTable dt = DBHelper.GetDataSet(CommandType.Text, str);
            return dt;
        }

        public DataTable GETWLTYPELB()
        {
            string str = "SELECT typeID,typeSN+' '+typeName as NAME FROM MSType WHERE isDelete = '0' ORDER BY NAME";
            DataTable dt = DBHelper.GetDataSet(CommandType.Text, str);
            return dt;
        }

        public DataTable GETWLTYPE(string typeID)
        {
            string str = "SELECT materialID,mtName,mtSN FROM MSMaterial WHERE typeID=@typeID AND isDelete = '0' order by mtSN";
            SqlParameter[] paras = {
                                       new SqlParameter("@typeID",typeID)
                                   };
            DataTable dt = DBHelper.GetDataSet(CommandType.Text, str, paras);
            return dt;
        }

        public DataTable GETWLTYPEKC(string typeID)
        {
            string str = "SELECT distinct MSMaterial.* from MSMaterial,MSRealStock where MSMaterial.materialID=MSRealStock.materialID and MSRealStock.mtNumber>0 and MSMaterial.typeID=@typeID";
            SqlParameter[] paras = {
                                       new SqlParameter("@typeID",typeID)
                                   };
            DataTable dt = DBHelper.GetDataSet(CommandType.Text, str, paras);
            return dt;
        }

        public DataTable GETLBTYPE(string typeID)
        {
            string str = "SELECT * FROM MSType WHERE typeID=@typeID AND isDelete = '0'";
            SqlParameter[] paras ={
                                      new SqlParameter("@typeID",typeID)
                                  };
            DataTable dt = DBHelper.GetDataSet(CommandType.Text, str, paras);
            return dt;
        }

        public DataTable GETWLTYPEbymaterialID(string materialID)
        {
            string str = "SELECT * FROM MSMaterial WHERE materialID=@materialID";
            SqlParameter[] paras = {
                                       new SqlParameter("@materialID",materialID)
                                   };
            DataTable dt = DBHelper.GetDataSet(CommandType.Text, str, paras);
            return dt;
        }

        public DataTable SELECT_MSType_MSMaterial(string materialID)
        {
            string str = "select MSType.* from MSType,MSMaterial where MSMaterial.typeid = MSType.typeid and MSMaterial.materialID=@materialID";
            SqlParameter[] paras = {
                                       new SqlParameter("@materialID",materialID)
                                   };
            DataTable dt = DBHelper.GetDataSet(CommandType.Text, str, paras);
            return dt;
        }

        public void DELETELB(string typeID)
        {
            IDAL objDAL = DALFactory.GetDBO();
            objDAL.Open();
            string sql = "UPDATE MSType SET isDelete='1' WHERE typeID=@typeID";
            Parameter[] paras = {
                                    new Parameter("@typeID",typeID)
                                };
            objDAL.ExecuteNonQuery(CommandType.Text, sql, paras);
            objDAL.Close();
        }

        public void DELETEWL(string materialID)
        {
            IDAL objDAL = DALFactory.GetDBO();
            objDAL.Open();
            string sql = "UPDATE MSMaterial SET isDelete='1' WHERE materialID=@materialID";
            Parameter[] paras = {
                                    new Parameter("@materialID",materialID)
                                };
            objDAL.ExecuteNonQuery(CommandType.Text, sql, paras);
            objDAL.Close();
        }

        public void insertWLLB(string typeSN, string typeName, string typeType, string typeMem)
        {
            //string str = "SELECT MAX(typeID) FROM MSType";
            string s1 = "SELECT SYSINT FROM HG_SYSINS where SYSNAME = 'MSType'";
            DataTable dt = DBHelper.GetDataSet(CommandType.Text, s1);

            int s = Convert.ToInt32(dt.Rows[0][0].ToString()) + 1;

            IDAL objDAL = DALFactory.GetDBO();
            objDAL.Open();
            string sql = "INSERT INTO MSType(typeID,typeSN,typeName,typeType,typeMem,isDelete) VALUES(@typeID,@typeSN,@typeName,@typeType,@typeMem,'0')";
            Parameter[] paras = {
                                    new Parameter("@typeID",s.ToString()),
                                    new Parameter("@typeSN",typeSN),
                                    new Parameter("@typeName",typeName),
                                    new Parameter("@typeType",typeType),
                                    new Parameter("@typeMem",typeMem)
                                };
            objDAL.ExecuteNonQuery(CommandType.Text, sql, paras);

            string sql1 = "UPDATE HG_SYSINS SET SYSINT=@SYSINT WHERE upper(SYSNAME)='MSType'";
            Parameter[] paras1 = {
                                    new Parameter("@SYSINT",s.ToString())
                                };
            objDAL.ExecuteNonQuery(CommandType.Text, sql1, paras1);

            objDAL.Close();
        }

        public string insertWL(string typeID, string mtName, string mtNO, string mtSN, string mtSpec, string mtUnit, string mtBuyPrice, string MinNum, string MaxNum, string mtMemo)
        {
            //string str = "SELECT MAX(materialID) FROM MSMaterial";
            string s1 = "SELECT SYSINT FROM HG_SYSINS where SYSNAME = 'MSMaterial'";
            DataTable dt = DBHelper.GetDataSet(CommandType.Text, s1);

            int s = Convert.ToInt32(dt.Rows[0][0].ToString()) + 1;

            IDAL objDAL = DALFactory.GetDBO();
            objDAL.Open();
            string sql = "INSERT INTO MSMaterial(materialID,typeID,mtName,mtNO,mtSN,mtSpec,mtUnit,mtBuyPrice,mtSalePrice,MinNum,MaxNum,isDelete,mtMemo)";
            sql = sql + " VALUES(@materialID,@typeID,@mtName,@mtNO,@mtSN,@mtSpec,@mtUnit,@mtBuyPrice,@mtSalePrice,@MinNum,@MaxNum,'0',@mtMemo)";

            Parameter[] paras = {
                                    new Parameter("@materialID",s.ToString()),
                                    new Parameter("@typeID",typeID),
                                    new Parameter("@mtName",mtName),
                                    new Parameter("@mtNO",mtNO),
                                    new Parameter("@mtSN",mtSN),
                                    new Parameter("@mtSpec",mtSpec),
                                    new Parameter("@mtUnit",mtUnit),
                                    new Parameter("@mtBuyPrice",mtBuyPrice),
                                    new Parameter("@mtSalePrice",mtBuyPrice),
                                    new Parameter("@MinNum",MinNum),
                                    new Parameter("@MaxNum",MaxNum),
                                    new Parameter("@mtMemo",mtMemo)
                                };
            objDAL.ExecuteNonQuery(CommandType.Text, sql, paras);

            string sql1 = "UPDATE HG_SYSINS SET SYSINT=@SYSINT WHERE upper(SYSNAME)='MSMaterial'";
            Parameter[] paras1 = {
                                    new Parameter("@SYSINT",s.ToString())
                                };
            objDAL.ExecuteNonQuery(CommandType.Text, sql1, paras1);

            objDAL.Close();
            return s.ToString();
        }

        public void UPDATELB(string typeSN, string typeName, string typeType, string typeMem, string typeID)
        {
            IDAL objDAL = DALFactory.GetDBO();
            objDAL.Open();
            string sql = "UPDATE MSType SET typeSN=@typeSN,typeName=@typeName,typeType=@typeType,typeMem=@typeMem WHERE typeID=@typeID";
            Parameter[] paras = {
                                    new Parameter("@typeID",typeID),
                                    new Parameter("@typeSN",typeSN),
                                    new Parameter("@typeName",typeName),
                                    new Parameter("@typeType",typeType),
                                    new Parameter("@typeMem",typeMem)
                                };
            objDAL.ExecuteNonQuery(CommandType.Text, sql, paras);
            objDAL.Close();
        }

        public void UPDATEWL(string typeID, string mtName, string mtNO, string mtSN, string mtSpec, string mtUnit, string mtBuyPrice, string MinNum, string MaxNum, string mtMemo, string materialID)
        {
            IDAL objDAL = DALFactory.GetDBO();
            objDAL.Open();
            string sql = "UPDATE MSMaterial SET typeID=@typeID,mtName=@mtName,mtNO=@mtNO,mtSN=@mtSN,mtSpec=@mtSpec,mtUnit=@mtUnit,mtBuyPrice=@mtBuyPrice,MinNum=@MinNum,MaxNum=@MaxNum,mtMemo=@mtMemo WHERE materialID=@materialID";
            Parameter[] paras = {
                                    new Parameter("@typeID",typeID),
                                    new Parameter("@mtName",mtName),
                                    new Parameter("@mtNO",mtNO),
                                    new Parameter("@mtSN",mtSN),
                                    new Parameter("@mtSpec",mtSpec),
                                    new Parameter("@mtUnit",mtUnit),
                                    new Parameter("@mtBuyPrice",mtBuyPrice),
                                    new Parameter("@MinNum",MinNum),
                                    new Parameter("@MaxNum",MaxNum),
                                    new Parameter("@mtMemo",mtMemo),
                                    new Parameter("@materialID",materialID)
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

        public void UPDATE_MSRealStock(string materialID, string mtUnit, string mtPrice)
        {
            IDAL objDAL = DALFactory.GetDBO();
            objDAL.Open();
            string sql = "UPDATE MSRealStock SET mtUnit=@mtUnit,mtPrice=@mtPrice where materialID=@materialID";
            Parameter[] paras = {
                                     new Parameter("@materialID",materialID),
                                     new Parameter("@mtUnit",mtUnit),
                                     new Parameter("@mtPrice",mtPrice)
                                 };
            objDAL.ExecuteNonQuery(CommandType.Text, sql, paras);
            objDAL.Close();
        }

        public DataTable XZISHAVELB(string typeSN)
        {
            string str = "SELECT * FROM MSType WHERE typeSN=@typeSN";
            SqlParameter[] paras ={
                                      new SqlParameter("@typeSN",typeSN)
                                  };
            DataTable dt = DBHelper.GetDataSet(CommandType.Text, str, paras);
            return dt;
        }

        public DataTable XZISHAVEWL(string mtSN)
        {
            string str = "SELECT * FROM MSMaterial WHERE mtSN=@mtSN";
            SqlParameter[] paras ={
                                      new SqlParameter("@mtSN",mtSN)
                                  };
            DataTable dt = DBHelper.GetDataSet(CommandType.Text, str, paras);
            return dt;
        }

        public DataTable ISHAVELB(string typeSN, string typeID)
        {
            string str = "SELECT * FROM MSType WHERE typeSN=@typeSN AND typeID<>@typeID";
            SqlParameter[] paras ={
                                      new SqlParameter("@typeSN",typeSN),
                                      new SqlParameter("@typeID",typeID)
                                  };
            DataTable dt = DBHelper.GetDataSet(CommandType.Text, str, paras);
            return dt;
        }

        public DataTable ISHAVEWL(string mtSN, string materialID)
        {
            string str = "SELECT * FROM MSMaterial WHERE mtSN=@mtSN AND materialID<>@materialID";
            SqlParameter[] paras ={
                                      new SqlParameter("@mtSN",mtSN),
                                      new SqlParameter("@materialID",materialID)
                                  };
            DataTable dt = DBHelper.GetDataSet(CommandType.Text, str, paras);
            return dt;
        }

        public DataTable GETKC(string StockID, string typeID, string mtSN, string mtName, string mtNO, string mtSpec)
        {
            string str = "select x.*,d.PlaceSN,d.PlaceName from (select a.materialID,a.typeid,a.mtName,a.mtNO,a.mtSN, a.mtSpec,a.mtUnit,a.mtBuyPrice,a.mtSalePrice,a.isDelete, b.typeSN,b.typeName,c.PlaceID, c.mtNumber,c.mtTotal from MSMaterial a,MSType b,MSRealStock c where a.typeid=b.typeid and c.materialID=a.materialID and c.mtNumber<>0 and c.StockID=@StockID and a.mtName like '%'+@mtName+'%' and a.mtSN like '%'+@mtSN+'%' and a.mtSpec like '%'+@mtSpec+'%' and a.mtNO like '%'+@mtNO+'%'";
            if (typeID != "0")
            {
                str = str + " and a.typeid=@typeID ";
            }
            str = str + " )x left join MSPlace d on x.PlaceID=d.PlaceID order by x.typeSN,x.typeName,x.mtSN,x.mtName,x.mtSpec";
            SqlParameter[] paras ={
                                      new SqlParameter("@StockID",StockID),
                                      new SqlParameter("@typeID",typeID),
                                      new SqlParameter("@mtSN",mtSN),
                                      new SqlParameter("@mtName",mtName),
                                      new SqlParameter("@mtNO",mtNO),
                                      new SqlParameter("@mtSpec",mtSpec)
                                  };
            DataTable dt = DBHelper.GetDataSet(CommandType.Text, str, paras);
            return dt;
        }

        public DataTable GETKC(string typeID, string mtNO, string mtSpec, string materialID)
        {
            string str = " select m.*,n.* from(select a.materialID,a.typeid, a.mtName,a.mtNO,a.mtSN,a.mtSpec,a.mtUnit,a.mtBuyPrice, a.mtSalePrice,b.typeSN,b.typeName,a.MinNum,a.MaxNum from MSMaterial a left join MSType b on a.typeid=b.typeid where a.isDelete=0)m left join( select materialID as smaterialID,sum(mtNumber)  as smtNumber,sum(mtTotal) as smtTotal  from MSRealStock group by materialID)n on  m.materialID=n.smaterialID  where (isnull(n.smtNumber,0) NOT BETWEEN m.MinNum and m.MaxNum ) and m.mtSpec like '%'+@mtSpec+'%' and m.mtNO like '%'+@mtNO+'%' ";
            if (typeID != "0")
            {
                str = str + " and m.typeid=@typeID ";
            }
            if (materialID != "0")
            {
                str = str + " and m.materialID=@materialID ";
            }
            str = str + " order by m.typeSN,m.typeName,m.mtSN,m.mtName,m.mtSpec";
            SqlParameter[] paras ={
                                      new SqlParameter("@typeID",typeID),
                                      new SqlParameter("@mtNO",mtNO),
                                      new SqlParameter("@mtSpec",mtSpec),
                                      new SqlParameter("@materialID",materialID)
                                  };
            DataTable dt = DBHelper.GetDataSet(CommandType.Text, str, paras);
            return dt;
        }

        public DataTable SELECT_KC(string typeID, string materialID)
        {
            string str = "select x.*,d.PlaceSN,d.PlaceName,x.mtTotal/x.mtNumber as PJJ from (select a.materialID,a.typeid,a.mtName,a.mtNO,a.mtSN, a.mtSpec,a.mtUnit,a.mtBuyPrice,a.mtSalePrice,a.isDelete,b.typeType, b.typeSN,b.typeName,c.PlaceID, c.mtNumber,c.mtTotal from MSMaterial a,MSType b,MSRealStock c where a.typeid=b.typeid and c.materialID=a.materialID and c.mtNumber<>0 and c.StockID=1 ";
            if (typeID != "0")
            {
                str = str + " and a.typeID=@typeID ";
            }
            if (materialID != "0")
            {
                str = str + " and a.materialID=@materialID ";
            }
            str = str + " )x left join MSPlace d on x.PlaceID=d.PlaceID order by x.typeSN,x.typeName,x.mtSN,x.mtName,x.mtSpec ";
            SqlParameter[] paras ={
                                      new SqlParameter("@typeID",typeID),
                                      new SqlParameter("@materialID",materialID)
                                  };
            DataTable dt = DBHelper.GetDataSet(CommandType.Text, str, paras);
            return dt;
        }


        public DataTable SELECT_RUKWK(string typeID, string mtSN, string mtName, string mtNO, string mtSpec)
        {
            string str = "select * from MSMaterial,MSType where MSMaterial.isDelete=0 and MSType.isDelete=0 and MSMaterial.typeID = MSType.typeID ";
            if (typeID != "0")
            {
                str = str + " and MSType.typeID=@typeID ";
            }
            if (mtSN != "")
            {
                str = str + " and MSMaterial.mtSN like @mtSN";
            }
            if (mtName != "")
            {
                str = str + " and MSMaterial.mtName like @mtName";
            }
            if (mtNO != "")
            {
                str = str + " and MSMaterial.mtNO like @mtNO";
            }
            if (mtSpec != "")
            {
                str = str + " and MSMaterial.mtSpec like @mtSpec";
            }
            str = str + " order by MSMaterial.typeID ";
            SqlParameter[] paras ={
                                      new SqlParameter("@typeID",typeID),
                                      new SqlParameter("@mtSN","%"+mtSN+"%"),
                                      new SqlParameter("@mtName","%"+mtName+"%"),
                                      new SqlParameter("@mtNO","%"+mtNO+"%"),
                                      new SqlParameter("@mtSpec","%"+mtSpec+"%")
                                  };
            DataTable dt = DBHelper.GetDataSet(CommandType.Text, str, paras);
            return dt;
        }
    }
}
