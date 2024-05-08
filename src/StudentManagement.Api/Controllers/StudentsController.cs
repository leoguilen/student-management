namespace StudentManagement.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class StudentsController : ControllerBase
{
    /// <summary>Get a list of students</summary>
    /// <remarks>Endpoint to get a list of students. It's possible to filter the students by any property.</remarks>
    /// <returns>A list of students</returns>
    /// <response code="200">Returns the list of students</response>
    /// <response code="400">If the request is invalid</response>
    /// <response code="401">If the user is not authenticated</response>
    /// <response code="403">If the user is not authorized</response>
    /// <response code="500">If there was an internal server error</response>
    [HttpGet(Name = "GetStudents")]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(PagedResponse<StudentResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(HttpValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public Task<IActionResult> GetAsync(CancellationToken cancellationToken = default)
    {
        return Task.FromResult<IActionResult>(Ok());
    }

    /// <summary>Get a student by ID</summary>
    /// <remarks>Endpoint to get a student by ID.</remarks>
    /// <param name="id" example="12345678-1234-1234-1234-123456789012">The ID of the student</param>
    /// <returns>A student</returns>
    /// <response code="200">Returns the student</response>
    /// <response code="400">If the request is invalid</response>
    /// <response code="401">If the user is not authenticated</response>
    /// <response code="403">If the user is not authorized</response>
    /// <response code="404">If the student was not found</response>
    /// <response code="500">If there was an internal server error</response>
    /// <response code="503">If the service is unavailable</response>
    [HttpGet("{id:guid:required}", Name = "GetStudentById")]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(StudentResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(HttpValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public Task<IActionResult> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return Task.FromResult<IActionResult>(Ok());
    }
}
