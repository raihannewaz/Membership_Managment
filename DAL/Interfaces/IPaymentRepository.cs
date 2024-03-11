using Membership_Management.DAL.Interfaces;
using Membership_Managment.Models;

namespace Membership_Managment.DAL.Interfaces
{
    public interface IPaymentRepository:IGenericRepository<Payment>
    {
        Task<List<Payment>> GetByMemberIdAsync(int memberId);
        Task<List<(string Name, string PackageName, DateTime NextPaymentDate)>> GetNextPaymentDates();
        Task<DateTime?> GetNextPaymentDateByMemberId(int memberId);
    }
}
