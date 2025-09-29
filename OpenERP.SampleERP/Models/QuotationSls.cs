using SeptaKit.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AbrPlus.Integration.OpenERP.SampleERP.Models;

[Table("Quotation", Schema = "SLS3")]
public class QuotationSls : BaseEntity
{
    [Key]
    public long QuotationId { get; init; }

    [Timestamp]
    public byte[] Version { get; init; }
}
