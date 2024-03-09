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
                if (package.PaymentType == "Daily")
                {
                    paymentAmount = Convert.ToDecimal((package.PackagePrice * memberPackage.Quantity) * 0.10m);
                }
                else if (package.PaymentType == "Monthly")
                {
                    paymentAmount = Convert.ToDecimal((package.PackagePrice * memberPackage.Quantity) * 0.30m);
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
                    AdvancePaymentDuration = entity.AdvancePaymentDuration
                };


                if (Convert.ToBoolean(entity.PaidInAdvance))
                {
                    memberPackage.StartDate = DateTime.Now;
                    memberPackage.EndDate = DateTime.Now.AddDays(entity.AdvancePaymentDuration);
                }


                if (entity.PaidInAdvance == false && memberPackage.EndDate < DateTime.Now)
                {

                    var duePayment = new DuePayment
                    {
                        MemberPackageID = entity.MemberPackageID,
                        DueDate = memberPackage.EndDate,
                        Amount = paymentAmount
                    };
                    _context.DuePayments.Add(duePayment);
                }

                _context.Payments.Add(newPayment);
                await _context.SaveChangesAsync();
                entity.PaymentID = newPayment.PaymentID;
                return entity;
            }
            catch (Exception ex)
            {
                throw new ArgumentNullException("Error: ", ex);
            }
        }

        public Task<Payment> Delete(Payment entity)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<Payment>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Payment> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Payment> Update(int id, Payment entity)
        {
            throw new NotImplementedException();
        }
    }
}
