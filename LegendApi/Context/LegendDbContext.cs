using System;
using System.Collections.Generic;
using LegendApi.Models;
using Microsoft.EntityFrameworkCore;

namespace LegendApi.Context;

public partial class LegendDbContext : DbContext
{
    public LegendDbContext()
    {
    }

    public LegendDbContext(DbContextOptions<LegendDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Animal> Animals { get; set; }

    public virtual DbSet<Historyvisitlocation> Historyvisitlocations { get; set; }

    public virtual DbSet<Location> Locations { get; set; }

    public virtual DbSet<Typeanimal> Typeanimals { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost; Username=postgres; Database=LegendDB; Password=1330");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Animal>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("animal_pk");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Animaltype).HasColumnName("animaltype");
            entity.Property(e => e.Chipperid).HasColumnName("chipperid");
            entity.Property(e => e.Chippingdatetime)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("chippingdatetime");
            entity.Property(e => e.Chippinglocationid).HasColumnName("chippinglocationid");
            entity.Property(e => e.Deathdatetime)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("deathdatetime");
            entity.Property(e => e.Gender)
                .HasMaxLength(80)
                .HasColumnName("gender");
            entity.Property(e => e.Height).HasColumnName("height");
            entity.Property(e => e.Lenght).HasColumnName("lenght");
            entity.Property(e => e.Lifestatus)
                .HasMaxLength(80)
                .HasColumnName("lifestatus");
            entity.Property(e => e.Visitedlocations).HasColumnName("visitedlocations");
            entity.Property(e => e.Weight).HasColumnName("weight");

            entity.HasOne(d => d.AnimaltypeNavigation).WithMany(p => p.Animals)
                .HasForeignKey(d => d.Animaltype)
                .HasConstraintName("animals_fk");

            entity.HasOne(d => d.Chipper).WithMany(p => p.Animals)
                .HasForeignKey(d => d.Chipperid)
                .HasConstraintName("animals_fk_2");

            entity.HasOne(d => d.Chippinglocation).WithMany(p => p.Animals)
                .HasForeignKey(d => d.Chippinglocationid)
                .HasConstraintName("animals_fk_1");
        });

        modelBuilder.Entity<Historyvisitlocation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("historyvisitlocation_pk");

            entity.ToTable("historyvisitlocation");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Datetimeofvisitlocationpoint)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("datetimeofvisitlocationpoint");
            entity.Property(e => e.Idanimal).HasColumnName("idanimal");
            entity.Property(e => e.Locationpointid).HasColumnName("locationpointid");

            entity.HasOne(d => d.IdanimalNavigation).WithMany(p => p.Historyvisitlocations)
                .HasForeignKey(d => d.Idanimal)
                .HasConstraintName("historyvisitlocation_fk");

            entity.HasOne(d => d.Locationpoint).WithMany(p => p.Historyvisitlocations)
                .HasForeignKey(d => d.Locationpointid)
                .HasConstraintName("historyvisitlocation_fk_1");
        });

        modelBuilder.Entity<Location>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("location_pk");

            entity.ToTable("Location");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Latitude).HasColumnName("latitude");
            entity.Property(e => e.Longitude).HasColumnName("longitude");
        });

        modelBuilder.Entity<Typeanimal>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("typeanimal_pk");

            entity.ToTable("typeanimal");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Type).HasMaxLength(80);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("users_pk");

            entity.ToTable("users");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Email)
                .HasMaxLength(80)
                .HasColumnName("email");
            entity.Property(e => e.Firstname)
                .HasMaxLength(80)
                .HasColumnName("firstname");
            entity.Property(e => e.Lastname)
                .HasMaxLength(80)
                .HasColumnName("lastname");
            entity.Property(e => e.Password).HasMaxLength(80);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
