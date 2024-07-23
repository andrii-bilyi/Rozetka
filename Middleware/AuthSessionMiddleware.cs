//using Rozetka.Services;
//using System.Security.Claims;

//namespace Rozetka.Middleware
//{
//    public class AuthSessionMiddleware
//    {
//        private readonly RequestDelegate _next;
//        public AuthSessionMiddleware(RequestDelegate next)
//        {
//            _next = next;
//        }

//        public async Task Invoke(HttpContext context, IUserService _userService)
//        {
//            if (context.Session.Keys.Contains("AuthUserId"))
//            {
//                //var user = _userService.Users.

//                var userId = context.Session.GetString("AuthUserId");
//                var user = await _userService.GetUserByIdAsync(userId);

//                if (user != null)
//                {
//                    var claims = new Claim[]
//                    {
//                        //как здесь правильно занести данные user в Claim
//                        new Claim(ClaimTypes.Sid, user.Id),
//                        new Claim(ClaimTypes.Name, user.FirstName),
//                        new Claim(ClaimTypes.Email, user.UserEmail),
//                        new Claim(ClaimTypes.Role, user.Role),
//                        new Claim(ClaimTypes.GivenName, user.LastName),
//                        new(ClaimTypes.SerialNumber, user.UserPhone)
//                    };
//                    context.User = new ClaimsPrincipal(new ClaimsIdentity(claims, nameof(AuthSessionMiddleware)));
//                    //запитати кошик та покласти у context
//                    //context.Items["ShoppingCart"] = user.Cart;
//                    //context.Items["WishList"] = user.Wish;
//                    //context.Items["Purchase"] = user.Purchases;

//                }
//            }

//            await _next(context);
//        }
//    }
//}

using Rozetka.Services;
using System.Security.Claims;

namespace Rozetka.Middleware
{
    public class AuthSessionMiddleware
    {
        private readonly RequestDelegate _next;

        public AuthSessionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, IUserService userService)
        {
            if (context.Session.Keys.Contains("AuthUserId"))
            {
                var userId = context.Session.GetString("AuthUserId");
                if (Guid.TryParse(userId, out Guid userGuid))
                {
                    var user = await userService.GetUserByIdAsync(userGuid);

                    if (user != null)
                    {
                        var claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.Sid, user.Id.ToString()),
                            new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}"),
                            new Claim(ClaimTypes.Email, user.Email ?? string.Empty),
                            new Claim(ClaimTypes.MobilePhone, user.PhoneNumber ?? string.Empty)

                        };

                        context.User = new ClaimsPrincipal(new ClaimsIdentity(claims, nameof(AuthSessionMiddleware)));
                    }
                }
            }

            await _next(context);
        }
    }
}

