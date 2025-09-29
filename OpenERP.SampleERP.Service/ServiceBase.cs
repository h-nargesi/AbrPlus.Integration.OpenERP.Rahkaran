using AbrPlus.Integration.OpenERP.Api.DataContracts;
using AbrPlus.Integration.OpenERP.Enums;
using AbrPlus.Integration.OpenERP.SampleERP.Repository;
using AbrPlus.Integration.OpenERP.SampleERP.Shared;
using System.Linq;
using System.Threading.Tasks;

namespace AbrPlus.Integration.OpenERP.SampleERP.Service;

public class ServiceBase(IGenericRepository repository)
{
    public Task<string[]> GetAllIds()
    {
        return repository.GetAllIdsAsync();
    }

    public async Task<ChangeInfo> GetChanges(string lastTrackedVersionStamp)
    {
        var currentTrackingVersion = await repository.GetMaxRowVersionAsync();
        var currentTrackingVersionStamp = currentTrackingVersion.TimestampToString();

        if (currentTrackingVersionStamp == lastTrackedVersionStamp)
        {
            return new ChangeInfo() { LastTrackedVersion = lastTrackedVersionStamp };
        }

        var changedId = await repository.GetLastChangesAsync(lastTrackedVersionStamp.ToTimestamp());

        var changeDetails = changedId.Select(id => new ChangeDetail
        {
            Action = ActionType.Update, // TODO seperate insert/update
            Id = id.ToString(),
        });

        var changeInfo = new ChangeInfo()
        {
            LastTrackedVersion = currentTrackingVersionStamp,
            Changes = [.. changeDetails],
        };

        return changeInfo;
    }
}
