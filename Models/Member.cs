using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Membership_Managment.Models
{
    public class Member
    {
        [Key]
        public int MemberId { get; set; }
        public string? Name { get; set; }
        public int? Phone {  get; set; }
        public string? PresentAddress { get; set; }
        public string? PermanentAddress { get; set; }
        public string? Photo { get; set; }
        public decimal? MembershipAmount { get; set; }
        public DateTime? Dob { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? ActivaitonDate { get; set; }
        public DateTime? ExpDate { get; set; }
        public bool? IsActive { get; set; }

        public string? ActionType { get; set; }
        public DateTime? ActionDate { get; set; }

        public virtual ICollection<Document>? DocumentList { get; set; }
        public virtual ICollection<FeeCollection>? FeeCollection { get; set; }
        public virtual ICollection<MemberPackage>? MemberPackage { get; set; }

        [NotMapped]
        public IFormFile? ImageFile { get; set; }



    }
}
