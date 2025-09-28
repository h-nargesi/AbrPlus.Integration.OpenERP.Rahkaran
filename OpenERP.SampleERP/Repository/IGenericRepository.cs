using System.Threading.Tasks;

namespace AbrPlus.Integration.OpenERP.SampleERP.Repository;

public interface IGenericRepository
{
    Task<long[]> GetAllIdsAsync();

    Task<byte[]> GetMaxRowVersionAsync();
}
