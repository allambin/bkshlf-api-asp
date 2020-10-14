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
    [Route("api/authors")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly IAuthorRepository _repository;
        private readonly IMapper _mapper;

        public AuthorsController(IAuthorRepository repository, IMapper mapper)
        {
            _repository = repository;
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
            _repository.CreateAuthor(author);
            if(_repository.SaveChanges() == false)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError); 
            }

            return CreatedAtAction("GetAuthor", new {Id = author.Id}, _mapper.Map<Dto.Author>(author));
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
    }
}