using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;
using TournamentAPI.Data;
using TournamentAPI.Services.Token;

namespace TournamentAPI.Filters
{
    public class AuthenticatedUserAttribute : AuthorizeAttribute, IAsyncAuthorizationFilter
    {
        private readonly TokenController _tokenController;
        private readonly ApplicationDbContext _context;

        public AuthenticatedUserAttribute(TokenController tokenController, ApplicationDbContext context)
        {
            _tokenController = tokenController;
            _context = context;
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            try
            {
                var token = OnRequestToken(context);
                var email = _tokenController.GetEmail(token);

                var user = _context.Users.FirstOrDefault(x => x.Email.Equals(email));

                if (user is null)
                {
                    throw new Exception();
                }
            }
            catch (SecurityTokenExpiredException)
            {
                TokenExpired(context);
            }
            catch
            {
                UserWithoutPermission(context);
            }
        }

        private string OnRequestToken(AuthorizationFilterContext context)
        {
            var authorization = context.HttpContext.Request.Headers["Authorization"].ToString();

            if (string.IsNullOrWhiteSpace(authorization))
            {
                throw new Exception();
            }

            return authorization["Bearer".Length..].Trim();
        }

        private void TokenExpired(AuthorizationFilterContext filterContext)
        {
            filterContext.Result = new UnauthorizedObjectResult("Expired Token");
        }

        private void UserWithoutPermission(AuthorizationFilterContext filterContext)
        {
            filterContext.Result = new UnauthorizedObjectResult("No Perm");
        }
    }
}