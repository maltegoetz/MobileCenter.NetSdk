using MobileCenterSdk.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileCenterSdk.Models
{

    public class McApp : McAppSlim
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "app_secret")]
        public string AppSecret { get; set; }

        [JsonProperty(PropertyName = "azure_subscription_id")]
        public string AzureSubscriptionId { get; set; }

        [JsonProperty(PropertyName = "icon_url")]
        public string IconUrl { get; set; }
        
        [JsonProperty(PropertyName = "member_permissions")]
        public List<string> MemberPermissions { get; set; } = new List<string>();
        
        [JsonProperty(PropertyName = "owner")]
        public McOwner Owner { get; set; }
        
        [JsonProperty(PropertyName = "origin")]
        public string Origin { get; set; }


        [JsonIgnore]
        public Origin OriginType
        {
            get
            {
                return StringToOriginConverter.Convert(Origin);
            }
            set
            {
                Origin = StringToOriginConverter.ConvertBack(value);
            }
        }
    }
    public class McAppSlim : McAppBase
    {

        [JsonProperty(PropertyName = "os")]
        public string Os { get; set; }

        [JsonProperty(PropertyName = "platform")]
        public string Platform { get; set; }

        [JsonIgnore]
        public AppOs OsType
        {
            get
            {
                return StringToOsTypeConverter.Convert(Os);
            }
            set
            {
                Os = StringToOsTypeConverter.ConvertBack(value);
            }
        }

        [JsonIgnore]
        public AppPlatform PlatformType
        {
            get
            {
                return StringToPlatformTypeConverter.Convert(Platform);
            }
            set
            {
                Platform = StringToPlatformTypeConverter.ConvertBack(value);
            }
        }
        
    }
    public class McAppBase
    {
        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }

        [JsonProperty(PropertyName = "display_name")]
        public string DisplayName { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
    }
    public enum AppPlatform
    {
        Cordova,
        Java,
        ObjectiveCSwift,
        ReactNative,
        Unity,
        UWP,
        Xamarin,
        Unknown
    }
    public enum AppOs
    {
        Android,
        IOs,
        MacOs,
        Tizen,
        Windows,
        Custom,
        Unknown
    }

}
