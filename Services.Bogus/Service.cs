using Models;
using Services.Bogus.Fakers;
using Services.Interfaces;

namespace Services.Bogus
{
	public class Service<T> : IService<T> where T : Entity
	{
		private readonly ICollection<T> _entities;

		public Service(EntityFaker<T> entityFaker)
		{
			_entities = entityFaker.Generate(10);
		}

		public Task DeleteAsync(int id)
		{
			_entities.Remove(_entities.SingleOrDefault(x => x.Id == id)!);
			return Task.CompletedTask;
		}

		public Task<IEnumerable<T>> ReadAsync()
		{
			return Task.FromResult(_entities.ToList().AsEnumerable());
		}

		public Task<T?> ReadAsync(int id)
		{
			var entity = _entities.FirstOrDefault(x => x.Id == id);
			return Task.FromResult(entity);
		}
	}
}