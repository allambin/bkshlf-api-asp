using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BKSHLF.Models
{
    public class Publisher : IHasTimestamp
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        public ICollection<Edition> Editions { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}