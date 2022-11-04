using Models;

namespace Services.Interfaces
{
	public interface IService<T> where T : Entity
	{
		Task<IEnumerable<T>> ReadAsync();
        Task<T?> ReadAsync(int id);
		Task DeleteAsync(int id);
		Task UpdateAsync(int id, T entity);
    }
}