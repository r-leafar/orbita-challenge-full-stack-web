using EdTech.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdTech.UnitTest.Helpers
{
    public static class StudentTestHelper
    {
        public static Student CreateValidStudent_V1()
        {
           return new Student("Rafael", "slboia@gmail.com", "123", NationalIdentifierTestHelper.CreateValidCpf());
        }
        public static Student CreateValidStudent_V2()
        {
            return new Student("Eliseu", "elisu@hotmail.com", "584", NationalIdentifierTestHelper.CreateValidCpf());
        }
        public static Student CreateInvalidStudent_NoEmail()
        {
            return new Student("Rafael", "", "123", NationalIdentifierTestHelper.CreateValidCpf());
        }
        public static Student CreateInvalidStudent_NoName()
        {
            return new Student("", "slboia@gmail.com", "123", NationalIdentifierTestHelper.CreateValidCpf());
        }
        public static Student CreateInvalidStudent_NoSchoolId()
        {
            return new Student("Rafael", "slboia@gmail.com", "", NationalIdentifierTestHelper.CreateValidCpf());
        }

        public static List<Student> CreateListOfValidStudent()
        {
            return new List<Student>
            {
                CreateValidStudent_V1(),
                CreateValidStudent_V2()
            };
        }
    }
}
