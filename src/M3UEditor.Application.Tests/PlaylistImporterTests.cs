using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
using Xunit;

namespace M3UEditor.App.Tests
{
    public class PlaylistImporterTests
    {
        private readonly IFixture fixture;
        private readonly PlaylistImporter subject;

        public PlaylistImporterTests()
        {
            this.fixture = new Fixture();
            this.subject = new PlaylistImporter();
        }

        [Fact]
        public async Task ImportAsync()
        {
            // arrange
            const string fileLocation = "/Users/george.vasilchenko/Documents/Private/code/github/m3u-editor/temp/all.m3u";

            // act
            var lines = await this.subject.ImportAsync(fileLocation, CancellationToken.None);

            // assert
        }
    }
}