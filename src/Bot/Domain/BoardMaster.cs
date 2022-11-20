using System;
using Azure;
using Azure.Data.Tables;

namespace Bot.Domain;

public class BoardMaster
{
    private readonly BoardMasterState _state;

    public BoardMaster(BoardMasterState state)
    {
        _state = state;
    }

    public TeamMember Pick()
    {
        var teamSize = Team.TeamMembers.Count;

        var index = _state.TeamMemberIndex + 1 >= teamSize
                ? 0
                : _state.TeamMemberIndex + 1;

        // persist index

        return Team.TeamMembers[index];
    }

    public class BoardMasterState : ITableEntity
    {
        public BoardMasterState()
        {
            RowKey = "1";
            PartitionKey = "1";
            Timestamp = DateTimeOffset.UtcNow;
        }

        public string PartitionKey { get; set; }
        public string RowKey { get; set; }
        public DateTimeOffset? Timestamp { get; set; }
        public ETag ETag { get; set; }

        public int TeamMemberIndex { get; set; }
    }
}
