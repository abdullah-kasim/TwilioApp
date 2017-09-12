using Microsoft.AspNetCore.Mvc;

namespace TwilioApp.Backend.Common.Controllers
{
    public class Main : Controller
    {
        // GET
        public IActionResult Index()
        {
            return Ok();
        }
    }
}