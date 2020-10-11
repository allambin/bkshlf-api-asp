using System;
using System.Collections.Generic;
using System.Linq;
using BKSHLF.Models;

namespace BKSHLF.Data
{
    public class SqlBookRepository : IBookRepository
    {
        private readonly BkshlfContext _context;

        public SqlBookRepository(BkshlfContext context)
        {
            _context = context;
        }

        public IEnumerable<Book> GetAllBooks()
        {
            return _context.Books.ToList();
        }

        public Book GetBook(Guid id)
        {
            return _context.Books.FirstOrDefault(b => b.Id == id);
        }
    }
}