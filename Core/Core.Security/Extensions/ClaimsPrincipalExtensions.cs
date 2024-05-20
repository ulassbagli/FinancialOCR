﻿using System.Security.Claims;

namespace Core.Security.Extensions;

public static class ClaimsPrincipalExtensions
{
    public static List<string>? Claims(this ClaimsPrincipal claimsPrincipal, string claimType)
    {
        var claims = claimsPrincipal.FindAll(claimType).Select(x => x.Value).ToList();
        return claims.Any() ? claims : null;
    }
    
    public static List<string>? ClaimRoles(this ClaimsPrincipal claimsPrincipal)
    {
        return claimsPrincipal.Claims(ClaimTypes.Role);
    }
    
    public static string GetUserId(this ClaimsPrincipal claimsPrincipal)
    {
        var userId = claimsPrincipal.Claims(ClaimTypes.NameIdentifier)?.FirstOrDefault();
        return userId ?? string.Empty;
    }
}