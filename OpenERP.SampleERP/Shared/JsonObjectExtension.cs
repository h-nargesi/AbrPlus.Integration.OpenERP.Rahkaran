using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace AbrPlus.Integration.OpenERP.SampleERP.Shared;
public static class JsonObjectExtension
{
    public static string SerializeJson(this object obj)
    {
        var serializerSettings = new JsonSerializerSettings
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver()
        };
        return JsonConvert.SerializeObject(obj, serializerSettings);
    }
}
