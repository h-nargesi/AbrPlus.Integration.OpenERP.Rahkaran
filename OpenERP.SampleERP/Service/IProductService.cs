using AbrPlus.Integration.OpenERP.Api.DataContracts;
using System.Threading.Tasks;

namespace AbrPlus.Integration.OpenERP.SampleERP.Service
{
    public interface IProductService
    {
        Task<string[]> GetAllIds();
        Task<ProductBundle> GetBundle(string key);
        ChangeInfo GetChanges(string lastTrackedVersionStamp);
        Task<bool> Save(ProductBundle item);
        void SetTrackingStatus(bool enabled);
        bool Validate(ProductBundle item);
    }
}
