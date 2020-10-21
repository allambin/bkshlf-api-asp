using System;
using System.ComponentModel.DataAnnotations;

namespace BKSHLF.Dto
{
    public class EditionRequest
    {
        [Required]
        [MaxLength(10)]
        public string ISBN { get; set; }
        
        [MaxLength(13)]
        public string ISBN13 { get; set; }
        public int NumberOfPages { get; set; }
        public string Language { get; set; }

        [Required]
        public int Format { get; set; }

        [Required]
        public Guid BookId { get; set; }
        public string PublisherName { get; set; }
        public DateTime PublishedAt { get; set; }
    }
}