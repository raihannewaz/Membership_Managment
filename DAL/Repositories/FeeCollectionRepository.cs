using Membership_Managment.Context;
using Membership_Managment.DAL.Interfaces;
using Membership_Managment.Models;

namespace Membership_Managment.DAL.Repositories
{
    public class FeeCollectionRepository : IFeeCollectionRepository
    {
        private ApplicationDbContext _context;

        public FeeCollectionRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<FeeCollection> Add(FeeCollection entity)
        {
            entity.CollectionDate= DateTime.Now;

            _context.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public Task<FeeCollection> Delete(FeeCollection entity)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<FeeCollection>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<FeeCollection> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<FeeCollection> Update(int id, FeeCollection entity)
        {
            throw new NotImplementedException();
        }
    }
}
