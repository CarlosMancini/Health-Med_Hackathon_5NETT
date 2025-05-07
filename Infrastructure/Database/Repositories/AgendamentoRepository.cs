using Core.Entities;
using Core.Inputs.Cadastrar;
using Core.Inputs.Pesquisar;
using Core.Interfaces.Repositories;
using Core.Utils.Enums;
using Infrastructure.Database.Repository;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database.Repositories
{
    public class AgendamentoRepository : RepositoryBase<Agendamento>, IAgendamentoRepository
    {
        public AgendamentoRepository(ApplicationDbContext context) : base(context) { }

        public async Task<ICollection<Agendamento>> Pesquisar(FiltroPesquisaAgendamentoInput input)
        {
            var query = _context.Agendamento
                .Include(a => a.Paciente)
                .Include(a => a.Medico)
                .Include(a => a.AgendamentoStatus)
                .Include(a => a.MotivoCancelamento)
                .AsQueryable();

            if (input.PacienteId.HasValue)
                query = query.Where(a => a.PacienteId == input.PacienteId.Value);

            if (input.MedicoId.HasValue)
                query = query.Where(a => a.MedicoId == input.MedicoId.Value);

            if (input.StatusId.HasValue)
                query = query.Where(a => a.AgendamentoStatusId == input.StatusId.Value);

            if (input.DataHoraInicio.HasValue)
                query = query.Where(a => a.AgendamentoDataHora >= input.DataHoraInicio.Value);

            if (input.DataHoraFim.HasValue)
                query = query.Where(a => a.AgendamentoDataHora <= input.DataHoraFim.Value);

            return await query.ToListAsync();
        }


        public async Task AgendarConsulta(CadastrarAgendamentoInput input)
        {
            // Valida médico e especialidade
            var medicoEspecialidadeValida = await _context.MedicoEspecialidade
                .AnyAsync(me => me.MedicoId == input.MedicoId && me.EspecialidadeId == input.EspecialidadeId);

            if (!medicoEspecialidadeValida)
                throw new Exception("O médico não atende essa especialidade.");

            // Valida horário disponível
            var diaSemana = input.DataHora.DayOfWeek;
            var hora = input.DataHora.TimeOfDay;

            var disponibilidade = await _context.HorarioDisponivel
                .AnyAsync(h => h.MedicoId == input.MedicoId &&
                               h.HorarioDisponivelDiaSemana == diaSemana &&
                               h.HorarioDisponivelHoraInicio <= hora &&
                               h.HorarioDisponivelHoraFim > hora);

            if (!disponibilidade)
                throw new Exception("O médico não está disponível nesse horário.");

            // Verifica conflitos
            var conflito = await _context.Agendamento
                .AnyAsync(a => a.MedicoId == input.MedicoId && a.AgendamentoDataHora == input.DataHora);

            if (conflito)
                throw new Exception("Esse horário já está agendado para o médico.");

            var valorConsulta = await _context.Medico
                .Where(x => x.Id == input.MedicoId)
                .Select(x => x.MedicoValorConsulta)
                .FirstOrDefaultAsync();

            var novoAgendamento = new Agendamento
            {
                MedicoId = input.MedicoId,
                PacienteId = input.PacienteId,
                EspecialidadeId = input.EspecialidadeId,
                AgendamentoDataHora = input.DataHora,
                AgendamentoStatusId = (int)AgendamentoStatusEnum.Pendente,
                AgendamentoValor = valorConsulta
            };

            _context.Agendamento.Add(novoAgendamento);
            await _context.SaveChangesAsync();
        }
    }
}
