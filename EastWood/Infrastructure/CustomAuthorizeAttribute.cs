﻿using EastWood.Infrastructure;
using EastWood.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace CustomAuthorizationFilter.Infrastructure
{
    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {
        private readonly string[] allowedroles;
        public CustomAuthorizeAttribute(params string[] roles)
        {
            this.allowedroles = roles;
        }
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            bool authorize = false;
            try
            {
                int userId = int.Parse(Convert.ToString(httpContext.Session["UserId"]));
                if (userId != 0)
                    using (var context = new EastWoodEntities())
                    {
                        var userRole = (from u in context.Users
                                        join r in context.Roles on u.RoleId equals r.RoleId
                                        where u.UserId == userId
                                        select new
                                        {
                                            r.RoleName
                                        }).FirstOrDefault();
                        foreach (var role in allowedroles)
                        {
                            if (role == userRole.RoleName) return true;
                        }
                    }
            }
            catch { };

            return authorize;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new RedirectToRouteResult(
               new RouteValueDictionary
               {
                    { "controller", allowedroles.Contains(RoleTypes.STUDENT)? "Student" : "Admin" },
                    { "action", "Login" },
                    { "ReturnUrl", filterContext.HttpContext.Request.Url}
               });
        }
    }
}