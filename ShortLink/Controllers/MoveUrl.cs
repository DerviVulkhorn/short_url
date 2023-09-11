using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShortLink.Entity;
using ShortLink.Models;

namespace ShortLink.Controllers
{
    public class MoveUrl : Controller
    {
        private readonly EntityDb context;
        public MoveUrl(EntityDb appDbContext)
        {
            this.context = appDbContext;
        }

        [HttpGet("/s/{value}")]
        public IActionResult Move(string value)
        {
            var code = context.codes.FirstOrDefault(u=>u.code== value);
            var originalLink = context.originalLinks
            .FirstOrDefault(link => link.code.Any(c => c.id == code.id));
            if (originalLink != null)
            {
                code.countMove++;
                context.SaveChanges();
                return Redirect(originalLink.link);
            }
            else
            {
                return Redirect("/home");
            }
            
        }
    }
}
