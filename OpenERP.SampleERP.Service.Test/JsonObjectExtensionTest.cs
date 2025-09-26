using AbrPlus.Integration.OpenERP.SampleERP.Shared;
using System.Text.RegularExpressions;

namespace AbrPlus.Integration.OpenERP.SampleERP.Test;

public class JsonObjectExtensionTest
{
    [Fact]
    public void MicrosoftDateTimeConverter_SerializeJson_Simple()
    {
        var now = DateTime.Now;
        var today = now.Date;

        var data = new
        {
            Name = "name",
        };

        var result = data.SerializeJson();

        Assert.NotNull(result);

        Assert.Contains("\"Name\": \"name\"", result);
    }

    [Fact]
    public void MicrosoftDateTimeConverter_SerializeJson_Date()
    {
        var today = DateTime.Now.Date;

        var data = new
        {
            Today = today,
        };

        var result = data.SerializeJson();

        Assert.NotNull(result);

        Assert.Contains(@$"""/Date({new DateTimeOffset(today).ToUnixTimeMilliseconds()})/""", result);
    }

    [Fact]
    public void MicrosoftDateTimeConverter_SerializeJson_DateTime()
    {
        var now = DateTime.Now;

        var data = new
        {
            Now = now,
        };

        var result = data.SerializeJson();

        Assert.NotNull(result);

        Assert.Contains(@$"""/Date({new DateTimeOffset(now).ToUnixTimeMilliseconds()})/""", result);
    }

}
