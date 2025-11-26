using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace KutyakApi.Models;

public partial class KutyakContext : DbContext
{
    public KutyakContext()
    {
    }

    public KutyakContext(DbContextOptions<KutyakContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Gazdum> Gazda { get; set; }

    public virtual DbSet<Kutya> Kutyas { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySQL("SERVER=localhost;PORT=3306;DATABASE=kutyak;USER=root;PASSWORD=;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Gazdum>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("gazda");

            entity.HasIndex(e => e.Email, "Email").IsUnique();

            entity.Property(e => e.Id).HasColumnType("int(11)");
            entity.Property(e => e.Cim).HasMaxLength(256);
            entity.Property(e => e.Email).HasMaxLength(64);
            entity.Property(e => e.Nev).HasMaxLength(64);
        });

        modelBuilder.Entity<Kutya>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("kutya");

            entity.HasIndex(e => e.GazdaId, "GazdaId");

            entity.Property(e => e.Id).HasColumnType("int(11)");
            entity.Property(e => e.Fajta).HasMaxLength(64);
            entity.Property(e => e.GazdaId).HasColumnType("int(11)");
            entity.Property(e => e.Kep).HasColumnType("mediumblob");
            entity.Property(e => e.Marmagassag).HasColumnType("int(11)");
            entity.Property(e => e.Nev).HasMaxLength(64);
            entity.Property(e => e.Tomeg).HasColumnType("int(11)");

            entity.HasOne(d => d.Gazda).WithMany(p => p.Kutyas)
                .HasForeignKey(d => d.GazdaId)
                .HasConstraintName("kutya_ibfk_1");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
