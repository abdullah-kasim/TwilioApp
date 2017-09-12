using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Threading.Tasks;
using JWT;
using Microsoft.AspNetCore.Mvc;

namespace TwilioApp.Backend.Auth.Filters
{
    public class JwtAuthAttribute : ResultFilterAttribute
    {
        private readonly string _name;
        private readonly string _value;

        public JwtAuthAttribute()
        {
            
        }

        public override void OnResultExecuting(ResultExecutingContext context)
        {
            // let's validate our jwt
            var objectResult = new ObjectResult(new {});
            try
            {
                Libraries.Auth.IsValidJwt(context.HttpContext.Request.Headers["Authorization"].ToString());
            }
            catch (ArgumentException)
            {
                objectResult.StatusCode = 403;
                objectResult.Value = new {error = "Unable to parse the JWT token. Probably wrong format"};
                context.Result = objectResult;
            }
            catch (TokenExpiredException)
            {
                objectResult.StatusCode = 403;
                objectResult.Value = new {error = "Token has expired"};
                context.Result = objectResult;
            }
            catch (SignatureVerificationException)
            {
                objectResult.StatusCode = 403;
                objectResult.Value = new {error = "The JWT Token's signature doesn't match"};
                context.Result = objectResult;
            }
        }

        public override async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
//            var headers = context.HttpContext.Request.Headers;
//            var authorizationHeader = headers["Authorization"].ToString();
            await base.OnResultExecutionAsync(context, next);
        }
    }
}