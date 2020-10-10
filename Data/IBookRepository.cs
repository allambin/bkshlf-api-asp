using System;
using System.Collections.Generic;
using BKSHLF.Models;

namespace BKSHLF.Data
{
    public interface IBookRepository
    {
        IEnumerable<Book> GetAllBooks();
        Book GetBook(Guid id);
    }
}