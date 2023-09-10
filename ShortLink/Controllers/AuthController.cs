using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using ShortLink.Entity;
using ShortLink.Models;
using ShortLink.Script;
using System;

namespace ShortLink.Controllers
{
    public class AuthController : Controller
    {
        private readonly EntityDb context;
        public AuthController(EntityDb appDbContext)
        {
            this.context = appDbContext;
        }
        // GET: AuthController
        public ActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(Users user)
        {

            if (user.email != null && user.password != null)
            {
                try
                {
                    user.createdAt = DateTime.UtcNow;
                    // Хеширование пароля
                    user.password = HashS.HashPassword(user.password);
                    if (context.users.FirstOrDefault(u=>u.email == user.email) != null)
                    {
                        return View("Registration");
                    }
                    context.users.Add(user);
                    context.SaveChanges();
                    return Redirect("/");
                }
                catch(NpgsqlException ex) 
                {
                    // Обработка ошибки
                    if (ex.InnerException is InvalidOperationException innerException)
                    {
                        if (innerException.InnerException is InvalidOperationException nestedInnerException)
                        {
                            if (nestedInnerException.Message.Contains("Can't close, connection is in state Connecting"))
                            {
                                // Повторная попытка закрытия подключения
                                context.Database.CloseConnection();
                            }
                        }
                    }
                }
            }
            return View("Registration");
        }
    }
}
