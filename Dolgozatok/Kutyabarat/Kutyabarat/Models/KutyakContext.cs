using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Kutyabarat.Models;

public partial class KutyakContext : DbContext
{
    public KutyakContext()
    {
    }

    public KutyakContext(DbContextOptions<KutyakContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Kutya> Kutyas { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySQL("SERVER=localhost;PORT=3306;DATABASE=kutyak;USER=root;PASSWORD=;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Kutya>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("kutya");

            entity.Property(e => e.Id).HasColumnType("int(11)");
            entity.Property(e => e.Fajta).HasMaxLength(64);
            entity.Property(e => e.Kep).HasColumnType("mediumblob");
            entity.Property(e => e.Marmagassag).HasColumnType("int(11)");
            entity.Property(e => e.Tomeg).HasColumnType("int(11)");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
