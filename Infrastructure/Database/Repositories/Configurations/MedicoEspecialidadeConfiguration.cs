using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Repository.Configurations
{
    public class MedicoEspecialidadeConfiguration : IEntityTypeConfiguration<MedicoEspecialidade>
    {
        public void Configure(EntityTypeBuilder<MedicoEspecialidade> builder)
        {
            builder.HasKey(me => me.Id);
            builder.HasOne(me => me.Medico).WithMany(m => m.Especialidades).HasForeignKey(me => me.MedicoId);
            builder.HasOne(me => me.Especialidade).WithMany().HasForeignKey(me => me.EspecialidadeId);
        }
    }
}
