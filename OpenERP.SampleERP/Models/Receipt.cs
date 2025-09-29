using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AbrPlus.Integration.OpenERP.SampleERP.Models;

[Table("Receipt", Schema = "RPA3")]
public class Receipt : BaseModel
{
    [Key]
    [Column("ReceiptId")]
    public override long Id { get; init; }
}
