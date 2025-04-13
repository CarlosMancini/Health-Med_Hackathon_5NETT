namespace Core.Interfaces.Service
{
    public interface IService<T> where T : class
    {
        Task<IList<T>> ObterTodos();
        Task<T> ObterPorId(int id);
    }
}
