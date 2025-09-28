using SeptaKit.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AbrPlus.Integration.OpenERP.SampleERP.Models;

[Table("Party", Schema = "GNR3")]
public class Party : BaseEntity
{
    [Key]
    public long PartyId { get; init; }

    [Timestamp]
    public byte[] Version { get; init; }
}
