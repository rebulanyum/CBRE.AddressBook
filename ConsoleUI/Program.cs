using CBRE.AddressBook.Business;
using CBRE.AddressBook.Repository;
using Microsoft.Extensions.CommandLineUtils;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;

namespace CBRE.AddressBook.ConsoleUI
{
    public static class Program
    {
        public static int Main(string[] args)
        {
            IConfiguration config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false, true)
                .Build();

            var serviceProvider = SetupServices(config);

            var app = serviceProvider.GetService<AddressBookConsoleApp>();
            return app.Run(args);
        }

        static ServiceProvider SetupServices(IConfiguration config)
        {
            var serviceProvider = new ServiceCollection()
                .AddSingleton((sp) => new CommandLineApplication(false))
                .AddTransient<AddressBookConsoleApp>()
                .AddTransient<IAddressBookBusiness, AddressBookBusiness>()
                .AddTransient<IAddressBookRepository, AddressBookCsvRepository>(sp => new AddressBookCsvRepository(config.GetSection("AddressBook").GetSection("DataSource").Value))
            .BuildServiceProvider();

            return serviceProvider;
        }
    }
}
