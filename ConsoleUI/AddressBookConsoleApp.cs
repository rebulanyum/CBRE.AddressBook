using CBRE.AddressBook.Business;
using CBRE.AddressBook.Repository;
using Microsoft.Extensions.CommandLineUtils;
using System;
using System.Collections.Generic;
using System.Text;

namespace CBRE.AddressBook.ConsoleUI
{
    public class AddressBookConsoleApp
    {
        const string HelpFlag = "-? |-h |--help";
        private CommandLineApplication app;
        public AddressBookConsoleApp(CommandLineApplication app)
        {
            this.app = app;

            CommandOption helpOption = app.HelpOption(HelpFlag);
            helpOption.Inherited = true;

            app.FullName = typeof(Program).FullName;
            
            app.OnExecute(() =>
            {
                app.ShowHelp();
                return 0;
            });
        }

        public int Run(string[]args)
        {
            return this.app.Execute(args);
        }
    }
}
