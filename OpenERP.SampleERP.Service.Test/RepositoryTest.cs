using AbrPlus.Integration.OpenERP.SampleERP.Repository;
using Microsoft.Extensions.Logging;

namespace AbrPlus.Integration.OpenERP.SampleERP.Test;

public class RepositoryTest : BaseServiceTest
{
    private readonly ILoggerFactory LoggerFactory;

    public RepositoryTest()
    {
        LoggerFactory = Microsoft.Extensions.Logging.LoggerFactory.Create(builder =>
        {
            builder.AddConsole().SetMinimumLevel(LogLevel.Information);
        });
    }

    [Fact]
    public async Task GnrPartyRepository_GetAllIds()
    {
        var db = new RahkaranDbContext(Company.Object, LoggerFactory);
        var repo = new PartyRepository(db, LoggerFactory);
        var ids = await repo.GetAllIdsAsync();
        Assert.NotNull(ids);
    }

    [Fact]
    public async Task GnrPartyRepository_GetMaxRowVersionAsync()
    {
        var repo = new PartyRepository(new RahkaranDbContext(Company.Object, LoggerFactory), LoggerFactory);
        var lastChange = await repo.GetMaxRowVersionAsync();
        Assert.NotNull(lastChange);
        Assert.True(lastChange.Length > 0);
    }
}
