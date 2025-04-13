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
            builder.HasOne(h => h.Medico).WithMany().HasForeignKey(h => h.MedicoId);
            builder.Property(h => h.DataHora).IsRequired();
        }
    }
}
