using Models;
using System.Dynamic;

namespace Services.Interfaces
{
    public interface ICrudService<T> where T : Entity
    {
        Task<T?> ReadAsync(int id);
        Task<IEnumerable<T>> ReadAsync();

        Task DeleteAsync(int id);
    }
}