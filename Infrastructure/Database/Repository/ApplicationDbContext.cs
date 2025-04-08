using Core.Entities;
using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Perfil> Perfis { get; set; }
    public DbSet<Medico> Medicos { get; set; }
    public DbSet<Paciente> Pacientes { get; set; }
    public DbSet<Especialidade> Especialidades { get; set; }
    public DbSet<MedicoEspecialidade> MedicoEspecialidades { get; set; }
    public DbSet<HorarioDisponivel> HorariosDisponiveis { get; set; }
    public DbSet<AgendamentoStatus> AgendamentoStatus { get; set; }
    public DbSet<MotivoCancelamento> MotivosCancelamento { get; set; }
    public DbSet<Agendamento> Agendamentos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }
}