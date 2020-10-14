using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BKSHLF.Models
{
    public class Book : Model, IHasTimestamp
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string Title { get; set; }
        //public ICollection<Author> Authors { get; set; }
        public ICollection<BookAuthor> BooksAuthors { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set ; }
    }
}