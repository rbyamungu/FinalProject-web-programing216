using Microsoft.EntityFrameworkCore;
using HalfAndHalf.Models;

namespace HalfAndHalf.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Company> Companies { get; set; }
    public DbSet<Railroad> Railroads { get; set; }
    public DbSet<IncidentTrain> IncidentTrains { get; set; }
    public DbSet<Incident> Incidents { get; set; }
    public DbSet<IncidentTrainCar> IncidentTrainCars { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Incident configuration
        modelBuilder.Entity<Incident>(entity =>
        {
            entity.ToTable("incident");
            entity.HasKey(e => e.Seqnos);
            
            entity.Property(e => e.CompanyId)
                .HasColumnName("company_id");
                
            entity.Property(e => e.RailroadId)
                .HasColumnName("railroad_id");
                
            entity.Property(e => e.IncidentTrainId)
                .HasColumnName("incident_train_id");

            entity.HasOne(d => d.Company)
                .WithMany()
                .HasForeignKey(d => d.CompanyId)
                .HasConstraintName("incident_company_id_fkey");

            entity.HasOne(d => d.Railroad)
                .WithMany()
                .HasForeignKey(d => d.RailroadId)
                .HasConstraintName("incident_railroad_id_fkey");

            entity.HasOne(d => d.IncidentTrain)
                .WithMany()
                .HasForeignKey(d => d.IncidentTrainId)
                .HasConstraintName("incident_incident_train_id_fkey");
        });

        // Company configuration
        modelBuilder.Entity<Company>(entity =>
        {
            entity.ToTable("company");
            entity.HasKey(e => e.CompanyId);
            
            entity.Property(e => e.CompanyId)
                .HasColumnName("company_id");
        });

        // Railroad configuration
        modelBuilder.Entity<Railroad>(entity =>
        {
            entity.ToTable("railroad");
            entity.HasKey(e => e.RailroadId);
            
            entity.Property(e => e.RailroadId)
                .HasColumnName("railroad_id");
        });

        // IncidentTrain configuration
        modelBuilder.Entity<IncidentTrain>(entity =>
        {
            entity.ToTable("incident_train");
            entity.HasKey(e => e.TrainId);

            entity.Property(e => e.TrainId)
                .HasColumnName("train_id");
            
            entity.Property(e => e.RailroadId)
                .HasColumnName("railroad_id");

            entity.HasOne(d => d.Railroad)
                .WithMany()
                .HasForeignKey(d => d.RailroadId)
                .HasConstraintName("incident_train_railroad_id_fkey");
        });
    }
}