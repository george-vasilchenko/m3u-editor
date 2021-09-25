using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace M3UEditor.App
{
    public class PlaylistExporter : IPlaylistExporter
    {
        public async ValueTask ExportAsync(string fileLocation, IPlaylist playlist, CancellationToken cancellationToken)
        {
            if (fileLocation is null)
            {
                throw new ArgumentNullException(nameof(fileLocation));
            }

            if (playlist is null)
            {
                throw new ArgumentNullException(nameof(playlist));
            }

            if (File.Exists(fileLocation))
            {
                File.Delete(fileLocation);
            }

            var fileContent = playlist.ToString();
            await File.WriteAllTextAsync(fileLocation, fileContent, cancellationToken);
        }
    }
}