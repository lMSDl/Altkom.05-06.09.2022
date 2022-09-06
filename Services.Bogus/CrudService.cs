using Models;
using Services.Bogus.Fakers;
using Services.Interfaces;

namespace Services.Bogus
{
    public class CrudService<T> : ICrudService<T> where T : Entity
    {
        public CrudService(EntityFaker<T> faker, int count = 5)
        {
            Entities = faker.Generate(count);
        }

        private ICollection<T> Entities { get; }

        public Task<int> CreateAsync(T entity)
        {
            entity.Id = Entities.Max(x => x.Id) + 1;
            Entities.Add(entity);
            return Task.FromResult(entity.Id);
        }

        public Task DeleteAsync(int id)
        {
            Entities.Remove(Entities.SingleOrDefault(x => x.Id == id)!);
            return Task.CompletedTask;
        }

        public Task<T?> ReadAsync(int id)
        {
            return Task.FromResult(Entities.SingleOrDefault(x => x.Id == id));
        }

        public Task<IEnumerable<T>> ReadAsync()
        {
            return Task.FromResult(Entities.AsEnumerable());
        }

        public async Task UpdateAsync(int id, T entity)
        {
            entity.Id = id;
            await DeleteAsync(id);
            Entities.Add(entity);
        }
    }
}