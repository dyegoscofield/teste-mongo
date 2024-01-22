using MongoDB.Driver;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
using TesteMongoDb.Api.Domain.AggregatesModel;

namespace TesteMongoDb.Api.Infrastructure.Mongo;

[ExcludeFromCodeCoverage]
/// <summary>
/// A base repository class for MongoDB operations on a given entity type.
/// </summary>
/// <typeparam name="TEntity">The type of the entity this repository is responsible for.</typeparam>
public class BaseMongoRepository<TEntity> : IBaseMongoRepository<TEntity> where TEntity : class
{
    /// <summary>
    /// The MongoDB context.
    /// </summary>
    protected readonly IContextMongo Context;

    /// <summary>
    /// The MongoDB collection for the entity type.
    /// </summary>
    protected IMongoCollection<TEntity> DbSet;

    /// <summary>
    /// Initializes a new instance of the BaseMongoRepository class.
    /// </summary>
    /// <param name="context">The MongoDB context.</param>
    /// <param name="collectionName">The name of the MongoDB collection.</param>
    protected BaseMongoRepository(IContextMongo context, string collectionName)
    {
        Context = context;
        DbSet = Context.GetCollection<TEntity>(collectionName);
    }

    /// <summary>
    /// Inserts a new entity into the collection.
    /// </summary>
    /// <param name="obj">The entity to insert.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    public virtual async Task Insert(TEntity obj, CancellationToken cancellationToken)
    {
        await DbSet.InsertOneAsync(obj);
    }

    /// <summary>
    /// Retrieves an entity by its identifier.
    /// </summary>
    /// <param name="id">The entity's identifier.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    /// <returns>The entity found, or null if not found.</returns>
    public virtual async Task<TEntity> GetById(Guid id, CancellationToken cancellationToken)
    {
        var data = await DbSet.FindAsync(Builders<TEntity>.Filter.Eq("_id", id.ToString()));
        return data.SingleOrDefault();
    }

    /// <summary>
    /// Retrieves all entities in the collection.
    /// </summary>
    /// <param name="cancellationToken">A cancellation token.</param>
    /// <returns>An enumerable of all entities.</returns>
    public virtual async Task<IEnumerable<TEntity>> ToList(CancellationToken cancellationToken)
    {
        var all = await DbSet.FindAsync(Builders<TEntity>.Filter.Empty);
        return all.ToList();
    }

    /// <summary>
    /// Replaces an existing entity with a new entity in the collection.
    /// </summary>
    /// <param name="obj">The new entity.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    public virtual async Task Modify(TEntity obj, CancellationToken cancellationToken)
    {
        await DbSet.ReplaceOneAsync(Builders<TEntity>.Filter.Eq("_id", obj.GetId().ToString()), obj);
    }

    /// <summary>
    /// Deletes an entity from the collection by its identifier.
    /// </summary>
    /// <param name="id">The entity's identifier.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    public virtual async Task Delete(Guid id, CancellationToken cancellationToken)
    {
        await DbSet.DeleteOneAsync(Builders<TEntity>.Filter.Eq("_id", id.ToString()));
    }

    /// <summary>
    /// Disposes the current object.
    /// </summary>
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    /// <summary>
    /// Disposes the MongoDB context if disposing is true.
    /// </summary>
    /// <param name="disposing">A boolean value indicating whether the method has been called from the Dispose method.</param>
    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            Context?.Dispose();
        }
    }
}
