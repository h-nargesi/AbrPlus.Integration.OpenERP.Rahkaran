using AbrPlus.Integration.OpenERP.Repository;
using AbrPlus.Integration.OpenERP.SampleERP.Models;
using SeptaKit.Repository;

namespace AbrPlus.Integration.OpenERP.SampleERP.Repository
{
    public interface ICustomerRepository : IBaseOpenErpApiRepository<Customer>, ITrackingSupportRepository
    {
    }
}