using MediatR;
using System.Threading.Tasks;
using System;
using System.Threading;
using System.Collections.Generic;
using TesteMongoDb.Api.Domain.AggregatesModel;

namespace TesteMongoDb.Api.Commands
{
    /// <summary>
    /// Handles the deletion of an entity.
    /// </summary>
    public class DeleteCommandHandler : IRequestHandler<DeleteCommand>
    {
        private readonly IMongoDataRepository repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteCommandHandler"/> class.
        /// </summary>
        /// <param name="repository">The repository to perform the delete operation.</param>
        /// <exception cref="ArgumentNullException">Thrown if the repository is null.</exception>
        public DeleteCommandHandler(IMongoDataRepository repository)
        {
            this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        /// <summary>
        /// Handles the delete command.
        /// </summary>
        /// <param name="command">The delete command containing the ID of the entity to be deleted.</param>
        /// <param name="cancellationToken">A cancellation token that can be used to cancel the operation.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task Handle(DeleteCommand command, CancellationToken cancellationToken)
        {
            await repository.Delete(command.Id, cancellationToken);
        }
    }
}