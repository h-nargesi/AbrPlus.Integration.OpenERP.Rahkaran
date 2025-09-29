using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AbrPlus.Integration.OpenERP.SampleERP.Models;

[Table("Party", Schema = "GNR3")]
public class Party : BaseModel
{
    [Key]
    [Column("PartyId")]
    public override long Id { get; init; }
}
