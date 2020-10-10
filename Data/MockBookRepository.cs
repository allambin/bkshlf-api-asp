using System;
using System.Collections.Generic;
using BKSHLF.Models;

namespace BKSHLF.Data
{
    public class MockBookRepository : IBookRepository
    {
        public IEnumerable<Book> GetAllBooks()
        {
            return new List<Book>
            {
                new Book { Id = Guid.NewGuid(), Title = "Jurassic Park" },
                new Book { Id = Guid.NewGuid(), Title = "Lost World" },
                new Book { Id = Guid.NewGuid(), Title = "Jurassoc World" },
            };
        }

        public Book GetBook(Guid id)
        {
            return new Book
            {
                Id = Guid.NewGuid(),
                Title = "Jurassic Park"
            };
        }
    }
}