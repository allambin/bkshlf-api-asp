using System;
using System.Collections.Generic;
using AutoMapper;
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
        private readonly IMapper _mapper;

        public BooksController(IBookRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Book>> GetAllBooks()
        {
            var books = _repository.GetAllBooks();

            return Ok(_mapper.Map<IEnumerable<Dto.Book>>(books));
        }

        [HttpGet("{id}")]
        public ActionResult<Book> GetBook(Guid id)
        {
            var book = _repository.GetBook(id);
            if (book == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<Dto.Book>(book));
        }
    }
}