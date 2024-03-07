namespace Membership_Management.DAL.Interfaces
{
    public interface IGenericRepository<T> where T : class 
    {
        Task<T> Add(T entity);
        Task<T> Update(int id, T entity);
        Task<T> Delete(T entity);

        Task<T> GetByIdAsync(int id);
        Task<IReadOnlyList<T>> GetAllAsync();

    }

}
