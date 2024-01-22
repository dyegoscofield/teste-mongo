using MediatR;
using System;
using System.ComponentModel.DataAnnotations;

namespace TesteMongoDb.Api.Commands
{
    /// <summary>
    /// Command for inserting a new entity with a specified name.
    /// </summary>
    public class InsertCommand : IRequest<Guid>
    {
        /// <summary>
        /// Constructor for the insert command.
        /// </summary>
        /// <param name="nome">The name of the entity to be inserted.</param>
        public InsertCommand(string nome)
        {
            Nome = nome;
        }

        /// <summary>
        /// Gets the name of the entity to be inserted.
        /// </summary>
        [Required]
        public string Nome { get; private set; }
    }
}