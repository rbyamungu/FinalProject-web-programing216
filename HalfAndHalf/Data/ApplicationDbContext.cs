using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using HalfAndHalf.Models;

namespace HalfAndHalf.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Incident> Incidents { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Railroad> Railroads { get; set; }
        public DbSet<IncidentTrain> IncidentTrains { get; set; }
        public DbSet<IncidentTrainCar> IncidentTrainCars { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Company>(entity =>
            {
                entity.ToTable("company", schema: "public");
                entity.HasKey(e => e.CompanyId);
                entity.Property(e => e.CompanyId).HasColumnName("company_id");
                entity.Property(e => e.CompanyName).HasColumnName("company_name");
                entity.Property(e => e.OrgType).HasColumnName("org_type");
            });

            modelBuilder.Entity<Railroad>(entity =>
            {
                entity.ToTable("railroad", schema: "public");
                entity.HasKey(e => e.RailroadId);
                entity.Property(e => e.RailroadId).HasColumnName("railroad_id");
                entity.Property(e => e.RailroadName).HasColumnName("railroad_name");
            });

            modelBuilder.Entity<Incident>(entity =>
            {
                entity.ToTable("incident", schema: "public");
                entity.HasKey(e => e.Seqnos);

                // Configure columns
                entity.Property(e => e.Seqnos).HasColumnName("seqnos");
                entity.Property(e => e.DateTimeReceived).HasColumnName("date_time_received");
                entity.Property(e => e.DateTimeComplete).HasColumnName("date_time_complete");
                entity.Property(e => e.CallType).HasColumnName("call_type");
                entity.Property(e => e.ResponsibleCity).HasColumnName("responsible_city");
                entity.Property(e => e.ResponsibleState).HasColumnName("responsible_state");
                entity.Property(e => e.ResponsibleZip).HasColumnName("responsible_zip");
                entity.Property(e => e.DescriptionOfIncident).HasColumnName("description_of_incident");
                entity.Property(e => e.TypeOfIncident).HasColumnName("type_of_incident");
                entity.Property(e => e.IncidentCause).HasColumnName("incident_cause");
                entity.Property(e => e.InjuryCount).HasColumnName("injury_count");
                entity.Property(e => e.HospitalizationCount).HasColumnName("hospitalization_count");
                entity.Property(e => e.FatalityCount).HasColumnName("fatality_count");
                entity.Property(e => e.CompanyId).HasColumnName("company_id");
                entity.Property(e => e.RailroadId).HasColumnName("railroad_id");
                entity.Property(e => e.IncidentTrainId).HasColumnName("incident_train_id");

                // Configure relationships
                entity.HasOne(d => d.Company)
                    .WithMany(p => p.Incidents)
                    .HasForeignKey(d => d.CompanyId);

                entity.HasOne(d => d.Railroad)
                    .WithMany(p => p.Incidents)
                    .HasForeignKey(d => d.RailroadId);
            });

            modelBuilder.Entity<IncidentTrain>(entity =>
            {
                entity.ToTable("incident_train", schema: "public");
                entity.HasKey(e => e.TrainId);
                entity.Property(e => e.TrainId).HasColumnName("train_id");
                entity.Property(e => e.NameNumber).HasColumnName("name_number");
                entity.Property(e => e.TrainType).HasColumnName("train_type");
                entity.Property(e => e.RailroadId).HasColumnName("railroad_id");
            });

            modelBuilder.Entity<IncidentTrainCar>(entity =>
            {
                entity.ToTable("incident_train_car", schema: "public");
                entity.HasKey(e => e.TrainCarId);
                entity.Property(e => e.TrainCarId).HasColumnName("train_car_id");
                entity.Property(e => e.CarNumber).HasColumnName("car_number");
                entity.Property(e => e.CarContent).HasColumnName("car_content");
                entity.Property(e => e.PositionInTrain).HasColumnName("position_in_train");
                entity.Property(e => e.CarType).HasColumnName("car_type");
                entity.Property(e => e.IncidentTrainId).HasColumnName("incident_train_id");
            });
        }
    }
}