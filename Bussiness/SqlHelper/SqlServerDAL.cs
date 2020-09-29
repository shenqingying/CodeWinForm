using System;
using System.Data;
using System.Data.SqlClient;

namespace Bussiness.SqlHelper
{
	/// <summary>
	/// SqlServer���ݿ���ʲ㡣
	/// </summary>
	public class SqlServerDAL:IDAL
	{
		private  SqlTransaction trans; //�������� 
		private  SqlConnection conn; //���ݿ����� 

		public SqlServerDAL(){}

		/// <summary>
		/// �ڸ���DataSet�Ļ������ٰ�һ����,����DataSet;
		/// </summary>
		/// <param name="cmdType">��������</param>
		/// <param name="cmdText">�����洢������</param>
		/// <param name="objSet">DataSet����</param>
		/// <param name="tableName">�����ӵı���</param>
		/// <param name="cmdParms">����Ĳ�������</param>
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
		/// //ִ�������һ��DataSet;
		/// </summary>
		/// <param name="cmdType">��������</param>
		/// <param name="cmdText">�����洢������</param>
		/// <param name="tableName">�����ӵı���</param>
		/// <param name="cmdParms">����Ĳ�������</param>
		/// <returns></returns>
        public DataSet ReturnDatSet(System.Data.CommandType cmdType, string cmdText, string tableName, params Parameter[] cmdParms)
		{
			SqlCommand cmd = new SqlCommand();
			PrepareCommand(cmd, cmdType, cmdText, cmdParms);
			DataSet objSet = new DataSet(); 
   ����     SqlDataAdapter objDataAdapter = new SqlDataAdapter(); 
			objDataAdapter.SelectCommand  = cmd;
			objDataAdapter.Fill(objSet,tableName);
			SetParameterValue(cmd,cmdParms);
			return objSet;
		}

        /// <summary>
        /// //ִ�������һ��DataSet;
        /// </summary>
        /// <param name="cmdType">��������</param>
        /// <param name="cmdText">�����洢������</param>
        /// <param name="tableName">�����ӵı���</param>
        /// <param name="cmdParms">����Ĳ�������</param>
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
		/// ִ�������һ����ҳ�õ�DataSet;
		/// </summary>
		/// <param name="cmdType">��������</param>
		/// <param name="cmdText">�����洢������</param>
		/// <param name="tableName">�����ӵı���</param>
		/// <param name="startRow">��ʼ��</param>
		/// <param name="pageSize">ҳ���С</param>
		/// <param name="cmdParms">����Ĳ�������</param>
		/// <returns></returns>
		public DataSet PageDataAdapter(CommandType cmdType, string cmdText, string tableName, int startRow,int pageSize,params Parameter[] cmdParms )
		{
			SqlCommand cmd = new SqlCommand();
			PrepareCommand(cmd, cmdType, cmdText, cmdParms);
			DataSet objSet = new DataSet(); 
   ����     SqlDataAdapter objDataAdapter = new SqlDataAdapter(); 
			objDataAdapter.SelectCommand  = cmd;
			objDataAdapter.Fill(objSet,startRow,pageSize,tableName);
			SetParameterValue(cmd,cmdParms);
			return objSet;
		}

		/// <summary>
		/// ִ������ص�һ�е�һ�е�ֵ
		/// </summary>
		/// <param name="cmdType">��������</param>
		/// <param name="cmdText">�����洢������</param>
		/// <param name="cmdParms">����Ĳ�������</param>
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
		/// ִ�����������Ӱ�������
		/// </summary>
		/// <param name="cmdType">��������</param>
		/// <param name="cmdText">�����洢������</param>
		/// <param name="cmdParms">����Ĳ�������</param>
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
		/// ����sqlServer������
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

	



		//��ʼ����
		public void BeginTrans()
		{
			trans=conn.BeginTransaction() ;
		}

		//�ύ����
		public void CommitTrans()
		{
			trans.Commit();
		}
        
		//��������
		public void RollbackTrans()
		{
			trans.Rollback(); 
		}
        //�����ݿ�
		public void Open()
		{
			conn  =this.SqlConnect();
   ����		if (conn.State  == ConnectionState.Closed )
			{ 
  ��������	conn.Open();
			}
		}
			
		//�ر����ݿ�
		public void Close()
		{
			if (conn.State  == ConnectionState.Open )
			{ 
				conn.Close();
			}
		}


		//ִ������ǰ�����������ĸ�ֵ
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

		//����ִ����ɺ󽫷���ֵ��ֵ����
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

		//�������ݿ����Ӷ���
		private SqlConnection SqlConnect()
		{   		
			string connectString = DALFactory.ConnectString(); 
			SqlConnection Connect = new SqlConnection(connectString); 
			return Connect;
		}
	}
}
