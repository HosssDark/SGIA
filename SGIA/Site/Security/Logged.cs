﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace Site
{
    public class Logged : Attribute, IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            //throw new NotImplementedException();
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var Logged = context.HttpContext.Session.GetString("Login.User") != null ? true : false;

            if (!Logged)
            {
                context.Result = new RedirectResult("/Login");
            }
        }
    }
}