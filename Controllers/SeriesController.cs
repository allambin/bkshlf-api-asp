using System;
using System.Collections.Generic;
using AutoMapper;
using BKSHLF.Data;
using BKSHLF.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BKSHLF.Controllers
{
    [Route("api/series")]
    [ApiController]
    public class SeriesController : ControllerBase
    {
        private readonly ISerieRepository _repository;
        private readonly IBookRepository _bookRepository;

        // private readonly IAuthorRepository _authorRepository;
        private readonly IMapper _mapper;

        public SeriesController(ISerieRepository repository, IBookRepository bookRepository, IMapper mapper)
        {
            _repository = repository;
            _bookRepository = bookRepository;
            _mapper = mapper;
        }

        [HttpGet("{id}"), ActionName("GetSerie")]
        public ActionResult<Serie> GetSerie(int id)
        {
            var serie = _repository.GetSerie(id);
            if (serie == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<Dto.Serie>(serie));
        }

        [HttpPost]
        public ActionResult<Dto.Serie> CreateSerie(Dto.SerieRequest request)
        {
            var serie = _mapper.Map<Serie>(request);

            _repository.CreateSerie(serie);
            if(_repository.SaveChanges() == false)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError); 
            }

            return CreatedAtAction("GetSerie", new {Id = serie.Id}, _mapper.Map<Dto.Serie>(serie));
        }

        [HttpPut("{id}")]
        public ActionResult UpdateSerie(int id, Dto.SerieRequest request)
        {
            var serie = _repository.GetSerie(id);
            if (serie == null)
            {
                return NotFound();
            }

            _mapper.Map(request, serie);
            _repository.UpdateSerie(serie);
            if(_repository.SaveChanges() == false)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError); 
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteSerie(int id)
        {
            var serie = _repository.GetSerie(id);
            if (serie == null)
            {
                return NotFound();
            }

            _repository.DeleteSerie(serie);
            if(_repository.SaveChanges() == false)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError); 
            }

            return NoContent();
        }

        [HttpDelete("{id}/books/{bookId}")]
        public ActionResult RemoveBookFromSerie(int id, Guid bookId)
        {
            var serie = _repository.GetSerie(id);
            if (serie == null)
            {
                return NotFound();
            }
            var book = _bookRepository.GetBook(bookId);
            if (book == null)
            {
                return NotFound();
            }

            _repository.RemoveBookFromSerie(serie, book);
            if(_repository.SaveChanges() == false)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError); 
            }

            return NoContent();
        }

        [HttpPut("{id}/books/{bookId}")]
        public ActionResult AddBookToSerie(int id, Guid bookId, Dto.SerieBookRequest request)
        {
            var serie = _repository.GetSerie(id);
            if (serie == null)
            {
                return NotFound();
            }
            var book = _bookRepository.GetBook(bookId);
            if (book == null)
            {
                return NotFound();
            }

            _repository.AddBookToSerie(serie, book, request.Order, request.Type);
            try
            {
                if(_repository.SaveChanges() == false)
                {
                    return new StatusCodeResult(StatusCodes.Status500InternalServerError); 
                }
            }
            catch
            {
                return BadRequest("This book is already linked to this serie.");
            }
            

            return NoContent();
        }
    }
}