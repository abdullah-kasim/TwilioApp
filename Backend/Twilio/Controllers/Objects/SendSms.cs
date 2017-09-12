using Newtonsoft.Json;

namespace TwilioApp.Backend.Twilio.Controllers.Objects
{
  public class SendSms
  {
    [JsonProperty("message")]
    public string Message { get; set; }
    [JsonProperty("phoneNumber")]
    public string PhoneNumber { get; set; }
  }
}
