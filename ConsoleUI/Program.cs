using CBRE.AddressBook.Business;
using CBRE.AddressBook.Repository;
using Microsoft.Extensions.CommandLineUtils;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace CBRE.AddressBook.ConsoleUI
{
    public static class Program
    {
        public static int Main(string[] args)
        {
            var serviceProvider = SetupServices();

            var app = serviceProvider.GetService<AddressBookConsoleApp>();
            return app.Run(args);
        }

        static ServiceProvider SetupServices()
        {
            var serviceProvider = new ServiceCollection()
                .AddSingleton((sp) => new CommandLineApplication(false))
                .AddTransient<AddressBookConsoleApp>()
                .AddTransient<IAddressBookBusiness, AddressBookBusiness>()
                .AddTransient<IAddressBookRepository, AddressBookCsvRepository>()

            .BuildServiceProvider();

            return serviceProvider;
        }
    }
}
