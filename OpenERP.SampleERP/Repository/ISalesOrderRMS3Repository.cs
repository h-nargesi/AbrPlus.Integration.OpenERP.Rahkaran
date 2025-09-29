using AbrPlus.Integration.OpenERP.Repository;
using AbrPlus.Integration.OpenERP.SampleERP.Models;

namespace AbrPlus.Integration.OpenERP.SampleERP.Repository;

public interface ISalesOrderRMS3Repository : IBaseOpenErpApiRepository<SalesOrderRms>, IGenericRepository, ITrackingSupportRepository
{
}