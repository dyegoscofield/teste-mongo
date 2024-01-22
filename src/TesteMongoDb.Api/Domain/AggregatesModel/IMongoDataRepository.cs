
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace TesteMongoDb.Api.Domain.AggregatesModel;

/**
 * Interface for a data repository specific to MongoDB that extends the base MongoDB repository interface.
 * This interface is tailored for operations on `Example` objects.
 */
public interface IMongoDataRepository : IBaseMongoRepository<Example> { }