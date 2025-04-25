using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Repository.Configurations
{
    public class HorarioDisponivelConfiguration : IEntityTypeConfiguration<HorarioDisponivel>
    {
        public void Configure(EntityTypeBuilder<HorarioDisponivel> builder)
        {
            builder.HasKey(h => h.Id);

            builder.Property(h => h.HorarioDisponivelDiaSemana).IsRequired();
            builder.Property(h => h.HorarioDisponivelHoraInicio).IsRequired();
            builder.Property(h => h.HorarioDisponivelHoraFim).IsRequired();

            // Relacionamentos
            builder.HasOne(h => h.Medico)
                   .WithMany(m => m.HorariosDisponiveis)
                   .HasForeignKey(h => h.MedicoId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
