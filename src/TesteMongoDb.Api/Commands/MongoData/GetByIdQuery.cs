using MediatR;
using System.ComponentModel.DataAnnotations;
using TesteMongoDb.Api.Domain.AggregatesModel;

namespace TesteMongoDb.Api.Commands;

/// <summary>
/// A query to retrieve an Example aggregate by its identifier.
/// </summary>
public class GetByIdQuery : IRequest<Example?>
{
    /// <summary>
    /// Initializes a new instance of the GetByIdQuery class with the specified identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the Example aggregate to retrieve.</param>
    public GetByIdQuery(Guid id)
    {
        Id = id;
    }

    /// <summary>
    /// Initializes a new instance of the GetByIdQuery class.
    /// </summary>
    public GetByIdQuery()
    {
    }

    /// <summary>
    /// Gets or sets the unique identifier of the Example aggregate to retrieve.
    /// </summary>
    /// <value>The identifier of the Example aggregate.</value>
    [Required]
    public Guid Id { get; set; }
}