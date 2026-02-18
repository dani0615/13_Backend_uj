using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace TuristaAPI.Models;

public partial class TuristadbContext : DbContext
{
    public TuristadbContext()
    {
    }

    public TuristadbContext(DbContextOptions<TuristadbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Nehezseg> Nehezsegs { get; set; }

    public virtual DbSet<Tura> Turas { get; set; }

    public virtual DbSet<Turavezeto> Turavezetos { get; set; }

    public virtual DbSet<Utvonal> Utvonals { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySQL("SERVER=localhost;PORT=3306;DATABASE=turistadb;USER=root;PASSWORD=;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Nehezseg>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("nehezseg");

            entity.HasIndex(e => e.Jelzes, "jelzes").IsUnique();

            entity.Property(e => e.Id).HasColumnType("int(11)");
            entity.Property(e => e.Jelzes)
                .HasMaxLength(1)
                .HasColumnName("jelzes");
            entity.Property(e => e.Leiras)
                .HasMaxLength(128)
                .HasColumnName("leiras");
        });

        modelBuilder.Entity<Tura>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("tura");

            entity.HasIndex(e => e.TuravezetoId, "turavezetoID");

            entity.HasIndex(e => e.UtvonalId, "utvonalID");

            entity.Property(e => e.Id).HasColumnType("int(11)");
            entity.Property(e => e.Idopont)
                .HasColumnType("int(11)")
                .HasColumnName("idopont");
            entity.Property(e => e.Koltseg)
                .HasColumnType("int(11)")
                .HasColumnName("koltseg");
            entity.Property(e => e.Maxletszam)
                .HasColumnType("int(11)")
                .HasColumnName("maxletszam");
            entity.Property(e => e.TuravezetoId)
                .HasColumnType("int(11)")
                .HasColumnName("turavezetoID");
            entity.Property(e => e.UtvonalId)
                .HasColumnType("int(11)")
                .HasColumnName("utvonalID");

            entity.HasOne(d => d.Turavezeto).WithMany(p => p.Turas)
                .HasForeignKey(d => d.TuravezetoId)
                .HasConstraintName("tura_ibfk_1");

            entity.HasOne(d => d.Utvonal).WithMany(p => p.Turas)
                .HasForeignKey(d => d.UtvonalId)
                .HasConstraintName("tura_ibfk_2");
        });

        modelBuilder.Entity<Turavezeto>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("turavezeto");

            entity.HasIndex(e => e.Email, "email").IsUnique();

            entity.Property(e => e.Id).HasColumnType("int(11)");
            entity.Property(e => e.Email)
                .HasMaxLength(32)
                .HasColumnName("email");
            entity.Property(e => e.Minosites)
                .HasColumnType("int(8)")
                .HasColumnName("minosites");
            entity.Property(e => e.Nev).HasMaxLength(64);
            entity.Property(e => e.Telefon).HasMaxLength(15);
        });

        modelBuilder.Entity<Utvonal>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("utvonal");

            entity.HasIndex(e => e.NehezsegId, "nehezsegID");

            entity.Property(e => e.Id).HasColumnType("int(11)");
            entity.Property(e => e.Allomasok)
                .HasMaxLength(256)
                .HasColumnName("allomasok");
            entity.Property(e => e.NehezsegId)
                .HasColumnType("int(11)")
                .HasColumnName("nehezsegID");
            entity.Property(e => e.Szint)
                .HasColumnType("int(11)")
                .HasColumnName("szint");
            entity.Property(e => e.Tav)
                .HasColumnType("int(11)")
                .HasColumnName("tav");

            entity.HasOne(d => d.Nehezseg).WithMany(p => p.Utvonals)
                .HasForeignKey(d => d.NehezsegId)
                .HasConstraintName("utvonal_ibfk_1");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
