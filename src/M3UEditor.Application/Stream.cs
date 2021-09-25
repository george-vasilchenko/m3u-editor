namespace M3UEditor.App
{
    public class Stream : IStream
    {
        public string Duration { get; set; }

        public string Id { get; set; }

        public string Name { get; set; }

        public string LogoUri { get; set; }

        public string GroupTitle { get; set; }

        public string Uri { get; set; }

        public string LinePropertiesToString() =>
            $"#EXTINF:{this.Duration} " +
            $"tvg-id=\"{this.Id}\" " +
            $"tvg-name=\"{this.Name}\" " +
            $"tvg-logo=\"{this.LogoUri}\" " +
            $"group-title=\"{this.GroupTitle}\"," +
            $"{this.Name}";
    }
}