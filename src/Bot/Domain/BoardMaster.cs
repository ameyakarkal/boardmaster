using System;
using System.Threading.Tasks;
using Azure;
using Azure.Data.Tables;

namespace Bot.Domain;

public class BoardMaster
{
    private readonly BoardMasterState _state;

    public BoardMaster()
    {
    }

    public BoardMaster(BoardMasterState state)
    {
        _state = state;
    }

    public TeamMember Pick()
    {
        var teamSize = Team.TeamMembers.Count;

        _state.TeamMemberIndex = _state.TeamMemberIndex + 1 >= teamSize ? 0 : _state.TeamMemberIndex + 1;

        return Team.TeamMembers[_state.TeamMemberIndex];
    }

    public BoardMasterState State => _state;


    public static async Task<BoardMaster> Get(IPersistence persistence)
    {
        var state = await persistence.Fetch<BoardMaster.BoardMasterState>(
            BoardMaster.BoardMasterState.DefaultPartitionKey,
            BoardMaster.BoardMasterState.DefaultRowKey);

        return new BoardMaster(
            state ?? new BoardMasterState
            {
                PartitionKey = BoardMaster.BoardMasterState.DefaultPartitionKey,
                RowKey = BoardMaster.BoardMasterState.DefaultRowKey
            });
    }

    public async Task<BoardMaster> Save(IPersistence persistence)
    {
        await persistence.Store(State);

        return this;
    }

    public class BoardMasterState : ITableEntity
    {
        public const string DefaultRowKey = "1";
        public const string DefaultPartitionKey = "1";

        public BoardMasterState()
        {
            Timestamp = DateTimeOffset.UtcNow;
        }

        public string PartitionKey { get; set; }
        public string RowKey { get; set; }
        public DateTimeOffset? Timestamp { get; set; }
        public ETag ETag { get; set; }
        public int TeamMemberIndex { get; set; }
    }
}
