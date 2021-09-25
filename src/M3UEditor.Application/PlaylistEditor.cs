using System;
using System.Collections.Generic;
using System.Linq;

namespace M3UEditor.App
{
    public class PlaylistEditor : IPlaylistEditor
    {
        public IPlaylist RemoveGroups(IPlaylist playlist, IEnumerable<string> groupNames)
        {
            if (playlist is null)
            {
                throw new ArgumentNullException(nameof(playlist));
            }

            if (groupNames is null)
            {
                throw new ArgumentNullException(nameof(groupNames));
            }

            
            
            return playlist;
        }

        public IPlaylist RemoveAllGroupsExcept(IPlaylist playlist, IEnumerable<string> groupsToKeep)
        {
            if (playlist is null)
            {
                throw new ArgumentNullException(nameof(playlist));
            }

            if (groupsToKeep is null)
            {
                throw new ArgumentNullException(nameof(groupsToKeep));
            }

            var groupTitles = playlist.Streams
                .Where(s => groupsToKeep.All(gt => gt != s.GroupTitle))
                .Select(s => s.GroupTitle)
                .ToList();
            playlist.RemoveGroups(groupTitles);

            return playlist;
        }
    }
}