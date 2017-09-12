using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TwilioApp.Backend.Auth.Filters;
using TwilioApp.Backend.Twilio.Controllers.Objects;
using TwilioApp.Backend.Twilio.Libraries;

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


    [JwtAuth]
    [HttpPost("sendSms")]
    public async Task<IActionResult> SendSms()
    {
      var sendSms =
        Common.Libraries.Json.DeserializeFromStream<SendSms>(this.Request.Body);

      var messageResponse = await Task.Run(() => TwilioAPI.SendSms(sendSms.PhoneNumber, sendSms.Message));
      if (!(messageResponse.ErrorCode is null))
      {
        return BadRequest(Json(new {error = messageResponse.ErrorCode}));
      }
      return Ok(new {status = true, statusMessage = "Message sent!"});
    }
  }
}
