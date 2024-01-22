using MediatR;
using System;
using System.ComponentModel.DataAnnotations;

namespace TesteMongoDb.Api.Commands
{
    /// <summary>
    /// Represents a command to delete an entity by its identifier.
    /// </summary>
    public class DeleteCommand : IRequest
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteCommand"/> class.
        /// </summary>
        /// <param name="id">The unique identifier of the entity to be deleted.</param>
        public DeleteCommand(Guid id)
        {
            Id = id;
        }

        /// <summary>
        /// Gets the unique identifier of the entity to be deleted.
        /// </summary>
        [Required]
        public Guid Id { get; private set; }
    }
}