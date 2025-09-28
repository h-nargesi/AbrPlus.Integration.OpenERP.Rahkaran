using AbrPlus.Integration.OpenERP.Api.Contracts;
using AbrPlus.Integration.OpenERP.Api.DataContracts;
using AbrPlus.Integration.OpenERP.SampleERP.Service;
using SeptaKit.DI;

namespace AbrPlus.Integration.OpenERP.SampleERP.Api.Api;

public class ProductApi(IProductService productService) : IProductApi, IApi
{
    public string[] GetAllIds(int? companyId)
    {
        return productService.GetAllIds().Result;
    }
    public ProductBundle GetBundle(string key, int? companyId)
    {
        return productService.GetBundle(key).Result;
    }
    public ChangeInfo GetChanges(string lastTrackedVersion, int? companyId)
    {
        return productService.GetChanges(lastTrackedVersion);
    }
    public bool Save(ProductBundle item, int? companyId)
    {
        return productService.Save(item).Result;
    }
    public void SetTrackingStatus(bool enabled, int? companyId)
    {
        productService.SetTrackingStatus(enabled);
    }
    public bool Validate(ProductBundle item)
    {
        return productService.Validate(item);
    }
}
