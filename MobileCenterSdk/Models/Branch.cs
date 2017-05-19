using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileCenterSdk.Models
{

    public class McBranchStatus
    {
        [JsonProperty(PropertyName = "branch")]
        public McBranch Branch { get; set; }

        [JsonProperty(PropertyName = "configured")]
        public bool IsConfigured { get; set; }

        [JsonProperty(PropertyName = "lastBuild")]
        public McBuild LastBuild { get; set; }

        [JsonProperty(PropertyName = "trigger")]
        public string Trigger { get; set; }
    }

    public class McBranch
    {
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "commit")]
        public McCommitBase Commit { get; set; }
    }

    public class McCommitBase
    {
        [JsonProperty(PropertyName = "sha")]
        public string Sha { get; set; }

        [JsonProperty(PropertyName = "url")]
        public string Url { get; set; }
    }
    public class McCommitDetail : McCommitBase
    {
        [JsonProperty(PropertyName = "commit")]
        public McCommit Commit { get; set; }
    }

    public class McCommit
    {
        [JsonProperty(PropertyName = "message")]
        public string Message { get; set; }

        [JsonProperty(PropertyName = "author")]
        public McAuthor Author { get; set; }
    }

    public class McAuthor
    {
        [JsonProperty(PropertyName = "date")]
        public string CommitDate { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "email")]
        public string Email { get; set; }
    }


}
