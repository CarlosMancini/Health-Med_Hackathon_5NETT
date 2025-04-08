using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Repository.Configurations
{
    public class AgendamentoConfiguration : IEntityTypeConfiguration<Agendamento>
    {
        public void Configure(EntityTypeBuilder<Agendamento> builder)
        {
            builder.HasKey(a => a.Id);
            builder.HasOne(a => a.Paciente).WithMany().HasForeignKey(a => a.PacienteId);
            builder.HasOne(a => a.Medico).WithMany().HasForeignKey(a => a.MedicoId);
            builder.HasOne(a => a.Horario).WithMany().HasForeignKey(a => a.HorarioId);
            builder.HasOne(a => a.Status).WithMany().HasForeignKey(a => a.StatusId);
            builder.HasOne(a => a.MotivoCancelamento).WithMany().HasForeignKey(a => a.MotivoCancelamentoId);
        }
    }
}
