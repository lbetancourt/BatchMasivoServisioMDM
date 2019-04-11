using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace BatchMasivoServisioMDM.Models
{
    public partial class IntegracionsContext : DbContext
    {
        public IntegracionsContext()
        {
        }

        public IntegracionsContext(DbContextOptions<IntegracionsContext> options)
            : base(options)
        {
        }

        public virtual DbSet<LogIntegraciones> LogIntegraciones { get; set; }    

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LogIntegraciones>(entity =>
            {
                entity.HasKey(e => e.Codigo);

                entity.ToTable("LOG_INTEGRACIONES");

                entity.Property(e => e.Codigo)
                    .HasColumnType("numeric(18, 0)")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.CodigoCanalSistema)
                    .HasColumnName("Codigo_Canal_Sistema")
                    .HasColumnType("numeric(18, 0)");

                entity.Property(e => e.CodigoEstado)
                    .HasColumnName("Codigo_Estado")
                    .HasColumnType("numeric(18, 0)");

                entity.Property(e => e.CodigoSistemaDestino)
                    .HasColumnName("Codigo_Sistema_Destino")
                    .HasColumnType("numeric(18, 0)");

                entity.Property(e => e.CodigoSistemaOrigen)
                    .HasColumnName("Codigo_Sistema_Origen")
                    .HasColumnType("numeric(18, 0)");

                entity.Property(e => e.CodigoTipoIntegracion)
                    .HasColumnName("Codigo_Tipo_Integracion")
                    .HasColumnType("numeric(18, 0)");

                entity.Property(e => e.FechaFin).HasColumnType("datetime");

                entity.Property(e => e.FechaInicio).HasColumnType("datetime");

                entity.Property(e => e.Operacion)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Usuario)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.XmlRequest).IsUnicode(false);

                entity.Property(e => e.XmlResponse).IsUnicode(false);
            });
        }
    }
}
