// See https://aka.ms/new-console-template for more information

using Trippin;

var peopleService = new PeopleService("https://services.odata.org/TripPinRESTierService");
while (true)
{
    Console.WriteLine("Press 1 to get a list of people");
    Console.WriteLine("Press 2 to search a person by name");
    Console.WriteLine("Press q or CTRL+C to finish");
    var key = Console.ReadKey().KeyChar;
    Console.WriteLine(Environment.NewLine);
    if (key == 'q')
        break;
    if (key == '1')
    {
        peopleService.List();
    }
    else if (key == '2')
    {
        peopleService.Search();
    }
    else
    {
        Console.WriteLine("Invalid option.");
    }
}

sealed class PeopleService
{
    private readonly Container container;

    public PeopleService(string uri)
    {
        container = new(new Uri(uri));
    }

    public void List()
    {
        var people = container.People.ToArray();
        Array.ForEach(people.Select(p => p.FirstName).ToArray(), Console.WriteLine);
    }
    public void Search()
    {
        Console.WriteLine("Search not yet implemented");
    }
}