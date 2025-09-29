using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AbrPlus.Integration.OpenERP.SampleERP.Models;

[Table("Invoice", Schema = "RMS3")]
public class InvoiceRms : BaseModel
{
    [Key]
    [Column("InvoiceId")]
    public override long Id { get; init; }
}
