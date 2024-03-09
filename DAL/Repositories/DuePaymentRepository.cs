using Membership_Managment.Context;
using Membership_Managment.DAL.Interfaces;
using Membership_Managment.Models;

namespace Membership_Managment.DAL.Repositories
{
    public class DuePaymentRepository : IDuePaymentRepository
    {
        private readonly IMemberPackageRepository _memberPackageRepository;
        private readonly ApplicationDbContext _context;


        public DuePaymentRepository(IMemberPackageRepository memberPackageRepository, ApplicationDbContext dbContext)
        {
            _memberPackageRepository = memberPackageRepository;
            _context = dbContext;
        }





        public Task<DuePayment> Add(DuePayment entity)
        {
            throw new NotImplementedException();
        }

        public Task<DuePayment> Delete(DuePayment entity)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<DuePayment>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<DuePayment> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }


        public Task<DuePayment> Update(int id, DuePayment entity)
        {
            throw new NotImplementedException();
        }



        public async Task HandleDuePayments()
        {
           
            var dueMemberPackages = await _memberPackageRepository.GetDueMemberPackagesAsync();

            foreach (var memberPackage in dueMemberPackages)
            {
               
                decimal dueAmount = CalculateDueAmount(memberPackage);

                DateTime dueDate = memberPackage.EndDate.Value.AddDays(1);

                var duePayment = new DuePayment
                {
                    MemberPackageID = memberPackage.MemberPackageID,
                    DueDate = dueDate,
                    Amount = dueAmount
                };

                ExtendMemberPackageEndDate(memberPackage, duePayment);
                await _context.DuePayments.AddAsync(duePayment);
            }


        }

        private decimal CalculateDueAmount(MemberPackage memberPackage)
        {
           
            var package = memberPackage.Package;
            decimal dueAmount = 0;

            if (package.PaymentType == "daily")
            {
                
                int daysMissed = (int)(DateTime.Now - memberPackage.EndDate).Value.TotalDays;

                dueAmount = Convert.ToDecimal((package.PackagePrice * memberPackage.Quantity) * daysMissed * 0.10m);
            }
            else if (package.PaymentType == "monthly")
            {

                if (DateTime.Now.Month != memberPackage.EndDate.Value.Month)
                {
                    dueAmount = Convert.ToDecimal((package.PackagePrice * memberPackage.Quantity) * 0.30m);
                }
            }

            return dueAmount;
        }



        private void ExtendMemberPackageEndDate(MemberPackage memberPackage, DuePayment duePayment)
        {
            var package = memberPackage.Package;
            if (package.PaymentType == "daily")
            {
              
                int daysMissed = (int)(DateTime.Now - memberPackage.EndDate).Value.TotalDays;

                memberPackage.EndDate = memberPackage.EndDate.Value.AddDays(daysMissed);
            }
            else if (package.PaymentType == "monthly")
            {
                memberPackage.EndDate = memberPackage.EndDate.Value.AddMonths(1);
            }
        }
    }
}
