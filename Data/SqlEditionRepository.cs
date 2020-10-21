using System;
using System.Collections.Generic;
using System.Linq;
using BKSHLF.Models;

namespace BKSHLF.Data
{
    public class SqlEditionRepository : SqlBaseRepository, IEditionRepository
    {
        public SqlEditionRepository(BkshlfContext context) : base(context)
        {
        }

        public Edition GetEdition(int id)
        {
            return _context.Editions.FirstOrDefault(p => p.Id == id); 
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
    }
}