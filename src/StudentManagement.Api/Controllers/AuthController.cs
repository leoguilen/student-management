namespace StudentManagement.Api.Controllers;

[ApiController]
[AllowAnonymous]
[Route("api/[controller]")]
public class AuthController(
    IStudentRepository studentRepository,
    ITokenService tokenService,
    ILogger<AuthController> logger)
    : ControllerBase
{
    /// <summary>Generates an authentication token for a student.</summary>
    /// <remarks>Endpoint to generate an authentication token for a student.</remarks>
    /// <param name="request">The request to generate an authentication token</param>
    /// <param name="cancellationToken">The cancellation token</param>
    /// <returns>An authentication token</returns>
    /// <response code="200">Returns the authentication success response</response>
    /// <response code="400">If the request is invalid</response>
    /// <response code="401">If the authentication failed</response>
    /// <response code="500">If there was an internal server error</response>
    [HttpPost("token", Name = "GenerateAuthToken")]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(AuthResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(HttpValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GenerateAuthTokenAsync(
        [FromBody(EmptyBodyBehavior = EmptyBodyBehavior.Allow)] AuthRequest? request,
        CancellationToken cancellationToken = default)
    {
        Student? student = null;

        if (request is not null)
        {
            if (!ModelState.IsValid)
            {
                logger.LogWarning("Invalid attempt to generate an authentication token for request {Request} with errors {Errors}", request, ModelState);
                return BadRequest(ModelState);
            }

            student = await studentRepository.GetByIdAsync(request.StudentId, cancellationToken);
            if (student is null)
            {
                logger.LogWarning("Failed to generate an authentication token for student {StudentId}", request.StudentId);
                return Unauthorized();
            }
        }

        var (token, expiry) = tokenService.Generate(student);
        
        return Ok(new AuthResponse
        {
            AccessToken = token,
            ExpiresIn = expiry,
        });
    }
}
