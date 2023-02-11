using System.Threading.Tasks;
using Azure.Data.Tables;

namespace Bot.State
{
    public interface IPersistence
    {
        public Task Store(ITableEntity t);

        public Task<T> Fetch<T>(string partitionKey, string rowKey) where T : class, ITableEntity, new();
    }
}
