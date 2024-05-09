namespace StudentManagement.Api.Controllers;

[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[Route("api/[controller]")]
public class StudentsController(IStudentService studentService) : ControllerBase
{
    /// <summary>Get a list of students</summary>
    /// <remarks>Endpoint to get a list of students. It's possible to filter the students by any property.</remarks>
    /// <param name="studentQuery">The query parameters to filter the students</param>
    /// <param name="paginationQuery">The query parameters to paginate the items</param>
    /// <param name="cancellationToken">The cancellation token</param>
    /// <returns>A list of students</returns>
    /// <response code="200">Returns the list of students</response>
    /// <response code="400">Invalid request</response>
    /// <response code="401">User is not authenticated</response>
    /// <response code="403">User is not authorized</response>
    /// <response code="500">Internal server error</response>
    [HttpGet(Name = "GetStudents")]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(PagedResponse<StudentResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(HttpValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAsync(
        [FromQuery] StudentQueryParams studentQuery,
        [FromQuery] PaginationQueryParams paginationQuery,
        CancellationToken cancellationToken = default)
    {
        var (students, totalStudents) = await studentService.GetAsync(
            studentQuery,
            paginationQuery,
            cancellationToken);
        
        return Ok(new PagedResponse<StudentResponse>(
            items: students.Select(StudentResponse.From),
            totalItems: totalStudents,
            page: paginationQuery.Page,
            pageSize: paginationQuery.Size));
    }

    /// <summary>Get a student by ID</summary>
    /// <remarks>Endpoint to get a student by ID.</remarks>
    /// <param name="id" example="12345678-1234-1234-1234-123456789012">The ID of the student</param>
    /// <param name="cancellationToken">The cancellation token</param>
    /// <returns>A student</returns>
    /// <response code="200">Returns the student</response>
    /// <response code="400">Invalid request</response>
    /// <response code="401">User is not authenticated</response>
    /// <response code="403">User is not authorized</response>
    /// <response code="404">Not found</response>
    /// <response code="500">Internal server error</response>
    [HttpGet("{id:guid:required}", Name = "GetStudentById")]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(StudentResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(HttpValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetByIdAsync(
        [FromRoute] Guid id,
        CancellationToken cancellationToken = default)
    {
        var student = await studentService.GetByIdAsync(id, cancellationToken);
        return student is null
            ? NotFound()
            : Ok(StudentResponse.From(student));
    }

    /// <summary>Create a student</summary>
    /// <remarks>Endpoint to create a student.</remarks>
    /// <param name="request">The request to create a student</param>
    /// <param name="cancellationToken">The cancellation token</param>
    /// <returns>A created student</returns>
    /// <response code="201">Returns the registered student</response>
    /// <response code="400">Invalid request</response>
    /// <response code="401">User is not authenticated</response>
    /// <response code="403">User is not authorized</response>
    /// <response code="500">Internal server error</response>
    [HttpPost(Name = "CreateStudent")]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(StudentResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(HttpValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateAsync(
        [FromBody] StudentRequest request,
        CancellationToken cancellationToken = default)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var student = await studentService.CreateAsync(request, cancellationToken);

        return Created(Url.Link("GetStudentById", new { id = student.Id }), StudentResponse.From(student));
    }

    /// <summary>Update a student</summary>
    /// <remarks>Endpoint to update a student.</remarks>
    /// <param name="id" example="12345678-1234-1234-1234-123456789012">The ID of the student</param>
    /// <param name="request">The request to update a student</param>
    /// <param name="cancellationToken">The cancellation token</param>
    /// <returns>A updated student</returns>
    /// <response code="200">Returns the updated student</response>
    /// <response code="400">Invalid request</response>
    /// <response code="401">User is not authenticated</response>
    /// <response code="403">User is not authorized</response>
    /// <response code="404">Not found</response>
    /// <response code="500">Internal server error</response>
    [HttpPut("{id:guid:required}", Name = "UpdateStudent")]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(StudentResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(HttpValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateAsync(
        [FromRoute] Guid id,
        [FromBody] StudentRequest request,
        CancellationToken cancellationToken = default)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var student = await studentService.UpdateAsync(id, request, cancellationToken);

        return student is null
            ? NotFound()
            : Ok(StudentResponse.From(student));
    }

    /// <summary>Delete a student</summary>
    /// <remarks>Endpoint to delete a student.</remarks>
    /// <param name="id" example="12345678-1234-1234-1234-123456789012">The ID of the student</param>
    /// <param name="cancellationToken">The cancellation token</param>
    /// <returns>No content</returns>
    /// <response code="204">No content</response>
    /// <response code="401">User is not authenticated</response>
    /// <response code="403">User is not authorized</response>
    /// <response code="404">Not found</response>
    /// <response code="500">Internal server error</response>
    [HttpDelete("{id:guid:required}", Name = "DeleteStudent")]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteAsync(
        [FromRoute] Guid id,
        CancellationToken cancellationToken = default)
        => await studentService.DeleteAsync(id, cancellationToken)
            ? NoContent()
            : NotFound();

    /// <summary>Get a list of grades of a student</summary>
    /// <remarks>Endpoint to get a list of grades of a student.</remarks>
    /// <param name="id" example="12345678-1234-1234-1234-123456789012">The ID of the student</param>
    /// <param name="cancellationToken">The cancellation token</param>
    /// <returns>A list of grades of a student</returns>
    /// <response code="200">Returns the list of grades of a student</response>
    /// <response code="401">User is not authenticated</response>
    /// <response code="403">User is not authorized</response>
    /// <response code="404">Not found</response>
    /// <response code="500">Internal server error</response>
    [HttpGet("{id:guid:required}/grades", Name = "GetStudentGrades")]
    [Authorize(Policy = "StudentIdPolicy")]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(StudentGradesResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(HttpValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetStudentGradesAsync(
        [FromRoute] Guid id,
        CancellationToken cancellationToken = default)
    {
        var studentGrades = await studentService.GetStudentGradesAsync(id, cancellationToken);
        return studentGrades is null
            ? NotFound()
            : Ok(StudentGradesResponse.From(studentGrades));
    }
}
