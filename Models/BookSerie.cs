using System;
using System.ComponentModel.DataAnnotations;

namespace BKSHLF.Models
{
    public class BookSerie
    {
        public enum BookType
        {
            Primary, 
            Secondary
        }

        [Required]
        public Guid BookId { get; set; }
        public Book Book { get; set; }
        
        [Required]
        public int SerieId { get; set; }
        public Serie Serie { get; set; }
        
        [Required]
        public int Order { get; set; }
        
        [Required]
        public BookType Type { get; set; }
    }
}