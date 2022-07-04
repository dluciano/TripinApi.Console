using Mapster;
using Trippin;

namespace TrippinApi.Services;

internal static class ApiEntitiesToDtoExtensions
{
    static readonly TypeAdapterSetter<Person, PersonPersonalInfoDto> PersonInfoAdapter = TypeAdapterConfig<Person, PersonPersonalInfoDto>.NewConfig()
           .Map(dest => dest.FirstName, src => src.FirstName ?? string.Empty)
           .Map(dest => dest.LastName, src => src.LastName ?? string.Empty)
           .Map(dest => dest.MiddleName, src => src.MiddleName ?? string.Empty)
           .AddDestinationTransform(DestinationTransform.EmptyCollectionIfNull);

    static readonly TypeAdapterSetter<Person, PersonDetailDto> PersonDetailAdapter = TypeAdapterConfig<Person, PersonDetailDto>.NewConfig()
        .Map(dest => dest.FirstName, src => src.FirstName ?? string.Empty)
        .Map(dest => dest.LastName, src => src.LastName ?? string.Empty)
        .Map(dest => dest.MiddleName, src => src.MiddleName ?? string.Empty)
        .Map(dest => dest.HomeAddress.Address, src => src.HomeAddress.Address ?? string.Empty)
        .Map(dest => dest.HomeAddress.City.Name, src => src.HomeAddress.City.Name ?? string.Empty)
        .Map(dest => dest.HomeAddress.City.Region, src => src.HomeAddress.City.Region ?? string.Empty)
        .Map(dest => dest.HomeAddress.City.CountryRegion, src => src.HomeAddress.City.CountryRegion ?? string.Empty)
        .AddDestinationTransform(DestinationTransform.EmptyCollectionIfNull);

    public static PersonPersonalInfoDto AsPersonalInfoDto(this Person person) =>
        person.Adapt<PersonPersonalInfoDto>(PersonInfoAdapter.Config);

    public static PersonDetailDto AsPersonalDetailDto(this Person person) =>
        person.Adapt<PersonDetailDto>(PersonDetailAdapter.Config);
}
