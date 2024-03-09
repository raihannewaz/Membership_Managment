using Membership_Managment.Context;
using Membership_Managment.DAL.Interfaces;
using Membership_Managment.Models;
using Microsoft.EntityFrameworkCore;

namespace Membership_Managment.DAL.Repositories
{
    public class MemberPackageRepository : IMemberPackageRepository
    {
        private readonly ApplicationDbContext _context;

        public MemberPackageRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<MemberPackage> Add(MemberPackage entity)
        {
            
            var package = await _context.Packages.FindAsync(entity.PackageID);
            if (package == null)
            {
                throw new ArgumentException("Invalid package ID");
            }

            
            decimal paymentAmount;
            if (package.PaymentType == "Daily")
            {
                paymentAmount = Convert.ToDecimal((package.PackagePrice * entity.Quantity) * 0.10m);
            }
            else if (package.PaymentType == "Monthly")
            {
                paymentAmount = Convert.ToDecimal((package.PackagePrice * entity.Quantity) * 0.30m);
            }
            else
            {
                throw new ArgumentException("Invalid payment type");
            }

            
            var payment = new Payment
            {
                Amount = paymentAmount,
                PaymentDate = DateTime.Now,
                PaymentType = package.PaymentType,
                PaidInAdvance = false,
                MemberPackage = entity,
            };

            _context.Payments.Add(payment);


            
            entity.IsActive = true; 
            entity.StartDate = DateTime.Now; 
            entity.EndDate = DateTime.Now.AddDays(Convert.ToDouble(package.Duration));
            entity.Payment.Add(payment);

            _context.MemberPackages.Add(entity);
            await _context.SaveChangesAsync();

            return entity;
        }



        public async Task<MemberPackage> Delete(MemberPackage entity)
        {
            _context.MemberPackages.Remove(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<IReadOnlyList<MemberPackage>> GetAllAsync()
        {
            return await _context.MemberPackages.Include(m=>m.Member).Include(p=>p.Package).ToListAsync();
        }

        public async Task<MemberPackage> GetByIdAsync(int id)
        {
            return await _context.MemberPackages.Include(m => m.Member).Include(p => p.Package).FirstOrDefaultAsync(a=>a.MemberID==id);
        }

        public Task<MemberPackage> Update(int id, MemberPackage entity)
        {
            throw new NotImplementedException();
        }


        public async Task<List<MemberPackage>> GetDueMemberPackagesAsync()
        {
    
            var currentDate = DateTime.Now;

            var dueMemberPackages = await _context.MemberPackages
                .Include(mp => mp.Package)
                .Where(mp => mp.EndDate < currentDate && !_context.Payments.Any(p => p.MemberPackageID == mp.MemberPackageID && p.PaymentDate.Date == currentDate.Date))
                .ToListAsync();

            return dueMemberPackages;



                //var dueMemberPackages = await _context.MemberPackages
                //    .Include(mp => mp.Package)
                //    .Where(mp => mp.EndDate < currentDate && !mp.IsActive)
                //    .ToListAsync();

        }


    }
}
