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
    public async Task InvoiceRMS3Repository_GetAllIds_RowVersion()
    {
        var repo = new InvoiceRMS3Repository(new RahkaranDbContext(Company.Object, LoggerFactory), LoggerFactory);

        var ids = await repo.GetAllIdsAsync();
        Assert.NotNull(ids);

        var lastChange = await repo.GetMaxRowVersionAsync();
        if (ids.Length > 0)
        {
            Assert.NotNull(lastChange);
            Assert.True(lastChange.Length > 0);
        }
    }

    [Fact]
    public async Task InvoiceSLS3Repository_GetAllIds_RowVersion()
    {
        var repo = new InvoiceSLS3Repository(new RahkaranDbContext(Company.Object, LoggerFactory), LoggerFactory);

        var ids = await repo.GetAllIdsAsync();
        Assert.NotNull(ids);


        var lastChange = await repo.GetMaxRowVersionAsync();
        if (ids.Length > 0)
        {
            Assert.NotNull(lastChange);
            Assert.True(lastChange.Length > 0);
        }
    }

    [Fact]
    public async Task PartyRepository_GetAllIds_RowVersion()
    {
        var repo = new PartyRepository(new RahkaranDbContext(Company.Object, LoggerFactory), LoggerFactory);
        var ids = await repo.GetAllIdsAsync();
        Assert.NotNull(ids);

        var lastChange = await repo.GetMaxRowVersionAsync();
        if (ids.Length > 0)
        {
            Assert.NotNull(lastChange);
            Assert.True(lastChange.Length > 0);
        }
    }

    [Fact]
    public async Task PaymentRepository_GetAllIds_RowVersion()
    {
        var repo = new PaymentRepository(new RahkaranDbContext(Company.Object, LoggerFactory), LoggerFactory);
        var ids = await repo.GetAllIdsAsync();
        Assert.NotNull(ids);

        var lastChange = await repo.GetMaxRowVersionAsync();
        if (ids.Length > 0)
        {
            Assert.NotNull(lastChange);
            Assert.True(lastChange.Length > 0);
        }
    }

    [Fact]
    public async Task ProductRepository_GetAllIds_RowVersion()
    {
        var repo = new ProductRepository(new RahkaranDbContext(Company.Object, LoggerFactory), LoggerFactory);
        var ids = await repo.GetAllIdsAsync();
        Assert.NotNull(ids);

        var lastChange = await repo.GetMaxRowVersionAsync();
        if (ids.Length > 0)
        {
            Assert.NotNull(lastChange);
            Assert.True(lastChange.Length > 0);
        }
    }

    [Fact]
    public async Task QuotationSLS3Repository_GetAllIds_RowVersion()
    {
        var repo = new QuotationSLS3Repository(new RahkaranDbContext(Company.Object, LoggerFactory), LoggerFactory);
        var ids = await repo.GetAllIdsAsync();
        Assert.NotNull(ids);

        var lastChange = await repo.GetMaxRowVersionAsync();
        if (ids.Length > 0)
        {
            Assert.NotNull(lastChange);
            Assert.True(lastChange.Length > 0);
        }
    }

    [Fact]
    public async Task ReceiptRepository_GetAllIds_RowVersion()
    {
        var repo = new ReceiptRepository(new RahkaranDbContext(Company.Object, LoggerFactory), LoggerFactory);
        var ids = await repo.GetAllIdsAsync();
        Assert.NotNull(ids);

        var lastChange = await repo.GetMaxRowVersionAsync();
        if (ids.Length > 0)
        {
            Assert.NotNull(lastChange);
            Assert.True(lastChange.Length > 0);
        }
    }

    [Fact]
    public async Task SalesOrderRMS3Repository_GetAllIds_RowVersion()
    {
        var repo = new SalesOrderRMS3Repository(new RahkaranDbContext(Company.Object, LoggerFactory), LoggerFactory);
        var ids = await repo.GetAllIdsAsync();
        Assert.NotNull(ids);

        var lastChange = await repo.GetMaxRowVersionAsync();
        if (ids.Length > 0)
        {
            Assert.NotNull(lastChange);
            Assert.True(lastChange.Length > 0);
        }
    }
}
