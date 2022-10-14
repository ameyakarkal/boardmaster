namespace Bot.Domain;

public class Team
{
    static Team()
    {
        TeamMembers = new List<TeamMember>
        {
            new TeamMember("Bob")
        };
    }

    public static IList<TeamMember> TeamMembers { get; }
}