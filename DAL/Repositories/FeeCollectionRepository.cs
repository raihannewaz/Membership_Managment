using Membership_Managment.Context;
using Membership_Managment.DAL.Interfaces;
using Membership_Managment.Models;
using Microsoft.EntityFrameworkCore;

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
            if (entity.Amount <1)
            {
                throw new ArgumentException("Ammount is very low");
            }
            if (entity.CollectionType == null)
            {
                throw new ArgumentException("Collect Type is required");
            }
            entity.CollectionDate = DateTime.Now;

            _context.FeeCollections.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public Task<FeeCollection> Delete(FeeCollection entity)
        {
            throw new NotImplementedException();
        }

        public async Task<IReadOnlyList<FeeCollection>> GetAllAsync()
        {
            return await _context.FeeCollections.Include(a=>a.Member).ToListAsync();
        }

        public async Task<FeeCollection> GetByIdAsync(int id)
        {
            var a = await _context.FeeCollections.FirstOrDefaultAsync(a=>a.MemberId== id);
            return a;
        }

        public async Task<List<FeeCollection>> GetByMemberIdAsync(int memberId)
        {
            var feeCollections = await _context.FeeCollections
                .Where(fc => fc.MemberId == memberId)
                .ToListAsync();
            return feeCollections;
        }



        public Task<FeeCollection> Update(int id, FeeCollection entity)
        {
            throw new NotImplementedException();
        }
    }
}
