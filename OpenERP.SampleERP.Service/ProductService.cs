using AbrPlus.Integration.OpenERP.Api.DataContracts;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace AbrPlus.Integration.OpenERP.SampleERP.Service;

public class ProductService(ILogger<ProductService> logger) : IProductService
{
    public Task<ProductBundle> GetBundle(string key)
    {
        throw new NotImplementedException();
    }
    public ChangeInfo GetChanges(string lastTrackedVersionStamp)
    {
        throw new NotImplementedException();
    }
    public Task<bool> Save(ProductBundle item)
    {
        throw new NotImplementedException();
    }
    public void SetTrackingStatus(bool enabled)
    {

    }
    public bool Validate(ProductBundle item)
    {
        throw new NotImplementedException();
    }
    public Task<string[]> GetAllIds()
    {
        throw new NotImplementedException();
    }
}
