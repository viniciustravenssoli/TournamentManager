using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TournamentAPI.Models;

namespace TournamentAPI.DTOs
{
    public class CreateTournamentRequest
    {
        public string TournamentName { get; set; }
        public List<Participant> Participants { get; set; }
    }
}