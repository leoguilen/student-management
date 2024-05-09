namespace StudentManagement.Core.Services.Impl;

internal sealed class GradeService(IGradeRepository gradeRepository) : IGradeService
{
    public async Task<GradeDto> CreateAsync(
        GradeDto grade,
        CancellationToken cancellationToken = default)
    {
        var newGrade = await gradeRepository.AddAsync((Grade)grade, cancellationToken);
        return (GradeDto)newGrade;
    }

    public async Task<bool> DeleteAsync(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        var deleted = await gradeRepository.DeleteAsync(id, cancellationToken);
        return deleted > 0;
    }

    public async Task<GradeDto?> UpdateAsync(
        Guid id,
        GradeDto grade,
        CancellationToken cancellationToken = default)
    {
        var existentGrade = await gradeRepository.GetByIdAsync(id, cancellationToken);
        if (existentGrade is null)
        {
            return null;
        }

        existentGrade.Update(grade);

        var updatedGrade = await gradeRepository.UpdateAsync(existentGrade, cancellationToken);
        return (GradeDto)updatedGrade;
    }
}
