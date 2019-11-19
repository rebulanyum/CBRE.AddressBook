using CBRE.AddressBook.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace CBRE.AddressBook.Business
{
    public class AddressBookBusiness : AddressBookBusinessBase
    {
        public AddressBookBusiness(IAddressBookRepository addressBookRepository) : base(addressBookRepository)
        {
        }

        public override uint CountGenders(Gender gender)
        {
            try
            {
                return (uint)(from person in AddressBookRepository.RetrieveAllPersons()
                              where person.Gender == gender
                              select person).Count();
            }
            catch (Exception e)
            {
                throw new AddressBookBusinessException(e);
            }
        }

        public override Person FindOldest()
        {
            try
            {
                return (from person in AddressBookRepository.RetrieveAllPersons()
                        orderby person.BirthDate ascending
                        select person).FirstOrDefault();
            }
            catch (Exception e)
            {
                throw new AddressBookBusinessException(e);
            }
        }

        public override IEnumerable<Person> FindPersonByName(string personName)
        {
            try
            {
                return from person in AddressBookRepository.RetrieveAllPersons()
                       where person.FullName.StartsWith(personName, StringComparison.InvariantCultureIgnoreCase)
                       select person;
            }
            catch (Exception e)
            {
                throw new AddressBookBusinessException(e);
            }
        }

        public override Person GetPersonByID(int personID)
        {
            try
            {
                return (from person in AddressBookRepository.RetrieveAllPersons()
                        where person.ID == personID
                        select person).SingleOrDefault();
            }
            catch (Exception e)
            {
                throw new AddressBookBusinessException(e);
            }
        }
    }
}
