namespace MyDotNetEventSourcedProject;

public class CommandBus
{
    IEventStore events;
    public CommandBus(IEventStore eventStore)
    {
        events = eventStore;
    }

    public Game Send(CommandPlayerEnterTheGame aCommand)
    {
        //aCommand.PlayeNickName
        events.PushNewEvent(new PlayerEnteredTheGame(1));
        return Game.GetGame(events);
    }
}
