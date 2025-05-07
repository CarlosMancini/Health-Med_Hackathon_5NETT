using Core.Entities;
using Core.Inputs.Cadastrar;
using Core.Interfaces.Repositories;
using Core.Utils.Enums;
using Infrastructure.Database.Repository;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database.Repositories
{
    public class AgendamentoRepository : RepositoryBase<Agendamento>, IAgendamentoRepository
    {
        public AgendamentoRepository(ApplicationDbContext context) : base(context) { }

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

            var novoAgendamento = new Agendamento
            {
                MedicoId = input.MedicoId,
                PacienteId = input.PacienteId,
                EspecialidadeId = input.EspecialidadeId,
                AgendamentoDataHora = input.DataHora,
                AgendamentoStatusId = (int)AgendamentoStatusEnum.Pendente
            };

            _context.Agendamento.Add(novoAgendamento);
            await _context.SaveChangesAsync();
        }
    }
}
