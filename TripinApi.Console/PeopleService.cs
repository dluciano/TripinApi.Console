using Microsoft.Extensions.Options;
using Trippin;

namespace TripinApi.Console;

sealed class PeopleService
{
    private readonly Container container;

    public PeopleService(IOptions<AppSettings> options) =>
        container = new(new Uri(options.Value.TripPinApiUrl));

    public async Task<Person[]> ListAsync(CancellationToken cancellationToken)
    {
        var people = await container.People.AddQueryOption("$select", "UserName, FirstName, LastName")
            .ExecuteAsync(cancellationToken);
        return people.ToArray();
    }

    public async Task<Person[]> SearchByNamesAsync(string names, CancellationToken cancellationToken)
    {
        var namesArr = names.Split(" ").Select(s => s.Trim()).ToArray();
        string BuildContainsQuery(string field) =>
           string.Join(" or ", namesArr.Select(name => $"({field} ne null and contains({field},'{name}'))"));
        var firstNameQuery = BuildContainsQuery("FirstName");
        var middleNameQuery = BuildContainsQuery("MiddleName");
        var lastNameQuery = BuildContainsQuery("LastName");
        var query = $"{firstNameQuery} or {middleNameQuery} or {lastNameQuery}";
        var people = await container.People
            .AddQueryOption("$filter", query)
            .AddQueryOption("$select", "UserName, FirstName, LastName")
            .ExecuteAsync(cancellationToken);
        return people.ToArray();
    }

    internal async Task<Person> DetailsAsync(string userName, CancellationToken token)
    {
        var person = await container.People.ByKey(userName).GetValueAsync(token);
        return person;
    }
}