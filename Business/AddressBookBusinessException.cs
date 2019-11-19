using System;
using System.Collections.Generic;
using System.Text;

namespace CBRE.AddressBook.Business
{
    public class AddressBookBusinessException : Exception
    {
        public AddressBookBusinessException(Exception innerException, string message = "") : base(!string.IsNullOrWhiteSpace(message) ? message : "AddressBookBusinessException occured.", innerException)
        {

        }
    }
}
