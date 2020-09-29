using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using Sonluk.Utility.Security;

namespace Sonluk.Utility
{
    public sealed class AppConfig
    {
        const string keymy = "Sonluk@19542014";
        private AppConfig() { }

        public static string Settings(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }

        public static string Settings(string key, string secretKey)
        {
            return DESE.Decrypt(ConfigurationManager.AppSettings[key], secretKey);
        }

        public static string Settings_key(string key)
        {
            return DESE.Decrypt(key, keymy);
        }

        public static string Settings_key_jm(string key)
        {
            return DESE.Encrypt(key, keymy);
        }
    }
}
