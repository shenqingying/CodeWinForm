using Bussiness.SSO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace STSY.Modles
{
    public class SSOModels
    {
        private TOKEN_TOKENIDINFO _TOKEN_TOKENIDINFO;

        public TOKEN_TOKENIDINFO TOKEN_TOKENIDINFO
        {
            get
            {
                if (_TOKEN_TOKENIDINFO == null)
                    _TOKEN_TOKENIDINFO = new TOKEN_TOKENIDINFO();
                return _TOKEN_TOKENIDINFO;
            }
            set { _TOKEN_TOKENIDINFO = value; }
        }
    }
}