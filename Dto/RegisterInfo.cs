using System.ComponentModel.DataAnnotations;

namespace BKSHLF.Dto
{
    public class RegisterInfo
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}