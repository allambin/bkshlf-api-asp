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

        public void DeleteAuthor(Author author)
        {
            if (author == null)
            {
                throw new ArgumentNullException(nameof(author));
            }

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