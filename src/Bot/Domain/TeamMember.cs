namespace Bot.Domain;


public record TeamMember(
    string Name, 
    string AboutMarkDown = "Apparently, this user prefers to keep an air of mystery about them.");