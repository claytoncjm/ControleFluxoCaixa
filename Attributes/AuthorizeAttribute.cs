using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ControleFluxoCaixa.Attributes
{
    // Decorator Pattern - Adiciona funcionalidade de autorização aos controllers
    public class AuthorizeAttribute : TypeFilterAttribute
    {
        public AuthorizeAttribute() : base(typeof(AuthorizeFilter))
        {
        }
    }

    public class AuthorizeFilter : IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (!context.HttpContext.User.Identity.IsAuthenticated)
            {
                context.Result = new RedirectToActionResult("Login", "Auth", null);
            }
        }
    }
}
