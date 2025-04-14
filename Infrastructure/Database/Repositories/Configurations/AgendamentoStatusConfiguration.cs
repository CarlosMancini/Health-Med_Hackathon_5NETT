using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Repository.Configurations
{
    public class AgendamentoStatusConfiguration : IEntityTypeConfiguration<AgendamentoStatus>
    {
        public void Configure(EntityTypeBuilder<AgendamentoStatus> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.AgendamentoStatusDescricao).IsRequired().HasMaxLength(50);
        }
    }
}
