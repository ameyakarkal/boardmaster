using System.Collections.Generic;
using System.Linq;

namespace Bot.Domain;

public class Team
{
    static Team()
    {
        TeamMembers = SuicideSquad().ToList();
    }

    public static IList<TeamMember> TeamMembers { get; }

    private static IEnumerable<TeamMember> SuicideSquad()
    {
        yield return new TeamMember("Ryan Fiust-Klink");
        yield return new TeamMember("George Pineda");
        yield return new TeamMember("Cody Pritchett");
        yield return new TeamMember("David Meyers");
    }
}