using MarketInventory.Domain.Entities;

namespace MarketInventory.Infrastructure.Repositories.Interfaces;

public interface IGenericRepository<T> where T : class
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<T?> GetByIdAsync(int id);
    Task AddAsync(T entity);
    void Update(T entity);
    void Delete(T entity);
    Task SaveAsync();
    Task UpdateAsync(Birim existing);
    Task UpdateAsync(KullaniciTuru entity);
    Task DeleteAsync(KullaniciTuru entity);
}