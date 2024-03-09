using Membership_Managment.Context;
using Membership_Managment.DAL.Interfaces;
using Membership_Managment.Models;
using Microsoft.EntityFrameworkCore;
using System.Formats.Asn1;

namespace Membership_Managment.DAL.Repositories
{
    public class PackageRepository : IPackageRepository
    {

        private readonly ApplicationDbContext _context;

        public PackageRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Package> Add(Package entity)
        {
            if (entity.PackagePrice < 1)
            {
                throw new ArgumentException("Ammount is very low");
            }
            if (entity.PaymentType == null)
            {
                throw new ArgumentException("Collect Type is required");
            }
            if (entity.Duration < 30)
            {
                throw new ArgumentException("Minimum 30 Days Duration");
            }

            _context.Packages.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Package> Delete(Package entity)
        {
            _context.Packages.Remove(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<IReadOnlyList<Package>> GetAllAsync()
        {
            return await _context.Packages.ToListAsync();
        }

        public async Task<Package> GetByIdAsync(int id)
        {
            return await _context.Packages.FirstOrDefaultAsync(x => x.PackageId == id);
        }

        public async Task<Package> Update(int id, Package entity)
        {
            var existingEntity = await GetByIdAsync(id);
            if (existingEntity == null)
            {
                throw new ArgumentException("Entity with the given id not found.");
            }


            foreach (var property in typeof(Package).GetProperties())
            {
                var newValue = property.GetValue(entity);
                if (newValue != null)
                {
                    property.SetValue(existingEntity, newValue);
                }
            }


            await _context.SaveChangesAsync();

            return existingEntity;
        }

    }
}
