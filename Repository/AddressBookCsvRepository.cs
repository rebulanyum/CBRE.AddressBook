using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using TinyCsvParser;
using TinyCsvParser.Mapping;

namespace CBRE.AddressBook.Repository
{
    public class AddressBookCsvRepository : IAddressBookRepository
    {
        private string filePath;

        public AddressBookCsvRepository(string filePath)
        {
            this.filePath = filePath;
        }
        public IEnumerable<Person> RetrieveAllPersons()
        {
            var csvMapper = new PersonCsvMapping();
            var csvParserOptions = new CsvParserOptions(false, ',');
            var csvParser = new CsvParser<Person>(csvParserOptions, csvMapper);

            var result = csvParser.ReadFromFile(filePath, Encoding.ASCII).ToArray();

            var error = (from res in result
                         where !res.IsValid
                         select res.Error).FirstOrDefault();
            
            if (error != null)
            {
                throw new Exception(error.Value);
            }

            int sequence = 0;
            var persons = from res in result
                          let a = res.Result.ID = ++sequence
                          select res.Result;
            return persons.ToArray();
        }
    }
}
