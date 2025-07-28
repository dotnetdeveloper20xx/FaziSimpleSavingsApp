using System.Security.Claims;

namespace API.Common.Helpers;

public static class UserContextHelper
{
    public static Guid GetUserId(ClaimsPrincipal user)
    {
        var userIdString = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (!Guid.TryParse(userIdString, out var userId))
            throw new UnauthorizedAccessException("Invalid or missing user ID.");

        return userId;
    }
}
