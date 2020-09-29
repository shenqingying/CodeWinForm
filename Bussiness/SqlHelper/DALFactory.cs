using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Collections;

namespace Bussiness.SqlHelper
{
	/// <summary>
	/// 选择不同的数据库
	/// </summary>
	public class DALFactory
	{

		public static IDAL GetDBO() 
		{ 
  　　		if(ConnectString().IndexOf("Provider=")<0) //SqlServer 
			{ 
  　　		 return new SqlServerDAL();  
			} 
  　　		else 
			{ 
		  　 return new OleDbDAL();  　
			} 
		}

		//返回数据库连接参数
		public  static string  ConnectString()
		{
            
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString; 
    
			return connectionString;			
		}
	}
}
