using EdTech.Core.Entities;
using EdTech.Core.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace EdTech.Infrastructure.Persistence.Configurations
{
    public class StudentConfiguration : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.HasKey(x => x.Id);


            builder.OwnsOne(s => s.NationalIdentifier, ni =>
            {
                ni.Property(n => n.Type)
                  .HasColumnType("varchar(20)")
                  .HasColumnName("NationalIdType")
                  .IsRequired();

                ni.Property(n => n.Number)
                  .HasColumnType("varchar(20)")
                  .HasColumnName("NationalIdValue")
                  .IsRequired();
            });

        }
    }
}
