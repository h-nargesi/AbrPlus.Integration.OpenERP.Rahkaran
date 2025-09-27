using AbrPlus.Integration.OpenERP.SampleERP.Service;
using AbrPlus.Integration.OpenERP.SampleERP.Service.SessionManagement;
using AbrPlus.Integration.OpenERP.SampleERP.Settings;

namespace AbrPlus.Integration.OpenERP.SampleERP.Test;

public abstract class BaseServiceTest
{
    protected readonly Mock<ISampleErpCompanyService> Company = new();

    protected BaseServiceTest()
    {
        Company.Setup(x => x.GetCompanyConfig())
            .Returns(new RahkaranErpCompanyConfig
            {
                IdleTimeout = 10,
                BaseUrl = Utility.RahkaranBaseUrl,
                Username = Utility.RahkaranUsername,
                Password = Utility.RahkaranPassword,
            });
    }

    protected Session GetSession(out TokenService tokenService)
    {
        var authService = new AuthenticationHttpService(Company.Object, Utility.GetLogger<AuthenticationHttpService>());
        tokenService = new TokenService(authService, Utility.GetLogger<TokenService>(), Company.Object);
        return new Session(tokenService);
    }
}
