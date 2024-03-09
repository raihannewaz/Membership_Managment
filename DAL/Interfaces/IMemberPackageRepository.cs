using Membership_Management.DAL.Interfaces;
using Membership_Managment.Models;
using Microsoft.EntityFrameworkCore;

namespace Membership_Managment.DAL.Interfaces
{
    public interface IMemberPackageRepository:IGenericRepository<MemberPackage>
    {
        Task<List<MemberPackage>> GetDueMemberPackagesAsync();
        
    }
}
