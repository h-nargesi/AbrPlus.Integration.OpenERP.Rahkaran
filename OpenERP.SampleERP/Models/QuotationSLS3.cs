using SeptaKit.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AbrPlus.Integration.OpenERP.SampleERP.Models;

[Table("Quotation", Schema = "SLS3")]
public class QuotationSLS3 : BaseEntity
{
    [Key]
    public long QuotationId { get; set; }

    [Timestamp]
    public byte[] Version { get; set; }
}
