using System;
using System.Threading.Tasks;
using Azure;
using Azure.Data.Tables;
using Bot.State;

namespace Bot.Domain;

public class BoardMaster
{
    private readonly IPersistence _persistence;
    private readonly BoardMasterState _state;

    public BoardMaster(BoardMasterState state, IPersistence persistence)
    {
        _state = state;
        _persistence = persistence;
    }

    public BoardMasterState State => _state;

    public async Task<Team.TeamMember> Pick()
    {
        var teamSize = Team.TeamMembers.Count;

        _state.TeamMemberIndex = _state.TeamMemberIndex + 1 >= teamSize ? 0 : _state.TeamMemberIndex + 1;

        await Save();
        
        return Team.TeamMembers[_state.TeamMemberIndex];
    }

    public static async Task<BoardMaster> GetInstance(IPersistence persistence)
    {
        var state = await persistence.Fetch<BoardMasterState>(
            BoardMasterState.DefaultPartitionKey,
            BoardMasterState.DefaultRowKey);

        return new BoardMaster(
            state ?? new BoardMasterState
            {
                PartitionKey = BoardMasterState.DefaultPartitionKey,
                RowKey = BoardMasterState.DefaultRowKey
            }, persistence);
    }

    public async Task<BoardMaster> Save()
    {
        await _persistence.Store(State);

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
