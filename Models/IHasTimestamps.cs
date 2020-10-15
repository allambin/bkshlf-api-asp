using System;

namespace BKSHLF.Models
{
    public interface IHasTimestamp
    {
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}