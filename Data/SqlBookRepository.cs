using System;
using System.Collections.Generic;
using System.Linq;
using BKSHLF.Models;

namespace BKSHLF.Data
{
    public class SqlBookRepository : SqlBaseRepository, IBookRepository
    {
        public SqlBookRepository(BkshlfContext context) : base(context)
        {
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
            book.UpdatedAt = DateTime.Now;
        }

        public void CreateBook(Book book)
        {
            if (book == null)
            {
                throw new ArgumentNullException(nameof(book));
            }
            
            book.CreatedAt = DateTime.Now;
            book.UpdatedAt = DateTime.Now;

            _context.Books.Add(book);
        }

        public void CreateBook(Book book, Author author)
        {
            CreateBook(book);
            
            if (author == null)
            {
                throw new ArgumentNullException(nameof(author));
            }

            var ba = new BookAuthor {
                Author = author,
                Book = book
            };

            _context.BookAuthors.Add(ba);
        }

        public void DeleteBook(Book book)
        {
            if (book == null)
            {
                throw new ArgumentNullException(nameof(book));
            }
 
            _context.BookAuthors.RemoveRange(_context.BookAuthors.Where(ba => ba.BookId == book.Id));
            _context.Books.Remove(book);
        }
    }
}