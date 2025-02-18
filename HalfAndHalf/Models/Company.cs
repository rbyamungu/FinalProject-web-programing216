using System;
namespace HalfAndHalf.Models
{
    public class Company
    {
        public Company()
        {
            Incidents = new HashSet<Incident>();
        }

        public int CompanyId { get; set; }
        public string CompanyName { get; set; } = string.Empty;
        public string OrgType { get; set; } = string.Empty;

        // Navigation property
        public virtual ICollection<Incident> Incidents { get; set; }
    }
}