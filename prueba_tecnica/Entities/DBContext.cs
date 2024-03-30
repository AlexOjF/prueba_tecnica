using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace prueba_tecnica.Entities;

public partial class DBContext : DbContext
{
    public DBContext()
    {
    }

    public DBContext(DbContextOptions<DBContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Aspirante> Aspirantes { get; set; }

    public virtual DbSet<Matricula> Matriculas { get; set; }

    public virtual DbSet<Programa> Programas { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Aspirante>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("Aspirante");

            entity.HasIndex(e => e.Id, "ID").IsUnique();

            entity.Property(e => e.Id)
                .HasMaxLength(13)
                .HasColumnName("ID");
            entity.Property(e => e.Apellido).HasColumnType("text");
            entity.Property(e => e.Celular).HasMaxLength(13);
            entity.Property(e => e.Correo).HasColumnType("text");
            entity.Property(e => e.Nombre).HasColumnType("text");
        });

        modelBuilder.Entity<Matricula>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("Matricula");

            entity.HasIndex(e => e.IdAspirante, "ID_Aspirante");

            entity.HasIndex(e => e.IdPrograma, "ID_Programa");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CostoMatricula).HasColumnName("Costo_Matricula");
            entity.Property(e => e.FechaIncripcin)
                .HasColumnType("datetime")
                .HasColumnName("Fecha_Incripcin");
            entity.Property(e => e.IdAspirante)
                .HasMaxLength(13)
                .HasColumnName("ID_Aspirante");
            entity.Property(e => e.IdPrograma)
                .HasMaxLength(30)
                .HasColumnName("ID_Programa");
            entity.Property(e => e.LimitePago)
                .HasColumnType("datetime")
                .HasColumnName("Limite_Pago");

            entity.HasOne(d => d.IdAspiranteNavigation).WithMany(p => p.Matriculas)
                .HasForeignKey(d => d.IdAspirante)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Matricula_ibfk_1");

            entity.HasOne(d => d.IdProgramaNavigation).WithMany(p => p.Matriculas)
                .HasForeignKey(d => d.IdPrograma)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Matricula_ibfk_2");
        });

        modelBuilder.Entity<Programa>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("Programa");

            entity.Property(e => e.Id)
                .HasMaxLength(30)
                .HasColumnName("ID");
            entity.Property(e => e.Descripcin).HasColumnType("text");
            entity.Property(e => e.Nivel).HasMaxLength(20);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
