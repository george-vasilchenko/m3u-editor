using System.Collections.Generic;

namespace M3UEditor.App
{
    public interface IPlaylistEditor
    {
        IPlaylist RemoveGroups(IPlaylist playlist, IEnumerable<string> groupNames);

        IPlaylist RemoveAllGroupsExcept(IPlaylist playlist, IEnumerable<string> groupsToKeep);
    }
}