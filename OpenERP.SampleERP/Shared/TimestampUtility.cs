namespace AbrPlus.Integration.OpenERP.SampleERP.Shared;

public static class TimestampUtility
{
    public static string TimestampToString(this byte[] timestamp)
    {
        return timestamp.TimestampToLong().ToString();
    }

    public static long TimestampToLong(this byte[] timestamp)
    {
        long value = 0;

        foreach (var part in timestamp)
        {
            value <<= 8;
            value |= part;
        }

        return value;
    }
}
