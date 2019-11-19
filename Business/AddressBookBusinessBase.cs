using CBRE.AddressBook.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CBRE.AddressBook.Business
{
    public abstract class AddressBookBusinessBase : IAddressBookBusiness
    {
        protected IAddressBookRepository AddressBookRepository { get; private set; }
        protected AddressBookBusinessBase(IAddressBookRepository addressBookRepository)
        {
            AddressBookRepository = addressBookRepository;
        }

        public abstract uint CountGenders(Gender gender);
        public abstract Person FindOldest();
        public virtual int CalculateAgeDifferenceInDays(int person1ID, int person2ID)
        {
            var person1 = GetPersonByID(person1ID);
            var person2 = GetPersonByID(person2ID);
            return CalculateAgeDifferenceInDays(person1, person2);
        }
        public virtual int CalculateAgeDifferenceInDays(Person person1, Person person2)
        {
            try
            {
                TimeSpan difference = person2.BirthDate - person1.BirthDate;
                return (int)difference.TotalDays;
            }
            catch (Exception e)
            {
                throw new AddressBookBusinessException(e, "Couldn't find the person(s).");
            }
        }

        public abstract IEnumerable<Person> FindPersonByName(string personName);
        public abstract Person GetPersonByID(int personID);


    }
}
