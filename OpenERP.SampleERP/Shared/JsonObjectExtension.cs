using System.Text.Json;

namespace AbrPlus.Integration.OpenERP.SampleERP.Shared;
public static class JsonObjectExtension
{
    private static readonly JsonSerializerOptions Options = new()
    {
        Converters = { new MicrosoftDateTimeConverter() },
        WriteIndented = true
    };
    
    public static string SerializeJson(this object obj)
    {
        return JsonSerializer.Serialize(obj, Options);
    }
}
