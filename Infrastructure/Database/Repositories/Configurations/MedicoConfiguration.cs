using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Repository.Configurations
{
    public class MedicoConfiguration : IEntityTypeConfiguration<Medico>
    {
        public void Configure(EntityTypeBuilder<Medico> builder)
        {
            builder.HasKey(m => m.Id);
            builder.Property(m => m.CRM).IsRequired().HasMaxLength(20);
            builder.HasOne(m => m.Usuario).WithMany().HasForeignKey(m => m.Id);
        }
    }
}
