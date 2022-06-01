using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IdentityModel;

namespace InvoicierWebApiV1.Core.Shared.Helpers
{
    public class CustomAuthorizeAttributeMVC : AuthorizeAttribute
    {
        private readonly string[] rolesParams;
        public CustomAuthorizeAttributeMVC(params string[] roles)
        {
            this.rolesParams = roles;
        }

        public bool IsAuthorized
        {
            get
            {
                //Do your authorization logic here and return true if the current user has permission/role for the passed "rolesParams"
                string[] allowedRoles = new string[] { "User", "Admin" };

                return allowedRoles.Intersect(rolesParams).Any(); //for the example
            }
        }

        //protected override bool AuthorizeCore(HttpContextBase httpContext)
        //{
        //    return this.IsAuthorized;
        //}

        //protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        //{
        //    //...
        //}
    }

    public class AuthorizeHelper
    {
        public static bool HasPermission(params string[] roles)
        {
            return new CustomAuthorizeAttributeMVC(roles).IsAuthorized;
        }
    }
}
