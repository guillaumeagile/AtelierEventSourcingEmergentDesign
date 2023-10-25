using FluentAssertions;
using MyDotNetEventSourcedProject.Sources;
using Xunit;

namespace MyDotNetEventSourcedProject;

public class CommandsForGameTest
{
    [Fact]
    [Trait("Category", "SkipCI")]
    public void WhileOneCommand_BeginStateIsS()
    {
        var fakeEventStore = new FakeEventStore();
        var aCommand = new CommandPlayerEnterTheGame("mathieu");
        var commandBus = new CommandBus(fakeEventStore);

        var game = commandBus.Send(aCommand);
        // obtenir l'état du game direcment comme résultante d'une commande ??
        // ou bien passer par un objet de Query (CQRS) qui "questionne" l'état du game ??

          game.progession.Should().Be(ProgressionState.Running);
          game.listOfPlayers.Count().Should().Be(1);
    }

    [Fact]
    [Trait("Category", "SkipCI")]
    public void While2Commands()
    {
        var fakeEventStore = new FakeEventStore();
        var aCommand1 = new CommandPlayerEnterTheGame("mathieu");
        var aCommand2 = new CommandPlayerEnterTheGame("lisa");

        var commandBus = new CommandBus(fakeEventStore);
        var commandBus2 = new CommandBus(fakeEventStore);

        var game = commandBus.Send(aCommand1);
        game = commandBus2.Send(aCommand2);

        game.progession.Should().Be(ProgressionState.Running);
        game.listOfPlayers.Count().Should().Be(2);
    }
}
