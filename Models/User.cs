using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Identity;

namespace BKSHLF.Models
{
    public class User : IdentityUser
    {
        [NotMapped]
        [JsonIgnore]
        public string Password { get; set; }
    }
}