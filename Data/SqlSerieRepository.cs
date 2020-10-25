using System;
using System.Linq;
using BKSHLF.Models;
using Microsoft.EntityFrameworkCore;

namespace BKSHLF.Data
{
    public class SqlSerieRepository : SqlBaseRepository, ISerieRepository
    {
        public SqlSerieRepository(BkshlfContext context) : base(context)
        {
        }

        public void CreateSerie(Serie serie)
        {
            if (serie == null)
            {
                throw new ArgumentNullException(nameof(serie));
            }
            
            serie.CreatedAt = DateTime.Now;
            serie.UpdatedAt = DateTime.Now;

            _context.Series.Add(serie);
        }

        public Serie GetSerie(int id)
        {
            return _context.Series.Include("BooksSeries").Include("BooksSeries.Book").FirstOrDefault(b => b.Id == id);
        }

        public void RemoveBookFromSerie(Serie serie, Book book)
        {
            _context.BooksSeries
                .RemoveRange(_context.BooksSeries.Where(x => x.BookId == book.Id).Where(x => x.SerieId == serie.Id));

        }

        public void AddBookToSerie(Serie serie, Book book, int order, int type = 0)
        {
            var jointModel = new BookSerie {
                Serie = serie,
                Book = book,
                Order = order,
                Type = (BookSerie.BookType)type
            };

            _context.BooksSeries.Add(jointModel);
        }

        public void UpdateSerie(Serie serie)
        {
            serie.UpdatedAt = DateTime.Now;
        }

        public void DeleteSerie(Serie serie)
        {
            if (serie == null)
            {
                throw new ArgumentNullException(nameof(serie));
            }

            _context.BooksSeries.RemoveRange(_context.BooksSeries.Where(x => x.SerieId == serie .Id));
            _context.Series.Remove(serie);
        }
    }
}