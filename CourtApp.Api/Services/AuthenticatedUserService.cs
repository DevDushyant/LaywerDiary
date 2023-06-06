using CourtApp.Application.Interfaces.Shared;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace CourtApp.Api.Services
{
    public class AuthenticatedUserService : IAuthenticatedUserService
    {
        public AuthenticatedUserService(IHttpContextAccessor httpContextAccessor)
        {
            UserId = httpContextAccessor.HttpContext?.User?.FindFirstValue("uid");
            Username = httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Name) == null ? null : httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Name).Value;
            UserRole = httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Role) == null ? null : httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Role).Value;

        }

        public string UserId { get; }
        public string Username { get; }

        public string UserRole { get; }
    }
}