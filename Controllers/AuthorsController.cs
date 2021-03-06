using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using AutoMapper;
using BKSHLF.Data;
using BKSHLF.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace BKSHLF.Controllers
{
    [Route("api/authors")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly IAuthorRepository _repository;
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;

        public AuthorsController(IAuthorRepository repository, IBookRepository bookRepository, IMapper mapper)
        {
            _repository = repository;
            _bookRepository = bookRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Book>> GetAllAuthors()
        {
            var authors = _repository.GetAllAuthors();

            return Ok(_mapper.Map<IEnumerable<Dto.Author>>(authors));
        }

        [HttpGet("{id}"), ActionName("GetAuthor")]
        public ActionResult<Book> GetAuthor(int id)
        {
            var author = _repository.GetAuthor(id);
            if (author == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<Dto.Author>(author));
        }

        [HttpPost]
        public ActionResult<Dto.Author> CreateAuthor(Dto.AuthorRequest authorRequest)
        {
            var author = _mapper.Map<Author>(authorRequest);
            Book book = null;
            if (authorRequest.BookId != null) 
            {
                book = _bookRepository.GetBook(authorRequest.BookId);
            }
            _repository.CreateAuthor(author, book);
            if(_repository.SaveChanges() == false)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError); 
            }

            return CreatedAtAction("GetAuthor", new {Id = author.Id}, _mapper.Map<Dto.Author>(author));
        }
        
        [HttpPut("{id}")]
        public ActionResult UpdateAuthor(int id, Dto.AuthorRequest authorRequest)
        {
            var author = _repository.GetAuthor(id);
            if (author == null)
            {
                return NotFound();
            }

            _mapper.Map(authorRequest, author);
            _repository.UpdateAuthor(author);
            if(_repository.SaveChanges() == false)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError); 
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteAuthor(int id)
        {
            var author = _repository.GetAuthor(id);
            if (author == null)
            {
                return NotFound();
            }

            _repository.DeleteAuthor(author);
            if(_repository.SaveChanges() == false)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError); 
            }

            return NoContent();
        }

        [HttpGet("{id}/books")]
        public ActionResult<IEnumerable<Author>> GetAllAuthorBooks(int id)
        {
            var author = _repository.GetAuthorWithBooks(id);

            return Ok(_mapper.Map<Dto.AuthorWithBooks>(author));
        }

        [HttpDelete("{id}/books/{bookId}")]
        public ActionResult DeleteBookFromAuthor(int id, Guid bookId)
        {
            var author = _repository.GetAuthor(id);
            if (author == null)
            {
                return NotFound();
            }
            var book = _bookRepository.GetBook(bookId);
            if (book == null)
            {
                return NotFound();
            }

            _repository.DeleteBookAuthor(author, book);
            if(_repository.SaveChanges() == false)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError); 
            }

            return NoContent();
        }

        [HttpPut("{id}/books/{bookId}")]
        public ActionResult AddBookToAuthor(int id, Guid bookId)
        {
            var author = _repository.GetAuthor(id);
            if (author == null)
            {
                return NotFound();
            }
            var book = _bookRepository.GetBook(bookId);
            if (book == null)
            {
                return NotFound();
            }

            _repository.CreateBookAuthor(author, book);
            try
            {
                if(_repository.SaveChanges() == false)
                {
                    return new StatusCodeResult(StatusCodes.Status500InternalServerError); 
                }
            }
            catch
            {
                return BadRequest("This book is already linked to this author.");
            }
            

            return NoContent();
        }
    }
}