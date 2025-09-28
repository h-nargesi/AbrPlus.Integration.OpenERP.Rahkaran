using SeptaKit.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AbrPlus.Integration.OpenERP.SampleERP.Models;

[Table("SalesOrder", Schema = "RMS3")]
public class SalesOrderRms3 : BaseEntity
{
    [Key]
    public long SalesOrderId { get; init; }

    [Timestamp]
    public byte[] Version { get; init; }
}
