using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using MobileCenterSdk.Models;

namespace MobileCenterSdk.Utils
{
    class OwnerTypeConverter : JsonConverter
    {
        private const string Organization = "org";
        private const string User = "user";
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(OwnerType);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var ownerstring = serializer.Deserialize<string>(reader);
            switch(ownerstring)
            {
                case Organization:
                    return OwnerType.Organization;
                case User:
                    return OwnerType.User;
                default:
                    return OwnerType.User;
            }
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            string output = string.Empty;
            switch((OwnerType) value)
            {
                case OwnerType.Organization:
                    output = Organization;
                    break;
                case OwnerType.User:
                    output = User;
                    break;
            }
            serializer.Serialize(writer, output);
        }
    }
}
