using System;
using System.Collections.Generic;
using System.Linq;
using BKSHLF.Models;
using Microsoft.EntityFrameworkCore;

namespace BKSHLF.Data
{
    public class SqlEditionRepository : SqlBaseRepository, IEditionRepository
    {
        public SqlEditionRepository(BkshlfContext context) : base(context)
        {
        }

        public Edition GetEdition(int id)
        {
            return _context.Editions.Include("Book").Include("Publisher").FirstOrDefault(p => p.Id == id); 
        }

        public void CreateEdition(Edition edition, Book book, Publisher publisher = null)
        {
            if (edition == null)
            {
                throw new ArgumentNullException(nameof(edition));
            }

            if (book == null)
            {
                throw new ArgumentNullException(nameof(book));
            }
            
            edition.CreatedAt = DateTime.Now;
            edition.UpdatedAt = DateTime.Now;

            edition.Book = book;
            edition.Publisher = publisher;

            _context.Editions.Add(edition);
        }

        public void DeleteEdition(Edition edition)
        {
            if (edition == null)
            {
                throw new ArgumentNullException(nameof(edition));
            }
 
            _context.Editions.Remove(edition);
        }

        public void UpdateEdition(Edition edition, Book book, Publisher publisher = null)
        {
            edition.UpdatedAt = DateTime.Now;

            edition.Book = book;
            edition.Publisher = publisher;
        }
    }
}