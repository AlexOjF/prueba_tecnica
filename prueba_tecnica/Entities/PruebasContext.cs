using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace prueba_tecnica.Entities;

public partial class PruebasContext : DbContext
{
    public PruebasContext()
    {
    }

    public PruebasContext(DbContextOptions<PruebasContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Aspirante> Aspirantes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySQL("server=localhost;port=3306;user=user_app;password=Elfenix11tsm*;database=pruebas");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Aspirante>(entity =>
        {
            entity.HasKey(e => e.IdAspirante).HasName("PRIMARY");

            entity.ToTable("Aspirante");

            entity.Property(e => e.IdAspirante)
                .HasMaxLength(20)
                .HasColumnName("ID_aspirante");
            entity.Property(e => e.Apellido1)
                .HasMaxLength(45)
                .HasColumnName("apellido1");
            entity.Property(e => e.Apellido2)
                .HasMaxLength(45)
                .HasColumnName("apellido2");
            entity.Property(e => e.Celular)
                .HasMaxLength(45)
                .HasColumnName("celular");
            entity.Property(e => e.Correo)
                .HasMaxLength(100)
                .HasColumnName("correo");
            entity.Property(e => e.Nombre1)
                .HasMaxLength(45)
                .HasColumnName("nombre1");
            entity.Property(e => e.Nombre2)
                .HasMaxLength(45)
                .HasColumnName("nombre2");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
