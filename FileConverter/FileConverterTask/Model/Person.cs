using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileConverterTask.Model
{
    public class Person
    {
        internal IList<Address> Addresses { get; set; } = new List<Address>();

        internal int? BirthDayYear { get; set; }

        internal IList<FamilyMember> FamilyMembers { get; set; } = new List<FamilyMember>();

        internal string? FirstName { get; set; }
        internal string? LastName { get; set; }

        internal IList<PhoneNumber> PhoneNumbers { get; set; } = new List<PhoneNumber>();
    }
}
