using System;
using System.Collections.Generic;
using System.Text;

namespace CBRE.AddressBook.Repository
{
    public interface IAddressBookRepository
    {
        IEnumerable<Person> GetAllPersons(); 
    }
}
