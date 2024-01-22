
using System;
using System.Diagnostics.CodeAnalysis;
using TesteMongoDb.Api.Domain.AggregatesModel;
using TesteMongoDb.Api.Domain;
using TesteMongoDb.Api.Infrastructure.Mongo;

namespace TesteMongoDb.Api.Infrastructure;

[ExcludeFromCodeCoverage]
/// <summary>
/// Represents a repository for accessing data stored in a MongoDB collection specific to Example objects.
/// </summary>
public class MongoDataRepository : BaseMongoRepository<Example>, IMongoDataRepository
{
    /// <summary>
    /// Initializes a new instance of the MongoDataRepository class with the specified MongoDB context.
    /// </summary>
    /// <param name="context">The MongoDB context used for data access operations.</param>
    public MongoDataRepository(IContextMongo context)
        : base(context, Constantes.Mongo.CollectionNameExample)
    {
    }
}