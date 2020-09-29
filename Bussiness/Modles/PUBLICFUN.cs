using Sonluk.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bussiness.Modles
{
    public class PUBLICFUN
    {
        public string get_my(string key)
        {
            return AppConfig.Settings_key(key);
        }

        public string get_my_jm(string key)
        {
            return AppConfig.Settings_key_jm(key);
        }

    }
}
