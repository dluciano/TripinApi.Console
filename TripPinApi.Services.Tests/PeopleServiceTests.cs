using FluentAssertions;
using Microsoft.Extensions.Options;
using NSubstitute;
using TrippinApi.Services;

namespace TripPinApi.Services.Tests;

public class PeopleServiceIntegrationTests
{
    private readonly PeopleService _sut;

    public PeopleServiceIntegrationTests()
    {
        var options = Substitute.For<IOptions<AppSettings>>();
        options.Value.Returns(new AppSettings()
        {
            TripPinApiUrl = "https://services.odata.org/TripPinRESTierService"
        });
        _sut = new PeopleService(options);
    }

    [Fact]
    public async Task WhenGetTheListOfPerson()
    {
        var expectedPeople = new PersonPersonalInfoDto[]{
            new("russellwhyte","Russell","","Whyte"),
            new("scottketchum","Scott","","Ketchum"),
            new("ronaldmundy","Ronald","","Mundy"),
            new("javieralfred","Javier","","Alfred"),
            new("willieashmore","Willie","","Ashmore"),
            new("vincentcalabrese","Vincent","","Calabrese"),
            new("clydeguess","Clyde","","Guess"),
            new("keithpinckney","Keith","","Pinckney"),
            new("marshallgaray","Marshall","","Garay"),
            new("ryantheriault","Ryan","","Theriault"),
            new("elainestewart","Elaine","","Stewart"),
            new("salliesampson","Sallie","","Sampson"),
            new("jonirosales","Joni","","Rosales"),
            new("georginabarlow","Georgina","","Barlow"),
            new("angelhuffman","Angel","","Huffman"),
            new("laurelosborn","Laurel","","Osborn"),
            new("sandyosborn","Sandy","","Osborn"),
            new("ursulabright","Ursula","","Bright"),
            new("genevievereeves","Genevieve","","Reeves"),
            new("kristakemp","Krista","","Kemp"),
        };

        // Act
        var people = await _sut.ListAsync(default);

        // Assert
        people.Should().BeEquivalentTo(expectedPeople);
    }

    [Fact]
    public async Task WhenGetAPersonDetails()
    {
        var expectedPersonDetails = new PersonDetailDto(
            UserName: "russellwhyte",
            FirstName: "Russell",
            MiddleName: "",
            LastName: "Whyte",
            PersonGenderDto.Male,
            Age: null,
            Emails: new string[] { "Russell@example.com", "Russell@contoso.com" },
            AddressInfo: new[]{
                new LocationDto("187 Suffolk Ln.", City:new ("Boise", "ID", "United States"))
                },
            HomeAddress: null,
            FeatureDto.Feature1,
            new[] { FeatureDto.Feature1, FeatureDto.Feature2 });

        // Act
        var people = await _sut.DetailsAsync("russellwhyte", default);

        // Assert
        people.Should().BeEquivalentTo(expectedPersonDetails);
    }

    [Fact]
    public async Task WhenSearchPeopleByNames()
    {
        var expectedPeople = new PersonPersonalInfoDto[]{
            new("ronaldmundy","Ronald","","Mundy"),
            new("jonirosales","Joni","","Rosales"),
        };

        // Act
        var people = await _sut.SearchByNamesAsync("Ro", default);

        // Assert
        people.Should().BeEquivalentTo(expectedPeople);
    }

    [Fact]
    public async Task WhenSearchPeopleByNamesAndResultIsEmpty()
    {
        // Act
        var people = await _sut.SearchByNamesAsync("-1", default);

        // Assert
        people.Should().BeEmpty();
    }
}