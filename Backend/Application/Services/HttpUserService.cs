using Application.Helpers.Constants;
using Application.Models.UserModels.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class HttpUserService : IUserService
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        public HttpUserService(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }
        public string GetUserEmail()
        {
            return httpContextAccessor.HttpContext.User.Claims.First(c => c.Type == IdentityConstants.preferedUsername).Value;
        }
    }
}
