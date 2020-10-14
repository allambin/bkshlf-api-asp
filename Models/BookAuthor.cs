using System;
using System.ComponentModel.DataAnnotations;

namespace BKSHLF.Models
{
    public class BookAuthor
    {
        [Required]
        public Guid BookId { get; set; }
        public Book Book { get; set; }
        [Required]
        public int AuthorId { get; set; }
        public Author Author { get; set; }
        [Required]
        public Author.AuthorType Type { get; set; }
    }
}