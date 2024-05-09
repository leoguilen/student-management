namespace StudentManagement.Core.Services.Impl;

internal sealed class SubjectService(ISubjectRepository subjectRepository) : ISubjectService
{
    public async Task<SubjectDto> CreateAsync(SubjectDto subject, CancellationToken cancellationToken = default)
    {
        var newSubject = await subjectRepository.AddAsync((Subject)subject, cancellationToken);
        return (SubjectDto)newSubject;
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var deleted = await subjectRepository.DeleteAsync(id, cancellationToken);
        return deleted > 0;
    }

    public async Task<(IEnumerable<SubjectDto> Subjects, int TotalSubjects)> GetAsync(SubjectFilter subjectFilter, PaginationFilter paginationFilter, CancellationToken cancellationToken = default)
    {
        var (subjects, totalSubjects) = await subjectRepository.GetAllPaginatedAsync(
            subjectFilter,
            paginationFilter,
            cancellationToken);
            
        return (subjects.Select(s => (SubjectDto)s), totalSubjects);
    }

    public async Task<SubjectDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var subject = await subjectRepository.GetByIdAsync(id, cancellationToken);
        return subject is null
            ? null
            : (SubjectDto)subject;
    }

    public async Task<SubjectDto?> UpdateAsync(
        Guid id,
        SubjectDto subject,
        CancellationToken cancellationToken = default)
    {
        var existentSubject = await subjectRepository.GetByIdAsync(id, cancellationToken);
        if (existentSubject is null)
        {
            return null;
        }

        existentSubject.Update(subject);

        var updatedSubject = await subjectRepository.UpdateAsync(existentSubject, cancellationToken);
        return (SubjectDto)updatedSubject;
    }
}
