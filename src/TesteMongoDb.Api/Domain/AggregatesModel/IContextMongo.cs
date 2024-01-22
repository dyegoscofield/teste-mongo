using MongoDB.Driver;
using System;

namespace TesteMongoDb.Api.Domain.AggregatesModel;

/// <summary>
/// Defines an interface for a MongoDB context that can be disposed.
/// </summary>
public interface IContextMongo : IDisposable
{
    /// <summary>
    /// Retrieves a Mongo collection for a given type.
    /// </summary>
    /// <typeparam name="T">The type of the documents in the collection.</typeparam>
    /// <param name="name">The name of the collection to retrieve.</param>
    /// <returns>An instance of <see cref="IMongoCollection{T}"/>.</returns>
    IMongoCollection<T> GetCollection<T>(string name);
}