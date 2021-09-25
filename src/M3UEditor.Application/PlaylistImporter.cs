using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace M3UEditor.App
{
    public class PlaylistImporter : IPlaylistImporter
    {
        private const string DurationRegex = "(?<=#EXTINF:)([+-]?[0-9]\\.[0-9]+|([+-])?[0-9]+)";
        private const string IdRegex = "(?<=tvg-id=\").*(?=\" tvg-n)";
        private const string NameRegex = "(?<=tvg-name=\").*(?=\" tvg-l)";
        private const string LogoRegex = "(?<=tvg-logo=\").*(?=\" g)";
        private const string GroupTitleRegex = "(?<=group-title=\").*(?=\",)";
        
        public async Task<IPlaylist> ImportAsync(string fileLocation, CancellationToken cancellationToken)
        {
            if (fileLocation is null)
            {
                throw new ArgumentNullException(nameof(fileLocation));
            }

            var allLines = await File
                .ReadAllLinesAsync(fileLocation, cancellationToken)
                .ConfigureAwait(false);

            var streams = CreateStreams(allLines);
            return new Playlist(streams);
        }

        private static IEnumerable<IStream> CreateStreams(IReadOnlyList<string> allLines)
        {
            var length = allLines.Count;
            var streams = new List<Stream>();

            for (var i = 1; i < length; i += 2)
            {
                if (i >= length)
                {
                    break;
                }

                streams.Add(CreateStream(allLines, i));
            }

            return streams;
        }

        private static Stream CreateStream(IReadOnlyList<string> allLines, int index)
        {
            var sampleLine = allLines[index];
            return new Stream
            {
                Duration = Regex.Match(sampleLine, DurationRegex).Value,
                Id = Regex.Match(sampleLine, IdRegex).Value,
                Name = Regex.Match(sampleLine, NameRegex).Value,
                LogoUri = Regex.Match(sampleLine, LogoRegex).Value,
                GroupTitle = Regex.Match(sampleLine, GroupTitleRegex).Value,
                Uri = allLines[index + 1]
            };
        }
    }
}