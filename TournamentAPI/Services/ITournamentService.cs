using TournamentAPI.DTOs;
using TournamentAPI.Models;

namespace TournamentAPI.Services;

public interface ITournamentService
{
    Task<List<TournamentDto>> GetAllTournamentsAsync();
    Task<TournamentDto> GetTournamentDetailsAsync(int id);
    Task<bool> SetMatchWinnerAsync(int matchId, int winnerId);
    Task CreateTournamentAsync(Tournament tournament, List<Participant> participants);
    Task<Match> FindById(int id);
}
