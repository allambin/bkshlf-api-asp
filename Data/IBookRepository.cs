using System;
using System.Collections.Generic;
using BKSHLF.Models;

namespace BKSHLF.Data
{
    public interface IBookRepository
    {
        bool SaveChanges();

        IEnumerable<Book> GetAllBooks();
        Book GetBook(Guid id);
        void CreateBook(Book book);
        void CreateBook(Book book, Author author);
        void UpdateBook(Book book);
        void DeleteBook(Book book);
    }
}