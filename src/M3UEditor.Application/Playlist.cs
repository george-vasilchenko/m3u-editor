using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace M3UEditor.App
{
    public class Playlist : IPlaylist
    {
        private const string FileTypeFlag = "#EXTM3U";
        private IStream[] streams;

        public Playlist(IEnumerable<IStream> streams)
        {
            if (streams is null)
            {
                throw new ArgumentNullException(nameof(streams));
            }

            this.streams = streams.ToArray();
        }

        public IEnumerable<IStream> Streams => this.streams;

        public void RemoveGroups(IReadOnlyList<string> groupTitles)
        {
            if (groupTitles is null)
            {
                throw new ArgumentNullException(nameof(groupTitles));
            }

            var groupTitlesSet = new HashSet<string>(groupTitles);
            var resultCollectionLength = this.streams.Length - groupTitles.Count;
            var resultsCollection = new List<IStream>();

            foreach (var current in this.streams)
            {
                if (!groupTitlesSet.Contains(current.GroupTitle))
                {
                    resultsCollection.Add(current);
                }

                if (resultCollectionLength == resultsCollection.Count)
                {
                    break;
                }
            }

            this.streams = resultsCollection.ToArray();
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.AppendLine(FileTypeFlag);

            foreach (var stream in this.streams)
            {
                builder.AppendLine(stream.LinePropertiesToString());
                builder.AppendLine(stream.Uri);
            }

            return builder.ToString();
        }
    }
}