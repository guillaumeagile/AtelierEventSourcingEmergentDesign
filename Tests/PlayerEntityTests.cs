using FluentAssertions;
using MyDotNetEventSourcedProject.Sources.Entity;
using Xunit;

namespace MyDotNetEventSourcedProject.Tests;

public class PlayerEntityTests
{
   // [Fact(DisplayName = "09")]
    [Trait("Category", "SkipCI")]
    public void PlayerIsWoundedOnce()
    {
        var initPlayer = new Player(1, 100);

        var actualNewPlayer = initPlayer.ReveceiveAttack(25, new FakeEventStore());

        actualNewPlayer.Id.Should().Be(1);
        actualNewPlayer.LifePoints.Should().Be(0);
    }

    //[Fact(DisplayName = "08")]
    [Trait("Category", "SkipCI")]
    public void PlayerIsWoundedTwice()
    {
        var initPlayer = new Player(1, 100);

        var transit = initPlayer.ReveceiveAttack(25, new FakeEventStore());
        var actualNewPlayer = transit.ReveceiveAttack(25, new FakeEventStore());

        actualNewPlayer.Id.Should().Be(1);
        actualNewPlayer.LifePoints.Should().Be(0);
    }



}
