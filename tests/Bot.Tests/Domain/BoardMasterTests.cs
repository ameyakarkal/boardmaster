using Xunit;
using Bot.Domain;
namespace Bot.Tests.Domain;

public class BoardMasterTests
{
    [Fact]
    public void ShouldPickTeamMember()
    {
        var bm = new BoardMaster(new BoardMaster.BoardMasterState());
        Assert.NotNull(bm.Pick());
    }
}