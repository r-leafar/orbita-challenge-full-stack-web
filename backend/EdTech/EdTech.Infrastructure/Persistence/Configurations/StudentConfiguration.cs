using EdTech.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EdTech.Infrastructure.Persistence.Configurations
{
    public class StudentConfiguration : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne( s => s.NationalIdentifier)
                   .WithOne()
                   .HasForeignKey<NationalIdentifier>(ni => ni.StudentId)
                   .IsRequired();
        }
    }
}
