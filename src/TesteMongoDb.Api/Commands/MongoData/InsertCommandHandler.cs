using System;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.Generic;
using MediatR;
using System.Threading.Tasks;
using System;
using System.Threading;
using System.Collections.Generic;
using TesteMongoDb.Api.Domain.AggregatesModel;

namespace TesteMongoDb.Api.Commands;

/// <summary>
/// Handles the insertion of a new Example entity.
/// </summary>
public class InsertCommandHandler : IRequestHandler<InsertCommand, Guid>
{
    private readonly IMongoDataRepository repository;

    /// <summary>
    /// Initializes a new instance of the <see cref="InsertCommandHandler"/> class.
    /// </summary>
    /// <param name="repository">The repository to perform data operations on.</param>
    public InsertCommandHandler(IMongoDataRepository repository)
    {
        this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    /// <summary>
    /// Handles the execution of the InsertCommand.
    /// </summary>
    /// <param name="command">The command containing the data to insert.</param>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the operation.</param>
    /// <returns>The GUID of the newly inserted entity.</returns>
    public async Task<Guid> Handle(InsertCommand command, CancellationToken cancellationToken)
    {
        var id = Guid.NewGuid();
        Example example = new(id, command.Nome);
        await repository.Insert(example, cancellationToken);
        return id;
    }
}