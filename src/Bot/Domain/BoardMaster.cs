using System.Linq;

namespace Bot.Domain;

public class BoardMaster
{
    private readonly Messenger messenger;

    public BoardMaster(Messenger messenger)
    {
        this.messenger = messenger;
    }
    public TeamMember Pick()
    {
        return Team.TeamMembers.First();
    }

    public TeamMember Nominate()
    {
        var tm = Pick();

        this.messenger.Nudge(tm);

        return tm;
    }
}
