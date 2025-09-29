using SeptaKit.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AbrPlus.Integration.OpenERP.SampleERP.Models;

[Table("Invoice", Schema = "SLS3")]
public class InvoiceSls : BaseEntity
{
    [Key]
    public long InvoiceId { get; init; }

    [Timestamp]
    public byte[] Version { get; init; }
}
