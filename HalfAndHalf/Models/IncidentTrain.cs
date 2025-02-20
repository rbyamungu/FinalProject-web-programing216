using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace HalfAndHalf.Models
{
    [Table("incident_train")]
    public class IncidentTrain
    {
        public IncidentTrain()
        {
            TrainCars = new HashSet<IncidentTrainCar>();
            Incidents = new HashSet<Incident>();
        }
        [Key]
        [Column("train_id")]
        public int TrainId { get; set; }
        [Column("name_number")]
        public string NameNumber { get; set; } = string.Empty;
        [Column("train_type")]
        public string TrainType { get; set; } = string.Empty;
        [Column("railroad_id")]
        // Foreign key
        public int? RailroadId { get; set; }

        // Navigation properties
        public Railroad? Railroad { get; set; }
        public virtual ICollection<IncidentTrainCar> TrainCars { get; set; }
        public virtual ICollection<Incident> Incidents { get; set; }
    }
}