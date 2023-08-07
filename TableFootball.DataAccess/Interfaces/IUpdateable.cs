namespace TableFootball.DataAccess.Interfaces
{
    public interface IUpdateable<T>
    {
        void UpdateAsync(T entity);
    }
}
