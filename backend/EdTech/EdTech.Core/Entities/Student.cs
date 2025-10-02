using EdTech.Core.Interfaces;
using EdTech.Core.Shared.Ensure;
using EdTech.Core.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace EdTech.Core.Entities
{
    public class Student : BaseEntity<Guid>
    {
        private Student() { }
        public Student(string name, string email, string schoolId, NationalIdentifier nationalIdentifier)
        {
            Ensure.NotNullOrWhiteSpace(name, nameof(name));
            Ensure.NotNullOrWhiteSpace(email, nameof(email));
            Ensure.NotNullOrWhiteSpace(schoolId, nameof(schoolId));

            SetId(Guid.CreateVersion7());
            Name = name;
            Email = email;
            SchoolId = schoolId;
            NationalIdentifier = nationalIdentifier;
        }
        public string Name{ get; set; }
        public string Email { get; set; }
        public string SchoolId { get; }
        public NationalIdentifier NationalIdentifier { get; }

       
    }
}
