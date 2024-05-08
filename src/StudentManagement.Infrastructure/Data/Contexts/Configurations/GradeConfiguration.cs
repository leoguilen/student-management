namespace StudentManagement.Infrastructure.Data.Contexts.Configurations;

internal sealed class GradeConfiguration : IEntityTypeConfiguration<Grade>
{
    public void Configure(EntityTypeBuilder<Grade> builder)
    {
        builder.Property(x => x.Id).ValueGeneratedOnAdd();
        builder.Property(x => x.Value).IsRequired();

        builder.HasKey(x => x.Id);
        builder.HasIndex(x => new { x.StudentId, x.SubjectId }).IsUnique();

        builder.HasOne(x => x.Student).WithMany(x => x.Grades).HasForeignKey(x => x.StudentId);
        builder.HasOne(x => x.Subject).WithMany(x => x.Grades).HasForeignKey(x => x.SubjectId);
    }
}
