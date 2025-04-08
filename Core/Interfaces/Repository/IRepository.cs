﻿namespace Core.Interfaces.Repository
{
    public interface IRepository<T> where T : class
    {
        Task Cadastrar(T entidade);
        Task Atualizar(T entidade);
        Task Excluir(int id);
        Task<T> ObterPorId(int id);
        Task<IList<T>> ObterTodos();
    }
}
