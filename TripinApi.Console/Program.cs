using Microsoft.Extensions.DependencyInjection;
using TrippinApi.Services;
using TripPinApi.Console;

using var cts = new CancellationTokenSource();
var cancellationToken = cts.Token;

Console.CancelKeyPress += Console_CancelKeyPress;
var serviceCollection = new ServiceCollection();
var configuration = ConfigurationExtension.SetupConfigurationBuilder(args).Build();
var section = configuration.GetSection(AppSettings.ConfigurationSectionName);
serviceCollection.AddOptions<AppSettings>().Bind(section).ValidateOnStart();
serviceCollection.ConfigurePeopleServices();
using var provider = serviceCollection.BuildServiceProvider();
using var scope = provider.CreateScope();
var scopeProvider = scope.ServiceProvider;
var peopleService = scopeProvider.GetRequiredService<IPeopleService>();

while (true)
{
    Console.WriteLine("Press 1 to get a list of people");
    Console.WriteLine("Press 2 to search a person by full name");
    Console.WriteLine("Press 3 to search a person by username");
    Console.WriteLine("Press q or CTRL+C to finish");
    var key = Console.ReadKey().KeyChar;
    Console.WriteLine();

    if (key == 'q')
        break;

    if (key == '1')
    {
        var people = await peopleService.ListAsync(cancellationToken);
        await ShowPeopleList(people);
    }
    else if (key == '2')
    {
        Console.WriteLine("Type the full name of the person you would like to search");
        var names = Console.ReadLine();
        if (string.IsNullOrEmpty(names))
        {
            Console.WriteLine("Invalid search name. The name cannot be empty");
            continue;
        }
        var people = await peopleService.SearchByNamesAsync(names, cancellationToken);
        await ShowPeopleList(people);
    }
    else
    {
        Console.WriteLine("Invalid option.");
    }
    Console.WriteLine();
}
Console.CancelKeyPress -= Console_CancelKeyPress;

void Console_CancelKeyPress(object? sender, ConsoleCancelEventArgs e)
{
    cts.Cancel();
}

async Task ShowPeopleList(PersonPersonalInfoDto[] people)
{
    Console.WriteLine("Number\tUser Name\t\tFull Name");
    Console.WriteLine("------\t---------\t\t---------");
    for (var i = 0; i < people.Length; i++)
    {
        var person = people[i];
        Console.WriteLine($"{i}\t{person.UserName}\t\t{person.FirstName} {person.LastName}");
    }
    if (people.Length == 0) return;
    Console.WriteLine("Type the number in the first column to see the details of a person, or any other key to go back to the main menu");
    if (int.TryParse(Console.ReadLine(), out var personNumber))
    {
        var personDetails = await peopleService.DetailsAsync(people[personNumber].UserName, cancellationToken);
        ShowPersonDetails(personDetails);
    }
}

static void ShowPersonDetails(PersonDetailDto person)
{
    Console.WriteLine("\n====================");
    Console.WriteLine($"Username: {person.UserName}");
    Console.WriteLine($"First name: {person.FirstName}");
    Console.WriteLine($"Middle name: {person.MiddleName}");
    Console.WriteLine($"First name: {person.LastName}");
    Console.WriteLine($"Gender: {person.Gender}");
    Console.WriteLine($"Age: {person.Age}");
    Console.WriteLine($"Emails: {string.Join(", ", person.Emails)}");
    Console.WriteLine($"Favorite Feature: {person.FavoriteFeature}");
    Console.WriteLine($"Features: {string.Join(", ", person.Features)}");
    Console.WriteLine("Addresses: ");
    Array.ForEach(person.AddressInfo, PrintLocation);
    Console.WriteLine("Home Address: ");
    PrintLocation(person.HomeAddress);
    Console.WriteLine("====================");
}

static void PrintLocation(LocationDto location)
{
    if (location == null) return;
    Console.WriteLine($"Address: {location.Address}");
    Console.WriteLine($"City: {location.City.Name}");
    Console.WriteLine($"Region: {location.City.Region}");
    Console.WriteLine($"Country: {location.City.CountryRegion}");
}