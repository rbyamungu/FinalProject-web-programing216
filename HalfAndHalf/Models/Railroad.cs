using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HalfAndHalf.Models;

[Table("railroad")]
public class Railroad
{
    [Key]
    [Column("railroad_id")]
    public int RailroadId { get; set; }

    [Column("railroad_name")]
    public string RailroadName { get; set; } = null!;
    public int Id { get; set; }
    public ICollection<Incident> Incidents { get; set; } = new List<Incident>();
}