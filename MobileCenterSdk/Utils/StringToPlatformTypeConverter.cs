using MobileCenterSdk.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileCenterSdk.Utils
{
    public static class StringToPlatformTypeConverter
    {
        private const string Cordova = "Cordova";
        private const string Java = "Java";
        private const string ObjectiveCSwift = "Objective-C-Swift";
        private const string ReactNative = "React-Native";
        private const string Unity = "Unity";
        private const string UWP = "UWP";
        private const string Xamarin = "Xamarin";
        private const string Unknown = "Unknown";

        public static AppPlatform Convert(string platformString)
        {
            switch (platformString)
            {
                case Cordova:
                    return AppPlatform.Cordova;
                case Java:
                    return AppPlatform.Java;
                case ObjectiveCSwift:
                    return AppPlatform.ObjectiveCSwift;
                case ReactNative:
                    return AppPlatform.ReactNative;
                case Unity:
                    return AppPlatform.Unity;
                case UWP:
                    return AppPlatform.UWP;
                case Xamarin:
                    return AppPlatform.Xamarin;
                default:
                    return AppPlatform.Unknown;
            }
        }
        public static string ConvertBack(AppPlatform platform)
        {
            switch(platform)
            {
                case AppPlatform.Cordova:
                    return Cordova;
                case AppPlatform.Java:
                    return Java;
                case AppPlatform.ObjectiveCSwift:
                    return ObjectiveCSwift;
                case AppPlatform.ReactNative:
                    return ReactNative;
                case AppPlatform.Unity:
                    return Unity;
                case AppPlatform.UWP:
                    return UWP;
                case AppPlatform.Xamarin:
                    return Xamarin;
                default:
                    return Unknown;
            }
        }
    }
}
