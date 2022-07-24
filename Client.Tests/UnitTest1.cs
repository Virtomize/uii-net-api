using FluentAssertions;

namespace Client.Tests;

public class OsListTests
{
    [Test]
    public async Task EmptyList()
    {
        // Arrange
        var emptyResponse = "{\"_embedded\": []}";
        var client = new UII.Client(new ClientsMockFactory(emptyResponse));

        // Act
        var list = await client.ReadOsList();

        // Assert 
        list.Should().HaveCount(0);
    }
}