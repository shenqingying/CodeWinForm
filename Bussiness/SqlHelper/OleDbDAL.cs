using System;
using System.Data;
using System.Data.OleDb;

namespace Bussiness.SqlHelper
{
	/// <summary>
	/// 通用OleDb访问数据库
	/// </summary>
	public class OleDbDAL:IDAL
	{
		private  OleDbTransaction trans; //事务处理类 
		private  OleDbConnection conn; //数据库连接 

		public OleDbDAL(){}		

		/// <summary>
		/// //执行命令返回一个DataSet;
		/// </summary>
		/// <param name="cmdType">命令类型</param>
		/// <param name="cmdText">命令串或存储过程名</param>
		/// <param name="tableName">新增加的表名</param>
		/// <param name="cmdParms">命令的参数对象</param>
		/// <returns></returns>
        public System.Data.DataSet ReturnDatSet(System.Data.CommandType cmdType, string cmdText, string tableName, params Parameter[] cmdParms)
		{
			OleDbCommand cmd = new OleDbCommand();
			PrepareCommand(cmd, cmdType, cmdText, cmdParms);
			DataSet objSet = new DataSet(); 
   　　     OleDbDataAdapter objDataAdapter = new OleDbDataAdapter(); 
			objDataAdapter.SelectCommand  = cmd;
			objDataAdapter.Fill(objSet,tableName);
			return objSet;
		}

        /// <summary>
        /// //执行命令返回一个DataSet;
        /// </summary>
        /// <param name="cmdType">命令类型</param>
        /// <param name="cmdText">命令串或存储过程名</param>
        /// <param name="tableName">新增加的表名</param>
        /// <param name="cmdParms">命令的参数对象</param>
        /// <returns></returns>
        public System.Data.DataSet ReturnDatSet(System.Data.CommandType cmdType, string cmdText, string tableName, int startrow,int pageSize,params Parameter[] cmdParms)
        {
            OleDbCommand cmd = new OleDbCommand();
            PrepareCommand(cmd, cmdType, cmdText, cmdParms);
            DataSet objSet = new DataSet();
            OleDbDataAdapter objDataAdapter = new OleDbDataAdapter();
            objDataAdapter.SelectCommand = cmd;
            objDataAdapter.Fill(objSet, tableName);
            return objSet;
        }

		/// <summary>
		/// 执行命令返回第一行第一列的值
		/// </summary>
		/// <param name="cmdType">命令类型</param>
		/// <param name="cmdText">命令串或存储过程名</param>
		/// <param name="cmdParms">命令的参数对象</param>
		/// <returns></returns>
		public object ExecuteScalar(System.Data.CommandType cmdType, string cmdText, params Parameter[] cmdParms)
		{
			OleDbCommand cmd = new OleDbCommand();
			PrepareCommand(cmd, cmdType, cmdText, cmdParms);
			object val = cmd.ExecuteScalar();
			cmd.Parameters.Clear();
			return val;
		}

		/// <summary>
		/// 执行命令并返回所影响的行数
		/// </summary>
		/// <param name="cmdType">命令类型</param>
		/// <param name="cmdText">命令串或存储过程名</param>
		/// <param name="cmdParms">命令的参数对象</param>
		/// <returns></returns>
		public int ExecuteNonQuery(System.Data.CommandType cmdType, string cmdText, params Parameter[] cmdParms)
		{
			OleDbCommand cmd = new OleDbCommand();
			PrepareCommand(cmd,cmdType, cmdText, cmdParms);
			int val = cmd.ExecuteNonQuery(); 
			cmd.Parameters.Clear();
			return val;
		}

		//开始事务
		public void BeginTrans()
		{
			trans=conn.BeginTransaction() ;
		}

		//提交事物
		public void CommitTrans()
		{
			trans.Commit();
		}
        
		//撤消事务
		public void RollbackTrans()
		{
			trans.Rollback(); 
		}
		//打开数据库
		public void Open()
		{
			conn  =this.OleDbConnect();
   　　		if (conn.State  == ConnectionState.Closed )
			{ 
  　　　　	conn.Open();
			}
		}
			
		//关闭数据库
		public void Close()
		{
			if (conn.State  == ConnectionState.Open )
			{ 
				conn.Close();
			}
		}

		//执行命令前对命令所做的赋值
		private void PrepareCommand(OleDbCommand cmd,   CommandType cmdType, string cmdText, Parameter[] cmdParms) 
		{			
			cmd.Connection = conn;
			cmd.CommandText = cmdText;
			cmd.CommandType = cmdType;
			if (trans != null)
				cmd.Transaction = trans;
			if (cmdParms != null) 
			{				
				foreach (Parameter parm in cmdParms)
				{
					OleDbParameter objParameter = new OleDbParameter();  
					if(parm._parameterName != null){objParameter.ParameterName = parm._parameterName;}  
					if(parm._parameterValue  != null){objParameter.Value = parm._parameterValue;} 
					if(parm._parameterType.ToString()!="AnsiString"){objParameter.DbType = parm._parameterType;} 
					if(parm._parameterSize   != int.MinValue){objParameter.Size = parm._parameterSize;} 
					if(parm._parameterDirection.ToString() != "0"){objParameter.Direction = parm._parameterDirection;} 
					cmd.Parameters.Add(objParameter); 
				}
			}
		} 	

		//返回数据库连接对象
		private OleDbConnection OleDbConnect()
		{   		
			string connectString = DALFactory.ConnectString(); 
			OleDbConnection Connect = new OleDbConnection(connectString); 
			return Connect;
		}
	}
}
