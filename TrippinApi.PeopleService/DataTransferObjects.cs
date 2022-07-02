namespace TrippinApi.Services;

public sealed record PersonPersonalInfoDto(string UserName, string FirstName, string MiddleName, string LastName);

public sealed record PersonDetailDto(
    string UserName,
    string FirstName,
    string MiddleName,
    string LastName,
    PersonGenderDto Gender,
    long? Age,
    string[] Emails,
    LocationDto[] AddressInfo,
    LocationDto HomeAddress,
    FeatureDto FavoriteFeature,
    FeatureDto[] Features);

public sealed record LocationDto(string Address, CityDto City);

public sealed record CityDto(string Name, string Region, string CountryRegion);

public enum PersonGenderDto
{
    Male,
    Female,
    Unknown
}

public enum FeatureDto
{
    Feature1 = 0,
    Feature2 = 1,
    Feature3 = 2,
    Feature4 = 3
}
