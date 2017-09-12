using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace TwilioApp.Backend.Auth.Controllers.Objects
{
    public class Credentials
    {
        [JsonProperty("username")]
        public string Username { get; set; }
        [JsonProperty("password")]
        public string Password { get; set; }
    }
}