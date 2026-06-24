using Microsoft.AspNetCore.Mvc;
using TaskManagerInAspNet.Migrations;

namespace TaskManagerInAspNet.Servicios
{
    public interface IUserServices
    {
        string GetUserId();
    }

    public class UserService : IUserServices
    {
        private readonly HttpContext httpContext;

        public UserService(IHttpContextAccessor httpContextAccessor)
        {
            httpContext = httpContextAccessor.HttpContext;
        }

        public string GetUserId()
        {
            if (httpContext.User.Identity.IsAuthenticated)
            {
                var idClaim = httpContext.User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
                if (idClaim == null)
                {
                    throw new Exception("Usuario no autenticado");
                }
                return idClaim;
            }
            else
            {
                throw new Exception("Usuario no autenticado");
            }
        }
    }
}
