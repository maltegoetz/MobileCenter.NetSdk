using MobileCenterSdk.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileCenterSdk.Utils
{
    public static class StringToSourceHostConverter
    {
        private const string GitHub = "github";
        private const string Bitbucket = "bitbucket";
        private const string Vsts = "vsts";

        public static SourceHost Convert(string sourceHostString)
        {
            switch(sourceHostString)
            {
                case GitHub:
                    return SourceHost.GitHub;
                case Bitbucket:
                    return SourceHost.Bitbucket;
                case Vsts:
                    return SourceHost.Vsts;
                default:
                    return SourceHost.Unknown;
            }
        }
        public static string ConvertBack(SourceHost sourceHost)
        {
            switch(sourceHost)
            {
                case SourceHost.GitHub:
                    return GitHub;
                case SourceHost.Bitbucket:
                    return Bitbucket;
                case SourceHost.Vsts:
                    return Vsts;
                default:
                    return string.Empty;
            }
        }
    }
}
