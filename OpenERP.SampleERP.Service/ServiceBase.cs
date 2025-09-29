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
        var currentTrackingStamp = await repository.GetMaxRowVersionAsync();

        if (currentTrackingStamp == lastTrackedVersionStamp)
        {
            return new ChangeInfo() { LastTrackedVersion = lastTrackedVersionStamp };
        }

        var lastTrackedVersionStampParts = lastTrackedVersionStamp.Split('|');

        var changedIds = repository.GetLastChangesAsync(lastTrackedVersionStampParts[1].ToTimestamp());

        var deletedIds = repository.GetLastDeletedIdsAsync(long.Parse(lastTrackedVersionStampParts[0]));

        var changeDetails = (await changedIds).Select(id => new ChangeDetail
        {
            Action = ActionType.Update, // TODO seperate insert/update
            Id = id.ToString(),
        });

        changeDetails = changeDetails.Union((await deletedIds).Select(id => new ChangeDetail
        {
            Action = ActionType.Delete,
            Id = id,
        }));

        var changeInfo = new ChangeInfo()
        {
            LastTrackedVersion = currentTrackingStamp,
            Changes = [.. changeDetails],
        };

        return changeInfo;
    }
}
