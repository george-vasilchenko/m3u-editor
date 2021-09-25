using System.Collections.Generic;

namespace M3UEditor.App
{
    public interface IAppConfiguration
    {
        string PlaylistImportPath { get; }

        string PlaylistExportPath { get; }

        IEnumerable<string> GroupsToKeep { get; }
    }
}