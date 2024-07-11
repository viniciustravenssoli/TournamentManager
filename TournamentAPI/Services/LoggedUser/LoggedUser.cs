using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TournamentAPI.Data;
using TournamentAPI.Models;
using TournamentAPI.Services.Token;

namespace TournamentAPI.Services.LoggedUser
{
    public class LoggedUser : ILoggedUser
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly TokenController _tokenController;
        private readonly ApplicationDbContext _usuarioRepository;

        public LoggedUser(IHttpContextAccessor contextAccessor, TokenController tokenController, ApplicationDbContext usuarioRepository)
        {
            _contextAccessor = contextAccessor;
            _tokenController = tokenController;
            _usuarioRepository = usuarioRepository;
        }

        public async Task<Models.User> GetUser()
        {
            var authorization = _contextAccessor.HttpContext.Request.Headers["Authorization"].ToString();

            var token = authorization["Bearer".Length..].Trim();

            var userEmail = _tokenController.GetEmail(token);

            return await _usuarioRepository.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Email.Equals(userEmail));
        }
    }
}