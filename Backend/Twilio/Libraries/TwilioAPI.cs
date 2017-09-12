using System;
using System.Reflection;
using System.Threading.Tasks;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace TwilioApp.Backend.Twilio.Libraries
{
    public class TwilioAPI
    {
        public static readonly Lazy<string> AccountSid = new Lazy<string>(() => System.Environment.GetEnvironmentVariable("TWILIO_ACCOUNT_SID"));

        public static readonly Lazy<string> AuthToken = new Lazy<string>(() => System.Environment.GetEnvironmentVariable("TWILIO_AUTH_TOKEN"));

        public static readonly Lazy<string> DefaultPhoneNumber = new Lazy<string>(() => System.Environment.GetEnvironmentVariable("TWILIO_DEFAULT_PHONE_NUMBER"));

        public static MessageResource SendSms(string toPhoneNumber, string message, string fromPhoneNumber = null)
        {
            if (fromPhoneNumber is null)
            {
                fromPhoneNumber = DefaultPhoneNumber.Value;
            }
            TwilioClient.Init(AccountSid.Value, AuthToken.Value);

            var payload = MessageResource.Create(
                to: new PhoneNumber(toPhoneNumber),
                from: new PhoneNumber(fromPhoneNumber),
                body: message);
            return payload;
        }
    }
}
