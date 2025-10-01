using EdTech.Core.Interfaces;
using EdTech.Core.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace EdTech.Core.Entities
{
    public class Student
    {
        public Guid Id { get; private set; }
        public string Name{ get; set; }
        public string Email { get; set; }
        public string SchoolId { get; set; }
        public INationalIdentifier NationalIdentifier { get; set; }

        public Student(string schoolId, INationalIdentifier nationalIdentifier)
        {
            Id = Guid.NewGuid();
            SchoolId = schoolId;
            NationalIdentifier = nationalIdentifier;
        }
    }
}
