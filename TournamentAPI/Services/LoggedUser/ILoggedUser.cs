using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TournamentAPI.Models;

namespace TournamentAPI.Services.LoggedUser
{
    public interface ILoggedUser
    {
         Task<Models.User> GetUser();
    }
}