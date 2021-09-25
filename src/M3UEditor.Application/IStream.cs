namespace M3UEditor.App
{
    public interface IStream
    {
        public string Duration { get; set; }

        public string Id { get; set; }

        public string Name { get; set; }

        public string LogoUri { get; set; }

        public string GroupTitle { get; set; }

        public string Uri { get; set; }

        string LinePropertiesToString();
    }
}