using System.ComponentModel.DataAnnotations;

namespace Membership_Managment.Models
{
    public class Package
    {
        [Key]
        public int PackageId { get; set; }


        public string? PackageName { get; set; }
     
        public decimal? PackagePrice { get; set; }
    
        public string? PaymentType { get; set; }

       
        public int? Duration { get; set; }

        public bool? IsActive { get; set; }

        public virtual ICollection<MemberPackage>? MemberPackage { get; set; }

    }
}
