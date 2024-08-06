using Microsoft.AspNetCore.Http;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using Microsoft.Extensions.Logging;

public static class TokenHelper
{
    public static int GetUserIdFromToken(HttpContext httpContext, ILogger logger)
    {
        if (httpContext.User == null)
        {
            logger.LogError("HttpContext.User is null.");
            throw new ArgumentNullException(nameof(httpContext.User));
        }

        logger.LogInformation("Claims in token:");
        foreach (var claim in httpContext.User.Claims)
        {
            logger.LogInformation("Claim Type: {Type}, Value: {Value}", claim.Type, claim.Value);
        }

        var userIdClaim = httpContext.User.Claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier");
        if (userIdClaim == null)
        {
            logger.LogError("User ID claim not found in token.");
            throw new Exception("User ID claim not found in token");
        }

        logger.LogInformation("User ID claim found: {UserId}", userIdClaim.Value);
        return int.Parse(userIdClaim.Value);
    }
}
