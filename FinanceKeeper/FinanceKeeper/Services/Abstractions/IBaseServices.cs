namespace FinanceKeeper.Services.Abstractions
{
    public interface IBaseServices<T>
    {
        Task<T> CreateEntryAsync(T entitie);
        Task<T> UpdateEntryAsync(T category);
        Task<bool> DeleteEntryAsync(int entitieId);
        IList<T> GetAllEntries();
        T GetEntryById(int entitieId);
    }
}
