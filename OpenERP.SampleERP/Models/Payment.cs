using SeptaKit.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AbrPlus.Integration.OpenERP.SampleERP.Models;

[Table("Payment", Schema = "RPA3")]
public class Payment : BaseEntity
{
    [Key]
    public long PaymentId { get; init; }

    [Timestamp]
    public byte[] Version { get; init; }
}
