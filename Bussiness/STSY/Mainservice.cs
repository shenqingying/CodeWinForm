using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.Data;
using System.Data.SqlClient;
using Bussiness.SqlHelper;

namespace Bussiness.STSY
{
    public class Mainservice
    {
        public bool LOGIN(string name, string password)
        {
            byte[] result = Encoding.Default.GetBytes(password);    //tbPass为输入密码的文本框
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] output = md5.ComputeHash(result);
            password = BitConverter.ToString(output).Replace("-", "");  //tbMd5pass为输出加密文本的文本框
            //password = UserMd5(password);
            string str = "SELECT COUNT(*) AS NO FROM HG_STAFF WHERE STAFFUSER=@STAFFUSER AND PWMD5=@PWMD5 AND ISACTIVENEW = '1'";
            SqlParameter[] paras = {
                                       new SqlParameter("@STAFFUSER",name),
                                       new SqlParameter("@PWMD5",password)
                                   };
            SqlDataReader reader = DBHelper.ExecuteReader(CommandType.Text, str, paras);
            if (reader.Read())
            {
                int count = Convert.ToInt32(reader["NO"].ToString());
                reader.Close();
                if (count == 1)
                    return true;
                else
                    return false;
            }
            else
            {
                reader.Close();
                return false;
            }
        }
        //public int GETSTAFFID(string STAFFNO)
        //{
        //    string str = "SELECT STAFFID,STAFFNAME FROM HG_STAFF WHERE STAFFNO=@STAFFNO";
        //    SqlParameter[] para = {
        //                              new SqlParameter("@STAFFNO",STAFFNO)
        //                          };
        //    SqlDataReader reader = DBHelper.ExecuteReader(CommandType.Text, str, para);
        //    if (reader.Read())
        //    {
        //        string a = reader["STAFFID"].ToString();
        //        reader.Close();
        //        try
        //        {
        //            int count = Convert.ToInt32(a);
        //            return count;
        //        }
        //        catch
        //        {
        //            return 0;
        //        }
        //    }
        //    else
        //    {
        //        return 0;
        //    }
        //}

        public DataTable GETSTAFFID(string STAFFUSER)
        {
            string str = "SELECT STAFFID,STAFFNAME FROM HG_STAFF WHERE STAFFUSER=@STAFFUSER";
            SqlParameter[] paras = {
                                       new SqlParameter("@STAFFUSER",STAFFUSER)
                                   };
            DataTable dt = DBHelper.GetDataSet(CommandType.Text, str, paras);
            return dt;
        }

        public bool SELECT_HG_RIGHT(string STAFFID, string RIGHTTAG)
        {
            string str = "select count(*) AS NO from HG_RIGHT,HG_STAFFRIGHT where HG_RIGHT.RIGHTID=HG_STAFFRIGHT.RIGHTID and HG_STAFFRIGHT.STAFFID=@STAFFID and HG_RIGHT.RIGHTTAG=@RIGHTTAG";
            SqlParameter[] paras = {
                                       new SqlParameter("@STAFFID",STAFFID),
                                       new SqlParameter("@RIGHTTAG",RIGHTTAG)
                                   };
            SqlDataReader reader = DBHelper.ExecuteReader(CommandType.Text, str, paras);
            if (reader.Read())
            {
                int count = Convert.ToInt32(reader["NO"].ToString());
                reader.Close();
                if (count >= 1)
                    return true;
                else
                    return false;
            }
            else
            {
                reader.Close();
                return false;
            }
        }
    }
}
