using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tournament.Core.Models;
using Tournament.DataAccess;
using Tournament.DataAccess.Interfaces;

namespace Tournament.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")]
    public class TournamentController : ControllerBase
    {
        private readonly IRepository<DataAccess.Models.Tournament> _tournamentRepository;
        private readonly IUnitOfWork<HollywoodDbContext> _unitOfWork;
        private readonly IMapper _mapper;

        public TournamentController(IRepository<DataAccess.Models.Tournament> tournamentRepository, IUnitOfWork<HollywoodDbContext> unitOfWork, IMapper mapper)
        {
            _tournamentRepository = tournamentRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTournaments()
        {
            var tournaments = await _tournamentRepository.LazyGetAll().ToListAsync();
            return Ok(tournaments);
        }

        [HttpGet]
        [Route("{id:long}", Name = "GetTournamentById")]
        public async Task<IActionResult> GetTournamentById(long id)
        {
            var tournament = await _tournamentRepository.LazyGet(t => t.TournamentId == id).SingleOrDefaultAsync();

            if (tournament == null)
                return NotFound();

            return Ok(tournament);
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
        public async Task<IActionResult> UpdateTournament(int id, [FromBody] TournamentRequest tournamentRequest)
        {
            var thisTournament = await _tournamentRepository.LazyGet(t => t.TournamentName == tournamentRequest.TournamentName).SingleOrDefaultAsync();

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
            var thisTournament = await _tournamentRepository.LazyGet(t => t.TournamentId == id).SingleOrDefaultAsync();

            if (thisTournament == null)
                return NotFound();

            
            _tournamentRepository.Delete(thisTournament);

            await _unitOfWork.CommitAsync();

            return Ok();
        }
    }
}
}
