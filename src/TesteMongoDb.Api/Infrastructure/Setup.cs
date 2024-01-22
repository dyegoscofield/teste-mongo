using TesteMongoDb.Api.Infrastructure.Mongo;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.Hosting;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;
using System;
using TesteMongoDb.Api.Domain.AggregatesModel;

namespace TesteMongoDb.Api.Infrastructure;

[ExcludeFromCodeCoverage]
/// <summary>
/// Extension class for setting up infrastructure-related services.
/// </summary>
public static class Setup
{
    /// <summary>
    /// Adds infrastructure services to the specified IServiceCollection.
    /// </summary>
    /// <param name="services">The IServiceCollection to add services to.</param>
    /// <returns>The IServiceCollection for chaining.</returns>
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        // Adds MongoDB related services.
        services.AddMongo();
        
        // Registers the MongoDataRepository as a singleton service.
        services.AddSingleton<IMongoDataRepository, MongoDataRepository>();
        
        return services;
    }
}