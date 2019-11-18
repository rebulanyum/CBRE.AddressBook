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
        /// <paramref name="person1ID"/> and <paramref name="person2ID"/> are the ID values of the 2 persons. If 1st person is older than 2nd person the result will be positive. If 2nd person is older than 1st person the result will be negative. Otherwise the result will be 0.
        /// </remarks>
        /// <param name="person1ID">The 1st person's ID.</param>
        /// <param name="person2ID">The 2nd person's ID.</param>
        /// <returns>The age difference in days.</returns>
        int CalculateAgeDifferenceInDays(int person1ID, int person2ID);
        /// <summary>
        /// Finds the person according to it's name.
        /// </summary>
        /// <remarks>The find action is done according to this rule: The name is considered as invariant culture string and the given name is compared from the beginning of the fullname of a person.</remarks>
        /// <example>if <paramref name="personName"/>="Bill" the returning Person's FullName would be "Billy The Kid" or "biLL somerly"</example>
        /// <param name="personName">The name of the person.</param>
        /// <returns>The person</returns>
        IEnumerable<Person> FindPersonByName(string personName);
        Person GetPersonByID(int personID);
    }
}
