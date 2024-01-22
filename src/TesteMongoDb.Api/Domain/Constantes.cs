using System;
using System.Diagnostics.CodeAnalysis;

namespace TesteMongoDb.Api.Domain;

[ExcludeFromCodeCoverage]
/// <summary>
/// Static class containing constants for the application.
/// </summary>
public static class Constantes
{
    /// <summary>
    /// Static class containing MongoDB related constants.
    /// </summary>
    public static class Mongo
    {
        /// <summary>
        /// Retrieves the MongoDB user from environment variables or throws an exception if not found.
        /// </summary>
        public static string Usuario => Environment.GetEnvironmentVariable("MONGODB_USER") ?? throw new ApplicationException(GetArgumentNullException("MONGODB_USER"));

        /// <summary>
        /// Retrieves the MongoDB password from environment variables or throws an exception if not found.
        /// </summary>
        public static string Senha => Environment.GetEnvironmentVariable("MONGODB_PASSWORD") ?? throw new ApplicationException(GetArgumentNullException("MONGODB_PASSWORD"));

        /// <summary>
        /// Retrieves the MongoDB database name from environment variables or throws an exception if not found.
        /// </summary>
        public static string Database => Environment.GetEnvironmentVariable("MONGODB_DATABASE") ?? throw new ApplicationException(GetArgumentNullException("MONGODB_DATABASE"));

        /// <summary>
        /// Retrieves the MongoDB collection name from environment variables or throws an exception if not found.
        /// </summary>
        public static string CollectionNameExample => Environment.GetEnvironmentVariable("MONGODB_COLLECTION_NAME") ?? throw new ApplicationException(GetArgumentNullException("MONGODB_COLLECTION_NAME"));

        /// <summary>
        /// Constructs the MongoDB connection string from environment variables, replacing placeholders with user credentials, or throws an exception if not found.
        /// </summary>
        public static string ConnectionString => Environment.GetEnvironmentVariable("MONGODB_CONNECTION")?.Replace("{USER}", Usuario).Replace("{PASSWORD}", Senha) ?? throw new ApplicationException(GetArgumentNullException("MONGODB_CONNECTION"));
    }

    /// <summary>
    /// Generates an error message for missing environment variables.
    /// </summary>
    /// <param name="argument">The name of the missing environment variable.</param>
    /// <returns>A formatted error message indicating the missing environment variable.</returns>
    static string GetArgumentNullException(string argument) => $"Environment Variable {argument} not informed.";
}