using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace TesteMongoDb.Api.Domain.AggregatesModel;

/// <summary>
/// Defines a base repository interface for MongoDB operations for a specific entity type.
/// </summary>
/// <typeparam name="TEntity">The type of the entity.</typeparam>
public interface IBaseMongoRepository<TEntity> : IDisposable where TEntity : class
{
    /// <summary>
    /// Inserts a new entity into the MongoDB collection.
    /// </summary>
    /// <param name="obj">The entity to insert.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task Insert(TEntity obj, CancellationToken cancellationToken);

    /// <summary>
    /// Retrieves an entity by its identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the entity.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>A task containing the entity if found; otherwise, null.</returns>
    Task<TEntity> GetById(Guid id, CancellationToken cancellationToken);

    /// <summary>
    /// Retrieves all entities in the MongoDB collection.
    /// </summary>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>A task containing an enumerable of all entities.</returns>
    Task<IEnumerable<TEntity>> ToList(CancellationToken cancellationToken);

    /// <summary>
    /// Modifies an existing entity in the MongoDB collection.
    /// </summary>
    /// <param name="obj">The entity with updated values.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task Modify(TEntity obj, CancellationToken cancellationToken);

    /// <summary>
    /// Deletes an entity from the MongoDB collection by its identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the entity to delete.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task Delete(Guid id, CancellationToken cancellationToken);
}
