﻿using Recademy.Application.Services.Abstractions;
using Recademy.Core.Models.Users;
using Recademy.Core.Types;

using System;
using System.Linq;
using System.Security.Claims;

namespace Recademy.Application.Services.Implementations;

public class OauthProviderService : IOauthProviderService
{
    public User GetUserFromGithubClaims(ClaimsPrincipal claims)
    {
        ArgumentNullException.ThrowIfNull(claims);

        if (!IsEnoughUserInfo(claims))
            return null;

        string githubLink = claims.FindFirst(claim => claim.Type == "urn:github:url")?.Value;
        string githubLogin = githubLink?.Split('/').LastOrDefault();

        var user = new User
        {
            Name = claims.FindFirst(c => c.Type == ClaimTypes.Name)?.Value,
            GithubUsername = githubLogin,
            UserType = UserType.CommonUser
        };

        return user;
    }

    private bool IsEnoughUserInfo(ClaimsPrincipal claims)
    {
        return claims.HasClaim(c => c.Type == ClaimTypes.Name) && claims.Claims.Any(c => c.Type == "urn:github:url");
    }
}