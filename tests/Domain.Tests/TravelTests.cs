namespace Domain.Tests;

public class TravelTests
{
    private readonly Fixture _fixture;

    public TravelTests()
    {
        _fixture = new Fixture();
    }

    [Fact]
    public void Empty_should_create_with_default_values()
    {
        // Arrange, Act
        var travel = Travel.Empty;

        // Assert
        travel.Type.ShouldBe(TravelType.Unknown);
        travel.Notes.ShouldBe(string.Empty);
        travel.DepartureLocation.ShouldBe(string.Empty);
        travel.ArrivalLocation.ShouldBe(string.Empty);
    }

    [Fact]
    public void ArrivalDate_if_empty_should_be_set_to_DepartureDate_when_DepartureDate_is_set()
    {
        // Arrange
        var travel = Travel.Empty;

        // Act
        travel.DepartureDate = _fixture.Create<DateTime>();

        // Assert
        travel.ArrivalDate.ShouldBe(travel.DepartureDate);
    }

    [Fact]
    public void ArrivalDate_if_not_empty_should_not_be_set_to_DepartureDate_when_DepartureDate_is_set()
    {
        // Arrange
        var travel = Travel.Empty;
        travel.DepartureDate = new DateTime(1991, 1, 9);

        // Act
        travel.ArrivalDate = new DateTime(2023, 5, 31);

        // Assert
        travel.DepartureDate.ShouldNotBe(travel.ArrivalDate);
    }

    [Theory]
    [InlineData("", "")]
    [InlineData("arrival", "")]
    [InlineData("", "departure")]
    public void Title_should_return_questionmark_when_locations_are_not_set(string arrivalLocation, string departureLocation)
    {
        // Arrange, Act
        var travel = Travel.Empty;
        travel.ArrivalLocation = arrivalLocation;
        travel.DepartureLocation = departureLocation;
        travel.ArrivalDate = _fixture.Create<DateTime>();
        travel.DepartureDate = _fixture.Create<DateTime>();

        // Assert
        travel.Title.ShouldBe("?");
    }

    [Fact]
    public void Title_should_return_arrival_and_departure_when_set()
    {
        // Arrange, Act
        var travel = Travel.Empty;
        travel.ArrivalLocation = "arrival location";
        travel.DepartureLocation = "departure location";
        travel.ArrivalDate = _fixture.Create<DateTime>();
        travel.DepartureDate = _fixture.Create<DateTime>();

        // Assert
        travel.Title.ShouldNotBe("?");
    }
}
