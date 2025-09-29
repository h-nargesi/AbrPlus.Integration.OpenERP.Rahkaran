using SeptaKit.Models;

namespace AbrPlus.Integration.OpenERP.SampleERP.Models;

public class DeleteChange : BaseEntity
{
    public long Id { get; set; }

    public string RecordIDs { get; set; }
}
