using System.Threading;
using System.Threading.Tasks;

namespace M3UEditor.App
{
    public interface IPlaylistExporter
    {
        ValueTask ExportAsync(string fileLocation, IPlaylist playlist, CancellationToken cancellationToken);
    }
}