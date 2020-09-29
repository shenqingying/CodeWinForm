using Bussiness.CRM.CRM_LoginService;
using Sonluk.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bussiness.CRM
{
    public class CRM_Login
    {
        private CRM_LoginSoapClient client = new CRM_LoginSoapClient("CRM_LoginSoap", AppConfig.Settings("RemoteAddress") + "CRM/CRM_Login.asmx");

        public CRM_LoginInfo Login(string name, string password, string OPENID, string WXDLCS, int PC, int WX)
        {
            return client.Login(name, password, OPENID, WXDLCS, PC, WX);
        }

        public int WX_SYS_Update(CRM_WX_SYS model, string ptoken)
        {
            return client.WX_SYS_Update(model, ptoken);
        }

        public CRM_WX_SYS WX_SYS_ReadByWXAPPID(string WXAPPID, string ptoken)
        {
            return client.WX_SYS_ReadByWXAPPID(WXAPPID, ptoken);
        }
        public CRM_LoginInfo Login_SSO_TOKEN(string TOKENID, int PC, int WX)
        {
            return client.Login_SSO_TOKEN(TOKENID, PC, WX);
        }
    }
}
