using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
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
    //[Authorize]
    [Route("[controller]")]
    [Produces("application/json")]
    public class TournamentController : ControllerBase
    {
        private readonly IRepository<DataAccess.Models.Tournament> _tournamentRepository;
        private readonly IRepository<Event> _eventRepository;
        private readonly IUnitOfWork<HollywoodDbContext> _unitOfWork;
        private readonly IMapper _mapper;

        public TournamentController(IRepository<DataAccess.Models.Tournament> tournamentRepository, IRepository<Event> eventRepository,
            IUnitOfWork<HollywoodDbContext> unitOfWork, IMapper mapper)
        {
            _tournamentRepository = tournamentRepository;
            _eventRepository = eventRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTournaments()
        {
            var tournaments = await _tournamentRepository.LazyGetAll().ToListAsync();
            return Ok(_mapper.Map<List<TournamentResponse>>(tournaments));
        }

        [HttpGet]
        [Route("{tournamentId}/events")]
        public async Task<IActionResult> GetEvents(long tournamentId)
        {
            var events = await _eventRepository.LazyGet(e => e.TournamentId == tournamentId).ToListAsync();
            return Ok(_mapper.Map<List<EventResponse>>(events));
        }

        [HttpGet]
        [Route("{id:long}", Name = "GetTournamentById")]
        public async Task<IActionResult> GetTournamentById(long id)
        {
            var tournament = await _tournamentRepository.LazyGet(t => t.TournamentId == id).SingleOrDefaultAsync();

            if (tournament == null)
                return NotFound();

            return Ok(_mapper.Map<TournamentResponse>(tournament));
        }

        [HttpGet]
        [Route("isNameValid/{name}")]
        public async Task<IActionResult> IsNameValid(string name)
        {
            var tournament = await _tournamentRepository.LazyGet(t => t.TournamentName == name).SingleOrDefaultAsync();

            if (tournament == null)
                return Ok(true);

            return Ok(false);
        }

        [HttpPost]
        [ProducesResponseType(typeof(TournamentResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status409Conflict)]
        public async Task<IActionResult> CreateTournament([FromBody] TournamentRequest tournamentRequest)
        {
            var nameExists = await _tournamentRepository.LazyGet(t => t.TournamentName == tournamentRequest.TournamentName).FirstOrDefaultAsync();

            if (nameExists != null)
                return Conflict(new ErrorResponse { Message = $"Tournament name {tournamentRequest.TournamentName} already exists" });

            var tournament = _mapper.Map<DataAccess.Models.Tournament>(tournamentRequest);
            _tournamentRepository.Insert(tournament);

            await _unitOfWork.CommitAsync();

            return CreatedAtRoute(nameof(GetTournamentById), new { id = tournament.TournamentId }, _mapper.Map<TournamentResponse>(tournament));
        }

        [HttpPut]
        [Route("{id:long}")]
        public async Task<IActionResult> UpdateTournament(long id, [FromBody] TournamentRequest tournamentRequest)
        {
            var thisTournament = await _tournamentRepository.LazyGet(t => t.TournamentId == id).SingleOrDefaultAsync();

            if (thisTournament == null)
                return NotFound();

            thisTournament.TournamentName = tournamentRequest.TournamentName;
            _tournamentRepository.Update(thisTournament);

            await _unitOfWork.CommitAsync();

            return Ok(_mapper.Map<TournamentResponse>(thisTournament));
        }

        [HttpDelete]
        [Route("{id:long}")]
        public async Task<IActionResult> DeleteTournament(long id)
        {
            var thisTournament = await _tournamentRepository.LazyGet(t => t.TournamentId == id)
                                                            .Include(t => t.Events)
                                                            .SingleOrDefaultAsync();

            if (thisTournament == null)
                return NotFound();


            if (thisTournament.Events.Any())
            {
                return BadRequest("This tournament has events associated with it and cannot be deleted");
            }

            _tournamentRepository.Delete(thisTournament);

            await _unitOfWork.CommitAsync();

            return Ok();
        }
    }
}
