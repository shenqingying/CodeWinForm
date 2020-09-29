using Bussiness.SqlHelper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Bussiness.FKSY
{
    public class GetException
    {
        public void Insert_FK_ExceptionINFO(string ExceptionMX, string ExceptionTIME, string ExceptionZD)
        {
            IDAL objDAL = DALFactory.GetDBO();
            objDAL.Open();
            string sql = "insert into FK_ExceptionINFO(ExceptionMX,ExceptionTIME,ExceptionZD) values(@ExceptionMX,@ExceptionTIME,@ExceptionZD)";
            Parameter[] paras = {
                                    new Parameter("@ExceptionMX",ExceptionMX),
                                    new Parameter("@ExceptionTIME",ExceptionTIME),
                                    new Parameter("@ExceptionZD",ExceptionZD)
                                };
            objDAL.ExecuteNonQuery(CommandType.Text, sql, paras);
            objDAL.Close();
        }
    }
}