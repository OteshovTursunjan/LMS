using System.Security.Claims;

namespace LMS.API.Middleware;

public class UserIdMiddleware
{
    private readonly RequestDelegate _next;
    public UserIdMiddleware(RequestDelegate next)
    {
        _next = next;
    }
    public async Task InvokeAsync(HttpContext context)
    {
        var userIdClaim = context.User?.FindFirst(ClaimTypes.NameIdentifier);
        if (userIdClaim != null)
        {
            context.Items["UserId"] = userIdClaim.Value; // Using "UserId" as the key
        }
        await _next(context);
    }
}
