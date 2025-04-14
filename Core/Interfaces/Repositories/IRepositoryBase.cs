﻿using Core.Entities;

namespace Core.Interfaces.Repository
{
    public interface IRepositoryBase<T> where T : EntityBase
    {
        Task Cadastrar(T entidade);
        Task Atualizar(T entidade);
        Task Excluir(int id);
        Task<T> ObterPorId(int id);
        Task<IList<T>> ObterTodos();
    }
}
