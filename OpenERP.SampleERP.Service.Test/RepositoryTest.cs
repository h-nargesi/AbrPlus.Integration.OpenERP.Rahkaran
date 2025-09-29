using AbrPlus.Integration.OpenERP.SampleERP.Repository;

namespace AbrPlus.Integration.OpenERP.SampleERP.Test;

public class RepositoryTest : BaseServiceTest
{
    [Fact]
    public async Task InvoiceRMS3Repository_GetAllIds_RowVersion()
    {
        var repo = new InvoiceRmsRepository(new RahkaranDbContext(Company.Object, LoggerFactory), LoggerFactory);

        var ids = await repo.GetAllIdsAsync();
        Assert.NotNull(ids);

        var lastChange = await repo.GetMaxRowVersionAsync();
        if (ids.Length > 0)
        {
            Assert.NotNull(lastChange);
            Assert.True(lastChange.Length > 0);
        }

        var lastChangeIds = await repo.GetLastChangesAsync(new byte[8]);
        Assert.NotNull(lastChangeIds);
        Assert.Equal(ids.Length, lastChangeIds.Length);
    }

    [Fact]
    public async Task InvoiceSLS3Repository_GetAllIds_RowVersion()
    {
        var repo = new InvoiceSls3Repository(new RahkaranDbContext(Company.Object, LoggerFactory), LoggerFactory);

        var ids = await repo.GetAllIdsAsync();
        Assert.NotNull(ids);


        var lastChange = await repo.GetMaxRowVersionAsync();
        if (ids.Length > 0)
        {
            Assert.NotNull(lastChange);
            Assert.True(lastChange.Length > 0);
        }

        var lastChangeIds = await repo.GetLastChangesAsync(new byte[8]);
        Assert.NotNull(lastChangeIds);
        Assert.Equal(ids.Length, lastChangeIds.Length);
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

        var lastChangeIds = await repo.GetLastChangesAsync(new byte[8]);
        Assert.NotNull(lastChangeIds);
        Assert.Equal(ids.Length, lastChangeIds.Length);
    }

    [Fact]
    public async Task PartyRepository_GetLastChanges()
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

        var lastChangeIds = await repo.GetLastChangesAsync(new byte[8]);
        Assert.NotNull(lastChangeIds);
        Assert.Equal(ids.Length, lastChangeIds.Length);
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

        var lastChangeIds = await repo.GetLastChangesAsync(new byte[8]);
        Assert.NotNull(lastChangeIds);
        Assert.Equal(ids.Length, lastChangeIds.Length);
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

        var lastChangeIds = await repo.GetLastChangesAsync(new byte[8]);
        Assert.NotNull(lastChangeIds);
        Assert.Equal(ids.Length, lastChangeIds.Length);
    }

    [Fact]
    public async Task QuotationSLS3Repository_GetAllIds_RowVersion()
    {
        var repo = new QuotationSlsRepository(new RahkaranDbContext(Company.Object, LoggerFactory), LoggerFactory);
        var ids = await repo.GetAllIdsAsync();
        Assert.NotNull(ids);

        var lastChange = await repo.GetMaxRowVersionAsync();
        if (ids.Length > 0)
        {
            Assert.NotNull(lastChange);
            Assert.True(lastChange.Length > 0);
        }

        var lastChangeIds = await repo.GetLastChangesAsync(new byte[8]);
        Assert.NotNull(lastChangeIds);
        Assert.Equal(ids.Length, lastChangeIds.Length);
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

        var lastChangeIds = await repo.GetLastChangesAsync(new byte[8]);
        Assert.NotNull(lastChangeIds);
        Assert.Equal(ids.Length, lastChangeIds.Length);
    }

    [Fact]
    public async Task SalesOrderRMS3Repository_GetAllIds_RowVersion()
    {
        var repo = new SalesOrderRmsRepository(new RahkaranDbContext(Company.Object, LoggerFactory), LoggerFactory);
        var ids = await repo.GetAllIdsAsync();
        Assert.NotNull(ids);

        var lastChange = await repo.GetMaxRowVersionAsync();
        if (ids.Length > 0)
        {
            Assert.NotNull(lastChange);
            Assert.True(lastChange.Length > 0);
        }

        var lastChangeIds = await repo.GetLastChangesAsync(new byte[8]);
        Assert.NotNull(lastChangeIds);
        Assert.Equal(ids.Length, lastChangeIds.Length);
    }
}
