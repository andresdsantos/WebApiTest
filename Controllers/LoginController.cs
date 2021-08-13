using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Security.Claims;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using WebApiJwt.Domain;
using WebApiJwt.Models;

namespace WebApiJwt.Controllers
{
    public class LoginController : ApiController
    {
        private readonly IUserRepository userRepository;
        public LoginController(IUserRepository repository)
        {
            this.userRepository = repository;
        }
        //UserRepository userRepository = new UserRepository();
        public HttpResponseMessage Authenticate(LoginRequest login)
        {
            var loginResponse = new LoginResponse { };
            LoginRequest loginRequest = new LoginRequest { };
            IHttpActionResult response;

            if (login == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            if (!ModelState.IsValid)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ApiHelper.getErrors(ModelState));
            }


            loginRequest.Username = login.Username.ToLower();
            loginRequest.Password = login.Password;


            bool IsUserNamePasswordValid = false;

            if (login != null)
                IsUserNamePasswordValid = userRepository.IsValid(loginRequest.Username, loginRequest.Password);

            if (IsUserNamePasswordValid)
            {
                var token = ApiHelper.createToken(loginRequest.Username);
                return Request.CreateResponse(HttpStatusCode.OK, token);
            }
            else
            {

                loginResponse.responseMsg.StatusCode = HttpStatusCode.Unauthorized;
                response = ResponseMessage(loginResponse.responseMsg);
                return Request.CreateResponse(HttpStatusCode.Unauthorized, "Invalid user or password");
            }

        }



    }
}
