using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AbrPlus.Integration.OpenERP.SampleERP.Models;

[Table("Invoice", Schema = "SLS3")]
public class InvoiceSls : BaseModel
{
    [Key]
    [Column("InvoiceId")]
    public override long Id { get; init; }
}
