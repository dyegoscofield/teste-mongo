using System;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.Generic;
using MediatR;
using TesteMongoDb.Api.Domain.AggregatesModel;
using System.Diagnostics;
using System.Net;

namespace TesteMongoDb.Api.Commands;

/// <summary>
/// Handles the ToListQuery by retrieving a list of Example objects from the repository.
/// </summary>
public class ToListQueryHandler : IRequestHandler<ToListQuery, IEnumerable<Example>>
{
    private readonly IMongoDataRepository mongoDataRepository;

    /// <summary>
    /// Initializes a new instance of the ToListQueryHandler class.
    /// </summary>
    /// <param name="mongoDataRepository">The repository to retrieve Example objects from.</param>
    /// <exception cref="ArgumentNullException">Thrown when mongoDataRepository is null.</exception>
    public ToListQueryHandler(IMongoDataRepository mongoDataRepository)
    {
        this.mongoDataRepository = mongoDataRepository ?? throw new ArgumentNullException(nameof(mongoDataRepository));
    }

    /// <summary>
    /// Handles the ToListQuery by delegating the call to the repository.
    /// </summary>
    /// <param name="command">The query to handle.</param>
    /// <param name="cancellationToken">A token to observe while waiting for the task to complete.</param>
    /// <returns>A task that represents the asynchronous operation, containing the list of Example objects.</returns>
    public async Task<IEnumerable<Example>> Handle(ToListQuery command, CancellationToken cancellationToken) => 
        await mongoDataRepository.ToList(cancellationToken);
}