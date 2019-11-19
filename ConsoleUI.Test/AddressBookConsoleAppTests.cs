using CBRE.AddressBook.Business;
using CBRE.AddressBook.ConsoleUI;
using CBRE.AddressBook.Repository;
using Microsoft.Extensions.CommandLineUtils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleUI.Test
{
    [TestClass]
    public class AddressBookConsoleAppTests
    {
        Mock<IAddressBookBusiness> addressBookBusinessMock;
        AddressBookConsoleApp addressBookConsoleApp;
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
            addressBookBusinessMock = new Mock<IAddressBookBusiness>();
            addressBookBusinessMock.Setup(abr => abr.CalculateAgeDifferenceInDays(It.IsAny<int>(), It.IsAny<int>())).Returns(1);
            addressBookBusinessMock.Setup(abr => abr.CountGenders(It.IsAny<Gender>())).Returns(1);
            addressBookBusinessMock.Setup(abr => abr.FindOldest()).Returns(persons[0]);
            addressBookBusinessMock.Setup(abr => abr.FindPersonByName(It.IsAny<string>())).Returns(persons);
            addressBookBusinessMock.Setup(abr => abr.GetPersonByID(It.IsAny<int>())).Returns(persons[0]);
            addressBookConsoleApp = new AddressBookConsoleApp(new CommandLineApplication(false), addressBookBusinessMock.Object);
        }

        [TestMethod]
        public void CountGenderCommandShouldWork()
        {
            int result = -1;
            try
            {
                result = addressBookConsoleApp.Run("countgender", "mAlE");
            }
            catch
            {
                throw;
            }
            finally
            {
                addressBookBusinessMock.Verify(abb => abb.CountGenders(Gender.Male), Times.Once);
                Assert.AreEqual(0, result);
            }
        }

        [TestMethod]
        public void FindOldestCommandShouldWork()
        {
            int result = -1;
            try
            {
                result = addressBookConsoleApp.Run("findoldest");
            }
            catch
            {
                throw;
            }
            finally
            {
                addressBookBusinessMock.Verify(abb => abb.FindOldest(), Times.Once);
                Assert.AreEqual(0, result);
            }
        }

        [TestMethod]
        public void CalculateAgeDiffCommandShouldWork()
        {
            int result = -1;
            try
            {
                result = addressBookConsoleApp.Run("calculateagediff", "1", "2");
            }
            catch
            {
                throw;
            }
            finally
            {
                addressBookBusinessMock.Verify(abb => abb.CalculateAgeDifferenceInDays(1, 2), Times.Once);
                Assert.AreEqual(0, result);
            }
        }

        [TestMethod]
        public void FindPersonCommandShouldWork()
        {
            int result = -1;
            try
            {
                result = addressBookConsoleApp.Run("findperson", "-n", "AsDaSd");
            }
            catch
            {
                throw;
            }
            finally
            {
                addressBookBusinessMock.Verify(abb => abb.FindPersonByName("AsDaSd"), Times.Once);
                Assert.AreEqual(0, result);
            }
        }
    }
}
