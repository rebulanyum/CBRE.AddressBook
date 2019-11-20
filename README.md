# CBRE.AddressBook
This repository is for the [Technical Assesment](https://github.com/tarikmiri/Recruitment) of CBRE.

In order to run this application;

1) Please open a commandline.
2) Type your command as it's stated.

> dotnet CBRE.AddressBook.ConsoleUI.dll [command] [arguments] [options]

Examples:
1) How many males are in the address book?
> dotnet CBRE.AddressBook.ConsoleUI.dll countgender male
>
> There are 3 Males.

2) Who is the oldest person in the address book?
> dotnet CBRE.AddressBook.ConsoleUI.dll findoldest
>
> 5,Chuck Jackson,Male,14.08.74

3) How many days older is Dana than Sarah?
> dotnet CBRE.AddressBook.ConsoleUI.dll findperson -n Dana
>
> 3,Dana Lane,Female,20/11/91
>
> dotnet CBRE.AddressBook.ConsoleUI.dll findperson --Name Sarah
>
> 4,Sarah Connor,Female,20/09/80
>
> dotnet CBRE.AddressBook.ConsoleUI.dll calculateagediff 3 4
>
> The age difference between the 2 people is -4078

In order to configure the underlying data source for the application please use the appsettings.json file. Or just change the ingredients of the file provided with the solution.
