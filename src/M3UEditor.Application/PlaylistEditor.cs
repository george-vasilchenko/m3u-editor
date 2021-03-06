using System;
using System.Collections.Generic;
using System.Linq;

namespace M3UEditor.App
{
    public class PlaylistEditor : IPlaylistEditor
    {
        public IPlaylist RemoveGroups(IPlaylist playlist, IEnumerable<string> groups)
        {
            if (playlist is null)
            {
                throw new ArgumentNullException(nameof(playlist));
            }

            if (groups is null)
            {
                throw new ArgumentNullException(nameof(groups));
            }

            playlist.RemoveGroups(groups.ToList());
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