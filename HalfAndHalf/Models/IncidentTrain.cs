using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HalfAndHalf.Models;

[Table("incident_train")]
public class IncidentTrain
{
    [Key]
    [Column("train_id")]
    public int TrainId { get; set; }

    [Column("name_number")]
    public string NameNumber { get; set; } = null!;

    [Column("train_type")]
    public string TrainType { get; set; } = null!;

    [Column("railroad_id")]
    public int? RailroadId { get; set; }

    [ForeignKey(nameof(RailroadId))]
    public virtual Railroad? Railroad { get; set; }
    public int Id { get; set; }
    public ICollection<IncidentTrainCar> TrainCars { get; set; } = new List<IncidentTrainCar>();

}