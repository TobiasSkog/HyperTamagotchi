﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace HyperTamagotchi_API.Filters;

public class AuthorizeByRoleAttribute(params string[] roles) : ActionFilterAttribute
{
    private readonly string[] _allowedRoles = roles;

    public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {

        var user = context.HttpContext.User;

        if (!user.Identity.IsAuthenticated || !_allowedRoles.Any(role => user.IsInRole(role)))
        {
            context.Result = new UnauthorizedResult();
        }
        else
        {
            await next();
        }
    }
}
