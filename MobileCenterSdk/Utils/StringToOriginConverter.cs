using MobileCenterSdk.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileCenterSdk.Utils
{
    public static class StringToOriginConverter
    {
        private const string MobileCenter = "mobile-center";
        private const string HockeyApp = "hockeyapp";
        public static Origin Convert(string originString)
        {
            switch (originString.ToLower())
            {
                case MobileCenter:
                    return Origin.MobileCenter;
                case HockeyApp:
                    return Origin.HockeyApp;
                default:
                    return Origin.Unknown;
            }
        }
        public static string ConvertBack(Origin origin)
        {
            switch(origin)
            {
                case Origin.MobileCenter:
                    return MobileCenter;
                case Origin.HockeyApp:
                    return HockeyApp;
                default:
                    return string.Empty;
            }
        }
    }
}
