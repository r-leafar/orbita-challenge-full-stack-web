using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EdTech.Core.Entities;

namespace EdTech.Infrastructure.Persistence.Configurations
{
    public class NationalIdentifierConfiguration : IEntityTypeConfiguration<NationalIdentifier>
    {
        public void Configure(EntityTypeBuilder<NationalIdentifier> builder)
        {

            builder.HasKey(ni => ni.StudentId); 

            builder.HasDiscriminator<string>("IdentifierType") 
                                                              
                   .HasValue<CpfIdentifier>("CPF".ToUpper());

            builder.Property(ni => ni.Number).IsRequired();
            builder.Ignore(ni => ni.Type);
        }
    }
}