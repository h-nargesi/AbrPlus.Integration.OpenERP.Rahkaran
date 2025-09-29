using System.Threading.Tasks;

namespace AbrPlus.Integration.OpenERP.SampleERP.Repository;

public interface IGenericRepository
{
    Task<string[]> GetAllIdsAsync();

    Task<byte[]> GetMaxRowVersionAsync();

    Task<long[]> GetLastChangesAsync(byte[] timestamp);
}
