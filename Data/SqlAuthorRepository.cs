using System;
using System.Collections.Generic;
using System.Linq;
using BKSHLF.Models;

namespace BKSHLF.Data
{
    public class SqlAuthorRepository : SqlBaseRepository, IAuthorRepository
    {
        public SqlAuthorRepository(BkshlfContext context) : base(context)
        {
        }

        public void CreateAuthor(Author author)
        {
            if (author == null)
            {
                throw new ArgumentNullException(nameof(author));
            }
            
            author.CreatedAt = DateTime.Now;
            author.UpdatedAt = DateTime.Now;

            _context.Authors.Add(author);
        }

        public void CreateAuthor(Author author, Book book)
        {
            CreateAuthor(author);

            if (book == null)
            {
                throw new ArgumentNullException(nameof(book));
            }

            var ba = new BookAuthor {
                Author = author,
                Book = book
            };

            _context.BookAuthors.Add(ba);
        }

        public void DeleteAuthor(Author author)
        {
            if (author == null)
            {
                throw new ArgumentNullException(nameof(author));
            }

            _context.BookAuthors.RemoveRange(_context.BookAuthors.Where(ba => ba.AuthorId == author.Id));
            _context.Authors.Remove(author);
        }

        public IEnumerable<Author> GetAllAuthors()
        {
            return _context.Authors.ToList();
        }

        public Author GetAuthor(int id)
        {
            return _context.Authors.FirstOrDefault(a => a.Id == id);
        }

        public void UpdateAuthor(Author author)
        {
            author.UpdatedAt = DateTime.Now;
        }
    }
}