using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ReceptAPI.Models;

public partial class ReceptdbContext : DbContext
{
    public ReceptdbContext()
    {
    }

    public ReceptdbContext(DbContextOptions<ReceptdbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Hozzavalo> Hozzavalos { get; set; }

    public virtual DbSet<Nehezseg> Nehezsegs { get; set; }

    public virtual DbSet<Recept> Recepts { get; set; }

    public virtual DbSet<Recepthozzavalo> Recepthozzavalos { get; set; }

    public virtual DbSet<Szakac> Szakacs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySQL("SERVER=localhost;PORT=3306;DATABASE=receptdb;USER=root;PASSWORD=;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Hozzavalo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("hozzavalo");

            entity.Property(e => e.Id).HasColumnType("int(11)");
            entity.Property(e => e.Kaloria)
                .HasColumnType("int(11)")
                .HasColumnName("kaloria");
            entity.Property(e => e.Nev)
                .HasMaxLength(100)
                .HasColumnName("nev");
        });

        modelBuilder.Entity<Nehezseg>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("nehezseg");

            entity.HasIndex(e => e.Szint, "szint").IsUnique();

            entity.Property(e => e.Id).HasColumnType("int(11)");
            entity.Property(e => e.Leiras)
                .HasMaxLength(255)
                .HasColumnName("leiras");
            entity.Property(e => e.Szint)
                .HasMaxLength(50)
                .HasColumnName("szint");
        });

        modelBuilder.Entity<Recept>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("recept");

            entity.HasIndex(e => e.NehezsegId, "nehezsegID");

            entity.HasIndex(e => e.SzakacsId, "szakacsId");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.ElkeszitesiIdo)
                .HasColumnType("int(11)")
                .HasColumnName("elkeszitesiIdo");
            entity.Property(e => e.Leiras)
                .HasColumnType("text")
                .HasColumnName("leiras");
            entity.Property(e => e.NehezsegId)
                .HasColumnType("int(11)")
                .HasColumnName("nehezsegID");
            entity.Property(e => e.Nev)
                .HasMaxLength(150)
                .HasColumnName("nev");
            entity.Property(e => e.SzakacsId)
                .HasColumnType("int(11)")
                .HasColumnName("szakacsId");

            entity.HasOne(d => d.Nehezseg).WithMany(p => p.Recepts)
                .HasForeignKey(d => d.NehezsegId)
                .HasConstraintName("recept_ibfk_1");

            entity.HasOne(d => d.Szakacs).WithMany(p => p.Recepts)
                .HasForeignKey(d => d.SzakacsId)
                .HasConstraintName("recept_ibfk_2");
        });

        modelBuilder.Entity<Recepthozzavalo>(entity =>
        {
            entity.HasKey(e => new { e.ReceptId, e.HozzavaloId }).HasName("PRIMARY");

            entity.ToTable("recepthozzavalo");

            entity.HasIndex(e => e.HozzavaloId, "fk_hozzavalo");

            entity.Property(e => e.ReceptId)
                .HasColumnType("int(11)")
                .HasColumnName("receptId");
            entity.Property(e => e.HozzavaloId)
                .HasColumnType("int(11)")
                .HasColumnName("hozzavaloId");
            entity.Property(e => e.Mennyiseg)
                .HasColumnType("int(50)")
                .HasColumnName("mennyiseg");

            entity.HasOne(d => d.Hozzavalo).WithMany(p => p.Recepthozzavalos)
                .HasForeignKey(d => d.HozzavaloId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_hozzavalo");

            entity.HasOne(d => d.Recept).WithMany(p => p.Recepthozzavalos)
                .HasForeignKey(d => d.ReceptId)
                .HasConstraintName("recepthozzavalo_ibfk_2");
        });

        modelBuilder.Entity<Szakac>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("szakacs");

            entity.HasIndex(e => e.Email, "email").IsUnique();

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.Nev)
                .HasMaxLength(100)
                .HasColumnName("nev");
            entity.Property(e => e.Telefonszam)
                .HasMaxLength(30)
                .HasColumnName("telefonszam");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
