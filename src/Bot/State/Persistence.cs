﻿using System;
using System.Threading.Tasks;
using Azure.Data.Tables;

namespace Bot.State;

public class Persistence
{
    private readonly TableServiceClient _client;

    public Persistence(TableServiceClient client)
    {
        _client = client;
    }
    public async Task Store(ITableEntity t)
    {
        var tableClient = _client.GetTableClient(t.TableName());

        await tableClient.CreateIfNotExistsAsync();

        await tableClient.AddEntityAsync(t);
    }

    public async Task<T> Fetch<T>(string partitionKey, string rowKey) where T : class, ITableEntity, new()
    {
        var tableClient = _client.GetTableClient(typeof(T).TableName());

        await tableClient.CreateIfNotExistsAsync();

        return await tableClient.GetEntityAsync<T>(partitionKey, rowKey);
    }
}

public static class PersistenceExtensions
{
    public static string TableName(this Type t)
        => t.Name.ToLower();

    public static string TableName(this object o)
        => o.GetType().Name.ToLower();
}