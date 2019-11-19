using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Moq;
using CBRE.AddressBook.Repository;

namespace CBRE.AddressBook.Business.Test
{
    [TestClass]
    public class AddressBookBusinessTest
    {
        Mock<IAddressBookRepository> addressBookRepositoryMock ;
        IAddressBookBusiness addressBookBusiness;
        static Person[] persons;

        [ClassInitialize]
        public static void InitializeForAllTests(TestContext testContext)
        {
            persons = new Person[]
            {
                new Person() { ID = 1, FullName = "Richard Azul", Gender = Gender.Male, BirthDate = DateTime.Today.AddYears(-30) },
                new Person() { ID = 2, FullName = "Clara White", Gender = Gender.Female, BirthDate = DateTime.Today.AddYears(-30).AddDays(-1) }
            };
        }

        [TestInitialize]
        public void InitializeForTest()
        {
            addressBookRepositoryMock = new Mock<IAddressBookRepository>();
            addressBookRepositoryMock.Setup(abr => abr.RetrieveAllPersons()).Returns(persons);
            addressBookBusiness = new AddressBookBusiness(addressBookRepositoryMock.Object);
        }

        [TestMethod]
        public void When2PersonsGiven_CanCalculatesAgeDifference()
        {
            int diffReal = (int)(persons[1].BirthDate - persons[0].BirthDate).TotalDays;
            int diffCalculated = addressBookBusiness.CalculateAgeDifferenceInDays(persons[0].ID, persons[1].ID);

            addressBookRepositoryMock.Verify(abr => abr.RetrieveAllPersons(), Times.AtLeastOnce);
            Assert.AreEqual(diffReal, diffCalculated);
        }

        [TestMethod]
        [ExpectedException(typeof(AddressBookBusinessException), AllowDerivedTypes = true)]
        public void When1PersonGiven_CalculateAgeDifference_ThrowsException()
        {
            addressBookBusiness.CalculateAgeDifferenceInDays(1, -1);
        }

        [TestMethod]
        public void WhenGivenGender_CanCountPersonsWithSpecifiedGender()
        {
            Gender gender;
            uint countReal, countCalculated;

            gender = Gender.Female;

            countCalculated = addressBookBusiness.CountGenders(gender);
            countReal = (uint)(from person in persons
                               where person.Gender == gender
                               select person).Count();

            addressBookRepositoryMock.Verify(abr => abr.RetrieveAllPersons(), Times.Once);
            Assert.AreEqual(countReal, countCalculated);
        }

        [TestMethod]
        public void CanFindTheOldest()
        {
            Person oldestReal, oldestCalculated;

            oldestReal = (from person in persons
                          orderby person.BirthDate ascending
                          select person).FirstOrDefault();

            oldestCalculated = addressBookBusiness.FindOldest();

            addressBookRepositoryMock.Verify(abr => abr.RetrieveAllPersons(), Times.Once);
            Assert.AreSame(oldestReal, oldestCalculated);
        }

        [TestMethod]
        public void WhenPersonNameGiven_CanFindThePerson()
        {
            string name = persons[0].FullName.Substring(0, 3).ToLowerInvariant();
            IEnumerable<Person> personsReal, personsCalculated;

            personsReal = from person in persons
                          where person.FullName.StartsWith(name, StringComparison.InvariantCultureIgnoreCase)
                          select person;

            personsCalculated = addressBookBusiness.FindPersonByName(name);

            addressBookRepositoryMock.Verify(abr => abr.RetrieveAllPersons(), Times.Once);
            CollectionAssert.AreEquivalent(personsReal.ToList(), personsCalculated.ToList());
        }

        [TestMethod]
        public void WhenPersonIDGiven_CanGetThePerson()
        {
            int personID = persons[0].ID;
            Person personReal, personCalculated;

            personReal = (from person in persons
                          where person.ID == personID
                          select person).Single();

            personCalculated = addressBookBusiness.GetPersonByID(personID);

            addressBookRepositoryMock.Verify(abr => abr.RetrieveAllPersons(), Times.Once);
            Assert.AreSame(personReal, personCalculated);
        }
    }
}
