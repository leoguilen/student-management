namespace StudentManagement.Api.Authorization.Handlers;

internal sealed class StudentIdHandler : AuthorizationHandler<StudentIdRequirement>
{
    protected override async Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        StudentIdRequirement requirement)
    {
        if (context is { User.Identity.IsAuthenticated: true, Resource: HttpContext httpContext })
        {
            if (httpContext.Request.RouteValues.TryGetValue("id", out var studentId) &&
                context.User.HasClaim(ClaimTypes.NameIdentifier, studentId!.ToString()!))
            {
                context.Succeed(requirement);
            }
        }

        await Task.CompletedTask;
    }
}
