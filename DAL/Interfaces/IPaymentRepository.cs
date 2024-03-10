using Membership_Management.DAL.Interfaces;
using Membership_Managment.Models;

namespace Membership_Managment.DAL.Interfaces
{
    public interface IPaymentRepository:IGenericRepository<Payment>
    {
        Task<List<Payment>> GetByMemberIdAsync(int memberId);
    }
}
