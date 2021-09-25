using System.Threading;
using System.Threading.Tasks;

namespace M3UEditor.App
{
    public interface IPlaylistImporter
    {
        Task<IPlaylist> ImportAsync(string fileLocation, CancellationToken cancellationToken);
    }
}