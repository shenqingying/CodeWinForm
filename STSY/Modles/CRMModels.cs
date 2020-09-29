using Bussiness.CRM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace STSY.Modles
{
    public class CRMModels
    {
        private CRM_Login _CRM_Login;

        public CRM_Login CRM_Login
        {
            get
            {
                if (_CRM_Login == null)
                    _CRM_Login = new CRM_Login();
                return _CRM_Login;
            }
            set { _CRM_Login = value; }
        }
    }
}