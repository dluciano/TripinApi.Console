using Mapster;
using Microsoft.Extensions.Options;
using Microsoft.OData.Client;
using Trippin;

namespace TrippinApi.Services;

internal sealed class PeopleService : IPeopleService
{
    private readonly Container container;
    public const string SelectPersonInfoQuery = "UserName, FirstName, LastName";


    public PeopleService(IOptions<AppSettings> options) =>
        container = new(new Uri(options.Value.TripPinApiUrl));

    public async Task<PersonPersonalInfoDto[]> ListAsync(CancellationToken cancellationToken)
    {
        var people = (await container.People.AddQueryOption("$select", SelectPersonInfoQuery)
            .ExecuteAsync(cancellationToken))
            .ToArray();

        var personDtos = people.Select(person => person.AsPersonalInfoDto()).ToArray();

        return personDtos;
    }

    /// <summary>
    /// Search a person by first name, middle name and last name. The search performs a contains on each of the aftermention field. The search is case sensitive
    /// </summary>
    /// <param name="names">A list of names separated by space</param>
    /// <param name="cancellationToken"></param>
    /// <returns>A list of person</returns>
    public async Task<PersonPersonalInfoDto[]> SearchByNamesAsync(string names, CancellationToken cancellationToken)
    {
        var namesArr = names.Split(" ").Select(s => s.Trim()).ToArray();
        string BuildContainsQuery(string field) =>
           string.Join(" or ", namesArr.Select(name => $"({field} ne null and contains({field},'{name}'))"));
        var firstNameQuery = BuildContainsQuery("FirstName");
        var middleNameQuery = BuildContainsQuery("MiddleName");
        var lastNameQuery = BuildContainsQuery("LastName");
        var query = $"{firstNameQuery} or {middleNameQuery} or {lastNameQuery}";
        var people = (await container.People
            .AddQueryOption("$filter", query)
            .AddQueryOption("$select", SelectPersonInfoQuery)
            .ExecuteAsync(cancellationToken))
            .ToArray();
        var personDtos = people.Select(p => p.AsPersonalInfoDto()).ToArray();
        return personDtos;
    }

    public async Task<PersonDetailDto?> DetailsAsync(string userName, CancellationToken cancellationToken)
    {
        try
        {
            var person = await container.People.ByKey(userName).GetValueAsync(cancellationToken);
            return person.AsPersonalDetailDto();
        }
        catch (DataServiceQueryException e)
        when (e is not null && e.InnerException is DataServiceClientException d)
        {
            if (d.StatusCode == 404) return null;
            throw;
        }
    }
}