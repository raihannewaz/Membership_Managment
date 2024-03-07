using Membership_Managment.Models;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography;
using System.Text.Json.Serialization;

namespace Membership_Managment.Models
{
    public class Document
    {
        [Key]
        public int DocId { get; set; }
        public string? DocumentType { get; set; }
        public string? DocumentLocation { get; set; }
  
        public int MemberId { get; set;}

        [JsonIgnore]
        public Member? Member { get; set; }

        public string? ActionType { get; set; }
        public DateTime? ActionDate { get; set; }

        [NotMapped]
        public IFormFile? NidFile { get; set; }
        [NotMapped]
        public IFormFile? UtilityFile { get; set; }



        public enum EDocType
        {
            Nid,
            UtilityBill
        }
    }
}



