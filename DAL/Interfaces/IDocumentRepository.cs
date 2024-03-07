using Membership_Management.DAL.Interfaces;
using Membership_Managment.Models;
using Microsoft.AspNetCore.Mvc;

namespace Membership_Managment.DAL.Interfaces
{
    public interface IDocumentRepository : IGenericRepository<Document>
    {
        Task<Document> AddNew(int memberId, Document document);
        Task<Document> UpdateDoc(int memberId, Document document);
        string GetNidByMember(int id);
        string GetUtilityByMember(int id);


    }
}
