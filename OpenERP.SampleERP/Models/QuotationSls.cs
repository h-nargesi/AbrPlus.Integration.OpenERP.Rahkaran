using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AbrPlus.Integration.OpenERP.SampleERP.Models;

[Table("Quotation", Schema = "SLS3")]
public class QuotationSls : BaseModel
{
    [Key]
    [Column("QuotationId")]
    public override long Id { get; init; }
}
