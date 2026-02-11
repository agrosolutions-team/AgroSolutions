using AgroSolutions.IoT.IngestaoDados.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgroSolutions.IoT.IngestaoDados.Infrastructure.Data.Configurations;

public class LeituraSensorTalhaoConfiguration : IEntityTypeConfiguration<LeituraSensorTalhao>
{
    public void Configure(EntityTypeBuilder<LeituraSensorTalhao> builder)
    {
        builder.ToTable("LeiturasSensorTalhao");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .IsRequired();

        builder.Property(x => x.SensorId)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.TalhaoId)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.PropriedadeId)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.NomeTalhao)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(x => x.CulturaPlantada)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(x => x.AreaEmHectares)
            .HasPrecision(10, 2)
            .IsRequired();

        builder.Property(x => x.Timestamp)
            .IsRequired();

        builder.Property(x => x.UmidadeSoloPercentual)
            .HasPrecision(5, 2)
            .IsRequired();

        builder.Property(x => x.TemperaturaCelsius)
            .HasPrecision(5, 2)
            .IsRequired();

        builder.Property(x => x.PrecipitacaoMm)
            .HasPrecision(7, 2)
            .IsRequired();

        builder.Property(x => x.UmidadeArPercentual)
            .HasPrecision(5, 2)
            .IsRequired();

        builder.Property(x => x.VelocidadeVentoKmh)
            .HasPrecision(6, 2)
            .IsRequired();

        builder.Property(x => x.CriadoEm)
            .IsRequired();

        builder.HasIndex(x => new { x.TalhaoId, x.Timestamp })
            .HasDatabaseName("idx_talhao_timestamp");

        builder.HasIndex(x => x.PropriedadeId)
            .HasDatabaseName("idx_propriedade_id");
    }
}