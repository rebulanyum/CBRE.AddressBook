using CBRE.AddressBook.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace CBRE.AddressBook.Business
{
    /// <summary>
    /// The base for the AddressBook business.
    /// </summary>
    public interface IAddressBookBusiness
    {
        /// <summary>
        /// Counts the number of persons with the specified <paramref name="gender"/> .
        /// </summary>
        /// <param name="gender">The gender.</param>
        /// <returns>The number of persons.</returns>
        uint CountGenders(Gender gender);
        /// <summary>
        /// Finds the oldest person.
        /// </summary>
        /// <returns>The oldest person.</returns>
        Person FindOldest();
        /// <summary>
        /// Calculate the 2 persons age difference in days.
        /// </summary>
        /// <remarks>
        /// If <paramref name="person1"/> is older than <paramref name="person2"/> the result will be positive. If <paramref name="person2"/> is older than <paramref name="person1"/> the result will be negative. Otherwise the result will be 0.
        /// </remarks>
        /// <param name="person1">The first person.</param>
        /// <param name="person2">The second person.</param>
        /// <returns>The age difference in days.</returns>
        int CalculateAgeDifferenceInDays(Person person1, Person person2);
    }
}
