using System;
using System.Collections.Generic;
using AutoMapper;
using BKSHLF.Data;
using BKSHLF.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
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

        [HttpGet("{id}"), ActionName("GetBook")]
        public ActionResult<Book> GetBook(Guid id)
        {
            var book = _repository.GetBook(id);
            if (book == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<Dto.Book>(book));
        }

        [HttpPost]
        public ActionResult<Dto.Book> CreateBook(Dto.BookRequest bookRequest)
        {
            var book = _mapper.Map<Book>(bookRequest);
            _repository.CreateBook(book);
            if(_repository.SaveChanges() == false)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError); 
            }

            return CreatedAtAction("GetBook", new {Id = book.Id}, _mapper.Map<Dto.Book>(book));
        }

        [HttpPut("{id}")]
        public ActionResult UpdateBook(Guid id, Dto.BookRequest bookRequest)
        {
            var book = _repository.GetBook(id);
            if (book == null)
            {
                return NotFound();
            }

            _mapper.Map(bookRequest, book);
            _repository.UpdateBook(book);
            if(_repository.SaveChanges() == false)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError); 
            }

            return NoContent();
        }

        [HttpPatch("{id}")]
        public ActionResult PartialUpdateBook(Guid id, JsonPatchDocument<Dto.BookPatchRequest> patchDocument)
        {
            var book = _repository.GetBook(id);
            if (book == null)
            {
                return NotFound();
            }

            var bookToPatch = _mapper.Map<Dto.BookPatchRequest>(book);
            patchDocument.ApplyTo(bookToPatch, ModelState);
            if (!TryValidateModel(bookToPatch))
            {
                return ValidationProblem(ModelState);
            }

            _mapper.Map(bookToPatch, book);
            _repository.UpdateBook(book);
            if(_repository.SaveChanges() == false)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError); 
            }

            return NoContent();
        }
    }
}