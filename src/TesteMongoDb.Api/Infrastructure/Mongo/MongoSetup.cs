using MongoDB.Driver;
using System.Security.Authentication;
using TesteMongoDb.Api.Domain.AggregatesModel;
using TesteMongoDb.Api.Domain;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace TesteMongoDb.Api.Infrastructure.Mongo;

/// <summary>
/// Static class for setting up MongoDB configurations.
/// </summary>
public static class MongoSetup
{
    /// <summary>
    /// Extension method for IServiceCollection to add MongoDB services.
    /// </summary>
    /// <param name="services">The IServiceCollection to add services to.</param>
    public static void AddMongo(this IServiceCollection services)
    {
        // Create MongoClientSettings from the connection string defined in Constantes.Mongo.
        var settings = MongoClientSettings.FromConnectionString(Constantes.Mongo.ConnectionString);
        
        // Set SSL settings to use TLS 1.2 protocol.
        settings.SslSettings = new SslSettings() { EnabledSslProtocols = SslProtocols.Tls12 };
        
        // Set the maximum time a thread will wait for a connection to become available.
        settings.WaitQueueTimeout = TimeSpan.FromSeconds(15);
        
        // Set the connection to use direct connection to the server.
        settings.DirectConnection = true;
        
        // Register the MongoClient as a singleton.
        services.AddSingleton<IMongoClient>(c => {
            return new MongoClient(settings);
        });
        
        // Register a scoped MongoDB session.
        services.AddScoped(c => c.GetService<IMongoClient>().StartSession());
        
        // Register the ContextMongo as a singleton.
        services.AddSingleton<IContextMongo, ContextMongo>();
    }
}