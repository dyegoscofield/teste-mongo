using MediatR;
using TesteMongoDb.Api.Domain.AggregatesModel;

namespace TesteMongoDb.Api.Commands;

/// <summary>
/// A query class used to request a list of Example objects.
/// </summary>
public class ToListQuery : IRequest<IEnumerable<Example>>
{
}