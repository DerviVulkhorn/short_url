using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ShortLink.Middleware
{
    public class JwtTokenMiddleware
    {
        private readonly RequestDelegate _next;

        public JwtTokenMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var token = context.Request.Cookies["token"]; // Получаем токен из куки (предполагая, что токен хранится в куках)

            if (!string.IsNullOrEmpty(token))
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes("AAAA9831FR34182HR2839EE"); // Замените на ваш секретный ключ

                try
                {
                    tokenHandler.ValidateToken(token, new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    }, out var validatedToken);

                    // Если токен валидный, мы можем извлечь нужные данные из него
                    var jwtToken = (JwtSecurityToken)validatedToken;
                    var userEmail = jwtToken.Claims.First(x => x.Type == "email").Value;
                    // Мы можем сохранить идентификатор пользователя в контексте для последующего использования
                    context.Items["UserEmail"] = userEmail;
                }
                catch
                {
                    //Console.Write(token);
                    context.Response.Cookies.Delete("token");
                }
            }

            await _next(context);
        }
    }
}
