namespace StudentManagement.Infrastructure.Data.Contexts.Configurations;

internal sealed class SubjectConfiguration : IEntityTypeConfiguration<Subject>
{
    public void Configure(EntityTypeBuilder<Subject> builder)
    {
        builder.Property(x => x.Id).ValueGeneratedOnAdd();
        builder.Property(x => x.Name).IsRequired().HasMaxLength(100);

        builder.HasKey(x => x.Id);
        builder.HasIndex(x => x.Name).IsUnique();

        builder.HasMany(x => x.Grades).WithOne(x => x.Subject).HasForeignKey(x => x.SubjectId);
    }
}
