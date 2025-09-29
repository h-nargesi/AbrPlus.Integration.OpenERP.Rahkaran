using AbrPlus.Integration.OpenERP.SampleERP.Shared;

namespace AbrPlus.Integration.OpenERP.SampleERP.Test;

public class TimestampUtilityTest
{
    [Fact]
    public void TimestampToLong_Test()
    {
        var data = new byte[] { 0x7F, 0xFF, 0x00, 0x0F, 0xF0, 0xFF, 0x00, 0x0F };
        ulong shuoildBe = 0x7FFF000FF0FF000F;

        var result = data.TimestampToLong();

        Assert.Equal(shuoildBe, result);

        var bytes = result.ToTimestamp();

        Assert.Equal(data.Length, bytes.Length);
        for (int i = 0; i < bytes.Length; i++)
            Assert.Equal(data[i], bytes[i]);
    }

    [Fact]
    public void TimestampToLong_Negative_Test()
    {
        var data = new byte[] { 0xF0, 0xFF, 0x00, 0x0F, 0xF0, 0xFF, 0x00, 0x0F };
        var shuoildBe = 0xF0FF000FF0FF000F;

        var result = data.TimestampToLong();

        Assert.Equal(shuoildBe, result);

        var bytes = result.ToTimestamp();

        Assert.Equal(data.Length, bytes.Length);
        for (int i = 0; i < bytes.Length; i++)
            Assert.Equal(data[i], bytes[i]);
    }

    [Fact]
    public void TimestampToString_Negative_Test()
    {
        var data = new byte[] { 0xF0, 0xFF, 0x00, 0x0F, 0xF0, 0xFF, 0x00, 0x0F };
        var shuoildBe = 0xF0FF000FF0FF000F;

        var result = data.TimestampToString();

        Assert.Equal(shuoildBe.ToString(), result);

        var bytes = result.ToTimestamp();

        Assert.Equal(data.Length, bytes.Length);
        for (int i = 0; i < bytes.Length; i++)
            Assert.Equal(data[i], bytes[i]);
    }
}
