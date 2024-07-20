using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Rozetka.Filters
{
    public class CustomAuthorizationFilter : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            // Проверяем принадлежность пользователя к роли "Admin"
            if (!context.HttpContext.User.IsInRole("admin"))
            {
                // Если у него нет этой роли, то отказать в доступе и вернуть статус forbidden
                context.Result = new StatusCodeResult(StatusCodes.Status403Forbidden);
            }
        }
    }
}
