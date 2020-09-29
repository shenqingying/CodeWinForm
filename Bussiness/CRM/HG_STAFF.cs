using Bussiness.CRM.HG_STAFFService;
using Sonluk.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bussiness.CRM
{
    public class HG_STAFF
    {
        private HG_STAFFSoapClient client = new HG_STAFFSoapClient("HG_STAFFSoap", AppConfig.Settings("RemoteAddress") + "CRM/HG_STAFF.asmx");

        public int Create(CRM_HG_STAFF model, string ptoken)
        {
            return client.Create(model, ptoken);
        }

        public int Update(CRM_HG_STAFF model, string ptoken)
        {
            return client.Update(model, ptoken);
        }

        public int Delete(int STAFFID, string ptoken)
        {
            return client.Delete(STAFFID, ptoken);
        }

        public CRM_HG_STAFFList[] Report(CRM_Report_STAFFModel model, string TYPE, string ptoken)
        {
            return client.Report(model, TYPE, ptoken);
        }
        public CRM_HG_STAFF ReadBySTAFFID(int STAFFID, string ptoken)
        {
            return client.ReadBySTAFFID(STAFFID, ptoken);
        }
        public CRM_HG_STAFF ReadBySTAFFNO(string STAFFNO, string ptoken)
        {
            return client.ReadBySTAFFNO(STAFFNO, ptoken);
        }
        public CRM_HG_STAFF ReadByROLEID(int ROLEID, string ptoken)
        {

            return client.ReadByROLEID(ROLEID, ptoken);

        }
        public CRM_HG_STAFF[] ReadByDUTYID(int DUTYID, string ptoken)
        {

            return client.ReadByDUTYID(DUTYID, ptoken);


        }
        public CRM_HG_STAFFList[] ReadByParam(CRM_HG_STAFF model, string ptoken)
        {
            return client.ReadByParam(model, ptoken);
        }
        public CRM_HG_STAFFList[] ReadUser(string ptoken)
        {

            return client.ReadUser(ptoken);

        }
        public STAFFDUTY_KH[] ReadSTAFFBYKHIDANDDUTY(int KHID, int DUTYID, string ptoken)
        {

            return client.ReadSTAFFBYKHIDANDDUTY(KHID, DUTYID, ptoken);


        }
        public CRM_HG_STAFF[] ReadSTAFF_KHGOURPBYSTAFFID(int STAFFID, string ptoken)
        {

            return client.ReadSTAFF_KHGOURPBYSTAFFID(STAFFID, ptoken);


        }
        public CRM_HG_STAFF[] ReadSTAFF_KHGroupByStaffidAndDuty(int STAFFID, int DUTYID, string ptoken)
        {
            return client.ReadSTAFF_KHGroupByStaffidAndDuty(STAFFID, DUTYID, ptoken);
        }
        public CRM_HG_STAFF[] ReadStaff_BYDEPID(int STAFFID, string ptoken)
        {

            return client.ReadStaff_BYDEPID(STAFFID, ptoken);

        }
        public CRM_HG_STAFFList[] ReadUser_STAFF(CRM_Report_STAFFModel model, string ptoken)
        {
            return client.ReadUser_STAFF(model, ptoken);

        }

    }
}
