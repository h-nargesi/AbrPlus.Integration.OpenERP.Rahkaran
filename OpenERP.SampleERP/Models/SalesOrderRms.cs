using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AbrPlus.Integration.OpenERP.SampleERP.Models;

[Table("SalesOrder", Schema = "RMS3")]
public class SalesOrderRms : BaseModel
{
    [Key]
    [Column("SalesOrderId")]
    public override long Id { get; init; }
}
