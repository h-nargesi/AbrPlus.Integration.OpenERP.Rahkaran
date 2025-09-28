using AbrPlus.Integration.OpenERP.Repository;
using AbrPlus.Integration.OpenERP.SampleERP.Models;

namespace AbrPlus.Integration.OpenERP.SampleERP.Repository;

public interface IInvoiceSLS3Repository : IBaseOpenErpApiRepository<InvoiceSls3>, IGenericRepository, ITrackingSupportRepository
{
}