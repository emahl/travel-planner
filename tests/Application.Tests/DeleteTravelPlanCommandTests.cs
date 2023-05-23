using Application.Interfaces;
using Domain.Entities;
using Moq;

namespace Application.Tests;

public class DeleteTravelPlanCommandTests
{
    private readonly CancellationToken _cancellationToken;
    private readonly Fixture _fixture;
    private readonly TravelPlan _travelPlan;
    private readonly Mock<ITravelPlanRepository> _repositoryMock;
    private readonly DeleteTravelPlanCommandHandler _sut;

    public DeleteTravelPlanCommandTests()
    {
        _fixture = new Fixture();
        _cancellationToken = _fixture.Create<CancellationToken>();

        _travelPlan = new TravelPlan("test plan") { Id = _fixture.Create<Guid>() };
        
        _repositoryMock = new Mock<ITravelPlanRepository>();
        _repositoryMock.Setup(s => 
            s.GetAsync(It.Is<Guid>(id => id == _travelPlan.Id), It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult(_travelPlan));

        _sut = new DeleteTravelPlanCommandHandler(_repositoryMock.Object);
    }

    [Fact]
    public async Task Handle_should_set_deleted_and_update_entity()
    {
        // Arrange
        var command = new DeleteTravelPlanCommand(_travelPlan.Id);

        // Act
        await _sut.Handle(command, _cancellationToken);

        // Assert
        _repositoryMock.Verify(s => s.UpdateAsync(_travelPlan, _cancellationToken), Times.Once);
        _travelPlan.IsDeleted.ShouldBeTrue();
    }
}