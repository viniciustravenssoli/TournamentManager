using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using TournamentAPI.Data;
using TournamentAPI.DTOs;
using TournamentAPI.Services.Password;
using TournamentAPI.Services.Token;

namespace TournamentAPI.Services.User
{
    public class UserService : IUserService
    {
        private readonly PasswordEncryption _passwordEncryption;
        private readonly ApplicationDbContext _dbContext;
        private readonly TokenController _tokenController;

        public UserService(PasswordEncryption passwordEncryption, ApplicationDbContext dbContext, TokenController tokenController)
        {
            _passwordEncryption = passwordEncryption;
            _dbContext = dbContext;
            _tokenController = tokenController;
        }

        public async Task Create(CreateUserDTO request)
        {
            var entity = new Models.User
            {
                Username = request.UserName,
                Email = request.Email,
                PasswordHash = _passwordEncryption.Criptograph(request.Password)
            };

            await _dbContext.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<ResponseLogin?> Login(string password, string email)
        {
            var hashedPasswordRequest = _passwordEncryption.Criptograph(password);

            var user = await _dbContext.Users
            .FirstOrDefaultAsync(c => c.Email.Equals(email) && c.PasswordHash.Equals(hashedPasswordRequest));

            if (user == null)
            {
                return null;
            }

            return new ResponseLogin
            {
                Name = user.Username,
                Token = _tokenController.GenerateToken(email),
            };

        }
    }
}