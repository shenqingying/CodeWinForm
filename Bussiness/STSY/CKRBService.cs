using Bussiness.SqlHelper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Bussiness.STSY
{
    public class CKRBService
    {
        public DataTable SELECT_MSDayUse(string UseDateS, string UseDateE)
        {
            string sql = "select a.DayUseID,a.UseDate,a.UseMem,a.StockID,b.StockName from MSDayUse a,MSStock b where a.StockID=b.StockID and a.UseDate>=@UseDateS and a.UseDate<@UseDateE order by a.StockID,a.UseDate";
            SqlParameter[] paras = {
                                     new SqlParameter("@UseDateS",UseDateS),
                                     new SqlParameter("@UseDateE",UseDateE)
                                 };
            DataTable dt = DBHelper.GetDataSet(CommandType.Text, sql, paras);
            return dt;
        }

        public DataTable SELECT_MSRptItem()
        {
            string sql = "SELECT * FROM MSRptItem WHERE isDelete=0";
            DataTable dt = DBHelper.GetDataSet(CommandType.Text, sql);
            return dt;
        }

        public DataTable SELECT_MSDayUse(string DayUseID)
        {
            string sql = "SELECT * FROM MSDayUse WHERE DayUseID=@DayUseID";
            SqlParameter[] paras =
            {
                new SqlParameter("@DayUseID",DayUseID)
            };
            DataTable dt = DBHelper.GetDataSet(CommandType.Text, sql, paras);
            return dt;
        }

        public DataTable SELECT_MSDayUseItem(string DayUseID)
        {
            string sql = "SELECT * FROM MSDayUseItem WHERE DayUseID=@DayUseID";
            SqlParameter[] paras =
            {
                new SqlParameter("@DayUseID",DayUseID)
            };
            DataTable dt = DBHelper.GetDataSet(CommandType.Text, sql, paras);
            return dt;
        }

        public void DELETE_MSDayUseItem(string DayUseID)
        {
            IDAL objDAL = DALFactory.GetDBO();
            objDAL.Open();
            string sql = "DELETE FROM MSDayUseItem WHERE DayUseID=@DayUseID";
            Parameter[] paras = {
                                    new Parameter("@DayUseID",DayUseID)
                                };
            objDAL.ExecuteNonQuery(CommandType.Text, sql, paras);
            objDAL.Close();
        }

        public void DELETE_MSDayUse(string DayUseID)
        {
            IDAL objDAL = DALFactory.GetDBO();
            objDAL.Open();
            string sql = "DELETE FROM MSDayUse WHERE DayUseID=@DayUseID";
            Parameter[] paras = {
                                    new Parameter("@DayUseID",DayUseID)
                                };
            objDAL.ExecuteNonQuery(CommandType.Text, sql, paras);
            objDAL.Close();
        }

        public DataTable SELECT_MSDayUse_COUNT(string UseDate, string DayUseID, string StockID)
        {
            string sql = "SELECT DayUseID FROM MSDayUse WHERE UseDate=@UseDate and DayUseID<>@DayUseID and StockID=@StockID";
            SqlParameter[] paras =
            {
                new SqlParameter("@UseDate",UseDate),
                new SqlParameter("@DayUseID",DayUseID),
                new SqlParameter("@StockID",StockID)
            };
            DataTable dt = DBHelper.GetDataSet(CommandType.Text, sql, paras);
            return dt;
        }

        public string insert_MSDayUse(string StockID, string UseDate, string UseMem, string isAudi, string nStatus, string fillID, string fillName, string fillTime)
        {
            string sql = "SELECT SYSINT FROM HG_SYSINS WHERE upper(SYSNAME)='MSDAYUSE'";
            DataTable dt = DBHelper.GetDataSet(CommandType.Text, sql);
            int count = Convert.ToInt32(dt.Rows[0][0].ToString());
            count = count + 1;
            IDAL objDAL = DALFactory.GetDBO();
            objDAL.Open();
            string sql1 = "UPDATE HG_SYSINS SET SYSINT=@SYSINT WHERE upper(SYSNAME)='MSDAYUSE'";
            Parameter[] paras = {
                                    new Parameter("@SYSINT",count)
                                };
            objDAL.ExecuteNonQuery(CommandType.Text, sql1, paras);
            string sql2 = "insert into MSDayUse(DayUseID,StockID,UseDate,UseMem,isAudi,nStatus,fillID,fillName,fillTime) values (@DayUseID,@StockID,@UseDate,@UseMem,@isAudi,@nStatus,@fillID,@fillName,@fillTime)";
            Parameter[] paras1 =
            {
                new Parameter("@DayUseID",count.ToString()),
                new Parameter("@StockID",StockID),
                new Parameter("@UseDate",UseDate),
                new Parameter("@UseMem",UseMem),
                new Parameter("@isAudi",isAudi),
                new Parameter("@nStatus",nStatus),
                new Parameter("@fillID",fillID),
                new Parameter("@fillName",fillName),
                new Parameter("@fillTime",fillTime)
            };
            objDAL.ExecuteNonQuery(CommandType.Text, sql2, paras1);
            string sql3 = "DELETE FROM MSDayUseItem WHERE DayUseID=@DayUseID";
            Parameter[] paras2 =
{
                new Parameter("@DayUseID",count.ToString())
            };
            objDAL.ExecuteNonQuery(CommandType.Text, sql3, paras2);
            objDAL.Close();
            return count.ToString();
        }

        public void insert_MSDayUseItem(string DayUseID, string ItemID, string ItemName, string userNumber)
        {
            IDAL objDAL = DALFactory.GetDBO();
            objDAL.Open();
            string sql1 = "INSERT INTO MSDayUseItem(DayUseID,ItemID,ItemName,userNumber)VALUES(@DayUseID,@ItemID,@ItemName,@userNumber)";
            Parameter[] paras = {
                                    new Parameter("@DayUseID",DayUseID),
                                    new Parameter("@ItemID",ItemID),
                                    new Parameter("@ItemName",ItemName),
                                    new Parameter("@userNumber",userNumber)
                                };
            objDAL.ExecuteNonQuery(CommandType.Text, sql1, paras);
            objDAL.Close();
        }

        public DataTable SELECT_MSMaterial(string typeType, string typeid)
        {
            string sql = "SELECT A.materialID,A.typeID,A.mtName,B.typeName FROM MSMaterial A,MSType B WHERE A.typeID=B.typeID and a.isDelete=0 ";
            if (typeType != "")
            {
                sql = sql + " and B.typeType=@typeType ";
            }
            if (typeid != "")
            {
                sql = sql + " and B.typeid=@typeid ";
            }
            sql = sql + " order by A.typeID";
            SqlParameter[] paras =
            {
                new SqlParameter("@typeType",typeType),
                new SqlParameter("@typeid",typeid)
            };
            DataTable dt = DBHelper.GetDataSet(CommandType.Text, sql, paras);
            return dt;
        }

        public DataTable SELECT_MSType(string typeType, string typeid)
        {
            string sql = "SELECT typeID,typeName FROM MSType WHERE isDelete=0";
            if (typeType != "")
            {
                sql = sql + " and typeType=@typeType ";
            }
            if (typeid != "")
            {
                sql = sql + " and typeid=@typeid ";
            }
            SqlParameter[] paras =
            {
                new SqlParameter("@typeType",typeType),
                new SqlParameter("@typeid",typeid)
            };
            DataTable dt = DBHelper.GetDataSet(CommandType.Text, sql, paras);
            return dt;
        }

        public DataTable SCHYBB(DataTable dtm, DataTable dti, int s, string typeType, string ChuKDateS, string ChuKDateE, string StockID, string typeid, string N)
        {
            string sql = "SELECT ";
            sql = sql + " ddate, ";
            if (dtm.Rows.Count > 0)
            {
                for (int i = 0; i < dtm.Rows.Count; i++)
                {
                    if (s == 0)
                    {
                        sql = sql + " sum(m" + dtm.Rows[i]["typeID"].ToString() + ") m" + dtm.Rows[i]["typeID"].ToString() + " ";
                        sql = sql + ",";
                    }
                    else
                    {
                        sql = sql + " sum(m" + dtm.Rows[i]["materialID"].ToString() + ") m" + dtm.Rows[i]["materialID"].ToString() + " ";
                        sql = sql + ",";
                    }
                }
            }
            if (N == "1")
            {
                sql = sql + " sum(mtTotal) sumall, ";
            }
            if (dti.Rows.Count > 0)
            {
                for (int i = 0; i < dti.Rows.Count; i++)
                {
                    sql = sql + " sum(i" + dti.Rows[i]["ItemID"].ToString() + ") i" + dti.Rows[i]["ItemID"].ToString() + " ";
                    if (i != dti.Rows.Count - 1)
                    {
                        sql = sql + ",";
                    }
                }
            }
            sql = sql + " FROM (SELECT  ddate, ";
            if (dtm.Rows.Count > 0)
            {
                for (int i = 0; i < dtm.Rows.Count; i++)
                {
                    if (s == 0)
                    {
                        sql = sql + " case mtcol when 'm" + dtm.Rows[i]["typeID"].ToString() + "' then mtNumber end m" + dtm.Rows[i]["typeID"].ToString() + " ";
                        sql = sql + ",";
                    }
                    else
                    {
                        sql = sql + " case mtcol when 'm" + dtm.Rows[i]["materialID"].ToString() + "' then mtNumber end m" + dtm.Rows[i]["materialID"].ToString() + " ";
                        sql = sql + ",";
                    }
                }
            }
            if (N == "1")
            {
                sql = sql + " mtTotal, ";
            }
            if (dti.Rows.Count > 0)
            {
                for (int i = 0; i < dti.Rows.Count; i++)
                {
                    sql = sql + " case mtcol when 'i" + dti.Rows[i]["ItemID"].ToString() + "' then mtNumber end i" + dti.Rows[i]["ItemID"].ToString() + " ";
                    if (i != dti.Rows.Count - 1)
                    {
                        sql = sql + ",";
                    }
                }
            }
            sql = sql + " FROM ( ";
            if (s == 0)
            {
                sql = sql + " select a.ChuKDate ddate,'m'+cast(b.materialID as varchar) mtcol, ";
            }
            else
            {
                sql = sql + " select a.ChuKDate ddate,'m'+cast(b.materialID as varchar) mtcol, ";
            }
            if (N == "0")
            {
                sql = sql + " b.mtNumber ";
            }
            else
            {
                sql = sql + " b.mtTotal mtNumber ";
            }
            sql = sql + " ,b.mtTotal from MSChuKD a,MSChuKMX b,MSMaterial c ";
            if (typeType != "")
            {
                sql = sql + " ,MSType d ";
            }
            sql = sql + " where a.ChuKDID=b.ChuKDID and a.isAudi=1 and b.materialID=c.materialID ";
            if (typeType != "")
            {
                sql = sql + " and c.typeID=d.typeID and d.typeType=@typeType ";
            }
            sql = sql + " and a.ChuKDate>=@ChuKDateS and a.ChuKDate<@ChuKDateE ";
            sql = sql + " and a.StockID=@StockID ";
            if (typeid != "")
            {
                sql = sql + " and c.typeid=@typeid ";
            }
            sql = sql + " union all select a.UseDate,'i'+cast(b.ItemID as varchar),b.userNumber,0 from MSDayUse a,MSDayUseItem b where a.DayUseID=b.DayUseID and isAudi=1 and a.UseDate>=@ChuKDateS and a.UseDate<@ChuKDateE ";
            sql = sql + " and a.StockID=@StockID ";
            sql = sql + " )x)y group by ddate";
            SqlParameter[] paras =
            {
                new SqlParameter("@typeType",typeType),
                new SqlParameter("@ChuKDateS",ChuKDateS),
                new SqlParameter("@ChuKDateE",ChuKDateE),
                new SqlParameter("@StockID",StockID),
                new SqlParameter("@typeid",typeid)
            };
            DataTable dt = DBHelper.GetDataSet(CommandType.Text, sql, paras);
            return dt;
        }

        public DataTable SELECT_type_COUNT(string typeID)
        {
            string sql = "select * from MSMaterial where typeID=@typeID and isDelete=0";
            SqlParameter[] paras = {
                                     new SqlParameter("@typeID",typeID)
                                 };
            DataTable dt = DBHelper.GetDataSet(CommandType.Text, sql, paras);
            return dt;
        }

        public DataTable SCRKBB(DataTable dtm, int s, string RuKDateS, string RuKDateE, string StockID, string typeid, string N, string ClientID)
        {
            string sql = "SELECT ";
            sql = sql + " ddate, ";
            if (dtm.Rows.Count > 0)
            {
                for (int i = 0; i < dtm.Rows.Count; i++)
                {
                    if (s == 0)
                    {
                        sql = sql + " sum(m" + dtm.Rows[i]["typeID"].ToString() + ") m" + dtm.Rows[i]["typeID"].ToString() + " ";
                        if (i != dtm.Rows.Count - 1)
                        {
                            sql = sql + ",";
                        }
                    }
                    else
                    {
                        sql = sql + " sum(m" + dtm.Rows[i]["materialID"].ToString() + ") m" + dtm.Rows[i]["materialID"].ToString() + " ";
                        if (i != dtm.Rows.Count - 1)
                        {
                            sql = sql + ",";
                        }
                    }
                }
            }
            if (N == "1")
            {
                sql = sql + " ,sum(mtNumber) sumall ";
            }
            sql = sql + " FROM (SELECT  ddate, ";
            if (dtm.Rows.Count > 0)
            {
                for (int i = 0; i < dtm.Rows.Count; i++)
                {
                    if (s == 0)
                    {
                        sql = sql + " case mtcol when 'm" + dtm.Rows[i]["typeID"].ToString() + "' then mtNumber end m" + dtm.Rows[i]["typeID"].ToString() + " ";
                        if (i != dtm.Rows.Count - 1)
                        {
                            sql = sql + ",";
                        }
                    }
                    else
                    {
                        sql = sql + " case mtcol when 'm" + dtm.Rows[i]["materialID"].ToString() + "' then mtNumber end m" + dtm.Rows[i]["materialID"].ToString() + " ";
                        if (i != dtm.Rows.Count - 1)
                        {
                            sql = sql + ",";
                        }
                    }
                }
            }
            if (N == "1")
            {
                sql = sql + " ,mtNumber ";
            }
            sql = sql + " FROM ( ";
            if (s == 0)
            {
                //sql = sql + " select a.ChuKDate ddate,'m'+cast(c.typeid as varchar) mtcol, ";
                sql = sql + " select a.RuKDate ddate,'m'+cast(c.typeid as varchar) mtcol, ";
            }
            else
            {
                //sql = sql + " select a.ChuKDate ddate,'m'+cast(b.materialID as varchar) mtcol, ";
                sql = sql + " select a.RuKDate ddate,'m'+cast(b.materialID as varchar) mtcol, ";
            }
            if (N == "0")
            {
                sql = sql + " b.mtNumber ";
            }
            else
            {
                sql = sql + " b.mtTotal mtNumber ";
            }
            sql = sql + " from MSRuKD a,MSRuKMX b,MSMaterial c where a.RuKDID=b.RuKDID and a.isAudi=1 and b.materialID=c.materialID ";
            sql = sql + " and a.RuKDate>=@RuKDateS and a.RuKDate<@RuKDateE ";
            sql = sql + " and a.StockID=@StockID ";
            if (typeid != "")
            {
                sql = sql + " and c.typeid=@typeid ";
            }
            if (ClientID != "")
            {
                sql = sql + " and a.ClientID=@ClientID ";
            }
            sql = sql + " )x)y group by ddate";
            SqlParameter[] paras =
            {
                new SqlParameter("@RuKDateS",RuKDateS),
                new SqlParameter("@RuKDateE",RuKDateE),
                new SqlParameter("@StockID",StockID),
                new SqlParameter("@typeid",typeid),
                new SqlParameter("@ClientID",ClientID)
            };
            DataTable dt = DBHelper.GetDataSet(CommandType.Text, sql, paras);
            return dt;
        }





        public DataTable SCCRBB(string typeType, string DateS, string DateE, string StockID, string typeid, string N)
        {
            string sql = "SELECT mtName,sum(preNumber) preNumber,sum(ru1) ru1, sum(chu1) chu1, sum(ru2) ru2, sum(chu2) chu2, sum(ru3) ru3, sum(chu3) chu3, sum(ru4) ru4, sum(chu4) chu4, sum(ru5) ru5, sum(chu5) chu5, sum(ru6) ru6, sum(chu6) chu6, sum(ru7) ru7, sum(chu7) chu7, sum(ru8) ru8, sum(chu8) chu8, sum(ru9) ru9, sum(chu9) chu9, sum(ru10) ru10, sum(chu10) chu10, sum(ru11) ru11, sum(chu11) chu11, sum(ru12) ru12, sum(chu12) chu12, sum(ru13) ru13, sum(chu13) chu13, sum(ru14) ru14, sum(chu14) chu14, sum(ru15) ru15, sum(chu15) chu15, sum(ru16) ru16, sum(chu16) chu16, sum(ru17) ru17, sum(chu17) chu17, sum(ru18) ru18, sum(chu18) chu18, sum(ru19) ru19, sum(chu19) chu19, sum(ru20) ru20, sum(chu20) chu20, sum(ru21) ru21, sum(chu21) chu21, sum(ru22) ru22, sum(chu22) chu22, sum(ru23) ru23, sum(chu23) chu23, sum(ru24) ru24, sum(chu24) chu24, sum(ru25) ru25, sum(chu25) chu25, sum(ru26) ru26, sum(chu26) chu26, sum(ru27) ru27, sum(chu27) chu27, sum(ru28) ru28, sum(chu28) chu28, sum(ru29) ru29, sum(chu29) chu29, sum(ru30) ru30, sum(chu30) chu30, sum(ru31) ru31, sum(chu31) chu31,sum(ruNumber) ruNumber,sum(chuNumber) chuNumber,sum(ruNumber)-sum(chuNumber) ruchuNumber,sum(ruNumber)-sum(chuNumber)+sum(preNumber) sumCount ";
            sql = sql + " FROM (SELECT  ";
            sql = sql + " case dday when 'ru1' then mtNumber end ru1, case dday when 'chu1' then mtNumber end chu1, case dday when 'ru2' then mtNumber end ru2, case dday when 'chu2' then mtNumber end chu2, case dday when 'ru3' then mtNumber end ru3, ";
            sql = sql + "case dday when 'chu3' then mtNumber end chu3, case dday when 'ru4' then mtNumber end ru4, case dday when 'chu4' then mtNumber end chu4, case dday when 'ru5' then mtNumber end ru5, case dday when 'chu5' then mtNumber end chu5, ";
            sql = sql + "case dday when 'ru6' then mtNumber end ru6, case dday when 'chu6' then mtNumber end chu6, case dday when 'ru7' then mtNumber end ru7, case dday when 'chu7' then mtNumber end chu7, case dday when 'ru8' then mtNumber end ru8, ";
            sql = sql + "case dday when 'chu8' then mtNumber end chu8, case dday when 'ru9' then mtNumber end ru9, case dday when 'chu9' then mtNumber end chu9, case dday when 'ru10' then mtNumber end ru10, case dday when 'chu10' then mtNumber end ";
            sql = sql + "chu10, case dday when 'ru11' then mtNumber end ru11, case dday when 'chu11' then mtNumber end chu11, case dday when 'ru12' then mtNumber end ru12, case dday when 'chu12' then mtNumber end chu12, case dday when 'ru13' then ";
            sql = sql + "mtNumber end ru13, case dday when 'chu13' then mtNumber end chu13, case dday when 'ru14' then mtNumber end ru14, case dday when 'chu14' then mtNumber end chu14, case dday when 'ru15' then mtNumber end ru15, case dday when 'chu15' then mtNumber end chu15, case dday when 'ru16' then mtNumber end ru16, case dday when 'chu16' then mtNumber end chu16, case dday when 'ru17' then mtNumber end ru17, case dday when 'chu17' then mtNumber end chu17, case dday when 'ru18' then mtNumber end ru18, case dday when 'chu18' then mtNumber end chu18, case dday when 'ru19' then mtNumber end ru19, case dday when 'chu19' then mtNumber end chu19, case dday when 'ru20' then mtNumber end ru20, case dday when 'chu20' then mtNumber end chu20, case dday when 'ru21' then mtNumber end ru21, case dday when 'chu21' then mtNumber end chu21, case dday when 'ru22' then mtNumber end ru22, case dday when 'chu22' then mtNumber end chu22, case dday when 'ru23' then mtNumber end ru23, case dday when 'chu23' then mtNumber end chu23, case dday when 'ru24' then mtNumber end ru24, case dday when 'chu24' then mtNumber end chu24, case dday when 'ru25' then mtNumber end ru25, case dday when 'chu25' then mtNumber end chu25, case dday when 'ru26' then mtNumber end ru26, case dday when 'chu26' then mtNumber end chu26, case dday when 'ru27' then mtNumber end ru27, case dday when 'chu27' then mtNumber end chu27, case dday when 'ru28' then mtNumber end ru28, case dday when 'chu28' then mtNumber end chu28, case dday when 'ru29' then mtNumber end ru29, case dday when 'chu29' then mtNumber end chu29, case dday when 'ru30' then mtNumber end ru30, case dday when 'chu30' then mtNumber end chu30, case dday when 'ru31' then mtNumber end ru31, case dday when 'chu31' then mtNumber end chu31,mtName,materialID,ruNumber,chuNumber,preNumber ";
            sql = sql + " FROM ( ";
            sql = sql + " select b.materialID, ";
            if (N == "0")
            {
                sql = sql + " b.mtNumber,b.mtNumber ruNumber,0 chuNumber,0 preNumber ";
            }
            else
            {
                sql = sql + " b.mtTotal mtNumber,b.mtTotal ruNumber,0 chuNumber,0 preNumber ";
            }
            sql = sql + " ,'ru'+cast(day(a.RuKDate)as varchar) dday,c.mtName from MSRuKD a,MSRuKMX b,MSMaterial c ";
            if (typeType != "")
            {
                sql = sql + " ,MSType d ";
            }
            sql = sql + " where a.RuKDID=b.RuKDID and a.isAudi=1 and b.materialID=c.materialID ";
            if (typeType != "")
            {
                sql = sql + " and c.typeID=d.typeID and d.typeType=@typeType  ";
            }
            sql = sql + " and a.RuKDate>=@DateS and a.RuKDate<@DateE ";
            if (StockID != "0")
            {
                sql = sql + " and a.StockID=@StockID ";
            }
            if (typeid != "")
            {
                sql = sql + " and c.typeid=@typeid ";
            }
            sql = sql + " union all select b.materialID, ";
            if (N == "0")
            {
                sql = sql + " b.mtNumber,0 ,b.mtNumber ,0 ";
            }
            else
            {
                sql = sql + " b.mtTotal,0,b.mtTotal,0 ";
            }
            sql = sql + " ,'chu'+cast(day(a.ChuKDate)as varchar),c.mtName from MSChuKD a,MSChuKMX b,MSMaterial c ";
            if (typeType != "")
            {
                sql = sql + " ,MSType d ";
            }
            sql = sql + " where a.ChuKDID=b.ChuKDID and isAudi=1 and b.materialID=c.materialID ";
            if (typeType != "")
            {
                sql = sql + " and c.typeID=d.typeID and d.typeType=@typeType  ";
            }
            sql = sql + " and a.ChuKDate>=@DateS and a.ChuKDate<@DateE ";
            if (StockID != "0")
            {
                sql = sql + " and a.StockID=@StockID ";
            }
            if (typeid != "")
            {
                sql = sql + " and c.typeid=@typeid ";
            }
            sql = sql + " union all select a.materialID, ";
            if (N == "0")
            {
                sql = sql + " 0,0,0,a.mtNumber ";
            }
            else
            {
                sql = sql + " 0,0,0,a.mtTotal ";
            }
            sql = sql + " ,NULL,c.mtName from MSInitStock a,MSMaterial c ";
            if (typeType != "")
            {
                sql = sql + " ,MSType d ";
            }
            sql = sql + " where a.materialID=c.materialID ";
            if (typeType != "")
            {
                sql = sql + " and c.typeID=d.typeID and d.typeType=@typeType  ";
            }
            if (StockID != "0")
            {
                sql = sql + " and a.StockID=@StockID ";
            }
            if (typeid != "")
            {
                sql = sql + " and c.typeid=@typeid ";
            }
            sql = sql + " union all select b.materialID, ";
            if (N == "0")
            {
                sql = sql + " 0,0 ,0 ,b.mtNumber ";
            }
            else
            {
                sql = sql + " 0 ,0 ,0 ,b.mtTotal ";
            }
            sql = sql + "  ,null,c.mtName from MSRuKD a,MSRuKMX b,MSMaterial c ";
            if (typeType != "")
            {
                sql = sql + " ,MSType d ";
            }
            sql = sql + " where a.RuKDID=b.RuKDID and a.isAudi=1 and b.materialID=c.materialID ";
            if (typeType != "")
            {
                sql = sql + " and c.typeID=d.typeID and d.typeType=@typeType  ";
            }
            sql = sql + " and a.RuKDate<@DateS";
            if (StockID != "0")
            {
                sql = sql + " and a.StockID=@StockID ";
            }
            if (typeid != "")
            {
                sql = sql + " and c.typeid=@typeid ";
            }
            sql = sql + " union all select b.materialID, ";
            if (N == "0")
            {
                sql = sql + " 0,0  ,0 ,-b.mtNumber ";
            }
            else
            {
                sql = sql + " 0 ,0  ,0 ,-b.mtTotal ";
            }
            sql = sql + " ,NULL,c.mtName from MSChuKD a,MSChuKMX b,MSMaterial c";
            if (typeType != "")
            {
                sql = sql + " ,MSType d ";
            }
            sql = sql + " where a.ChuKDID=b.ChuKDID and isAudi=1 and b.materialID=c.materialID ";
            if (typeType != "")
            {
                sql = sql + " and c.typeID=d.typeID and d.typeType=@typeType  ";
            }
            sql = sql + " and a.ChuKDate<@DateS ";
            if (StockID != "0")
            {
                sql = sql + " and a.StockID=@StockID ";
            }
            if (typeid != "")
            {
                sql = sql + " and c.typeid=@typeid ";
            }
            sql = sql + " )x)y group by mtName,materialID ";



            SqlParameter[] paras =
            {
                new SqlParameter("@typeType",typeType),
                new SqlParameter("@DateS",DateS),
                new SqlParameter("@DateE",DateE),
                new SqlParameter("@StockID",StockID),
                new SqlParameter("@typeid",typeid)
            };
            DataTable dt = DBHelper.GetDataSet(CommandType.Text, sql, paras);
            return dt;
        }

    }
}
