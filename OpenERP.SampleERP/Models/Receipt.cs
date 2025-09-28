using SeptaKit.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AbrPlus.Integration.OpenERP.SampleERP.Models;

[Table("Receipt", Schema = "RPA3")]
public class Receipt : BaseEntity
{
    [Key]
    public long ReceiptId { get; set; }

    [Timestamp]
    public byte[] Version { get; set; }
}
