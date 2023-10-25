namespace MyDotNetEventSourcedProject.Sources.Events;

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

public record PlayerIsAttacked(int PlayerId, int InjuryReceived): EventBase(PlayerId.ToString());

