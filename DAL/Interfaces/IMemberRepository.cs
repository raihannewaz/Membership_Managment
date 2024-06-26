﻿using Membership_Management.DAL.Interfaces;
using Membership_Managment.Models;

namespace Membership_Managment.DAL.Interfaces
{
    public interface IMemberRepository:IGenericRepository<Member>
    {
        Task UpdateExpiredMembersStatus();
        Task<IReadOnlyList<Member>> GetAllMembersWithPackages();
    }
}
