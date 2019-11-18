using CBRE.AddressBook.Repository;
using System;
using System.Collections.Generic;
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
        public virtual int CalculateAgeDifferenceInDays(Person person1, Person person2)
        {
            TimeSpan difference = person2.BirthDate - person1.BirthDate;
            return (int)difference.TotalDays;
        }
    }
}
