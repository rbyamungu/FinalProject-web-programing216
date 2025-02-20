using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HalfAndHalf.Models;
[Table("incident_train_car")]
public class IncidentTrainCar
{
    [Key]
    public int TrainCarId { get; set; }
    public string CarNumber { get; set; } = null!;
    public string CarContent { get; set; } = null!;
    public int? PositionInTrain { get; set; }
    public string CarType { get; set; } = null!;
    public int? IncidentTrainId { get; set; }
    public IncidentTrain? IncidentTrain { get; set; }
}
