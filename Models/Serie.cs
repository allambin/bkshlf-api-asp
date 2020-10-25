using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using BKSHLF.Models;

namespace BKSHLF.Models
{
    public class Serie : Model, IHasTimestamp
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public ICollection<BookSerie> BooksSeries { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set ; }
    }
}