using System.Collections.Generic;

namespace BKSHLF.Dto
{
    public class AuthorWithBooks
    {
        public int Id { get; set; }
        public string FirstName { get; set; } 
        public string LastName { get; set; }
        public IEnumerable<Book> Books { get; set; }
    }
}