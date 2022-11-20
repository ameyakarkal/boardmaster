namespace Bot.Domain;

public class Messenger
{    
    public Notification Nudge(TeamMember member)
    {
        return new Notification(member.AboutMarkDown, member.Name);
    }
}
