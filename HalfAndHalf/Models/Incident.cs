using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HalfAndHalf.Models;

[Table("incident")]
public class Incident
{
    [Key]
    [Column("seqnos")]
    public int Seqnos { get; set; }

    [Column("company_id")]
    public int? CompanyId { get; set; }

    [Column("railroad_id")]
    public int? RailroadId { get; set; }

    [Column("incident_train_id")]
    public int? IncidentTrainId { get; set; }

    [Column("date_time_received")]
    public DateTime DateTimeReceived { get; set; }

    [Column("date_time_complete")]
    public DateTime? DateTimeComplete { get; set; }

    [Column("call_type")]
    public string CallType { get; set; } = null!;

    [Column("responsible_city")]
    public string? ResponsibleCity { get; set; }

    [Column("responsible_state")]
    public string? ResponsibleState { get; set; }

    [Column("responsible_zip")]
    public string? ResponsibleZip { get; set; }

    [Column("description_of_incident")]
    public string? DescriptionOfIncident { get; set; }

    [Column("type_of_incident")]
    public string TypeOfIncident { get; set; } = null!;

    [Column("incident_cause")]
    public string IncidentCause { get; set; } = null!;

    [Column("injury_count")]
    public int? InjuryCount { get; set; }

    [Column("hospitalization_count")]
    public int? HospitalizationCount { get; set; }

    [Column("fatality_count")]
    public int? FatalityCount { get; set; }

    [ForeignKey(nameof(CompanyId))]
    public virtual Company? Company { get; set; }

    [ForeignKey(nameof(RailroadId))]
    public virtual Railroad? Railroad { get; set; }

    [ForeignKey(nameof(IncidentTrainId))]
    public virtual IncidentTrain? IncidentTrain { get; set; }
}