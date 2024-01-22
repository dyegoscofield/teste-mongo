using MongoDB.Driver;
using System;
using System.Diagnostics.CodeAnalysis;
using TesteMongoDb.Api.Domain;
using TesteMongoDb.Api.Domain.AggregatesModel;

namespace TesteMongoDb.Api.Infrastructure.Mongo;

[ExcludeFromCodeCoverage]
/// <summary>
/// Represents a MongoDB context that implements the IContextMongo interface.
/// </summary>
public class ContextMongo : IContextMongo
{
    /// <summary>
    /// The MongoDB database instance.
    /// </summary>
    private IMongoDatabase? Database { get; set; }

    /// <summary>
    /// The client session handle for transactions.
    /// </summary>
    public IClientSessionHandle? Session { get; set; }

    /// <summary>
    /// The MongoDB client.
    /// </summary>
    public MongoClient? MongoClient { get; set; }

    /// <summary>
    /// Retrieves a collection from the MongoDB database.
    /// </summary>
    /// <typeparam name="T">The type of the collection's documents.</typeparam>
    /// <param name="name">The name of the collection.</param>
    /// <returns>The collection from the MongoDB database.</returns>
    public IMongoCollection<T> GetCollection<T>(string name)
    {
        ConfigureMongo();
        return Database.GetCollection<T>(name);
    }

    /// <summary>
    /// Disposes the context, releasing any resources.
    /// </summary>
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    /// <summary>
    /// Disposes the context, releasing any resources if disposing is true.
    /// </summary>
    /// <param name="disposing">Indicates whether the method has been called from Dispose.</param>
    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            Session?.Dispose();
        }
    }

    /// <summary>
    /// Configures the MongoDB client and database if they have not been configured.
    /// </summary>
    private void ConfigureMongo()
    {
        if (MongoClient != null)
        {
            return;
        }

        MongoClient = new MongoClient(Constantes.Mongo.ConnectionString);
        Database = MongoClient.GetDatabase(Constantes.Mongo.Database);
        Session = MongoClient.StartSession();
    }
}
