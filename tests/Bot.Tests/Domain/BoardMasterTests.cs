using System.Threading.Tasks;
using Xunit;
using Bot.Domain;
using Bot.State;
using Moq;

namespace Bot.Tests.Domain;

public class BoardMasterTests
{
    [Fact]
    public async Task ShouldPickTeamMember()
    {
        var bm = new BoardMaster(new BoardMaster.BoardMasterState(), new Mock<IPersistence>().Object);
        Assert.NotNull(await bm.Pick());
    }
}