using MobileCenterSdk.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileCenterSdk.Utils
{
    public static class StringToRepoStateTypeConverter
    {
        private const string Unauthorized = "unauthorized";
        private const string Inactive = "inactive";
        private const string Active = "active";
        public static RepoState Convert(string repoStateString)
        {
            switch(repoStateString)
            {
                case Unauthorized:
                    return RepoState.Unauthorized;
                case Inactive:
                    return RepoState.Inactive;
                case Active:
                    return RepoState.Active;
                default:
                    return RepoState.Unknown;
            }
        }
        public static string ConvertBack(RepoState repoState)
        {
            switch(repoState)
            {
                case RepoState.Unauthorized:
                    return Unauthorized;
                case RepoState.Inactive:
                    return Inactive;
                case RepoState.Active:
                    return Active;
                default:
                    return string.Empty;
            }
        }
    }
}
