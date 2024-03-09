using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Membership_Managment.Models
{
    public class DuePayment
    {
        [Key]
        public int DuePaymentID { get; set; }
        public int MemberPackageID { get; set; }
        [JsonIgnore]
        public MemberPackage? MemberPackage { get; set; }
        public DateTime? DueDate { get; set; }
        public decimal? Amount { get; set; }
    }
}
