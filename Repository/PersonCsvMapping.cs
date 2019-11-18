using System;
using System.Collections.Generic;
using System.Text;
using TinyCsvParser.Mapping;
using TinyCsvParser.TypeConverter;

namespace CBRE.AddressBook.Repository
{
    public class PersonCsvMapping : CsvMapping<Person>
    {
        public PersonCsvMapping()
        {
            MapProperty(0, p => p.FullName);
            MapProperty(1, p => p.Gender, new EnumConverter<Gender>(true));
            MapProperty(2, p => p.BirthDate, new DateTimeConverter());
        }
    }
    
}
