using System.Text.RegularExpressions;

namespace TournamentAPI.Models;

public class Tournament
{
    public int TournamentId { get; set; }
    public string Name { get; set; }
    public ICollection<Participant> Participants { get; set; }
    public ICollection<Match> Matches { get; set; }
}