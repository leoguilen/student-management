namespace StudentManagement.Api.Controllers;

[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[Route("api/[controller]")]
public class GradesController(IGradeService gradeService) : ControllerBase
{
    /// <summary>Create a grade</summary>
    /// <remarks>Endpoint to create a grade.</remarks>
    /// <param name="request">The request to create a grade</param>
    /// <param name="cancellationToken">The cancellation token</param>
    /// <returns>A created grade</returns>
    /// <response code="201">Returns the created grade</response>
    /// <response code="400">Invalid request</response>
    /// <response code="401">User is not authenticated</response>
    /// <response code="403">User is not authorized</response>
    /// <response code="500">Internal server error</response>
    [HttpPost(Name = "CreateGrade")]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(GradeResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(HttpValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateAsync(
        [FromBody] GradeRequest request,
        CancellationToken cancellationToken = default)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var grade = await gradeService.CreateAsync(request, cancellationToken);

        return CreatedAtRoute("GetStudentGrades", new { id = grade.StudentId }, GradeResponse.From(grade));
    }

    /// <summary>Update a grade</summary>
    /// <remarks>Endpoint to update a grade.</remarks>
    /// <param name="id" example="12345678-1234-1234-1234-123456789012">The ID of the grade</param>
    /// <param name="request">The request to update a grade</param>
    /// <param name="cancellationToken">The cancellation token</param>
    /// <returns>A updated grade</returns>
    /// <response code="200">Returns the updated grade</response>
    /// <response code="400">Invalid request</response>
    /// <response code="401">User is not authenticated</response>
    /// <response code="403">User is not authorized</response>
    /// <response code="404">Not found</response>
    /// <response code="500">Internal server error</response>
    [HttpPut("{id:guid:required}", Name = "UpdateGrade")]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(GradeResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(HttpValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateAsync(
        [FromRoute] Guid id,
        [FromBody] GradeRequest request,
        CancellationToken cancellationToken = default)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var grade = await gradeService.UpdateAsync(id, request, cancellationToken);

        return grade is null
            ? NotFound()
            : Ok(GradeResponse.From(grade));
    }

    /// <summary>Delete a grade</summary>
    /// <remarks>Endpoint to delete a grade.</remarks>
    /// <param name="id" example="12345678-1234-1234-1234-123456789012">The ID of the grade</param>
    /// <param name="cancellationToken">The cancellation token</param>
    /// <returns>No content</returns>
    /// <response code="204">No content</response>
    /// <response code="401">User is not authenticated</response>
    /// <response code="403">User is not authorized</response>
    /// <response code="404">Not found</response>
    /// <response code="500">Internal server error</response>
    [HttpDelete("{id:guid:required}", Name = "DeleteGrade")]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteAsync(
        [FromRoute] Guid id,
        CancellationToken cancellationToken = default)
        => await gradeService.DeleteAsync(id, cancellationToken)
            ? NoContent()
            : NotFound();
}
