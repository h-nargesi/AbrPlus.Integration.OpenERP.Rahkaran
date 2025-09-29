using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AbrPlus.Integration.OpenERP.SampleERP.Models;

[Table("Product", Schema = "SLS3")]
public class Product : BaseModel
{
    [Key]
    [Column("ProductId")]
    public override long Id { get; init; }
}
