using System.Collections.Generic;

namespace TableFootball.DataAccess.Interfaces
{
    public interface IReadOnly<T>
    {
        ICollection<T> GetAll();
    }
}
