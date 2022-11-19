using Xunit;
using Bot.Domain;
namespace Bot.Tests.Domain;

public class BoardMasterTests
{
    [Fact]
    public void ShouldPickTeamMember()
    {
        var bm = new BoardMaster();
        Assert.NotNull(bm.Pick());
    }
}