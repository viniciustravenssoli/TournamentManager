using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TournamentAPI.DTOs
{
    public class SetWinnerRequest
    {
        public int MatchId { get; set; }
        public int WinnerId { get; set; }
    }
}