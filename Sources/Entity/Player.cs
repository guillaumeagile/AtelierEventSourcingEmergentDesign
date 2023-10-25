using MyDotNetEventSourcedProject.Sources.Events;

namespace MyDotNetEventSourcedProject.Sources.Entity;

public record Player(int Id, int LifePoints)
{
    public Player ReveceiveAttack(int InjuryReceived, IEventStore myeventStore)
    {
        var newLifePoints = LifePoints - InjuryReceived;
       // prendre une décision et faire savoir que qq chose s'est passé

        return this with { LifePoints = newLifePoints };
    }
}
