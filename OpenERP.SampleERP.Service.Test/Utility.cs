using Microsoft.Extensions.Logging;

namespace AbrPlus.Integration.OpenERP.SampleERP.Test;

internal static class Utility
{
    public const string RahkaranBaseUrl = "http://localhost:2005";
    public const string RahkaranUsername = "admin";
    public const string RahkaranPassword = "admin";
    public const string ConnectionString = "Data Source=MISVDIDB6\\SQL2022;Initial Catalog=Pakshuma;User ID=sa;Password=abc.123456;Encrypt=False;";

    public static ILogger<T> GetLogger<T>()
    {
        var logger = new Mock<ILogger<T>>();

        logger.Setup(x => x.Log(
            LogLevel.Information,
            It.IsAny<EventId>(),
            It.Is<It.IsAnyType>((v, t) => true),
            null,
            It.IsAny<Func<It.IsAnyType, Exception?, string>>()
        ));

        logger.Setup(x => x.Log(
            LogLevel.Error,
            It.IsAny<EventId>(),
            It.Is<It.IsAnyType>((v, t) => true),
            It.IsAny<Exception>(),
            It.IsAny<Func<It.IsAnyType, Exception?, string>>()
        ));

        return logger.Object;
    }
}
