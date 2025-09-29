using System.Threading.Tasks;

namespace AbrPlus.Integration.OpenERP.SampleERP.Repository;

public interface IGenericRepository
{
    Task<string[]> GetAllIdsAsync();

    Task<string> GetMaxRowVersionAsync();

    Task<long[]> GetLastChangesAsync(byte[] timestamp);

    Task<string[]> GetLastDeletedIdsAsync(long lastSeen);
}
