using System.Threading.Tasks;

namespace TableFootball.DataAccess.Interfaces
{
    public interface ICreateable<T>
    {
        Task<int> CreateAsync(T entity);
    }
}
