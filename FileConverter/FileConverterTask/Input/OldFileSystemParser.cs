using FileConverterTask.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileConverterTask.Input
{
    internal class OldFileSystemParser : IFileReader
    {
        public IEnumerable<Person> GetPersons(FileInfo fileInfo)
        {
            using (var streamReader = new StreamReader(fileInfo.FullName))
            {
                string? line = null;
                Person? currentPerson = null;
                FamilyMember? currentFamilyMember = null;
                var persons = new List<Person>();
                while ((line = streamReader.ReadLine()) != null)
                {
                    var obj = ParseLine(line);
                    if (obj == null) continue;

                    if (obj is FamilyMember fm)
                    {
                        currentPerson?.FamilyMembers.Add(fm);
                        // Set the current family member
                        currentFamilyMember = fm;
                        continue;
                    }

                    if (obj is Person p)
                    {
                        if (p != null) { persons.Add(p); }
                        // Set the current person
                        currentPerson = p;
                        // Reset current family member
                        currentFamilyMember = null;
                        continue;
                    }
                    if (obj is Address a)
                    {
                        (currentFamilyMember ?? currentPerson)?.Addresses.Add(a);
                        continue;
                    }
                    if (obj is PhoneNumber phone)
                    {
                        (currentFamilyMember ?? currentPerson)?.PhoneNumbers.Add(phone);
                        continue;
                    }
                }

                return persons;
            }
        }

        private string Delimiter { get; } = "|";

        private object ParseLine(string line)
        {
            var splitted = line.Split(Delimiter);
            switch(new string(splitted.FirstOrDefault())?.ToUpper())
            {
                case "P":
                    return new Person()
                    {
                        FirstName = GetElement(splitted, 1),
                        LastName = GetElement(splitted, 2)
                    };
                case "T":
                    return new PhoneNumber()
                    {
                        LandLine = GetElement(splitted, 2),
                        MobileNumber = GetElement(splitted, 1)
                    };
                case "A":
                    return new Address
                    {
                        Street = GetElement(splitted, 1),
                        City = GetElement(splitted, 2),
                        PostalCode = GetElement(splitted, 3)
                    };
                case "F":
                    var familyMember = new FamilyMember { FirstName = GetElement(splitted, 1) };
                    if (int.TryParse(GetElement(splitted, 2), out int parsedInt)) { familyMember.BirthDayYear = parsedInt; }
                    return familyMember;
                default:
                    return null;
            }
        }

        private static string? GetElement(string[] splitted, int v) => splitted.Length - 1 >= v ? splitted[v] : null;
    }
}
