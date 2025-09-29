using AbrPlus.Integration.OpenERP.Repository;
using AbrPlus.Integration.OpenERP.SampleERP.Models;

namespace AbrPlus.Integration.OpenERP.SampleERP.Repository;

public interface IInvoiceRmsRepository : IBaseOpenErpApiRepository<InvoiceRms3>, IGenericRepository, ITrackingSupportRepository
{
}