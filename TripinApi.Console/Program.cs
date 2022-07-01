// See https://aka.ms/new-console-template for more information

using Trippin;

using var cts = new CancellationTokenSource();
var token = cts.Token;
var peopleService = new PeopleService("https://services.odata.org/TripPinRESTierService");
while (true)
{
    Console.WriteLine("Press 1 to get a list of people");
    Console.WriteLine("Press 2 to search a person by name");
    Console.WriteLine("Press q or CTRL+C to finish");
    var key = Console.ReadKey().KeyChar;
    Console.WriteLine();

    if (key == 'q')
        break;

    if (key == '1')
    {
        var people = await peopleService.ListAsync(token);
        Console.WriteLine("Number\tUser Name\t\tFull Name");
        Console.WriteLine("---------\t\t---------");
        for (var i = 0; i < people.Length; i++)
        {
            var person = people[i];
            Console.WriteLine($"{i}\t{person.UserName}\t\t{person.FirstName} {person.LastName}");
        }
        Console.WriteLine("Type the number in the first column to see the details of a person, or any other key to go back to the main menu");
        if (int.TryParse(Console.ReadLine(), out var personNumber))
        {
            var personDetails = await peopleService.DetailsAsync(people[personNumber].UserName, token);
            Console.WriteLine("\n====================");
            Console.WriteLine($"Username: {personDetails.UserName}");
            Console.WriteLine($"First name: {personDetails.FirstName}");
            Console.WriteLine($"Middle name: {personDetails.MiddleName}");
            Console.WriteLine($"First name: {personDetails.LastName}");
            Console.WriteLine($"Gender: {personDetails.Gender}");
            Console.WriteLine($"Age: {personDetails.Age}");
            Console.WriteLine($"Emails: {string.Join(", ", personDetails.Emails)}");
            Console.WriteLine($"Favorite Feature: {personDetails.FavoriteFeature}");
            Console.WriteLine($"Features: {string.Join(", ", personDetails.Features)}");
            foreach (var address in personDetails.AddressInfo)
            {
                Console.WriteLine($"Address: {address.Address}");
                Console.WriteLine($"City: {address.City.Name}");
                Console.WriteLine($"Region: {address.City.Region}");
                Console.WriteLine($"Country: {address.City.CountryRegion}");
            }
            Console.WriteLine($"Home address: {personDetails.HomeAddress}");
            Console.WriteLine("====================");
        }
    }
    else if (key == '2')
    {
        peopleService.Search();
    }
    else
    {
        Console.WriteLine("Invalid option.");
    }
    Console.WriteLine();
}
