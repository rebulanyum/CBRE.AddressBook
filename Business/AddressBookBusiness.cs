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
            return (uint)(from person in AddressBookRepository.RetrieveAllPersons()
                          where person.Gender == gender
                          select person).Count();
        }

        public override Person FindOldest()
        {
            return (from person in AddressBookRepository.RetrieveAllPersons()
                    orderby person.BirthDate ascending
                    select person).FirstOrDefault();
        }

        public override IEnumerable<Person> FindPersonByName(string personName)
        {
            return from person in AddressBookRepository.RetrieveAllPersons()
                   where person.FullName.StartsWith(personName, StringComparison.InvariantCultureIgnoreCase)
                   select person;
        }

        public override Person GetPersonByID(int personID)
        {
            return (from person in AddressBookRepository.RetrieveAllPersons()
                    where person.ID == personID
                    select person).SingleOrDefault();
        }
    }
}
