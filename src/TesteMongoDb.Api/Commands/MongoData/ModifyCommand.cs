using MediatR;
using System;
using System.ComponentModel.DataAnnotations;

namespace TesteMongoDb.Api.Commands
{
    /// <summary>
    /// Command to modify an existing entity.
    /// </summary>
    public class ModifyCommand : IRequest
    {
        /// <summary>
        /// Initializes a new instance of the ModifyCommand class.
        /// </summary>
        /// <param name="id">The unique identifier of the entity to be modified.</param>
        /// <param name="nome">The new name for the entity.</param>
        public ModifyCommand(Guid id, string nome)
        {
            Id = id;
            Nome = nome;
        }

        /// <summary>
        /// Gets the unique identifier of the entity to be modified.
        /// </summary>
        [Required]
        public Guid Id { get; private set; }

        /// <summary>
        /// Gets the new name for the entity.
        /// </summary>
        [Required]
        public string Nome { get; private set; }
    }
}