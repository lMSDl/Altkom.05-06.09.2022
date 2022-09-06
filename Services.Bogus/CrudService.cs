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
    }
}