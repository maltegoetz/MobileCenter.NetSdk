using MobileCenterSdk.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileCenterSdk.Models
{
    public class McDistributionGroup : McDistributionGroupBase
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

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
    public class McDistributionGroupBase
    {

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

    }
}
