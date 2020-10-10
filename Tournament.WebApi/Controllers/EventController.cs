using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tournament.Core.Models;
using Tournament.DataAccess;
using Tournament.DataAccess.Interfaces;
using Tournament.DataAccess.Models;

namespace Tournament.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")]
    public class EventController : ControllerBase
    {
        private readonly IRepository<Event> _eventRepository;
        private readonly IUnitOfWork<HollywoodDbContext> _unitOfWork;
        private readonly IMapper _mapper;

        public EventController(IRepository<Event> eventRepository, IUnitOfWork<HollywoodDbContext> unitOfWork, IMapper mapper)
        {
            _eventRepository = eventRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEvents()
        {
            var events = await _eventRepository.LazyGetAll().ToListAsync();
            return Ok(_mapper.Map<List<EventResponse>>(events));
        }

        [HttpGet]
        [Route("{id:long}", Name = "GetEventById")]
        public async Task<IActionResult> GetEventById(long id)
        {
            var thisEvent = await _eventRepository.LazyGet(t => t.EventId == id).SingleOrDefaultAsync();

            if (thisEvent == null)
                return NotFound();

            return Ok(_mapper.Map<EventResponse>(thisEvent));
        }

        [HttpPost]
        [ProducesResponseType(typeof(EventResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status409Conflict)]
        public async Task<IActionResult> CreateEvent([FromBody] EventRequest eventRequest)
        {
            var nameExists = await _eventRepository.LazyGet(t => t.EventName == eventRequest.EventName).FirstOrDefaultAsync();

            if (nameExists != null)
                return Conflict(new ErrorResponse { Message = $"Event name {eventRequest.EventName} already exists" });

            var newEvent = _mapper.Map<Event>(eventRequest);
            _eventRepository.Insert(newEvent);

            await _unitOfWork.CommitAsync();

            return CreatedAtRoute(nameof(GetEventById), new { id = newEvent.EventId }, _mapper.Map<EventResponse>(newEvent));
        }

        [HttpPut]
        [Route("{id:long}")]
        public async Task<IActionResult> UpdateEvent(long id, [FromBody] EventRequest eventRequest)
        {
            var thisEvent = await _eventRepository.LazyGet(t => t.EventId == id).SingleOrDefaultAsync();

            if (thisEvent == null)
                return NotFound();

            thisEvent.TournamentId = eventRequest.TournamentId;
            thisEvent.EventName = eventRequest.EventName;
            thisEvent.EventNumber = eventRequest.EventNumber;
            thisEvent.EventDateTime = eventRequest.EventDateTime;
            thisEvent.EventEndDateTime = eventRequest.EventEndDateTime;
            thisEvent.AutoClose = eventRequest.AutoClose;

            _eventRepository.Update(thisEvent);

            await _unitOfWork.CommitAsync();

            return Ok(_mapper.Map<EventResponse>(thisEvent));
        }

        [HttpDelete]
        [Route("{id:long}")]
        public async Task<IActionResult> DeleteEvent(long id)
        {
            var thisEvent = await _eventRepository.LazyGet(t => t.EventId == id).SingleOrDefaultAsync();

            if (thisEvent == null)
                return NotFound();


            _eventRepository.Delete(thisEvent);

            await _unitOfWork.CommitAsync();

            return Ok();
        }
    }
}