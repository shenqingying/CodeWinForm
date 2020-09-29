//提供各种参数类型实体
using System;
using System.Data;

namespace Bussiness.SqlHelper
{
	public class Parameter
	{
		//参数名称
		public string _parameterName  ;
		public object _parameterValue ;
		public DbType _parameterType  ;
		public int    _parameterSize = int.MinValue;
		public ParameterDirection _parameterDirection ;

		/// <summary>
		/// 提供 参数名+参数值 构造函数
		/// </summary>
		/// <param name="parameterName">参数名</param>
		/// <param name="parameterValue">参数值</param>
		public Parameter(string parameterName,object parameterValue)
		{
			this._parameterName  = parameterName;
            this._parameterValue = parameterValue == null ? System.DBNull.Value : parameterValue;

		}

		/// <summary>
		/// 提供 参数名+参数类型 构造函数
		/// </summary>
		/// <param name="parameterName">参数名</param>
		/// <param name="parameterType">参数类型</param>
		/// <param name="parameterSize">参数大小</param>
		public Parameter(string parameterName,DbType parameterType,int parameterSize)
		{
			this._parameterName  = parameterName;
			this._parameterType  = parameterType;
			this._parameterSize  = parameterSize;
		}

		

		/// <summary>
		/// 提供 参数名+参数类型+参数大小+参数值 构造函数
		/// </summary>
		/// <param name="parameterName">参数名</param>
		/// <param name="parameterType">参数类型</param>
		/// <param name="parameterSize">参数大小</param>
		/// <param name="parameterValue">参数值</param>
		public Parameter(string parameterName,DbType parameterType,int parameterSize,object parameterValue)
		{
			this._parameterName  = parameterName;
			this._parameterType  = parameterType;
			this._parameterSize  = parameterSize;
            this._parameterValue = parameterValue == null ? System.DBNull.Value : parameterValue;	
		}


		/// <summary>
		/// 提供 参数名+参数类型+参数大小+参数值+参数方向 构造函数
		/// </summary>
		/// <param name="parameterName">参数名</param>
		/// <param name="parameterType">参数类型</param>
		/// <param name="parameterSize">参数大小</param>
		/// <param name="parameterValue">参数值</param>
		/// <param name="parameterDirection">方向</param>
		public Parameter(string parameterName,DbType parameterType,int parameterSize,object parameterValue,ParameterDirection parameterDirection)
		{
			this._parameterName  = parameterName;
			this._parameterType  = parameterType;
			this._parameterSize  = parameterSize;
            this._parameterValue = parameterValue == null ? System.DBNull.Value : parameterValue;
			this._parameterDirection = parameterDirection;
		}

        /// <summary>
        /// 提供 参数名+参数类型+参数大小+参数值+参数方向 构造函数
        /// </summary>
        /// <param name="parameterName">参数名</param>
        /// <param name="parameterType">参数类型</param>
        /// <param name="parameterSize">参数大小</param>
        /// <param name="parameterDirection">方向</param>
        public Parameter(string parameterName, DbType parameterType, int parameterSize,  ParameterDirection parameterDirection)
        {
            this._parameterName = parameterName;
            this._parameterType = parameterType;
            this._parameterSize = parameterSize;
            this._parameterDirection = parameterDirection;
        }

		
	}
}
