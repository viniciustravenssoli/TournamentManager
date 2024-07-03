using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TournamentAPI.DTOs
{
    public class TournamentDto
    {
        public int TournamentId { get; set; }
        public string Name { get; set; }
        public List<ParticipantDto> Participants { get; set; }
        public List<MatchDto> Matches { get; set; }
    }

    // DTOs/ParticipantDto.cs
    public class ParticipantDto
    {
        public int ParticipantId { get; set; }
        public string Name { get; set; }
    }

    // DTOs/MatchDto.cs
    public class MatchDto
    {
        public int MatchId { get; set; }
        public int TournamentId { get; set; }
        public int Participant1Id { get; set; }
        public string Participant1Name { get; set; }
        public int Participant2Id { get; set; }
        public string Participant2Name { get; set; }
        public int? WinnerId { get; set; }
        public string WinnerName { get; set; }
        public int Round { get; set; }
    }
}