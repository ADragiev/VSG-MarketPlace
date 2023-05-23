using Application.Helpers.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Helpers.Attributes
{
    public class AdminAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            bool hasNonAdminAttribute = context.ActionDescriptor.EndpointMetadata
           .Any(em => em.GetType() == typeof(NonAdminAttribute));

            if (!hasNonAdminAttribute && !context.HttpContext.User.HasClaim(IdentityConstants.AdminRoleClaimName, IdentityConstants.AdminRoleClaimValue))
            {
                context.Result = new ForbidResult();
            }
        }
    }
}
