using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductManagement.Models
{
    public class IncidentTrainCar
    {
        [Key]
        public int TrainCarId { get; set; }
        public string CarNumber { get; set; } = string.Empty;
        public string CarContent { get; set; } = string.Empty;
        public int? PositionInTrain { get; set; }
        public string CarType { get; set; } = string.Empty;

        // Foreign key
        public int? IncidentTrainId { get; set; }

        // Navigation property
        public IncidentTrain? IncidentTrain { get; set; }
    }
}