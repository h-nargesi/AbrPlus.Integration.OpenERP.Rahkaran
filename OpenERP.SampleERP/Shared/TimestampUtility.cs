using System;
using System.Linq;

namespace AbrPlus.Integration.OpenERP.SampleERP.Shared;

public static class TimestampUtility
{
    public static string TimestampToString(this byte[] timestamp)
    {
        return timestamp.TimestampToLong().ToString();
    }

    public static ulong TimestampToLong(this byte[] timestamp)
    {
        return BitConverter.ToUInt64([.. timestamp.Reverse()], 0);
    }

    public static byte[]  ToTimestamp(this string timestamp)
    {
        return ulong.Parse(timestamp).ToTimestamp();
    }

    public static byte[] ToTimestamp(this ulong timestamp)
    {
        var value = new byte[8];

        for (var i = 7; i >= 0; i--)
        {
            value[i] = (byte)(timestamp & 0xFF);
            timestamp >>= 8;
        }

        return value;
    }
}
