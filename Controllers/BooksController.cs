using System;
using System.Collections.Generic;
using BKSHLF.Data;
using BKSHLF.Models;
using Microsoft.AspNetCore.Mvc;

namespace BKSHLF.Controllers
{
    [Route("api/books")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookRepository _repository;

        public BooksController(IBookRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Book>> GetAllBooks()
        {
            var books = _repository.GetAllBooks();

            return Ok(books);
        }

        [HttpGet("{id}")]
        public ActionResult<Book> GetBook(Guid id)
        {
            var book = _repository.GetBook(id);
            if (book == null)
            {
                return NotFound();
            }

            return Ok(book);
        }
    }
}