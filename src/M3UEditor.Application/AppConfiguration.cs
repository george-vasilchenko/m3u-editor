using System.Collections.Generic;

namespace M3UEditor.App
{
    public record AppConfiguration(
            string PlaylistImportPath, 
            string PlaylistExportPath, 
            IEnumerable<string> GroupsToKeep) 
        : IAppConfiguration;
}