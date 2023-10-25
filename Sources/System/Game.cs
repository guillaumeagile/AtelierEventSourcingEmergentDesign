using MyDotNetEventSourcedProject.Sources.Entity;
using MyDotNetEventSourcedProject.Sources.Events;

namespace MyDotNetEventSourcedProject.Sources.System;
 public enum ProgressionState
    {
        NotStarted,
        Running,
        Ended
    }
public record Game(ProgressionState progession, IEnumerable<Player> listOfPlayers)
{
    public static Game Default() => new(ProgressionState.NotStarted, new List<Player>() );

    public static Game GetGame(IEventStore myeventStore)
    {
        return GetGame(myeventStore.Events);
    }

    public static Game GetGame(IEnumerable<object> events) =>
        events.Aggregate(Game.Default(), Game.When);

    public static Game When(Game game, object @event)
    {
        return @event switch
        {
            // https://www.educative.io/answers/what-is-non-destructive-mutation-in-c-sharp-90
            PlayerEnteredTheGame(int PlayerId) => game with  
            {
                progession = ProgressionState.Running,
                listOfPlayers =  game.listOfPlayers.Append((new Player(PlayerId, 100)))
            },
            PlayerIsAttacked(int PlayerId, int InjuryReceived) => game with
                          {
                              listOfPlayers = ListAfterOnePlayerHasBeenAttacked(game, PlayerId, InjuryReceived)
                          },
            PlayerDiedEvent(int PlayerId) => game with
                          {
                              listOfPlayers = ListAfterOnePlayerHasDied(game, PlayerId)
                          },
            _ => game
        };
    }

    private static IEnumerable<Player> ListAfterOnePlayerHasDied(Game game, int playerId)
    {
        return game.listOfPlayers.Filter(p => p.Id != playerId).ToList();

    }

    private static IEnumerable<Player> ListAfterOnePlayerHasBeenAttacked(Game game, int playerId, int injuryReceived)
    {
        var concernedPlayer = game.listOfPlayers.Filter(p => p.Id == playerId).FirstOrDefault();
        var newListOfPlayers = game.listOfPlayers.Filter(p => p.Id != playerId).ToList();
        
        newListOfPlayers.Add(concernedPlayer.ReveceiveAttack(injuryReceived, _myeventStore));

        return newListOfPlayers;
    }

    private static IEventStore _myeventStore = null!;
    public static void Subscribe(IEventStore myEventStore)
    {
        _myeventStore = myEventStore;
    }
}
