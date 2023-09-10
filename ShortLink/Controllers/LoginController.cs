using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ShortLink.Entity;
using ShortLink.Models;
using ShortLink.Script;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ShortLink.Controllers
{
    public class LoginController : Controller
    {
        private readonly EntityDb context;
        public LoginController(EntityDb appDbContext)
        {
            this.context = appDbContext;
        }
        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Loginer(Users users)
        {
            var checkUsers = context.users.SingleOrDefault(x => x.email == users.email && x.password == HashS.HashPassword(users.password));
            if (checkUsers == null)
            {
                return View("Login");
            }
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("AAAA9831FR34182HR2839EE");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Email, users.email.ToString()),
               }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
             };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);
            // Устанавливаем токен в куки
            Response.Cookies.Append("token", tokenString, new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.Now.AddDays(7) // Устанавливаем срок истечения куки
            });
            Console.Write(tokenString);
            return Redirect("/");
        }
    }
}
