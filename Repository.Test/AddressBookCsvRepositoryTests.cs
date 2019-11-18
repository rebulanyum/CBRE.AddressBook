using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using CBRE.AddressBook.Repository;
using TinyCsvParser;
using System.Linq;
using System.IO;

namespace Repository.Test
{
    [TestClass]
    public class AddressBookCsvRepositoryTests
    {
        [TestMethod]
        public void WhenGivenValidCsv_ShouldRetrieveAllPersons()
        {
            var person1 = new Person() { FullName = "Richard Azul", Gender = Gender.Male, BirthDate = DateTime.Today.AddYears(-30) };
            string filePath = CreateSampleCSVFile(person1);
            var repo = new AddressBookCsvRepository(filePath);

            IEnumerable<Person> persons = repo.RetrieveAllPersons();

            Assert.IsNotNull(persons);
            Assert.AreEqual(persons.Count(), 1);

            Person person1Copy = persons.First();
            Assert.AreEqual(person1Copy.FullName, person1.FullName);
            Assert.AreEqual(person1Copy.Gender, person1.Gender);
            Assert.AreEqual(person1Copy.BirthDate, person1.BirthDate);

            File.Delete(filePath);
        }

        private string CreateSampleCSVFile(params Person[] persons)
        {
            var res = from person in persons
                      select string.Join(',', new string[] { person.FullName, person.Gender.ToString(), person.BirthDate.ToShortDateString() });

            var value = string.Join(Environment.NewLine, res);
            string tempCsvPath = Path.GetTempFileName();
            File.WriteAllText(tempCsvPath, value);
            return tempCsvPath;
        }
    }
}
