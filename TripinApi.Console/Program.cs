// See https://aka.ms/new-console-template for more information

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
        Console.WriteLine("User Name\t\tFull Name");
        Console.WriteLine("---------\t\t---------");
        foreach (var p in people)
        {
            Console.WriteLine($"{p.UserName}\t\t{p.FirstName} {p.LastName}");
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
