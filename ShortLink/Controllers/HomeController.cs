using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using ShortLink.Entity;
using ShortLink.Models;
using ShortLink.Script;
using System.Diagnostics;

namespace ShortLink.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly EntityDb context;

        public HomeController(ILogger<HomeController> logger, EntityDb appDbContext)
        {
            _logger = logger;
            this.context = appDbContext;
        }


        public IActionResult Index()
        {
            
            return View();
        }

        public IActionResult Privacy()
        {

            var userEmail = HttpContext.Items["UserEmail"] as string;
            var user = context.users
       .Include(u => u.OriginalLink) // Включить связанные сущности OriginalLink
           .ThenInclude(ol => ol.code) // Включить связанные сущности Code
       .FirstOrDefault(u => u.email == userEmail);
            if(user != null)
            {
                return View(user);
            }
            else
            {
                return View(new Users());
            }
            

            
        }

        

        [HttpPost]
        public IActionResult AddUrl(OriginalLink originalLink) {
            if (originalLink.link != null)
            {
                try
                {
                    var userEmail = HttpContext.Items["UserEmail"] as string;
                    Users users = context.users.FirstOrDefault(x=>x.email==userEmail);
                    if (users.OriginalLink == null)
                    {
                        users.OriginalLink = new List<OriginalLink>();
                    }
                    users.OriginalLink.Add(originalLink);
                    context.SaveChanges();
                    OriginalLink link = context.originalLinks.FirstOrDefault(x => x.link == originalLink.link);
                    if (link.code == null)
                    {
                        link.code = new List<Code>();
                    }
                    Code code = new Code();
                    code.code = GenerateCode.GenCode();
                    link.code.Add(code);
                    context.SaveChanges();
                }
                catch (NpgsqlException ex)
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
            return Redirect("Privacy");

        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}