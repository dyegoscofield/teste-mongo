using Moq;
using System;
using TesteMongoDb.Api.Domain.AggregatesModel;
using TesteMongoDb.Api.Infrastructure;
using Xunit;

namespace TesteMongoDb.Tests.Infrastructure;

public class MongoDataRepositoryTests
{
    public MongoDataRepositoryTests()
    {
        // Configurar a variável de ambiente temporariamente para o teste
        Environment.SetEnvironmentVariable("MONGODB_COLLECTION_NAME", "MONGODB_COLLECTION_NAME");
    }

    [Fact]
    public void Constructor_WithValidParameters_ShouldNotThrowException()
    {
        // Arrange
        var contextMock = new Mock<IContextMongo>();

        // Act
        void act()
        {
            _ = new MongoDataRepository(contextMock.Object);
        }

        // Assert
        Assert.Null(Record.Exception(act));
    }

    [Fact]
    public void Constructor_WithNullContext_ShouldThrowArgumentNullException()
    {
        // Arrange
        IContextMongo context = null;

        // Act
        void act()
        {
            _ = new MongoDataRepository(context);
        }

        // Assert
        Assert.Throws<ArgumentNullException>(act);
    }
}