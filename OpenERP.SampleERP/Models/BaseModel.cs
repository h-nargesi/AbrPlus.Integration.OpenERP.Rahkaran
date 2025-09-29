using SeptaKit.Models;
using System.ComponentModel.DataAnnotations;

namespace AbrPlus.Integration.OpenERP.SampleERP.Models;

public abstract class BaseModel : BaseEntity
{
    public abstract long Id { get; init; }

    [Timestamp]
    public byte[] Version { get; init; }
}
