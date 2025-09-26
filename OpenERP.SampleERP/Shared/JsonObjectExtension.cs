using Refit;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace AbrPlus.Integration.OpenERP.SampleERP.Shared;
public static class JsonObjectExtension
{
    private static readonly JsonSerializerOptions Options = new()
    {
        Converters = { new MicrosoftDateTimeConverter() },
        WriteIndented = true,
        PropertyNamingPolicy = null,
        DictionaryKeyPolicy = null,
        DefaultIgnoreCondition = JsonIgnoreCondition.Never,
    };

    public static readonly RefitSettings RefitSettings = new()
    {
        ContentSerializer = new SystemTextJsonContentSerializer(Options)
    };

    public static string SerializeJson(this object obj)
    {
        return JsonSerializer.Serialize(obj, Options);
    }
}
