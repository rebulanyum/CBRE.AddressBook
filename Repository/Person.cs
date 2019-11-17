using System;
using System.Collections.Generic;
using System.Text;

namespace CBRE.AddressBook.Repository
{
    public enum Gender { Male, Female }
    public sealed class Person
    {
        public string FullName { get; set; }
        public Gender Gender { get; set; }
        public DateTime BirthDate { get; set; }
    }
}
