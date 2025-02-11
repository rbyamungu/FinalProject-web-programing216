using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace HalfAndHalf.Models;
[Table("company")]
public class Company
{
    [Key]
    [Column("company_id")]
    public int CompanyId { get; set; }

    [Column("company_name")]
    public string CompanyName { get; set; } = null!;

    [Column("org_type")]
    public string OrgType { get; set; } = null!;
}