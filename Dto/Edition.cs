using System;
using BKSHLF.Models;

namespace BKSHLF.Dto
{
    public class Edition
    {
        public int Id { get; set; }
        public string ISBN { get; set; }
        public string ISBN13 { get; set; }
        public int NumberOfPages { get; set; }
        public string Language { get; set; }
        public Models.Edition.FormatType Format { get; set; }
        public Book Book { get; set; }
        public Publisher Publisher { get; set; }
        public DateTime PublishedAt { get; set; }
    }
}