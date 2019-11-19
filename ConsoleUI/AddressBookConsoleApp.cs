using CBRE.AddressBook.Business;
using CBRE.AddressBook.Repository;
using Microsoft.Extensions.CommandLineUtils;
using System;
using System.Collections.Generic;
using System.Text;

namespace CBRE.AddressBook.ConsoleUI
{
    public static class PersonExtensions
    {
        public static void Print(this Person person)
        {
            Console.WriteLine($"{person.ID},{person.FullName},{person.Gender},{person.BirthDate.ToShortDateString()}");
        }
    }
    public class AddressBookConsoleApp
    {
        const string HelpFlag = "-? |-h |--help";
        private CommandLineApplication app;
        private IAddressBookBusiness addressBookBusiness;
        public AddressBookConsoleApp(CommandLineApplication app, IAddressBookBusiness addressBookBusiness)
        {
            this.app = app;
            this.addressBookBusiness = addressBookBusiness;

            CommandOption helpOption = app.HelpOption(HelpFlag);
            helpOption.Inherited = true;

            app.FullName = typeof(Program).FullName;
            
            app.OnExecute(() =>
            {
                app.ShowHelp();
                return 0;
            });

            ConfigureForCountGenderCommand(app);
            ConfigureForCalculateAgeDifferenceCommand(app);
            ConfigureForFindOldestCommand(app);
            ConfigureForFindUserByNameCommand(app);
        }

        public int Run(params string[]args)
        {
            return this.app.Execute(args);
        }

        private static string ToString(Person person)
        {
            return $"{person.ID},{person.FullName},{person.Gender},{person.BirthDate.ToShortDateString()}";
        }

        private void ConfigureForCountGenderCommand(CommandLineApplication cla)
        {
            cla.Command("countgender", (application) =>
            {
                application.Description = "With this command you can count the people with the given gender";
                CommandArgument genderArgument = application.Argument("gender", "The gender of Person to look for: Male | Female", (argument) =>
                {
                    argument.ShowInHelpText = true;
                });
                application.OnExecute(() =>
                {
                    if (cla.OptionHelp.HasValue())
                    {
                        application.ShowHelp("countgender");
                        return 0;
                    }

                    Gender gender;
                    if (!Enum.TryParse(genderArgument.Value, true, out gender))
                    {
                        application.Error.WriteLine($"The argument {genderArgument.Name} cannot be parsed.");
                        return -1;
                    }
                    uint count = addressBookBusiness.CountGenders(gender);
                    Console.WriteLine($"There are {count} {gender}s.");
                    return 0;
                });
            });
        }

        private void ConfigureForFindOldestCommand(CommandLineApplication cla)
        {
            cla.Command("findoldest", (application) =>
            {
                application.Description = "With this command you can find the oldest person.";
                application.OnExecute(() =>
                {
                    if (cla.OptionHelp.HasValue())
                    {
                        application.ShowHelp("findoldest");
                        return 0;
                    }
                    
                    Person person = addressBookBusiness.FindOldest();
                    person.Print();
                    return 0;
                });
            });
        }

        private void ConfigureForCalculateAgeDifferenceCommand(CommandLineApplication cla)
        {
            cla.Command("calculateagediff", (application) =>
            {
                application.Description = "With this command you can calculate the age difference between the given 2 people. The result will be presented in days; If the 1st person is older than 2nd one the result will be positive. If 2nd person is older than 1st person the result will be negative. Otherwise, the result will be 0.";
                CommandArgument idArgument1 = application.Argument("personID1", "The ID of Person.", (argument) =>
                {
                    argument.ShowInHelpText = true;
                });
                CommandArgument idArgument2 = application.Argument("personID2", "The ID of Person.", (argument) =>
                {
                    argument.ShowInHelpText = true;
                });
                application.OnExecute(() =>
                {
                    if (cla.OptionHelp.HasValue())
                    {
                        application.ShowHelp("calculateagediff");
                        return 0;
                    }

                    bool parseSuccess;
                    int person1ID, person2ID;
                    parseSuccess = int.TryParse(idArgument1.Value, out person1ID);
                    parseSuccess = parseSuccess & int.TryParse(idArgument2.Value, out person2ID);
                    if (!parseSuccess)
                    {
                        application.Error.WriteLineAsync("The given arguments cannot be parsed.");
                        return -1;
                    }

                    int diff = addressBookBusiness.CalculateAgeDifferenceInDays(person1ID, person2ID);
                    Console.WriteLine($"The age difference between the 2 people is {diff}");
                    return 0;
                });
            });
        }

        private void ConfigureForFindUserByNameCommand(CommandLineApplication cla)
        {
            cla.Command("findperson", (application) =>
            {
                application.Description = "With this command you can search for the people's names. The search is not case-sensitive and it starts searching from beginning of the people's fullname.";
                CommandOption nameOption = application.Option("-n |--Name", "The name of Person.", CommandOptionType.SingleValue, (option) =>
                {
                    option.ShowInHelpText = true;
                });
                application.OnExecute(() =>
                {
                    if (cla.OptionHelp.HasValue())
                    {
                        application.ShowHelp("findperson");
                        return 0;
                    }

                    string personName = nameOption.HasValue() ? nameOption.Value() : string.Empty;
                    IEnumerable<Person> persons = addressBookBusiness.FindPersonByName(personName);
                    foreach (Person person in persons)
                    {
                        person.Print();
                    }
                    return 0;
                });
            });
        }
    }
}
