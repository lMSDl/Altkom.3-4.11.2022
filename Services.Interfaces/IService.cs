using Models;

namespace Services.Interfaces
{
	public interface IService<T> where T : Entity
	{
		Task<IEnumerable<T>> ReadAsync();
        Task<T?> ReadAsync(int id);
    }
}