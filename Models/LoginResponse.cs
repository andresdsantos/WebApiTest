using System.Web.Http;
using System.Net.Http;
namespace WebApiJwt.Models
{
    public class LoginResponse
    {
        public LoginResponse()
        {
            this.Token = "";

            this.responseMsg = new HttpResponseMessage
            {
                StatusCode = System.Net.HttpStatusCode.Unauthorized
              
            };
        }
    public string Token { get; set; }
    public HttpResponseMessage responseMsg { get; set; }
        public object errors { get; set; }
    }
}