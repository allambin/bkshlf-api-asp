using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BKSHLF.Models
{
    public class Author : Model, IHasTimestamp
    {
        public enum AuthorType
        {
            Author, 
            CoAuthor,
            Translator
        }

        [Key]
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public AuthorType Type { get; set; }
        //public ICollection<Book> Books { get; set; }
        public ICollection<BookAuthor> BooksAuthors { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}