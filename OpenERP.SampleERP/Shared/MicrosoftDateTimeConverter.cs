using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace AbrPlus.Integration.OpenERP.SampleERP.Shared;

public partial class MicrosoftDateTimeConverter: JsonConverter<DateTime>
{
    public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var raw = reader.GetString();

        if (string.IsNullOrEmpty(raw)) 
            return default;
        
        var match = DateRegex().Match(raw);
        if (!match.Success || !long.TryParse(match.Groups[1].Value, out var ms))
            throw new JsonException($"Invalid date format: {raw}");

        return DateTimeOffset.FromUnixTimeMilliseconds(ms).UtcDateTime;
    }

    public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
    {
        var ms = new DateTimeOffset(value).ToUnixTimeMilliseconds();
        writer.WriteStringValue($"/Date({ms})/");
    }

    [GeneratedRegex(@"^/Date\((\d+)\)/$", RegexOptions.Compiled)]
    private static partial Regex DateRegex();
}