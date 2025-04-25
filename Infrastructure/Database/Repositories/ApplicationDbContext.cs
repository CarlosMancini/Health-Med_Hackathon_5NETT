using Core.Entities;
using Infrastructure.Database.Repository.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database.Repositories
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Perfil> Perfil { get; set; }
        public DbSet<Medico> Medico { get; set; }
        public DbSet<Paciente> Paciente { get; set; }
        public DbSet<Especialidade> Especialidade { get; set; }
        public DbSet<MedicoEspecialidade> MedicoEspecialidade { get; set; }
        public DbSet<HorarioDisponivel> HorarioDisponivel { get; set; }
        public DbSet<AgendamentoStatus> AgendamentoStatus { get; set; }
        public DbSet<MotivoCancelamento> MotivoCancelamento { get; set; }
        public DbSet<Agendamento> Agendamento { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }
    }
}