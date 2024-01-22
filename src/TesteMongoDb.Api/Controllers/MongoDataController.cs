using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.ComponentModel.DataAnnotations;
using TesteMongoDb.Api.Commands;
using TesteMongoDb.Api.Domain.AggregatesModel;

namespace TesteMongoDb.Api.Controllers;

[Route("[controller]")]
[ApiController]
/// <summary>
/// Controller for handling MongoDB data operations.
/// </summary>
public class MongoDataController : ControllerBase
{
    private readonly IMediator mediator;
    private readonly IMongoDataRepository mongoDataRepository;

    /// <summary>
    /// Initializes a new instance of the MongoDataController class.
    /// </summary>
    /// <param name="mediator">Mediator for command and query handling.</param>
    /// <param name="mongoDataRepository">Repository for MongoDB data operations.</param>
    public MongoDataController(IMediator mediator, IMongoDataRepository mongoDataRepository)
    {
        this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        this.mongoDataRepository = mongoDataRepository ?? throw new ArgumentNullException(nameof(mongoDataRepository));
    }

    /// <summary>
    /// Inserts a new record into the database.
    /// </summary>
    /// <param name="command">The insert command with the data to insert.</param>
    /// <param name="token">Cancellation token.</param>
    /// <returns>The ID of the created record.</returns>
    [HttpPost]
    [ProducesResponseType((int)HttpStatusCode.Created)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.UnprocessableEntity)]
    public async Task<ActionResult<Guid>> Insert([FromBody] InsertCommand command, CancellationToken token)
    {
        var id = await mediator.Send(command, token);
        return CreatedAtAction(nameof(GetById), new { id }, new { id });
    }

    /// <summary>
    /// Modifies an existing record in the database.
    /// </summary>
    /// <param name="command">The modify command with the data to update.</param>
    /// <param name="token">Cancellation token.</param>
    /// <returns>An ActionResult indicating success or failure.</returns>
    [HttpPut]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.UnprocessableEntity)]
    public async Task<ActionResult<Guid>> Modify([FromBody] ModifyCommand command, CancellationToken token)
    {
        await mediator.Send(command, token);
        return Ok();
    }

    /// <summary>
    /// Deletes a record from the database.
    /// </summary>
    /// <param name="command">The delete command with the ID of the record to remove.</param>
    /// <param name="token">Cancellation token.</param>
    /// <returns>An ActionResult indicating success or failure.</returns>
    [HttpDelete]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.UnprocessableEntity)]
    public async Task<ActionResult<Guid>> Delete([FromBody] DeleteCommand command, CancellationToken token)
    {
        await mediator.Send(command, token);
        return Ok();
    }

    /// <summary>
    /// Retrieves a list of all records in the database.
    /// </summary>
    /// <param name="token">Cancellation token.</param>
    /// <returns>An ActionResult containing an enumerable of records.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<Example>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<IEnumerable<Example>>> ToList(CancellationToken token)
    {
        var result = await mediator.Send(new ToListQuery(), token);
        return Ok(result);
    }

    /// <summary>
    /// Retrieves a single record by its ID.
    /// </summary>
    /// <param name="id">The ID of the record to retrieve.</param>
    /// <param name="token">Cancellation token.</param>
    /// <returns>An ActionResult containing the record, or a NoContent result if not found.</returns>
    [Route("{id}")]
    [HttpGet]
    [ProducesResponseType(typeof(Example), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    public async Task<ActionResult> GetById([FromRoute][Required] Guid id, CancellationToken token)
    {
        var result = await mediator.Send(new GetByIdQuery(id), token);
        if (result == null)
            return NoContent();

        return Ok(result);
    }
}
