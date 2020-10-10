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
    public class EventDetailController : ControllerBase
    {
        private readonly IRepository<EventDetail> _eventDetailRepository;
        private readonly IUnitOfWork<HollywoodDbContext> _unitOfWork;
        private readonly IMapper _mapper;

        public EventDetailController(IRepository<EventDetail> eventDetailRepository, IUnitOfWork<HollywoodDbContext> unitOfWork, IMapper mapper)
        {
            _eventDetailRepository = eventDetailRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEventDetails()
        {
            var events = await _eventDetailRepository.LazyGetAll().ToListAsync();
            return Ok(_mapper.Map<List<EventResponse>>(events));
        }

        [HttpGet]
        [Route("{id:long}", Name = "GetEventDetailById")]
        public async Task<IActionResult> GetEventDetailById(long id)
        {
            var thisEventDetail = await _eventDetailRepository.LazyGet(t => t.EventDetailId == id).SingleOrDefaultAsync();

            if (thisEventDetail == null)
                return NotFound();

            return Ok(_mapper.Map<EventDetailResponse>(thisEventDetail));
        }

        [HttpPost]
        [ProducesResponseType(typeof(EventDetailResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status409Conflict)]
        public async Task<IActionResult> CreateEventDetail([FromBody] EventDetailRequest eventDetailRequest)
        {
            var nameExists = await _eventDetailRepository.LazyGet(t => t.EventDetailName == eventDetailRequest.EventDetailName).FirstOrDefaultAsync();

            if (nameExists != null)
                return Conflict(new ErrorResponse { Message = $"EventDetail name {eventDetailRequest.EventDetailName} already exists" });

            var newEventDetail = _mapper.Map<EventDetail>(eventDetailRequest);
            _eventDetailRepository.Insert(newEventDetail);

            await _unitOfWork.CommitAsync();

            return CreatedAtRoute(nameof(GetEventDetailById), new { id = newEventDetail.EventDetailId }, _mapper.Map<EventDetailResponse>(newEventDetail));
        }

        [HttpPut]
        [Route("{id:long}")]
        public async Task<IActionResult> UpdateEventDetail(long id, [FromBody] EventDetailRequest EventDetailRequest)
        {
            var thisEventDetail = await _eventDetailRepository.LazyGet(t => t.EventDetailId == id).SingleOrDefaultAsync();

            if (thisEventDetail == null)
                return NotFound();

            thisEventDetail.EventId = EventDetailRequest.EventId;
            thisEventDetail.EventDetailName = EventDetailRequest.EventDetailName;
            thisEventDetail.EventDetailNumber = EventDetailRequest.EventDetailNumber;
            thisEventDetail.EventDetailOdd = EventDetailRequest.EventDetailOdd;
            thisEventDetail.EventDetailStatusId = EventDetailRequest.EventDetailStatusId;
            thisEventDetail.FinishingPosition = EventDetailRequest.FinishingPosition;
            thisEventDetail.FirstTimer = EventDetailRequest.FirstTimer;

            _eventDetailRepository.Update(thisEventDetail);

            await _unitOfWork.CommitAsync();

            return Ok(_mapper.Map<EventDetailResponse>(thisEventDetail));
        }

        [HttpDelete]
        [Route("{id:long}")]
        public async Task<IActionResult> DeleteEventDetail(long id)
        {
            var thisEventDetail = await _eventDetailRepository.LazyGet(t => t.EventDetailId == id).SingleOrDefaultAsync();

            if (thisEventDetail == null)
                return NotFound();

            _eventDetailRepository.Delete(thisEventDetail);

            await _unitOfWork.CommitAsync();

            return Ok();
        }
    }
}