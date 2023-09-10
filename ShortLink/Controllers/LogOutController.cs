using Microsoft.AspNetCore.Mvc;

namespace ShortLink.Controllers
{
    public class LogOutController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public LogOutController(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public IActionResult Index()
        {
            var context = _httpContextAccessor.HttpContext;
            context.Response.Cookies.Delete("token");
            return Redirect("/");
        }
    }
}
