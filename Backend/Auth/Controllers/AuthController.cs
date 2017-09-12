using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TwilioApp.Backend.Auth.Controllers.Objects;
using BCrypt.Net;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace TwilioApp.Backend.Auth.Controllers
{
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        [HttpPost("login")]
        public async Task<IActionResult> Login()
        {
            
            var credentials =
                Common.Libraries.Json.DeserializeFromStream<Credentials>(this.Request.Body);
            // username = superlongusername
            // password = superlongpassword, bcrypt hash = $2a$06$MsmJlwY6/NF3OZCvaXVYgu3JpgXLSV7HpeD3ldM0ztFlcFqvpm7eK
            if (credentials.Username != "superlongusername" || !BCrypt.Net.BCrypt.Verify(credentials.Password,
                    "$2a$06$MsmJlwY6/NF3OZCvaXVYgu3JpgXLSV7HpeD3ldM0ztFlcFqvpm7eK"))
                return await Task.Run(() => BadRequest(new {error = "These credentials do not exist."}));
            var jwt = Libraries.Auth.GetJwt(credentials);
            return await Task.Run(() => Ok(new {jwt = jwt}));
        }
    }
}