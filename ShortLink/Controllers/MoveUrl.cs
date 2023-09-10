using Microsoft.AspNetCore.Mvc;

namespace ShortLink.Controllers
{
    public class MoveUrl : Controller
    {
        [HttpGet("{value}")]
        public IActionResult Move(string value)
        {
            return Ok(value);
        }
    }
}
