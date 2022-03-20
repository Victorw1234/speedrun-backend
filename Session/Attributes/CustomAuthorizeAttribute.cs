using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Session.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Session.Attributes
{
    //
    // Summary:
    //     Requires the user to be logged in to access the endpoint.
    //     Add parameter "SiteModerator" to enforce the user to be sitemoderator
    public class CustomAuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        private  ApplicationDbContext _ctx;
        //Specifies the accesslevel the user must have.
        //"Sitemoderator" means that they must be sitemoderator to access the endpoint.
        //nothing means that you just check if they are logged in.
        string _accessLevel;

        public CustomAuthorizeAttribute()
        {

        }
        public CustomAuthorizeAttribute(string acessLevel)
        {
            _accessLevel = acessLevel;
        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            int? id = context.HttpContext.Session.GetInt32("id");
                
            if (id is null) // nott logged in!
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            if (_accessLevel != null)
            {

                _ctx = context.HttpContext
                    .RequestServices
                    .GetService(typeof(ApplicationDbContext)) as ApplicationDbContext; // gets dbcontext
                User user = _ctx.Users.Find(context.HttpContext.Session.GetInt32("id"));

                if (_accessLevel == "SiteModerator" && !user.SiteModerator) // user is not site moderator, but they need to be to access
                {
                    context.Result = new UnauthorizedResult();
                }
            }            
        }
    }
}
