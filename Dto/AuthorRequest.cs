using System;
using System.ComponentModel.DataAnnotations;

namespace BKSHLF.Dto
{
    public class AuthorRequest
    {
        [Required]
        public string FirstName { get; set; } 
        [Required]
        public string LastName { get; set; }
        public Guid BookId { get; set; }
    }
}