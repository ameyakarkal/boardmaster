using System.Collections.Generic;
using System.Linq;

namespace Bot.Domain;

public class Team
{
    public record TeamMember(
        string Name,
        string AboutMarkDown = "Apparently, this user prefers to keep an air of mystery about them.");

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