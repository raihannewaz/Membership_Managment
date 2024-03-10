using Membership_Managment.Context;
using Membership_Managment.DAL.Interfaces;
using Membership_Managment.Models;
using Microsoft.EntityFrameworkCore;

namespace Membership_Managment.DAL.Repositories
{
    public class PaymentRepository : IPaymentRepository
    {

        private readonly ApplicationDbContext _context;

        public PaymentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Payment> Add(Payment entity)
        {
            try
            {

                var memberPackage = await _context.MemberPackages.FindAsync(entity.MemberPackageID);
                if (memberPackage == null)
                {
                    throw new ArgumentException("Invalid MemberPackageID");
                }


                var package = await _context.Packages.FindAsync(memberPackage.PackageID);
                if (package == null)
                {
                    throw new ArgumentException("Invalid package ID");
                }

                decimal paymentAmount;
                int plusAmount;

                if (package.PaymentType == "Daily")
                {
                    paymentAmount = Convert.ToDecimal((package.PackagePrice * memberPackage.Quantity) * 0.10m);

                    if (entity.ActualAmount > paymentAmount)
                    {
                        decimal a = Convert.ToDecimal(entity.ActualAmount - paymentAmount);
                        plusAmount = Convert.ToInt32(a / paymentAmount);
                        entity.AdvancePaymentDuration = plusAmount;
                    }

                }
                else if (package.PaymentType == "Monthly")
                {
                    paymentAmount = Convert.ToDecimal((package.PackagePrice * memberPackage.Quantity) * 0.30m);

                    if (entity.ActualAmount > paymentAmount)
                    {
                        decimal a = Convert.ToDecimal(entity.ActualAmount - paymentAmount);
                        plusAmount = Convert.ToInt32(a / paymentAmount);
                        entity.AdvancePaymentDuration = plusAmount;
                    }
                }
                else
                {
                    throw new ArgumentException("Invalid payment type");
                }



                var newPayment = new Payment
                {
                    MemberPackageID = entity.MemberPackageID,
                    Amount = paymentAmount,
                    PaymentDate = DateTime.Now,
                    PaymentType = package.PaymentType,
                    PaidInAdvance = entity.PaidInAdvance,
                    AdvancePaymentDuration = entity.AdvancePaymentDuration,
                    ActualAmount = entity.ActualAmount
                };


                var duePayment = await _context.DuePayments.FirstOrDefaultAsync(dp => dp.MemberPackageID == entity.MemberPackageID);
                if (duePayment != null)
                {
                    
                    UpdateDuePayment(duePayment, (decimal)entity.ActualAmount);

                    AdjustMemberPackageEndDate(memberPackage, (decimal)entity.ActualAmount);
                }






                if (entity.PaidInAdvance == true)
                {
                    memberPackage.EndDate = memberPackage.EndDate.Value.AddDays(-entity.AdvancePaymentDuration);
                }



                _context.Payments.Add(newPayment);
                await _context.SaveChangesAsync();

                return entity;
            }
            catch (Exception ex)
            {
                throw new ArgumentNullException("Error: ", ex);
            }

        }


        private void UpdateDuePayment(DuePayment duePayment, decimal actualAmount)
        {
         
            decimal remainingDue = (decimal)(duePayment.Amount - actualAmount);
            if (remainingDue <= 0)
            {
                _context.DuePayments.Remove(duePayment);
            }
            else
            {

                duePayment.Amount = remainingDue;
                duePayment.DueDate = DateTime.Now;
            }
        }

        private void AdjustMemberPackageEndDate(MemberPackage memberPackage, decimal actualAmount)
        {

            double remainingDuration = (double)(actualAmount / memberPackage.Package.PackagePrice);

            memberPackage.EndDate = memberPackage.EndDate.Value.AddDays(remainingDuration);
        }





        public Task<Payment> Delete(Payment entity)
        {
            throw new NotImplementedException();
        }

        public async Task<IReadOnlyList<Payment>> GetAllAsync()
        {
           return await _context.Payments.Include(a=>a.MemberPackage).ToListAsync();
        }

        public async Task<Payment> GetByIdAsync(int id)
        {
            return await _context.Payments.FirstOrDefaultAsync(a => a.MemberPackage.MemberID == id);

        }

        public Task<Payment> Update(int id, Payment entity)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Payment>> GetByMemberIdAsync(int memberId)
        {
            var feeCollections = await _context.Payments
                .Where(fc => fc.MemberPackage.MemberID == memberId)
                .ToListAsync();
            return feeCollections;
        }
    }
}
