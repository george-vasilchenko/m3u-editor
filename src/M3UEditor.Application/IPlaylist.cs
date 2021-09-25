using System.Collections.Generic;

namespace M3UEditor.App
{
    public interface IPlaylist
    {
        IEnumerable<IStream> Streams { get; }

        void RemoveGroups(IReadOnlyList<string> groupTitles);
    }
}