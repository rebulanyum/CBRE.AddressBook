using System;
using System.Collections.Generic;
using System.Text;

namespace CBRE.AddressBook.Repository
{
    /// <summary>
    /// The base for the AddressBook repository.
    /// </summary>
    public interface IAddressBookRepository
    {
        /// <summary>
        /// Retrieves all the Person records from the data source..
        /// </summary>
        /// <returns>ll the Person records.</returns>
        IEnumerable<Person> RetrieveAllPersons(); 
    }
}
