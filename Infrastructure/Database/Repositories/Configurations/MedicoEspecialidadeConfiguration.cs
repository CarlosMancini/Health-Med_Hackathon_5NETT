﻿using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Repository.Configurations
{
    public class MedicoEspecialidadeConfiguration : IEntityTypeConfiguration<MedicoEspecialidade>
    {
        public void Configure(EntityTypeBuilder<MedicoEspecialidade> builder)
        {
            builder.HasKey(me => me.Id);
            builder.HasOne(me => me.Medico).WithMany(m => m.MedicoEspecialidades).HasForeignKey(me => me.MedicoId);
            builder.HasOne(me => me.Especialidade).WithMany().HasForeignKey(me => me.EspecialidadeId);

            // Relacionamentos
            builder.HasOne(me => me.Medico)
                .WithMany(m => m.MedicoEspecialidades)
                .HasForeignKey(me => me.MedicoId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(me => me.Especialidade)
                .WithMany()
                .HasForeignKey(me => me.EspecialidadeId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
