using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Membership_Managment.Models
{
    public class FeeCollection
    {
        [Key]
        public int CollectionId { get; set; }

        public DateTime CollectionDate { get; set; }

        public string? CollectionType { get; set; }
        public decimal Amount { get; set; }

        public int MemberId { get; set; }

        [JsonIgnore]
        public Member? Member { get; set; }
    }
}
