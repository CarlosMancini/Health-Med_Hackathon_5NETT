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
            builder.Property(a => a.AgendamentoDataHora);
            builder.Property(a => a.PacienteId);
            builder.Property(a => a.MedicoId);
            builder.Property(a => a.AgendamentoStatusId);
            builder.Property(a => a.MotivoCancelamentoId);
                  
            builder.HasOne(a => a.Paciente).WithMany().HasForeignKey(a => a.PacienteId);
            builder.HasOne(a => a.Medico).WithMany().HasForeignKey(a => a.MedicoId);
            builder.HasOne(a => a.AgendamentoStatus).WithMany().HasForeignKey(a => a.AgendamentoStatusId);
            builder.HasOne(a => a.MotivoCancelamento).WithMany().HasForeignKey(a => a.MotivoCancelamentoId);
        }
    }
}
