using System;
using System.ComponentModel.DataAnnotations;

namespace BKSHLF.Dto
{
    public class BookRequest
    {
        [Required]
        public string Title { get; set; }
        public int? AuthorId { get; set; }
    }
}