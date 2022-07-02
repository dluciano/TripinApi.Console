using Trippin;

namespace TripinApi.Console
{
    internal interface IPeopleService
    {
        Task<Person[]> ListAsync(CancellationToken cancellationToken);
        Task<Person> DetailsAsync(string userName, CancellationToken cancellationToken);
        Task<Person[]> SearchByNamesAsync(string names, CancellationToken cancellationToken);
    }
}