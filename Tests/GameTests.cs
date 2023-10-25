using System.Diagnostics.Tracing;
using FluentAssertions;
using MediatR;
using FluentAssertions.LanguageExt;
using MyDotNetEventSourcedProject.Sources;
using Xunit;

namespace MyDotNetEventSourcedProject;

public class GameTests
{
    List< IDomainEvent> events;
    public GameTests()
    {
        events = new ();
    }

   [ Fact]
    [Trait("Category", "SkipCI")]
    public void WhileNoEvents_BeginStateIsS()
    {
        var game = Game.GetGame(events);
        game.progession.Should().Be(ProgressionState.NotStarted);

    }

    [Fact]
    [Trait("Category", "SkipCI")]
    public void CreatedEventIsExisting()
    {
        events.Add(new PlayerEnteredTheGame(1));
        var game = Game.GetGame(events);

        game.progession.Should().Be(ProgressionState.Running);
    }

    [Fact]
    [Trait("Category", "SkipCI")]
    public void OneEventsForOnePlayerInTheGame()
    {
        events.Add(new PlayerEnteredTheGame(1));
        var game = Game.GetGame(events);

        game.progession.Should().Be(ProgressionState.Running);
        game.listOfPlayers.Count().Should().Be(1);
    }


    [Fact]
    [Trait("Category", "SkipCI")]
    public void TwoEventsForTwoPlayersInTheGame()
    {
        events.Add(new PlayerEnteredTheGame(1));
        events.Add(new PlayerEnteredTheGame(2));
        var game = Game.GetGame(events);

        game.progession.Should().Be(ProgressionState.Running);
        game.listOfPlayers.Count().Should().Be(2);
    }

    [Fact]
    [Trait("Category", "SkipCI")]
    public void OnePlayerInTheGameIsWounded()
    {
        events.Add(new PlayerEnteredTheGame(1));
        events.Add(new PlayerIsAttacked(1, 68));
        var game = Game.GetGame(events);

        game.listOfPlayers.First().LifePoints.Should().Be(32);
    }

    [Fact]
    [Trait("Category", "SkipCI")]
    public void OnePlayerInTheGameIsWoundedAndDied()
    {
        IEventStore myeventStore = new FakeEventStore();
        Game.Subscribe(myeventStore);

        myeventStore.PushNewEvent(new PlayerEnteredTheGame(1));
        myeventStore.PushNewEvent(new PlayerIsAttacked(1, 100));

        var game = Game.GetGame(myeventStore);
       game.listOfPlayers.First().LifePoints.Should().Be(0);
       myeventStore.Events.Should().Contain(new PlayerDiedEvent(1));

       var game2 = Game.GetGame(myeventStore);
       game2.listOfPlayers.Should().BeEmpty();
    }

    [Fact]
    [Trait("Category", "SkipCI")]
    public void OnePlayerInTheGameHasDiedAndThenHasLeftTheGame()
    {
        IEventStore myeventStore = new FakeEventStore();
        Game.Subscribe(myeventStore);

        events.Add(new PlayerEnteredTheGame(222));
        events.Add(new PlayerDiedEvent(222));

        var game = Game.GetGame(events);

        game.listOfPlayers.Count().Should().Be(0);

    }

    [Fact]
    [Trait("Category", "SkipCI")]
    public void OnePlayerOutotTwoInTheGameHasDiedAndThenHasLeftTheGame()
    {
        IEventStore myeventStore = new FakeEventStore();
        Game.Subscribe(myeventStore);

        events.Add(new PlayerEnteredTheGame(222));
        events.Add(new PlayerEnteredTheGame(111));
        events.Add(new PlayerDiedEvent(222));

        var game = Game.GetGame(events);

        game.listOfPlayers.Count().Should().Be(1);
        game.listOfPlayers.First().Id.Should().Be(111);
    }


    [Fact]
    [Trait("Category", "SkipCI")]
    public void OnePlayerOutOfTwoInTheGameIsWounded()
    {
        events.Add(new PlayerEnteredTheGame(1));
        events.Add(new PlayerEnteredTheGame(2));

        events.Add(new PlayerIsAttacked(1, 50));
        events.Add(new PlayerIsAttacked(2, 10));

        var game = Game.GetGame(events);

        game.listOfPlayers.First().Id.Should().Be(1);
        game.listOfPlayers.First().LifePoints.Should().Be(50);
        game.listOfPlayers.Last().LifePoints.Should().Be(90);
    }

    [Fact]
    [Trait("Category", "SkipCI")]
    public void OnePlayerOutOfTwoInTheGameIsWoundedButOrderOfEventChangeTheOrderInTheListOfPlayers()
    {
        events.Add(new PlayerEnteredTheGame(1));
        events.Add(new PlayerEnteredTheGame(2));

        events.Add(new PlayerIsAttacked(2, 10));
        events.Add(new PlayerIsAttacked(1, 50));

        var game = Game.GetGame(events);

        game.listOfPlayers.First().Id.Should().Be(2);
        game.listOfPlayers.First().LifePoints.Should().Be(90);
        // TEST DE CONSOLIDATION: pour nous, ca ne pose pas de probleme
    }



}
