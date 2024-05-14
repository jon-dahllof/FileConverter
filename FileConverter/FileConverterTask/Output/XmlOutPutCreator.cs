using FileConverterTask.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace FileConverterTask.Output
{
    internal class XmlOutPutCreator : IFileCreator
    {
        public void CreateOutput(IEnumerable<Person> persons, FileInfo outputFileName)
        {
            var xmlDoc = new XDocument(new XElement("people"));

            foreach (var p in persons)
            {
                var personElement = new XElement("person");
                PopulateXmlElement(personElement, p);
                xmlDoc?.Root?.Add(personElement);
            }

            xmlDoc?.Save(outputFileName.FullName);
        }

        private void PopulateXmlElement(XElement rootElement, Person p)
        {
            // First name
            if (string.IsNullOrWhiteSpace(p?.FirstName) == false) rootElement.Add(new XElement("firstname", p.FirstName));
            // Last name
            if (string.IsNullOrWhiteSpace(p?.LastName) == false) rootElement.Add(new XElement("lastname", p.LastName));

            // Address
            if (p?.Addresses != null)
            {
                var addr = p?.Addresses.LastOrDefault();
                var addressElem = new XElement("address");
                if (string.IsNullOrWhiteSpace(addr?.Street) == false) addressElem.Add(new XElement("street", addr.Street));
                if (string.IsNullOrWhiteSpace(addr?.City) == false) addressElem.Add(new XElement("city", addr.City));
                if (string.IsNullOrWhiteSpace(addr?.PostalCode) == false) addressElem.Add(new XElement("postalcode", addr.PostalCode));
                rootElement.Add(addressElem);
            }
            // Birthday
            if (p?.BirthDayYear != null) rootElement.Add(new XElement("born", p.BirthDayYear));

            // Phone
            if (p?.PhoneNumbers != null)
            {
                var phoneNumbers = new XElement("phone");
                var phoneNumber = p?.PhoneNumbers.LastOrDefault();
                if (string.IsNullOrWhiteSpace(phoneNumber?.MobileNumber) == false) phoneNumbers.Add(new XElement("mobile", phoneNumber.MobileNumber));
                if (string.IsNullOrWhiteSpace(phoneNumber?.LandLine) == false) phoneNumbers.Add(new XElement("landline", phoneNumber.LandLine));
                rootElement.Add(phoneNumbers);
            }

            // Family
            if ((p?.FamilyMembers ?? Enumerable.Empty<FamilyMember>()).Any())
            {
                foreach (var fm in p.FamilyMembers)
                {
                    var familyElem = new XElement("family");
                    PopulateXmlElement(familyElem, fm);
                    rootElement.Add(familyElem);
                }
            }
        }
    }
}
