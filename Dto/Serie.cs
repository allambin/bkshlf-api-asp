using System;
using System.Collections.Generic;

namespace BKSHLF.Dto
{
    public class Serie
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<Book> Books { get; set; }
    }
}