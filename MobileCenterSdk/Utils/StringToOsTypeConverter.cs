using MobileCenterSdk.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileCenterSdk.Utils
{
    public static class StringToOsTypeConverter
    {
        private const string Android = "Android";
        private const string IOs = "iOS";
        private const string MacOs = "macOS";
        private const string Tizen = "Tizen";
        private const string Windows = "Windows";
        private const string Custom = "Custom";

        public static AppOs Convert(string osString)
        {
            switch (osString)
            {
                case Android:
                    return AppOs.Android;
                case IOs:
                    return AppOs.IOs;
                case MacOs:
                    return AppOs.MacOs;
                case Tizen:
                    return AppOs.Tizen;
                case Windows:
                    return AppOs.Windows;
                case Custom:
                    return AppOs.Custom;
                default:
                    return AppOs.Unknown;
            }
        }
        public static string ConvertBack(AppOs os)
        {
            switch(os)
            {
                case AppOs.Android:
                    return Android;
                case AppOs.IOs:
                    return IOs;
                case AppOs.MacOs:
                    return MacOs;
                case AppOs.Tizen:
                    return Tizen;
                case AppOs.Windows:
                    return Windows;
                case AppOs.Custom:
                    return Custom;
                default:
                    return string.Empty;
            }
        }
    }
}
