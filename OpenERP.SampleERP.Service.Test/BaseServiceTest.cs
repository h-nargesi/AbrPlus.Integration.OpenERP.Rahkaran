using AbrPlus.Integration.OpenERP.SampleERP.Repository;
using AbrPlus.Integration.OpenERP.SampleERP.Service;
using AbrPlus.Integration.OpenERP.SampleERP.Service.SessionManagement;
using AbrPlus.Integration.OpenERP.SampleERP.Settings;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;
using SeptaKit.Models;
using SeptaKit.Repository;

namespace AbrPlus.Integration.OpenERP.SampleERP.Test;

public abstract class BaseServiceTest
{
    protected readonly Mock<IRahkaranCompanyService> Company = new();
    protected readonly ILoggerFactory LoggerFactory;

    protected BaseServiceTest()
    {
        Company.Setup(x => x.GetCompanyConfig())
            .Returns(new RahkaranCompanyConfig
            {
                IdleTimeout = 10,
                BaseUrl = Utility.RahkaranBaseUrl,
                Username = Utility.RahkaranUsername,
                Password = Utility.RahkaranPassword,
            });

        Company.Setup(x => x.GetConnectionStringOption())
            .Returns(new DbOption
            {
                Value = new ConnectionStringOption
                {
                    ConnectionString = "Data Source=MISVDIDB6\\SQL2022;Initial Catalog=Pakshuma;Integrated Security=False;User ID=sa;Password=abc.123456;Encrypt=False;",
                }
            });
        
        LoggerFactory = Microsoft.Extensions.Logging.LoggerFactory.Create(builder =>
        {
            builder.AddConsole().SetMinimumLevel(LogLevel.Information);
        });
    }

    protected Session GetSession(out TokenService tokenService)
    {
        var authService = new AuthenticationHttpService(Company.Object, Utility.GetLogger<AuthenticationHttpService>());
        tokenService = new TokenService(authService, Utility.GetLogger<TokenService>(), Company.Object);
        return new Session(tokenService, Company.Object);
    }

    protected TRepo? GenerateRepository<TRepo, T>() where TRepo : BaseRahkaranRepository<T> where T : BaseEntity
    {
        return Activator.CreateInstance(
            typeof(TRepo),
            new RahkaranDbContext(Company.Object, LoggerFactory),
            LoggerFactory
        ) as TRepo;
    }

    private class DbOption : IOptions<ConnectionStringOption>
    {
        public ConnectionStringOption Value { get; init; } = null!;
    }
}
