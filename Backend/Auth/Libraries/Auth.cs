using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using JWT;
using JWT.Algorithms;
using JWT.Serializers;
using TwilioApp.Backend.Auth.Controllers.Objects;

namespace TwilioApp.Backend.Auth.Libraries
{
    public static class Auth
    {
        private static readonly Lazy<string> Secret = new Lazy<string>(() => System.Environment.GetEnvironmentVariable("JWT_SECRET"));
        
        public static string GetJwt(Credentials credentials)
        {
            // insert database validation here and throw an error later.
            var payload = new Dictionary<string, object>
            {
                { "user", credentials.Username },
                { "claim2", "claim2-value" }
            };

            IJwtAlgorithm algorithm = new HMACSHA256Algorithm();
            IJsonSerializer serializer = new JsonNetSerializer();
            IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
            IJwtEncoder encoder = new JwtEncoder(algorithm, serializer, urlEncoder);

            var token = encoder.Encode(payload, Secret.Value);
            return token;
        }

        public static bool IsValidJwt(string authorizationHeader)
        {
            var regex = new Regex(@"Bearer\s(.+)$");
            var matches = regex.Match(authorizationHeader);
            var jwt = matches.Groups["1"].Value;
            if (jwt is null)
            {
                throw new ArgumentException("The authorization header doesn't contain a valid jwt");
            }

            IJsonSerializer serializer = new JsonNetSerializer();
            IDateTimeProvider provider = new UtcDateTimeProvider();
            IJwtValidator validator = new JwtValidator(serializer, provider);
            IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
            IJwtDecoder decoder = new JwtDecoder(serializer, validator, urlEncoder);

            var json = decoder.Decode(jwt, Secret.Value, verify: true);
            Console.WriteLine(json);
            return true;
        }
    }
}