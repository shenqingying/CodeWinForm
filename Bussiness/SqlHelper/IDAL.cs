//数据访问层接口
using System;
using System.Data;
using System.Data.SqlClient;


namespace Bussiness.SqlHelper
{
	public interface IDAL
	{
		/// <summary>
        /// //执行命令返回一个DataSet;
        /// </summary>
        /// <param name="cmdType">命令类型</param>
        /// <param name="cmdText">命令串或存储过程名</param>
        /// <param name="tableName">新增加的表名</param>
        /// <param name="cmdParms">命令的参数对象</param>
        /// <returns></returns>
		DataSet ReturnDatSet(CommandType cmdType, string cmdText, string tableName, params Parameter[] cmdParms );

        /// <summary>
        /// //执行命令返回一个分页DataSet;
        /// </summary>
        /// <param name="cmdType">命令类型</param>
        /// <param name="cmdText">命令串或存储过程名</param>
        /// <param name="tableName">新增加的表名</param>
        /// <param name="cmdParms">命令的参数对象</param>
        /// <returns></returns>
        DataSet ReturnDatSet(CommandType cmdType, string cmdText, string tableName, int startrow,int pageSize,params Parameter[] cmdParms);	
		
		/// <summary>
		/// 执行命令返回第一行第一列的值
		/// </summary>
		/// <param name="cmdType">命令类型</param>
		/// <param name="cmdText">命令串或存储过程名</param>
		/// <param name="cmdParms">命令的参数对象</param>
		/// <returns></returns>
		object ExecuteScalar( CommandType cmdType, string cmdText,params Parameter[] cmdParms);
		
		/// <summary>
		/// 执行命令并返回所影响的行数
		/// </summary>
		/// <param name="cmdType">命令类型</param>
		/// <param name="cmdText">命令串或存储过程名</param>
		/// <param name="cmdParms">命令的参数对象</param>
		/// <returns></returns>
		int ExecuteNonQuery(CommandType cmdType, string cmdText,params Parameter[] cmdParms);
		
		
		//开始事务
		void BeginTrans() ;
		//提交事物
		void CommitTrans();		
		//撤消事务
		void RollbackTrans();		
		//打开数据库
		void Open();		
		//关闭数据库
		void Close();
	}
}
