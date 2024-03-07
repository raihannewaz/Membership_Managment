using Membership_Management.DAL.Interfaces;
using Membership_Managment.Context;
using Microsoft.EntityFrameworkCore;

namespace Membership_Managment.DAL.Repositories
{
    public class GenereicRepository<T> : IGenericRepository<T> where T : class
    {
        public Task<T> Add(T entity)
        {
            throw new NotImplementedException();
        }

        public Task<T> Delete(T entity)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<T>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<T> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<T> Update(int id, T entity)
        {
            throw new NotImplementedException();
        }
    }
}
