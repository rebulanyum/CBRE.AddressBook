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
                new Person() { FullName = "Richard Azul", Gender = Gender.Male, BirthDate = DateTime.Today.AddYears(-30) },
                new Person() { FullName = "Clara White", Gender = Gender.Female, BirthDate = DateTime.Today.AddYears(-30).AddDays(-1) }
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
            int diffCalculated = addressBookBusiness.CalculateAgeDifferenceInDays(persons[0], persons[1]);

            Assert.AreEqual(diffReal, diffCalculated);
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
    }
}
