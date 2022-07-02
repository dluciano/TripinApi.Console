namespace TrippinApi.Services;

public interface IPeopleService
{
    Task<PersonPersonalInfoDto[]> ListAsync(CancellationToken cancellationToken);
    Task<PersonDetailDto?> DetailsAsync(string userName, CancellationToken cancellationToken);
    Task<PersonPersonalInfoDto[]> SearchByNamesAsync(string names, CancellationToken cancellationToken);
}