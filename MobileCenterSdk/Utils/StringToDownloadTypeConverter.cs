using MobileCenterSdk.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileCenterSdk.Utils
{
    public static class StringToDownloadTypeConverter
    {
        private const string Build = "build";
        private const string Symbols = "symbols";
        private const string Logs = "logs";
        private const string TestReportPreview = "test-report-preview";

        public static DownloadType Convert(string downloadTypeString)
        {
            switch(downloadTypeString)
            {
                case Build:
                    return DownloadType.Build;
                case Symbols:
                    return DownloadType.Symbols;
                case Logs:
                    return DownloadType.Logs;
                case TestReportPreview:
                    return DownloadType.TestReportPreview;
                default:
                    return DownloadType.Unknown;
            }
        }
        public static string ConvertBack(DownloadType downloadType)
        {
            switch(downloadType)
            {
                case DownloadType.Build:
                    return Build;
                case DownloadType.Symbols:
                    return Symbols;
                case DownloadType.Logs:
                    return Logs;
                case DownloadType.TestReportPreview:
                    return TestReportPreview;
                default:
                    return string.Empty;
            }
        }
    }
}
