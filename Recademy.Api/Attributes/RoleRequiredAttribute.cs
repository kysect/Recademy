using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Filters;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Recademy.Dto.Enums;

namespace Recademy.Api.Attributes;

public sealed class RoleRequiredAttribute : ActionFilterAttribute
{
    private readonly HashSet<UserTypeDto> _requiredRoles;

    public IReadOnlyCollection<UserTypeDto> RequiredRoles { get; }

    public RoleRequiredAttribute()
    {
        _requiredRoles = new HashSet<UserTypeDto>
        {
            UserTypeDto.Admin,
            UserTypeDto.Mentor,
        };
    }

    public RoleRequiredAttribute(params UserTypeDto[] requiredRoles)
    {
        RequiredRoles = requiredRoles;

        _requiredRoles = requiredRoles.ToHashSet();
    }

    public override void OnActionExecuting(ActionExecutingContext context)
    {
        ArgumentNullException.ThrowIfNull(context);

        if (!context.HttpContext.Request.Cookies.TryGetValue("JwtToken", out string token))
        {
            context.Result = new UnauthorizedResult();
            return;
        }

        var tokenHandler = new JwtSecurityTokenHandler();
        var securityToken = tokenHandler.ReadToken(token) as JwtSecurityToken;

        Claim userTypeClaim = securityToken?.Claims.FirstOrDefault(claim => claim.Type == "UserType");

        string userType = userTypeClaim?.Value;

        if (!Enum.TryParse(userType, ignoreCase: true, out UserTypeDto userTypeEnum))
        {
            context.Result = new UnauthorizedResult();
            return;
        }

        if (!_requiredRoles.Contains(userTypeEnum))
        {
            context.Result = new UnauthorizedResult();
        }
    }
}