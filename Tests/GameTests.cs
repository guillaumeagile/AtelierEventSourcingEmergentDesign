using FluentAssertions;
using MyDotNetEventSourcedProject.Sources.Events;
using MyDotNetEventSourcedProject.Sources.System;
using Xunit;

namespace MyDotNetEventSourcedProject.Tests;

public class GameTests
{
    
   [Fact]
   [Trait("Category", "SkipCI")]
    public void WhileNoEvents_BeginStateIsS()
    {
        IEventStore myeventStore = new FakeEventStore();
        Game.Subscribe(myeventStore);
        
        var game = Game.GetGame(myeventStore);
        game.progession.Should().Be(ProgressionState.NotStarted);

    }

    
    
    
    //[Fact(DisplayName = "14")]
    [Trait("Category", "SkipCI")]
    public void CreatedEventIsExisting()
    {
        IEventStore myeventStore = new FakeEventStore();
        Game.Subscribe(myeventStore);
        
        myeventStore.PushNewEvent(new PlayerEnteredTheGame(1));
        var game = Game.GetGame(myeventStore);

        game.progession.Should().Be(ProgressionState.Running);
    }

    //[Fact(DisplayName = "13")]
    [Trait("Category", "SkipCI")]
    public void OneEventsForOnePlayerInTheGame()
    {
        IEventStore myeventStore = new FakeEventStore();
        Game.Subscribe(myeventStore);
        
        myeventStore.PushNewEvent(new PlayerEnteredTheGame(1));
        var game = Game.GetGame(myeventStore);

        game.progession.Should().Be(ProgressionState.Running);
        game.listOfPlayers.Count().Should().Be(1);
    }


    //[Fact(DisplayName = "12")]
    [Trait("Category", "SkipCI")]
    public void TwoEventsForTwoPlayersInTheGame()
    {
        IEventStore myeventStore = new FakeEventStore();
        Game.Subscribe(myeventStore);
        
        myeventStore.PushNewEvent(new PlayerEnteredTheGame(1));
        myeventStore.PushNewEvent(new PlayerEnteredTheGame(2));
        var game = Game.GetGame(myeventStore);

        game.progession.Should().Be(ProgressionState.Running);
        game.listOfPlayers.Count().Should().Be(2);
    }

   // [Fact(DisplayName = "11")]
    [Trait("Category", "SkipCI")]
    public void OnePlayerInTheGameIsWoundedAndLifePointIsZero()
    {
        IEventStore myeventStore = new FakeEventStore();
        Game.Subscribe(myeventStore);
        
        myeventStore.PushNewEvent(new PlayerEnteredTheGame(1));
        myeventStore.PushNewEvent(new PlayerIsAttacked(1, 100));
        var game = Game.GetGame(myeventStore);

        game.listOfPlayers.First().LifePoints.Should().Be(0);
    }

 

 //   [Fact(DisplayName = "10")]
    [Trait("Category", "SkipCI")]
    public void OnePlayerInTheGameHasDiedAndThenHasLeftTheGame()
    {
        IEventStore myeventStore = new FakeEventStore();
        Game.Subscribe(myeventStore);

        myeventStore.PushNewEvent(new PlayerEnteredTheGame(222));
        myeventStore.PushNewEvent(new PlayerDiedEvent(222));

        var game = Game.GetGame(myeventStore);

        game.listOfPlayers.Count().Should().Be(0);

    }

    //[Fact(DisplayName = "10 optionnel") ]
    [Trait("Category", "SkipCI")]
    public void OnePlayerOutotTwoInTheGameHasDiedAndThenHasLeftTheGame()
    {
        IEventStore myeventStore = new FakeEventStore();
        Game.Subscribe(myeventStore);

        myeventStore.PushNewEvent(new PlayerEnteredTheGame(222));
        myeventStore.PushNewEvent(new PlayerEnteredTheGame(111));
        myeventStore.PushNewEvent(new PlayerDiedEvent(222));

        var game = Game.GetGame(myeventStore);

      //  game.listOfPlayers.Count().Should().Be(1);
      //  game.listOfPlayers.First().Id.Should().Be(111);
    }

    // [Fact(DisplayName = "06")]
    [Trait("Category", "SkipCI")]
    public void OnePlayerInTheGameIsWoundedAndLifePointsIsJustDecremented()
    {
        IEventStore myeventStore = new FakeEventStore();
        Game.Subscribe(myeventStore);
        
        myeventStore.PushNewEvent(new PlayerEnteredTheGame(1));
        myeventStore.PushNewEvent(new PlayerIsAttacked(1, 68));
        var game = Game.GetGame(myeventStore);

        game.listOfPlayers.First().LifePoints.Should().Be(32);
    }

   // [Fact(DisplayName = "05")] 05
    [Trait("Category", "SkipCI")]
    public void OnePlayerOutOfTwoInTheGameIsWounded()
    {
        IEventStore myeventStore = new FakeEventStore();
        Game.Subscribe(myeventStore);
        
        myeventStore.PushNewEvent(new PlayerEnteredTheGame(1));
        myeventStore.PushNewEvent(new PlayerEnteredTheGame(2));

        myeventStore.PushNewEvent(new PlayerIsAttacked(1, 50));
        myeventStore.PushNewEvent(new PlayerIsAttacked(2, 10));

        var game = Game.GetGame(myeventStore);

        game.listOfPlayers.First().Id.Should().Be(1);
        game.listOfPlayers.First().LifePoints.Should().Be(50);
        game.listOfPlayers.Last().LifePoints.Should().Be(90);
    }

    // [Fact(DisplayName = "04")] 04
    [Trait("Category", "SkipCI")]
    public void OnePlayerOutOfTwoInTheGameIsWoundedButOrderOfEventChangeTheOrderInTheListOfPlayers()
    {
        IEventStore myeventStore = new FakeEventStore();
        Game.Subscribe(myeventStore);
        
        myeventStore.PushNewEvent(new PlayerEnteredTheGame(1));
        myeventStore.PushNewEvent(new PlayerEnteredTheGame(2));

        myeventStore.PushNewEvent(new PlayerIsAttacked(2, 10));
        myeventStore.PushNewEvent(new PlayerIsAttacked(1, 50));

        var game = Game.GetGame(myeventStore);

        game.listOfPlayers.First().Id.Should().Be(2);
        game.listOfPlayers.First().LifePoints.Should().Be(90);
        // TEST DE CONSOLIDATION: pour nous, ca ne pose pas de probleme
    }

    // [Fact(DisplayName = "03")] 
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

}
