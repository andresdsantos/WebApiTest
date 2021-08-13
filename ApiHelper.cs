using System;
using System.Collections.Generic;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Http.ModelBinding;

namespace WebApiJwt
{
    public static class ApiHelper
    {
        public static string createToken(string username)
        {
            DateTime issuedAt = DateTime.UtcNow;
            DateTime expires = DateTime.UtcNow.AddMinutes(10);

            var tokenHandler = new JwtSecurityTokenHandler();

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(new[] {
            new Claim(ClaimTypes.Name,username)
            });

             string sec = ConfigurationManager.AppSettings["secret"];

            var now = DateTime.UtcNow;
            var securityKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(System.Text.Encoding.Default.GetBytes(sec));

            var signingCredencials = new Microsoft.IdentityModel.Tokens.SigningCredentials(securityKey,
                Microsoft.IdentityModel.Tokens.SecurityAlgorithms.HmacSha256Signature);

            var token = (JwtSecurityToken)tokenHandler.CreateJwtSecurityToken(issuer: "http://localhost:44349/",
                        audience: "http://localhost:44349/", subject: claimsIdentity, notBefore: issuedAt, expires: expires,
                        signingCredentials: signingCredencials);

            var tokenString = tokenHandler.WriteToken(token);
            return tokenString;
        }
        public static List<string> getErrors(System.Web.Http.ModelBinding.ModelStateDictionary modelState)
        {
            List<string> errors = new List<string>();
            foreach (var state in modelState)
            {
                foreach (var error in state.Value.Errors)
                {
                    errors.Add(error.ErrorMessage);
                }
            }
            return errors;
        }
    }

}