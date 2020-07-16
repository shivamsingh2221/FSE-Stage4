using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace HandsOn3.Filters
{

    public class CustomAuthFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.HttpContext.Request.Query.ContainsKey("Authorization") && context.HttpContext.Request.Query["Authorization"] == "true")
            {

                context.Result = new UnauthorizedResult();
            }
            else
            {
                
                base.OnActionExecuting(context);
            }
        }


    }
}
