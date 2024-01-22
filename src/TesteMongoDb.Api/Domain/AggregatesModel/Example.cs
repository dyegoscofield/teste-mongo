using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Diagnostics.CodeAnalysis;

namespace TesteMongoDb.Api.Domain.AggregatesModel;

using System;
using System.Diagnostics.CodeAnalysis;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

[ExcludeFromCodeCoverage]

/// <summary>
/// Represents an example class with an identifier and a name.
/// </summary>
public class Example
{
    /// <summary>
    /// Initializes a new instance of the Example class with the specified identifier and name.
    /// </summary>
    /// <param name="id">The unique identifier for the example.</param>
    /// <param name="nome">The name associated with the example.</param>
    public Example(Guid id, string nome)
    {
        Id = id;
        Nome = nome;
    }

    /// <summary>
    /// Gets the unique identifier for the example.
    /// The identifier is represented as a string in BSON.
    /// </summary>
    [BsonRepresentation(BsonType.String)]
    [BsonId]
    public Guid Id { get; private set; }

    /// <summary>
    /// Gets or sets the name associated with the example.
    /// The name can be null.
    /// </summary>
    public string? Nome { get; set; }
}