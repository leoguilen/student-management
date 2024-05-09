namespace StudentManagement.Api.Controllers;

[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[Route("api/[controller]")]
public class SubjectsController(ISubjectService subjectService) : ControllerBase
{
    /// <summary>Get a list of subjects</summary>
    /// <remarks>Endpoint to get a list of subjects. It's possible to filter the subjects by any property.</remarks>
    /// <param name="subjectQuery">The query parameters to filter the subjects</param>
    /// <param name="paginationQuery">The query parameters to paginate the items</param>
    /// <param name="cancellationToken">The cancellation token</param>
    /// <returns>A list of subjects</returns>
    /// <response code="200">Returns the list of subjects</response>
    /// <response code="400">Invalid request</response>
    /// <response code="401">User is not authenticated</response>
    /// <response code="403">User is not authorized</response>
    /// <response code="500">Internal server error</response>
    [HttpGet(Name = "GetSubjects")]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(PagedResponse<SubjectResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(HttpValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAsync(
        [FromQuery] SubjectQueryParams subjectQuery,
        [FromQuery] PaginationQueryParams paginationQuery,
        CancellationToken cancellationToken = default)
    {
        var (subjects, totalSubjects) = await subjectService.GetAsync(
            subjectQuery,
            paginationQuery,
            cancellationToken);
        
        return Ok(new PagedResponse<SubjectResponse>(
            items: subjects.Select(SubjectResponse.From),
            totalItems: totalSubjects,
            page: paginationQuery.Page,
            pageSize: paginationQuery.Size));
    }

    /// <summary>Get a subject by ID</summary>
    /// <remarks>Endpoint to get a subject by ID.</remarks>
    /// <param name="id" example="12345678-1234-1234-1234-123456789012">The ID of the subject</param>
    /// <param name="cancellationToken">The cancellation token</param>
    /// <returns>A student</returns>
    /// <response code="200">Returns the subject</response>
    /// <response code="400">Invalid request</response>
    /// <response code="401">User is not authenticated</response>
    /// <response code="403">User is not authorized</response>
    /// <response code="404">Not found</response>
    /// <response code="500">Internal server error</response>
    [HttpGet("{id:guid:required}", Name = "GetSubjectById")]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(SubjectResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(HttpValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var subject = await subjectService.GetByIdAsync(id, cancellationToken);
        return subject is null
            ? NotFound()
            : Ok(SubjectResponse.From(subject));
    }

    /// <summary>Create a subject</summary>
    /// <remarks>Endpoint to create a subject.</remarks>
    /// <param name="request">The request to subject a student</param>
    /// <param name="cancellationToken">The cancellation token</param>
    /// <returns>A created subject</returns>
    /// <response code="201">Returns the created subject</response>
    /// <response code="400">Invalid request</response>
    /// <response code="401">User is not authenticated</response>
    /// <response code="403">User is not authorized</response>
    /// <response code="500">Internal server error</response>
    [HttpPost(Name = "CreateSubject")]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(SubjectResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(HttpValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateAsync(
        [FromBody] SubjectRequest request,
        CancellationToken cancellationToken = default)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var subject = await subjectService.CreateAsync(request, cancellationToken);

        return Created(Url.Link("GetSubjectById", new { id = subject.Id }), SubjectResponse.From(subject));
    }

    /// <summary>Update a subject</summary>
    /// <remarks>Endpoint to update a subject.</remarks>
    /// <param name="id" example="12345678-1234-1234-1234-123456789012">The ID of the subject</param>
    /// <param name="request">The request to update a subject</param>
    /// <param name="cancellationToken">The cancellation token</param>
    /// <returns>A updated subject</returns>
    /// <response code="200">Returns the updated subject</response>
    /// <response code="400">Invalid request</response>
    /// <response code="401">User is not authenticated</response>
    /// <response code="403">User is not authorized</response>
    /// <response code="404">Not found</response>
    /// <response code="500">Internal server error</response>
    [HttpPut("{id:guid:required}", Name = "UpdateSubject")]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(SubjectResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(HttpValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateAsync(
        [FromRoute] Guid id,
        [FromBody] SubjectRequest request,
        CancellationToken cancellationToken = default)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var subject = await subjectService.UpdateAsync(id, request, cancellationToken);

        return subject is null
            ? NotFound()
            : Ok(SubjectResponse.From(subject));
    }

    /// <summary>Delete a subject</summary>
    /// <remarks>Endpoint to delete a subject.</remarks>
    /// <param name="id" example="12345678-1234-1234-1234-123456789012">The ID of the subject</param>
    /// <param name="cancellationToken">The cancellation token</param>
    /// <returns>No content</returns>
    /// <response code="204">No content</response>
    /// <response code="401">User is not authenticated</response>
    /// <response code="403">User is not authorized</response>
    /// <response code="404">Not found</response>
    /// <response code="500">Internal server error</response>
    [HttpDelete("{id:guid:required}", Name = "DeleteSubject")]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteAsync(
        [FromRoute] Guid id,
        CancellationToken cancellationToken = default)
        => await subjectService.DeleteAsync(id, cancellationToken)
            ? NoContent()
            : NotFound();
}
