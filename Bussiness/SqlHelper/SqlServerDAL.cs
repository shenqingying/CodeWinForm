using System;
using System.Data;
using System.Data.SqlClient;

namespace Bussiness.SqlHelper
{
	/// <summary>
	/// SqlServer数据库访问层。
	/// </summary>
	public class SqlServerDAL:IDAL
	{
		private  SqlTransaction trans; //事务处理类 
		private  SqlConnection conn; //数据库连接 

		public SqlServerDAL(){}

		/// <summary>
		/// 在给定DataSet的基础上再绑定一个表,返回DataSet;
		/// </summary>
		/// <param name="cmdType">命令类型</param>
		/// <param name="cmdText">命令串或存储过程名</param>
		/// <param name="objSet">DataSet对象</param>
		/// <param name="tableName">新增加的表名</param>
		/// <param name="cmdParms">命令的参数对象</param>
		/// <returns></returns>
		public DataSet AddDataAdapter(System.Data.CommandType cmdType, string cmdText, DataSet objSet, string tableName, params Parameter[] cmdParms)
		{
			SqlCommand cmd = new SqlCommand();
			PrepareCommand(cmd, cmdType, cmdText, cmdParms);
			SqlDataAdapter objDataAdapter = new SqlDataAdapter(); 
			objDataAdapter.SelectCommand  = cmd;
			objDataAdapter.Fill(objSet,tableName);
			SetParameterValue(cmd,cmdParms);
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
        public DataSet ReturnDatSet(System.Data.CommandType cmdType, string cmdText, string tableName, params Parameter[] cmdParms)
		{
			SqlCommand cmd = new SqlCommand();
			PrepareCommand(cmd, cmdType, cmdText, cmdParms);
			DataSet objSet = new DataSet(); 
   　　     SqlDataAdapter objDataAdapter = new SqlDataAdapter(); 
			objDataAdapter.SelectCommand  = cmd;
			objDataAdapter.Fill(objSet,tableName);
			SetParameterValue(cmd,cmdParms);
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
        public DataSet ReturnDatSet(System.Data.CommandType cmdType, string cmdText, string tableName,int startrow,int pageSize, params Parameter[] cmdParms)
        {
            SqlCommand cmd = new SqlCommand();
            PrepareCommand(cmd, cmdType, cmdText, cmdParms);
            DataSet objSet = new DataSet();
            SqlDataAdapter objDataAdapter = new SqlDataAdapter();
            objDataAdapter.SelectCommand = cmd;
            objDataAdapter.Fill(objSet,startrow,pageSize, tableName);
            SetParameterValue(cmd, cmdParms);
            return objSet;
        }

		/// <summary>
		/// 执行命令返回一个分页用的DataSet;
		/// </summary>
		/// <param name="cmdType">命令类型</param>
		/// <param name="cmdText">命令串或存储过程名</param>
		/// <param name="tableName">新增加的表名</param>
		/// <param name="startRow">开始行</param>
		/// <param name="pageSize">页面大小</param>
		/// <param name="cmdParms">命令的参数对象</param>
		/// <returns></returns>
		public DataSet PageDataAdapter(CommandType cmdType, string cmdText, string tableName, int startRow,int pageSize,params Parameter[] cmdParms )
		{
			SqlCommand cmd = new SqlCommand();
			PrepareCommand(cmd, cmdType, cmdText, cmdParms);
			DataSet objSet = new DataSet(); 
   　　     SqlDataAdapter objDataAdapter = new SqlDataAdapter(); 
			objDataAdapter.SelectCommand  = cmd;
			objDataAdapter.Fill(objSet,startRow,pageSize,tableName);
			SetParameterValue(cmd,cmdParms);
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
			SqlCommand cmd = new SqlCommand();
			PrepareCommand(cmd, cmdType, cmdText, cmdParms);
			object val = cmd.ExecuteScalar();
			SetParameterValue(cmd,cmdParms);
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
			SqlCommand cmd = new SqlCommand();
			PrepareCommand(cmd,cmdType, cmdText, cmdParms);
			int val = cmd.ExecuteNonQuery(); 
			SetParameterValue(cmd,cmdParms);
			cmd.Parameters.Clear();
			return val;
		}

		/// <summary>
		/// 返回sqlServer适配器
		/// </summary>
		/// <param name="cmdType"></param>
		/// <param name="cmdText"></param>
		/// <param name="cmdParms"></param>
		/// <returns></returns>
		public SqlDataAdapter sqlDataAdapter(System.Data.CommandType cmdType, string cmdText, params Parameter[] cmdParms)
		{
			SqlCommand cmd = new SqlCommand(); 
			PrepareCommand(cmd,cmdType, cmdText, cmdParms);
			SqlDataAdapter objDataAdapter = new SqlDataAdapter(); 
			objDataAdapter.SelectCommand  = cmd;
	    	SetParameterValue(cmd,cmdParms);
			return objDataAdapter;
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
			conn  =this.SqlConnect();
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
		private void PrepareCommand(SqlCommand cmd,   CommandType cmdType, string cmdText, Parameter[] cmdParms) 
		{			
			cmd.Connection = conn;
			cmd.CommandText = cmdText;
			cmd.CommandType = cmdType;
            cmd.CommandTimeout = 10000;
			if (trans != null)
				cmd.Transaction = trans;
			if (cmdParms != null) 
			{				
				foreach (Parameter parm in cmdParms)
				{
					SqlParameter objParameter = new SqlParameter(); 
					if(parm._parameterName != null){objParameter.ParameterName = parm._parameterName;}  
                    //if(parm._parameterValue  == null){objParameter.Value = parm._parameterValue;} 
                    objParameter.Value = parm._parameterValue;
					if(parm._parameterType.ToString()!="AnsiString"){objParameter.DbType = parm._parameterType;} 
					if(parm._parameterSize   != int.MinValue){objParameter.Size = parm._parameterSize;} 
					if(parm._parameterDirection.ToString() != "0"){objParameter.Direction =parm._parameterDirection;} 
					cmd.Parameters.Add(objParameter); 
				}
			}
		} 

		//命令执行完成后将返回值赋值参数
		private void SetParameterValue(SqlCommand cmd,Parameter[] cmdParms)
		{
			if (cmdParms != null) 
			{				
				for(int i = 0 ; i<cmd.Parameters.Count; i++) 
				{
					if(cmdParms[i]._parameterDirection.ToString() != "0")
					{
					  cmdParms[i]._parameterValue =  cmd.Parameters[i].Value; 
					}
				}
			}
		}

		//返回数据库连接对象
		private SqlConnection SqlConnect()
		{   		
			string connectString = DALFactory.ConnectString(); 
			SqlConnection Connect = new SqlConnection(connectString); 
			return Connect;
		}
	}
}
