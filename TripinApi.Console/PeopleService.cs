// See https://aka.ms/new-console-template for more information

using Trippin;

sealed class PeopleService
{
    private readonly Container container;

    public PeopleService(string uri)
    {
        container = new(new Uri(uri));
    }

    public async Task<Person[]> ListAsync(CancellationToken cancellationToken)
    {
        var people = await container.People.AddQueryOption("$select", "UserName, FirstName, LastName")
            .ExecuteAsync(cancellationToken);
        return people.ToArray();
    }

    public void Search()
    {
        Console.WriteLine("Search not yet implemented");
    }
}