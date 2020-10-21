using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BKSHLF.Models
{
    public class Edition : IHasTimestamp
    {
        public enum FormatType
        {
            Paperback, 
            Hardcover,
            MassMarketPaperback,
            Kindle,
            Nook,
            Ebook,
            Audiobook,
            Other
        }

        [Key]
        public int Id { get; set; }
        
        [Required]
        [Column(TypeName="varchar(10)")]
        public string ISBN { get; set; }
        
        [Column(TypeName="varchar(13)")]
        public string ISBN13 { get; set; }
        public int NumberOfPages { get; set; }
        public string Language { get; set; }

        [Required]
        public FormatType Format { get; set; }

        [Required]
        public Book Book { get; set; }
        public Publisher Publisher { get; set; }
        public DateTime? PublishedAt { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}