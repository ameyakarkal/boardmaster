using Azure.Data.Tables;
using System.Threading.Tasks;

namespace Bot.Domain
{
    public interface IPersistence
    {
        public Task Store(ITableEntity t);

        public Task<T> Fetch<T>(string partitionKey, string rowKey) where T : class, ITableEntity, new();
    }
}
