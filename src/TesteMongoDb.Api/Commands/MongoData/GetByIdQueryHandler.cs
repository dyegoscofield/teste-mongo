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
/// Handles the retrieval of an entity by its identifier.
/// </summary>
public class GetByIdQueryHandler : IRequestHandler<GetByIdQuery, Example?>
{
    private readonly IMongoDataRepository mongoDataRepository;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetByIdQueryHandler"/> class.
    /// </summary>
    /// <param name="mongoDataRepository">The repository to access the data.</param>
    public GetByIdQueryHandler(IMongoDataRepository mongoDataRepository)
    {
        this.mongoDataRepository = mongoDataRepository ?? throw new ArgumentNullException(nameof(mongoDataRepository));
    }

    /// <summary>
    /// Handles the GetByIdQuery request.
    /// </summary>
    /// <param name="command">The command containing the ID of the entity to retrieve.</param>
    /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
    /// <returns>A task that represents the asynchronous operation, containing the entity or null if not found.</returns>
    public async Task<Example?> Handle(GetByIdQuery command, CancellationToken cancellationToken) =>
        await mongoDataRepository.GetById(command.Id, cancellationToken);
}