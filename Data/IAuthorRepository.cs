using System;
using System.Collections.Generic;
using BKSHLF.Models;

namespace BKSHLF.Data
{
    public interface IAuthorRepository
    {
        bool SaveChanges();

        IEnumerable<Author> GetAllAuthors();
        Author GetAuthor(int id);
        Author GetAuthorWithBooks(int id);
        void CreateAuthor(Author author);
        void CreateAuthor(Author author, Book book);
        void UpdateAuthor(Author author);
        void DeleteAuthor(Author author);
    }
}