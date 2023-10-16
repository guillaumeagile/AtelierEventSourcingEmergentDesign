namespace MyDotNetEventSourcedProject;

public enum BodyPart
{
    Head,
    Chest,
    Belly,
    Genitals,
    Arms,
    Legs
}

public record PlayerEnteredTheGame(int PlayerId): EventBase(PlayerId.ToString());
public record PlayerDiedEvent(int PlayerId): EventBase(PlayerId.ToString());

//public record PlayerAttackedByZombieEvent(int PlayerId, int ZombieId, BodyPart Target): EventBase(PlayerId.ToString());


public record PlayerIsAttacked(int PlayerId, int InjuryReceived): EventBase(PlayerId.ToString());

