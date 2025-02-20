namespace HalfAndHalf.Models
{
    public class Railroad
    {
        public Railroad()
        {
            Incidents = new HashSet<Incident>();
            IncidentTrains = new HashSet<IncidentTrain>();
        }

        public int RailroadId { get; set; }
        public string RailroadName { get; set; } = string.Empty;

        // Navigation properties
        public virtual ICollection<Incident> Incidents { get; set; }
        public virtual ICollection<IncidentTrain> IncidentTrains { get; set; }
    }
}