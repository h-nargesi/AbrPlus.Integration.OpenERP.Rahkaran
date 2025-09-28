using AbrPlus.Integration.OpenERP.Repository;
using AbrPlus.Integration.OpenERP.SampleERP.Models;

namespace AbrPlus.Integration.OpenERP.SampleERP.Repository;

public interface IInvoiceRMS3Repository : IBaseOpenErpApiRepository<InvoiceRms3>, IGenericRepository, ITrackingSupportRepository
{
}