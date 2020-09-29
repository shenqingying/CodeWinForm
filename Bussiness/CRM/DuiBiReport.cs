using Bussiness.RuK.DuiBiReportService;
using Sonluk.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Bussiness.CRM
{
   public class DuiBiReport
    {
       private DuiBiReportSoapClient client = new DuiBiReportSoapClient("DuiBiReportSoap", AppConfig.Settings("RemoteAddress") + "CRM/DuiBiReport.asmx");

       public DataTable RuK_DuiBiReport(RuK_DuiBiReport model, string ptoken)
       {
           return client.RuK_DuiBiReport(model, ptoken);
       }
    }
}
