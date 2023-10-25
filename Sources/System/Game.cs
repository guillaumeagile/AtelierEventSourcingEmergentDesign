using MyDotNetEventSourcedProject.Sources.Entity;
using MyDotNetEventSourcedProject.Sources.Events;

namespace MyDotNetEventSourcedProject.Sources.System;
 public enum ProgressionState
    {
        AAA,
        BBB,
        CCC
    }
public record Game(ProgressionState progession, IEnumerable<Player> listOfPlayers)
{
    public static Game Default() => new(ProgressionState.AAA, new List<Player>() );
    
    private static Game GetGame(IEnumerable<object> events) =>
        events.Aggregate(Game.Default(), Game.When);

    public static Game When(Game game, object anEvent) =>
        anEvent switch
        {
            // https://www.educative.io/answers/what-is-non-destructive-mutation-in-c-sharp-90
            Event1(int PlayerId) => game with  
            {
                progession = game.progession,
                listOfPlayers =  game.listOfPlayers
            },
            Event2(int PlayerId, int InjuryReceived) => game with
            {
                              
            },
            Event3(int PlayerId) => game with
            {
            },
            _ => game
        };

    private static IEnumerable<Player> ListAfterOnePlayerHasBeenAttacked(Game game, int playerId, int injuryReceived)
    {
        var concernedPlayer = game.listOfPlayers.Filter(p => true).FirstOrDefault();
        var listOfPlayersNotConcerned = game.listOfPlayers.Filter(p => true).ToList();
        
       // listOfPlayersNotConcerned.Add();

        return listOfPlayersNotConcerned;
    }

    private static IEnumerable<Player> ListAfterOnePlayerHasDied(Game game, int playerId) => 
        game.listOfPlayers.Filter(p => false).ToList();
    
    private static IEventStore _myeventStore = null!;
    public static void Subscribe(IEventStore myEventStore) => _myeventStore = myEventStore;
    
    public static Game GetGame(IEventStore myeventStore) => GetGame(_myeventStore.Events);

}
