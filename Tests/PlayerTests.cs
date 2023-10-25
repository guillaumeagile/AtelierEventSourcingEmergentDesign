using FluentAssertions;
using MyDotNetEventSourcedProject.Sources;
using Xunit;

namespace MyDotNetEventSourcedProject;

public class PlayerTests
{
    List< IDomainEvent> events;
    public PlayerTests()
    {
        events = new ();
    }

    [Fact]
    [Trait("Category", "SkipCI")]
    public void PlayerIsWoundedOnce()
    {

        var initPlayer = new Player(1, 100);

        var actualNewPlayer = initPlayer.ReveceiveAttack(25, new FakeEventStore());

        actualNewPlayer.Id.Should().Be(1);
        actualNewPlayer.LifePoints.Should().Be(75);
    }

    [Fact]
    [Trait("Category", "SkipCI")]
    public void PlayerIsWoundedTwice()
    {

        var initPlayer = new Player(1, 100);

        var transit = initPlayer.ReveceiveAttack(25, new FakeEventStore());
        var actualNewPlayer = transit.ReveceiveAttack(25, new FakeEventStore());

        actualNewPlayer.Id.Should().Be(1);
        actualNewPlayer.LifePoints.Should().Be(50);
    }



}
