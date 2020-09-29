using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Collections;

namespace Bussiness.SqlHelper
{
	/// <summary>
	/// ѡ��ͬ�����ݿ�
	/// </summary>
	public class DALFactory
	{

		public static IDAL GetDBO() 
		{ 
  ����		if(ConnectString().IndexOf("Provider=")<0) //SqlServer 
			{ 
  ����		 return new SqlServerDAL();  
			} 
  ����		else 
			{ 
		  �� return new OleDbDAL();  ��
			} 
		}

		//�������ݿ����Ӳ���
		public  static string  ConnectString()
		{
            
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString; 
    
			return connectionString;			
		}
	}
}
