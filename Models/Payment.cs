using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Membership_Managment.Models
{
    public class Payment
    {
        [Key]
        public int PaymentID { get; set; }
        public int MemberPackageID { get; set; }
        [JsonIgnore]
        public MemberPackage? MemberPackage { get; set; }

       
        public decimal? Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public string? PaymentType { get; set; }
        public int AdvancePaymentDuration { get; set; }
        public bool? PaidInAdvance { get; set; }
        public decimal? ActualAmount { get; set; }
    }
}
