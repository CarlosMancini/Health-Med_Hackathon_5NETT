using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Repository.Configurations
{
    public class MotivoCancelamentoConfiguration : IEntityTypeConfiguration<MotivoCancelamento>
    {
        public void Configure(EntityTypeBuilder<MotivoCancelamento> builder)
        {
            builder.HasKey(m => m.Id);
            builder.Property(m => m.MotivoCancelamentoDescricao).IsRequired().HasMaxLength(200);
        }
    }
}
