using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Repository.Configurations
{
    public class EspecialidadeConfiguration : IEntityTypeConfiguration<Especialidade>
    {
        public void Configure(EntityTypeBuilder<Especialidade> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.EspecialidadeDescricao).IsRequired().HasMaxLength(100);
        }
    }
}
