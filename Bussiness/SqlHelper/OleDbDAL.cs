using System;
using System.Data;
using System.Data.OleDb;

namespace Bussiness.SqlHelper
{
	/// <summary>
	/// ͨ��OleDb�������ݿ�
	/// </summary>
	public class OleDbDAL:IDAL
	{
		private  OleDbTransaction trans; //�������� 
		private  OleDbConnection conn; //���ݿ����� 

		public OleDbDAL(){}		

		/// <summary>
		/// //ִ�������һ��DataSet;
		/// </summary>
		/// <param name="cmdType">��������</param>
		/// <param name="cmdText">�����洢������</param>
		/// <param name="tableName">�����ӵı���</param>
		/// <param name="cmdParms">����Ĳ�������</param>
		/// <returns></returns>
        public System.Data.DataSet ReturnDatSet(System.Data.CommandType cmdType, string cmdText, string tableName, params Parameter[] cmdParms)
		{
			OleDbCommand cmd = new OleDbCommand();
			PrepareCommand(cmd, cmdType, cmdText, cmdParms);
			DataSet objSet = new DataSet(); 
   ����     OleDbDataAdapter objDataAdapter = new OleDbDataAdapter(); 
			objDataAdapter.SelectCommand  = cmd;
			objDataAdapter.Fill(objSet,tableName);
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
		/// ִ������ص�һ�е�һ�е�ֵ
		/// </summary>
		/// <param name="cmdType">��������</param>
		/// <param name="cmdText">�����洢������</param>
		/// <param name="cmdParms">����Ĳ�������</param>
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
		/// ִ�����������Ӱ�������
		/// </summary>
		/// <param name="cmdType">��������</param>
		/// <param name="cmdText">�����洢������</param>
		/// <param name="cmdParms">����Ĳ�������</param>
		/// <returns></returns>
		public int ExecuteNonQuery(System.Data.CommandType cmdType, string cmdText, params Parameter[] cmdParms)
		{
			OleDbCommand cmd = new OleDbCommand();
			PrepareCommand(cmd,cmdType, cmdText, cmdParms);
			int val = cmd.ExecuteNonQuery(); 
			cmd.Parameters.Clear();
			return val;
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
			conn  =this.OleDbConnect();
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

		//�������ݿ����Ӷ���
		private OleDbConnection OleDbConnect()
		{   		
			string connectString = DALFactory.ConnectString(); 
			OleDbConnection Connect = new OleDbConnection(connectString); 
			return Connect;
		}
	}
}
