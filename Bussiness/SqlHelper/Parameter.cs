//�ṩ���ֲ�������ʵ��
using System;
using System.Data;

namespace Bussiness.SqlHelper
{
	public class Parameter
	{
		//��������
		public string _parameterName  ;
		public object _parameterValue ;
		public DbType _parameterType  ;
		public int    _parameterSize = int.MinValue;
		public ParameterDirection _parameterDirection ;

		/// <summary>
		/// �ṩ ������+����ֵ ���캯��
		/// </summary>
		/// <param name="parameterName">������</param>
		/// <param name="parameterValue">����ֵ</param>
		public Parameter(string parameterName,object parameterValue)
		{
			this._parameterName  = parameterName;
            this._parameterValue = parameterValue == null ? System.DBNull.Value : parameterValue;

		}

		/// <summary>
		/// �ṩ ������+�������� ���캯��
		/// </summary>
		/// <param name="parameterName">������</param>
		/// <param name="parameterType">��������</param>
		/// <param name="parameterSize">������С</param>
		public Parameter(string parameterName,DbType parameterType,int parameterSize)
		{
			this._parameterName  = parameterName;
			this._parameterType  = parameterType;
			this._parameterSize  = parameterSize;
		}

		

		/// <summary>
		/// �ṩ ������+��������+������С+����ֵ ���캯��
		/// </summary>
		/// <param name="parameterName">������</param>
		/// <param name="parameterType">��������</param>
		/// <param name="parameterSize">������С</param>
		/// <param name="parameterValue">����ֵ</param>
		public Parameter(string parameterName,DbType parameterType,int parameterSize,object parameterValue)
		{
			this._parameterName  = parameterName;
			this._parameterType  = parameterType;
			this._parameterSize  = parameterSize;
            this._parameterValue = parameterValue == null ? System.DBNull.Value : parameterValue;	
		}


		/// <summary>
		/// �ṩ ������+��������+������С+����ֵ+�������� ���캯��
		/// </summary>
		/// <param name="parameterName">������</param>
		/// <param name="parameterType">��������</param>
		/// <param name="parameterSize">������С</param>
		/// <param name="parameterValue">����ֵ</param>
		/// <param name="parameterDirection">����</param>
		public Parameter(string parameterName,DbType parameterType,int parameterSize,object parameterValue,ParameterDirection parameterDirection)
		{
			this._parameterName  = parameterName;
			this._parameterType  = parameterType;
			this._parameterSize  = parameterSize;
            this._parameterValue = parameterValue == null ? System.DBNull.Value : parameterValue;
			this._parameterDirection = parameterDirection;
		}

        /// <summary>
        /// �ṩ ������+��������+������С+����ֵ+�������� ���캯��
        /// </summary>
        /// <param name="parameterName">������</param>
        /// <param name="parameterType">��������</param>
        /// <param name="parameterSize">������С</param>
        /// <param name="parameterDirection">����</param>
        public Parameter(string parameterName, DbType parameterType, int parameterSize,  ParameterDirection parameterDirection)
        {
            this._parameterName = parameterName;
            this._parameterType = parameterType;
            this._parameterSize = parameterSize;
            this._parameterDirection = parameterDirection;
        }

		
	}
}
