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

        public void UpdateBook(Book book)
        {
            // No need to do anything, as entity framework will update the object through SaveChanges
        }

        void IBookRepository.CreateBook(Book book)
        {
            if (book == null)
            {
                throw new ArgumentNullException(nameof(book));
            }
            
            book.CreatedAt = DateTime.Now;
            book.UpdatedAt = DateTime.Now;

            _context.Books.Add(book);
        }

        void IBookRepository.DeleteBook(Book book)
        {
            if (book == null)
            {
                throw new ArgumentNullException(nameof(book));
            }

            _context.Books.Remove(book);
        }

        bool IBookRepository.SaveChanges()
        {
            return _context.SaveChanges() >= 0;
        }
    }
}