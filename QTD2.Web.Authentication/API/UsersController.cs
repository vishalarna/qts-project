using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Authorization;

namespace QTD2.Web.Authentication.API
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class UsersController : ControllerBase
    {
        protected SignInManager<Data.Entities.Authentication.AppUser> _signinManager;

        public UsersController(SignInManager<Data.Entities.Authentication.AppUser> signinManager)
        {
            _signinManager = signinManager;
        }

        public List<Data.Entities.Authentication.AppUser> Get()
        {
            throw new NotImplementedException();
        }

        public Data.Entities.Authentication.AppUser Get(string username)
        {
            throw new NotImplementedException();
        }

        public void Put(Data.Entities.Authentication.AppUser user)
        {
            throw new NotImplementedException();
        }

        public void Post(Data.Entities.Authentication.AppUser user)
        {
            throw new NotImplementedException();
        }

        public void Delete(Data.Entities.Authentication.AppUser user)
        {
            throw new NotImplementedException();
        }
    }
}
