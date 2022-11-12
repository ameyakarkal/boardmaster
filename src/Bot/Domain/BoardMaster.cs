using System.Linq;

namespace Bot.Domain;

public class BoardMaster
{
    public TeamMember Pick()
    {
        return Team.TeamMembers.First();
    }
}
