namespace MyDotNetEventSourcedProject.Sources.Events;

public record Event1(int PlayerId): EventBase(PlayerId.ToString());

public record Event2(int PlayerId, int InjuryReceived): EventBase(PlayerId.ToString());

public record Event3(int PlayerId): EventBase(PlayerId.ToString());

public enum BodyPart
{
    Head,
    Chest,
    Belly,
    Genitals,
    Arms,
    Legs
}