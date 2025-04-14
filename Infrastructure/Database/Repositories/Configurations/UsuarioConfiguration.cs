using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Repository.Configurations
{
    public class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.HasKey(u => u.Id);
            builder.Property(u => u.UsuarioNome).IsRequired().HasMaxLength(100);
            builder.Property(u => u.UsuarioEmail).IsRequired().HasMaxLength(100);
            builder.Property(u => u.UsuarioCPF).IsRequired().HasMaxLength(14);
            builder.Property(u => u.UsuarioSenha).IsRequired();
            builder.Property(u => u.CriadoEm).IsRequired();
            builder.HasOne(u => u.Perfil).WithMany().HasForeignKey(u => u.PerfilId);
        }
    }
}
