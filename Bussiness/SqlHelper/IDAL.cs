//���ݷ��ʲ�ӿ�
using System;
using System.Data;
using System.Data.SqlClient;


namespace Bussiness.SqlHelper
{
	public interface IDAL
	{
		/// <summary>
        /// //ִ�������һ��DataSet;
        /// </summary>
        /// <param name="cmdType">��������</param>
        /// <param name="cmdText">�����洢������</param>
        /// <param name="tableName">�����ӵı���</param>
        /// <param name="cmdParms">����Ĳ�������</param>
        /// <returns></returns>
		DataSet ReturnDatSet(CommandType cmdType, string cmdText, string tableName, params Parameter[] cmdParms );

        /// <summary>
        /// //ִ�������һ����ҳDataSet;
        /// </summary>
        /// <param name="cmdType">��������</param>
        /// <param name="cmdText">�����洢������</param>
        /// <param name="tableName">�����ӵı���</param>
        /// <param name="cmdParms">����Ĳ�������</param>
        /// <returns></returns>
        DataSet ReturnDatSet(CommandType cmdType, string cmdText, string tableName, int startrow,int pageSize,params Parameter[] cmdParms);	
		
		/// <summary>
		/// ִ������ص�һ�е�һ�е�ֵ
		/// </summary>
		/// <param name="cmdType">��������</param>
		/// <param name="cmdText">�����洢������</param>
		/// <param name="cmdParms">����Ĳ�������</param>
		/// <returns></returns>
		object ExecuteScalar( CommandType cmdType, string cmdText,params Parameter[] cmdParms);
		
		/// <summary>
		/// ִ�����������Ӱ�������
		/// </summary>
		/// <param name="cmdType">��������</param>
		/// <param name="cmdText">�����洢������</param>
		/// <param name="cmdParms">����Ĳ�������</param>
		/// <returns></returns>
		int ExecuteNonQuery(CommandType cmdType, string cmdText,params Parameter[] cmdParms);
		
		
		//��ʼ����
		void BeginTrans() ;
		//�ύ����
		void CommitTrans();		
		//��������
		void RollbackTrans();		
		//�����ݿ�
		void Open();		
		//�ر����ݿ�
		void Close();
	}
}
