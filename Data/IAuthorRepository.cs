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
        void CreateAuthor(Author author);
        void UpdateAuthor(Author author);
        void DeleteAuthor(Author author);
    }
}