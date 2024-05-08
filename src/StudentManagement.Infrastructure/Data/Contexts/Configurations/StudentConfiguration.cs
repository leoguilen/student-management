namespace StudentManagement.Infrastructure.Data.Contexts.Configurations;

internal sealed class StudentConfiguration : IEntityTypeConfiguration<Student>
{
    public void Configure(EntityTypeBuilder<Student> builder)
    {
        builder.Property(x => x.Id).ValueGeneratedOnAdd();
        builder.Property(x => x.Name).IsRequired().HasMaxLength(100);
        builder.Property(x => x.DateOfBirth).IsRequired();
        builder.Property(x => x.Cpf).IsRequired().HasMaxLength(15);
        builder.Property(x => x.Address).IsRequired().HasMaxLength(200);
        builder.Property(x => x.PhoneNumber).IsRequired().HasMaxLength(15);

        builder.HasKey(x => x.Id);
        builder.HasIndex(x => x.Cpf).IsUnique();

        builder.HasMany(x => x.Grades).WithOne(x => x.Student).HasForeignKey(x => x.StudentId);
    }
}
