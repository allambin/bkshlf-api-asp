using AutoMapper;
using BKSHLF.Data;
using BKSHLF.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BKSHLF.Controllers
{
    [Route("api/editions")]
    [ApiController]
    public class EditionsController : ControllerBase 
    {
        private readonly IEditionRepository _repository;
        private readonly IMapper _mapper;
        private readonly IBookRepository _bookRepository;
        private readonly IPublisherRepository _publisherRepository;

        public EditionsController(IEditionRepository repository, IBookRepository bookRepository, IPublisherRepository publisherRepository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
            _bookRepository = bookRepository;
            _publisherRepository = publisherRepository;
        }

        [HttpGet("{id}"), ActionName("GetEdition")]
        public ActionResult<Edition> GetEdition(int id)
        {
            var edition = _repository.GetEdition(id);
            if (edition == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<Dto.Edition>(edition));
        }

        [HttpPost]
        public ActionResult<Dto.Book> CreateEdition(Dto.EditionRequest request)
        {
            var edition = _mapper.Map<Edition>(request);
            var book = _bookRepository.GetBook(request.BookId);
            Publisher publisher = null;

            if (book == null)
            {
               return BadRequest("Book not found");
            }

            if (request.PublisherName != null) 
            {
                string publisherName = request.PublisherName;
                publisher = _publisherRepository.GetPublisher(publisherName);
                if (publisher == null)
                {
                    Publisher newPublisher = new Publisher
                    {
                        Name = request.PublisherName
                    };
                    _publisherRepository.CreatePublisher(newPublisher);
                    if(_publisherRepository.SaveChanges() == false)
                    {
                        return new StatusCodeResult(StatusCodes.Status500InternalServerError); 
                    }
                    publisher = _publisherRepository.GetPublisher(publisherName);
                }
            }

            _repository.CreateEdition(edition, book, publisher);

            if(_repository.SaveChanges() == false)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError); 
            }

            return CreatedAtAction("GetEdition", new {Id = edition.Id}, _mapper.Map<Dto.Edition>(edition));
        }

        [HttpPut("{id}")]
        public ActionResult UpdateEdition(int id, Dto.EditionRequest request)
        {
            var edition = _repository.GetEdition(id);
            if (edition == null)
            {
                return NotFound();
            }
            
            var book = _bookRepository.GetBook(request.BookId);
            Publisher publisher = null;

            if (book == null)
            {
               return BadRequest("Book not found");
            }

            if (request.PublisherName != null) 
            {
                string publisherName = request.PublisherName;
                publisher = _publisherRepository.GetPublisher(publisherName);
                if (publisher == null)
                {
                    Publisher newPublisher = new Publisher
                    {
                        Name = request.PublisherName
                    };
                    _publisherRepository.CreatePublisher(newPublisher);
                    if(_publisherRepository.SaveChanges() == false)
                    {
                        return new StatusCodeResult(StatusCodes.Status500InternalServerError); 
                    }
                    publisher = _publisherRepository.GetPublisher(publisherName);
                }
            }

            _mapper.Map(request, edition);
            _repository.UpdateEdition(edition, book, publisher);
            if(_repository.SaveChanges() == false)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError); 
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteEdition(int id)
        {
            var edition = _repository.GetEdition(id);
            if (edition == null)
            {
                return NotFound();
            }

            _repository.DeleteEdition(edition);
            if(_repository.SaveChanges() == false)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError); 
            }

            return NoContent();
        }
    }
}