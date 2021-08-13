using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApiJwt.Domain;
using WebApiJwt.Models;

namespace WebApiJwt.Controllers
{

    public class UserController : ApiController
    {
        private readonly IUserRepository userRepository;
        public UserController(IUserRepository repository)
        {
            this.userRepository = repository;
        }
        // GET api/values
      
        // GET api/values/5

        // POST api/
        // [valu
        [Authorize]
        [HttpGet]
        public HttpResponseMessage Get(string id)
        {
            UserViewModel model = new UserViewModel();

            var user = userRepository.GetByUsername(id);

            if (user == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "the user id not found");
            }
            else
            {
                model.email = user.Email;
                model.password = "";
                model.userName = user.Username;
                model.firstName = user.FirstName;
                model.lastName = user.LastName;
                model.phone = user.Phone;
                return Request.CreateResponse(HttpStatusCode.OK, model);
            }



        }
        [AllowAnonymous]
        [HttpPost]
        public HttpResponseMessage Register(UserViewModel userVM)
        {
            HttpResponseMessage response = new HttpResponseMessage { };
            if (userVM == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            if (ModelState.IsValid)
            {
                var userAdded = false;

                if (userRepository.GetByUsername(userVM.userName) == null)
                {
                    userAdded = userRepository.AddUser(new User
                    {
                        Username = userVM.userName,
                        Email = userVM.email,
                        FirstName = userVM.firstName,
                        LastName = userVM.lastName,
                        PasswordHash = MD5Hash.Hash.Content(userVM.password),
                        Phone = userVM.phone
                    });
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "The user is already taken");
                }
                if (userAdded)
                {

                    if (userVM.addresses != null)
                    {

                        userVM.addresses.ForEach(a =>
                        {
                            userRepository.AddAddress(new Address
                            {
                                City = a.city,
                                State = a.state,
                                Street = a.street,
                                Username = userVM.userName,
                                ZipCode = a.zipCode
                            });
                        });
                    }
                }

                if (userAdded)
                {
                    var token = ApiHelper.createToken(userVM.userName);
                    response = Request.CreateResponse(HttpStatusCode.OK, token);
                }
                else
                {
                    response = Request.CreateErrorResponse(HttpStatusCode.BadRequest, "User was not created");
                }
            }
            else
            {
                response = Request.CreateResponse(HttpStatusCode.BadRequest, ApiHelper.getErrors(ModelState));
            }
            return response;
        }

        // PUT api/values/5
        [Authorize]
        public HttpResponseMessage Put(string id, [FromBody] UserViewModel model)
        {
            if (model == null || id == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "the user data not found");
            }
            else
            {
                if (!ModelState.IsValid)
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, ApiHelper.getErrors(ModelState));
                }


                var userUpdated = userRepository.UpdateUser(new User
                {
                    Email = model.email,
                    FirstName = model.firstName,
                    LastName = model.lastName,
                    PasswordHash = MD5Hash.Hash.Content(model.password),
                    Phone = model.phone

                });
                if (userUpdated)
                {
                    return Request.CreateResponse(HttpStatusCode.OK);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "the user was not updated");
                }
            }

        }

        [Authorize]
        // DELETE api/values/5
        public HttpResponseMessage Delete(string id)
        {
            if (id == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            else
            {
                var userDeleted = userRepository.RemoveUser(id);

                if (userDeleted)
                {
                    return Request.CreateResponse(HttpStatusCode.OK);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "the user was not deleted");
                }

            }
        }
    }
}
