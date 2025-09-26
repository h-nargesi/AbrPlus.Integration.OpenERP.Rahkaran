using AbrPlus.Integration.OpenERP.SampleERP.Service;
using AbrPlus.Integration.OpenERP.SampleERP.Service.Customer;
using AbrPlus.Integration.OpenERP.SampleERP.Service.SessionManagement;
using AbrPlus.Integration.OpenERP.SampleERP.Settings;

namespace AbrPlus.Integration.OpenERP.SampleERP.Test;

public class CustomerServiceTest
{
    private readonly Mock<ISampleErpCompanyService> Company = new();

    public CustomerServiceTest()
    {
        Company.Setup(x => x.GetCompanyConfig())
            .Returns(new RahkaranErpCompanyConfig
            {
                IdleTimeout = 10,
                BaseUrl = "http://localhost:2005",
                Username = "RFID",
                Password = "RFID",
            });
    }

    [Fact]
    public void MakeTokenGetReady_Test()
    {
        var authService = new AuthenticationHttpService(Company.Object, Utility.GetLogger<AuthenticationHttpService>());
        var tokenService = new TokenService(authService, Utility.GetLogger<TokenService>(), Company.Object);
        using var session = new Session(tokenService);
        var service = new CustomerService(session, null, Company.Object, Utility.GetLogger<CustomerService>());

        var result = service.GetBundle("بنگاه پیش فرض");

        Assert.NotNull(result);
    }

}
