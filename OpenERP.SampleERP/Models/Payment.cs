using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AbrPlus.Integration.OpenERP.SampleERP.Models;

[Table("Payment", Schema = "RPA3")]
public class Payment : BaseModel
{
    [Key]
    [Column("PaymentId")]
    public override long Id { get; init; }
}
