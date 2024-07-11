using Microsoft.EntityFrameworkCore;
using TournamentAPI.Data;
using TournamentAPI.DTOs;
using TournamentAPI.Models;
using TournamentAPI.Services.LoggedUser;

namespace TournamentAPI.Services;

public class TournamentService : ITournamentService
{
    private readonly ApplicationDbContext _context;
    private readonly ILoggedUser _loggedUser;

    public TournamentService(ApplicationDbContext context, ILoggedUser loggedUser)
    {
        _context = context;
        _loggedUser = loggedUser;
    }

    public async Task<List<TournamentDto>> GetAllTournamentsAsync()
    {
        var loggedUser = await _loggedUser.GetUser();

        return await _context.Tournaments
        .Where(t => t.UserId == loggedUser.UserId)
            .Select(t => new TournamentDto
            {
                TournamentId = t.TournamentId,
                Name = t.Name,
                Participants = t.Participants.Select(p => new ParticipantDto
                {
                    ParticipantId = p.ParticipantId,
                    Name = p.Name
                }).ToList(),
                Matches = t.Matches.Select(m => new MatchDto
                {
                    MatchId = m.MatchId,
                    TournamentId = m.TournamentId,
                    Participant1Id = m.Participant1Id,
                    Participant1Name = m.Participant1.Name,
                    Participant2Id = m.Participant2Id,
                    Participant2Name = m.Participant2.Name,
                    WinnerId = m.WinnerId,
                    WinnerName = m.Winner != null ? m.Winner.Name : null,
                    Round = m.Round
                }).ToList()
            }).ToListAsync();
    }

    public async Task<TournamentDto> GetTournamentDetailsAsync(int id)
    {
        return await _context.Tournaments
            .Where(t => t.TournamentId == id)
            .Select(t => new TournamentDto
            {
                TournamentId = t.TournamentId,
                Name = t.Name,
                Participants = t.Participants.Select(p => new ParticipantDto
                {
                    ParticipantId = p.ParticipantId,
                    Name = p.Name
                }).ToList(),
                Matches = t.Matches.Select(m => new MatchDto
                {
                    MatchId = m.MatchId,
                    TournamentId = m.TournamentId,
                    Participant1Id = m.Participant1Id,
                    Participant1Name = m.Participant1.Name,
                    Participant2Id = m.Participant2Id,
                    Participant2Name = m.Participant2.Name,
                    WinnerId = m.WinnerId,
                    WinnerName = m.Winner != null ? m.Winner.Name : null,
                    Round = m.Round
                }).ToList()
            }).FirstOrDefaultAsync();
    }

    public async Task<bool> SetMatchWinnerAsync(int matchId, int winnerId)
    {
        var match = await _context.Matches.FindAsync(matchId);

        if (match == null)
        {
            return false;
        }

        match.WinnerId = winnerId;
        await _context.SaveChangesAsync();

        if (AreAllMatchesComplete(match.TournamentId))
        {
            GenerateNextRoundMatches(match.TournamentId);
        }

        return true;
    }

    public async Task CreateTournamentAsync(Tournament tournament, List<Participant> participants)
    {
        var loggedUser = await _loggedUser.GetUser();

        tournament.UserId = loggedUser.UserId;
        tournament.Participants = participants;
        
        _context.Add(tournament);
        await _context.SaveChangesAsync();
        GenerateInitialMatches(tournament);
    }

    private bool AreAllMatchesComplete(int tournamentId)
    {
        var tournament = _context.Tournaments
            .Include(t => t.Matches)
            .FirstOrDefault(t => t.TournamentId == tournamentId);

        return tournament.Matches
            .Where(m => m.Round == tournament.Matches.Max(r => r.Round))
            .All(m => m.WinnerId != null);
    }

    private void GenerateNextRoundMatches(int tournamentId)
    {
        var tournament = _context.Tournaments
            .Include(t => t.Matches)
                .ThenInclude(m => m.Winner)
            .FirstOrDefault(t => t.TournamentId == tournamentId);

        if (tournament == null)
        {
            return;
        }

        var winners = tournament.Matches
            .Where(m => m.WinnerId != null)
            .GroupBy(m => m.Round)
            .OrderByDescending(g => g.Key)
            .FirstOrDefault()
            ?.Select(m => m.Winner)
            .Distinct()
            .ToList();

        if (winners == null || winners.Count <= 1)
        {
            return;
        }

        var currentRound = tournament.Matches.Max(m => m.Round);
        var nextRoundMatches = new List<Match>();

        for (int i = 0; i < winners.Count; i += 2)
        {
            if (i + 1 < winners.Count)
            {
                nextRoundMatches.Add(new Match
                {
                    TournamentId = tournamentId,
                    Participant1Id = winners[i].ParticipantId,
                    Participant2Id = winners[i + 1].ParticipantId,
                    Round = currentRound + 1
                });
            }
        }

        _context.Matches.AddRange(nextRoundMatches);
        _context.SaveChanges();
    }

    private void GenerateInitialMatches(Tournament tournament)
    {
        var shuffledParticipants = tournament.Participants.OrderBy(p => Guid.NewGuid()).ToList();

        var matches = new List<Match>();
        for (int i = 0; i < shuffledParticipants.Count; i += 2)
        {
            if (i + 1 < shuffledParticipants.Count)
            {
                matches.Add(new Match
                {
                    TournamentId = tournament.TournamentId,
                    Participant1Id = shuffledParticipants[i].ParticipantId,
                    Participant2Id = shuffledParticipants[i + 1].ParticipantId,
                    Round = 1
                });
            }
        }

        _context.Matches.AddRange(matches);
        _context.SaveChanges();
    }

    public async Task<Match> FindById(int id)
    {
        return await _context.Matches.FindAsync(id);
    }
}
