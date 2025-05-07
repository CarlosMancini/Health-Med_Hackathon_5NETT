using Core.DTOs;
using Core.Entities;
using Core.Inputs.Pesquisar;
using Core.Interfaces.Repositories;
using Infrastructure.Database.Repository;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database.Repositories
{
    public class MedicoRepository : RepositoryBase<Medico>, IMedicoRepository
    {
        public MedicoRepository(ApplicationDbContext context) : base(context) { }

        public async Task<Medico> ObterMedicoPorId(int id)
        {
            return await _context.Medico
                .Include(m => m.MedicoEspecialidades)
                .Include(m => m.HorariosDisponiveis)
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<ICollection<Medico>> ObterPorEspecialidade(int especialidadeId)
        {
            return await _context.Medico
                .Where(m => m.MedicoEspecialidades.Any(me => me.EspecialidadeId == especialidadeId))
                .Include(m => m.MedicoEspecialidades)
                .ThenInclude(me => me.Especialidade)
                .ToListAsync();
        }

        public async Task<ICollection<MedicoDisponivelDto>> PesquisarMedicosDisponiveis(FiltroPesquisaMedicoDisponivelInput input)
        {
            var datasPeriodo = Enumerable.Range(0, (input.DataFim - input.DataInicio).Days + 1)
                .Select(offset => input.DataInicio.AddDays(offset))
                .ToList();

            var medicos = await _context.Medico
                .Include(m => m.Usuario)
                .Include(m => m.MedicoEspecialidades)
                    .ThenInclude(me => me.Especialidade)
                .Include(m => m.HorariosDisponiveis)
                .Where(m =>
                    m.MedicoEspecialidades.Any(me => me.EspecialidadeId == input.EspecialidadeId)
                )
                .ToListAsync();

            var resultado = new List<MedicoDisponivelDto>();

            foreach (var medico in medicos)
            {
                var diasDisponiveis = new List<DiaDisponivelDto>();

                foreach (var data in datasPeriodo)
                {
                    var diaSemana = data.DayOfWeek;
                    var horariosDoDia = medico.HorariosDisponiveis
                        .Where(h => h.HorarioDisponivelDiaSemana == diaSemana)
                        .Select(h => new HorarioDisponivelDto
                        {
                            HoraInicio = h.HorarioDisponivelHoraInicio.ToString(@"hh\:mm"),
                            HoraFim = h.HorarioDisponivelHoraFim.ToString(@"hh\:mm")
                        })
                        .ToList();

                    if (horariosDoDia.Any())
                    {
                        diasDisponiveis.Add(new DiaDisponivelDto
                        {
                            Data = data,
                            DiaSemana = data.DayOfWeek.ToString(),
                            Horarios = horariosDoDia
                        });
                    }
                }

                if (diasDisponiveis.Any())
                {
                    resultado.Add(new MedicoDisponivelDto
                    {
                        MedicoId = medico.Id,
                        Nome = medico.Usuario.UsuarioNome,
                        CRM = medico.MedicoCRM,
                        Especialidades = medico.MedicoEspecialidades
                            .Select(e => e.Especialidade.EspecialidadeDescricao)
                            .ToList(),
                        DiasDisponiveis = diasDisponiveis
                    });
                }
            }

            return resultado;
        }

        public async Task ExcluirMedico(int id)
        {
            var medico = await _context.Medico
                .Include(m => m.MedicoEspecialidades)
                .Include(m => m.HorariosDisponiveis)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (medico == null)
                throw new Exception("Médico não encontrado");

            _context.MedicoEspecialidade.RemoveRange(medico.MedicoEspecialidades);
            _context.HorarioDisponivel.RemoveRange(medico.HorariosDisponiveis);
            _context.Medico.Remove(medico);

            var usuario = await _context.Usuario.FindAsync(id);
            if (usuario != null)
                _context.Usuario.Remove(usuario);

            await _context.SaveChangesAsync();
        }
    }
}
