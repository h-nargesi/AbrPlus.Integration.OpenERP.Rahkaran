using AbrPlus.Integration.OpenERP.SampleERP.Shared;

namespace AbrPlus.Integration.OpenERP.SampleERP.Test;

public class TimestampUtilityTest
{
    [Fact]
    public void TimestampToLong_Test()
    {
        var data = new byte[] { 0x7F, 0xFF, 0x00, 0x0F, 0xF0, 0xFF, 0x00, 0x0F };
        long shuoildBe = 0x7FFF000FF0FF000F;

        var result = data.TimestampToLong();

        Assert.Equal(shuoildBe, result);
    }

    [Fact]
    public void TimestampToLong_Negative_Test()
    {
        var data = new byte[] { 0xF0, 0xFF, 0x00, 0x0F, 0xF0, 0xFF, 0x00, 0x0F };
        var shuoildBe = unchecked((long)0xF0FF000FF0FF000F);

        var result = data.TimestampToLong();

        Assert.Equal(shuoildBe, result);
    }

    [Fact]
    public void TimestampToString_Negative_Test()
    {
        var data = new byte[] { 0xF0, 0xFF, 0x00, 0x0F, 0xF0, 0xFF, 0x00, 0x0F };
        var shuoildBe = unchecked((long)0xF0FF000FF0FF000F);

        var result = data.TimestampToString();

        Assert.Equal(shuoildBe.ToString(), result);
    }
}
