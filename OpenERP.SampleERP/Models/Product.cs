using SeptaKit.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AbrPlus.Integration.OpenERP.SampleERP.Models;

[Table("Product", Schema = "SLS3")]
public class Product : BaseEntity
{
    [Key]
    public long ProductId { get; set; }

    [Timestamp]
    public byte[] Version { get; set; }
}
