using AbrPlus.Integration.OpenERP.Repository;
using AbrPlus.Integration.OpenERP.SampleERP.Models;

namespace AbrPlus.Integration.OpenERP.SampleERP.Repository;

public interface IQuotationSLS3Repository : IBaseOpenErpApiRepository<QuotationSls3>, IGenericRepository, ITrackingSupportRepository
{
}