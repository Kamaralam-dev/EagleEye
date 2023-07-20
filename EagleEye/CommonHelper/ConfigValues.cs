using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace EagleEye.CommonHelper
{
    public static class ConfigValues
    {
        public static string ImagePath
        {
            get {
                return  ConfigurationManager.AppSettings["ImagePath"];
            }
        }

        public static string ApiUrl
        {
            get
            {
                return ConfigurationManager.AppSettings["ApiUrl"];
            }
        }

        public static bool IsApiEnable
        {
            get
            {
                return Convert.ToBoolean(ConfigurationManager.AppSettings["IsApiEnable"]);
            }
        }
    }
}