using MediatR;
using System.Threading.Tasks;
using System;
using System.Threading;
using System.Collections.Generic;
using TesteMongoDb.Api.Domain.AggregatesModel;

namespace TesteMongoDb.Api.Commands;

/// <summary>
/// Handler for processing modify commands.
/// </summary>
public class ModifyCommandHandler : IRequestHandler<ModifyCommand>
{
    private readonly IMongoDataRepository repository;

    /// <summary>
    /// Initializes a new instance of the <see cref="ModifyCommandHandler"/> class.
    /// </summary>
    /// <param name="repository">The data repository.</param>
    public ModifyCommandHandler(IMongoDataRepository repository)
    {
        this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    /// <summary>
    /// Handles the modify command.
    /// </summary>
    /// <param name="command">The command to be handled.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public async Task Handle(ModifyCommand command, CancellationToken cancellationToken)
    {
        Example example = new(command.Id, command.Nome);
        await repository.Modify(example, cancellationToken);
    }
}