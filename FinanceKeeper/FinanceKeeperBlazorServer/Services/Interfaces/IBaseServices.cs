namespace FinanceKeeperBlazorServer.Services.Interfaces
{
    public interface IBaseServices<T>
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task CreateAsync(T model);
        Task UpdateAsync(T model);
        Task DeleteAsync(int id);
    }
}
