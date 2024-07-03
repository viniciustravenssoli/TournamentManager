using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TournamentAPI.Models;
using TournamentAPI.Services;

namespace TournamentAPI.Controllers;
[Route("api/[controller]")]
[ApiController]
public class TournamentsController : ControllerBase
{
    private readonly ITournamentService _tournamentService;

    public TournamentsController(ITournamentService tournamentService)
    {
        _tournamentService = tournamentService;
    }

    // GET: api/Tournaments
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Tournament>>> GetTournaments()
    {
        var tournaments = await _tournamentService.GetAllTournamentsAsync();
        return Ok(tournaments);
    }

    // GET: api/Tournaments/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Tournament>> GetTournament(int id)
    {
        var tournament = await _tournamentService.GetTournamentDetailsAsync(id);

        if (tournament == null)
        {
            return NotFound();
        }

        return Ok(tournament);
    }

    // POST: api/Tournaments/SetWinner
    [HttpPost("SetWinner")]
    public async Task<IActionResult> SetWinner([FromBody] SetWinnerRequest request)
    {
        var result = await _tournamentService.SetMatchWinnerAsync(request.MatchId, request.WinnerId);
        var match = await _tournamentService.FindById(request.MatchId);

        if (!result)
        {
            return NotFound();
        }

        return Ok();
    }

    // POST: api/Tournaments
    [HttpPost]
    public async Task<ActionResult<Tournament>> CreateTournament([FromBody] CreateTournamentRequest request)
    {
        var tournament = new Tournament
        {
            Name = request.TournamentName,
            Participants = request.Participants
        };

        await _tournamentService.CreateTournamentAsync(tournament, request.Participants);
        return Created();
    }
}

public class SetWinnerRequest
{
    public int MatchId { get; set; }
    public int WinnerId { get; set; }
}

public class CreateTournamentRequest
{
    public string TournamentName { get; set; }
    public List<Participant> Participants { get; set; }
}

