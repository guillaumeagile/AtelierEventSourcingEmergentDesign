using MyDotNetEventSourcedProject.Sources.Events;

namespace MyDotNetEventSourcedProject.Sources.Entity;

public record Player(int Id, int LifePoints)
{
    public Player ReveceiveAttack(int InjuryReceived, IEventStore myeventStore)
    {
       // changer l'etat du player
       
       // prendre une décision et faire savoir que qq chose s'est passé

        return this with {  };
    }
}
