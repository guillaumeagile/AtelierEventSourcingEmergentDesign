using FluentAssertions;
using MediatR;
using Xunit;

namespace MyDotNetEventSourcedProject;

public class EventsDefinitionTests
{
    object[] events;
    public EventsDefinitionTests()
    {
        events = new object[]
        {
            new PlayerEnteredTheGame(1),
            new PlayerIsAttacked(1 , 2),
            new PlayerDiedEvent(1),
        };
    }

    [Fact]
    [Trait("Category", "SkipCI")]
    public void AllEventTypes_ShouldBeDefined()
    {
        const int expectedEventTypesCount = 3;
        events.Should().HaveCount(expectedEventTypesCount);
        events.GroupBy(e => e.GetType()).Should().HaveCount(expectedEventTypesCount);
    }

}
