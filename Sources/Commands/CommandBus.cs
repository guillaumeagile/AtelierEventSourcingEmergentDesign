using MyDotNetEventSourcedProject.Sources.Events;
using MyDotNetEventSourcedProject.Sources.System;

namespace MyDotNetEventSourcedProject.Sources.Commands;

public class CommandBus
{
    readonly IEventStore events;
    public CommandBus(IEventStore eventStore)
    {
        events = eventStore;
    }

    public Game Send(CommandPlayerEnterTheGame aCommand)
    {
        // pousser l'evenement qui correspond à la commande
       
        return Game.GetGame(events);
    }
}
