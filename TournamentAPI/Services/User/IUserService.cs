using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TournamentAPI.DTOs;

namespace TournamentAPI.Services.User
{
    public interface IUserService
    {
        Task Create(CreateUserDTO user);
        Task<ResponseLogin> Login(string password, string email);
    }
}