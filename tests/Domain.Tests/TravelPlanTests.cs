using AutoFixture;
using Domain.Entities;
using Domain.Enums;
using Shouldly;

namespace Domain.Tests;

public class TravelPlanTests
{
    private Fixture _fixture;

    public TravelPlanTests()
    {
        _fixture = new Fixture();
    }

    [Fact]
    public void Empty_should_create_with_default_values()
    {
        // Arrange, Act
        var travelPlan = TravelPlan.Empty;

        // Assert
        travelPlan.Name.ShouldBe(string.Empty);
        travelPlan.Type.ShouldBe(TravelPlanType.Suggestion);
        travelPlan.ThingsToDo.ShouldBeEmpty();
        travelPlan.IsDeleted.ShouldBeFalse();
        travelPlan.Budget.ShouldBeNull();
        travelPlan.Description.ShouldBeNull();
    }

    [Fact]
    public void ChangeName_should_set_new_value()
    {
        // Arrange
        var travelPlan = TravelPlan.Empty;

        // Act 
        travelPlan.ChangeName("new_name");

        // Assert
        travelPlan.Name.ShouldBe("new_name");
    }

    [Fact]
    public void ChangeBudget_should_set_new_value()
    {
        // Arrange
        var travelPlan = TravelPlan.Empty;

        // Act 
        travelPlan.ChangeBudget(1000);

        // Assert
        travelPlan.Budget.ShouldBe(1000);
    }

    [Fact]
    public void ChangeDescription_should_set_new_value()
    {
        // Arrange
        var travelPlan = TravelPlan.Empty;

        // Act 
        travelPlan.ChangeDescription("new_description");

        // Assert
        travelPlan.Description.ShouldBe("new_description");
    }

    [Fact]
    public void ChangeType_should_set_new_value()
    {
        // Arrange
        var travelPlan = TravelPlan.Empty;

        // Act 
        travelPlan.ChangeType(TravelPlanType.Completed);

        // Assert
        travelPlan.Type.ShouldBe(TravelPlanType.Completed);
    }

    [Fact]
    public void SetDeleted_should_set_IsDeleted_to_true()
    {
        // Arrange
        var travelPlan = TravelPlan.Empty;

        // Act 
        travelPlan.SetDeleted();

        // Assert
        travelPlan.IsDeleted.ShouldBeTrue();
    }

    [Fact]
    public void AddThingToDo_should_add_todo_item()
    {
        // Arrange
        var travelPlan = TravelPlan.Empty;
        var newTodoItem = _fixture.Create<TodoItem>();

        // Act 
        travelPlan.AddThingToDo(newTodoItem);

        // Assert
        travelPlan.ThingsToDo.Count.ShouldBe(1);
        travelPlan.ThingsToDo[0].ShouldBe(newTodoItem);
    }

    [Fact]
    public void DeleteThingToDo_should_delete_todo_item()
    {
        // Arrange
        var travelPlan = TravelPlan.Empty;
        var newTodoItem = _fixture.Create<TodoItem>();
        travelPlan.AddThingToDo(newTodoItem);

        // Act 
        travelPlan.DeleteThingToDo(newTodoItem.Id);

        // Assert
        travelPlan.ThingsToDo.ShouldBeEmpty();
    }
}
