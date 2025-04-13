using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Repository.Configurations
{
    public class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable("Usuario");
            builder.HasKey(u => u.Id);
            builder.Property(u => u.Nome).IsRequired().HasMaxLength(100);
            builder.Property(u => u.Email).IsRequired().HasMaxLength(100);
            builder.Property(u => u.Senha).IsRequired();
            builder.Property(u => u.CriadoEm).IsRequired();
            builder.HasOne(u => u.Perfil).WithMany().HasForeignKey(u => u.PerfilId);
        }
    }
}
