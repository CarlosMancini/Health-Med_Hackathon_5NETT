﻿using Core.Entities;

namespace Core.Interfaces.Services
{
    public interface IMedicoService : IServiceBase<Medico>
    {
        Task Cadastrar(Medico medico);
    }
}
