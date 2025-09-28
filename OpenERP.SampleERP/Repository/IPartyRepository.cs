using AbrPlus.Integration.OpenERP.Repository;
using AbrPlus.Integration.OpenERP.SampleERP.Models;

namespace AbrPlus.Integration.OpenERP.SampleERP.Repository;

public interface IPartyRepository : IBaseOpenErpApiRepository<Party>, IGenericRepository, ITrackingSupportRepository
{
}