using SeptaKit.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AbrPlus.Integration.OpenERP.SampleERP.Models;

[Table("Invoice", Schema = "SLS3")]
public class InvoiceSLS3 : BaseEntity
{
    [Key]
    public long InvoiceId { get; set; }

    [Timestamp]
    public byte[] Version { get; set; }
}
