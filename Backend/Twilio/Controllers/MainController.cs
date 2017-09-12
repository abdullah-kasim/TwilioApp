using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TwilioApp.Backend.Auth.Filters;

namespace TwilioApp.Backend.Twilio.Controllers
{
    [Route("api/twilio/[controller]")]
    public class MainController : Controller
    {
        [JwtAuth]
        [HttpGet("test")]
        public async Task<IActionResult> Test()
        {
            var jsonToSend = new
            {
                Test = 123,
                Test2 = true
            };
            var test = await Task.Run(() => Ok(Json(jsonToSend)));
            return test;
        }
        
    }
}