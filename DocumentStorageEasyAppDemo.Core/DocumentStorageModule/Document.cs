namespace DocumentStorageEasyAppDemo.DocumentStorageModule
{
    public class Document
    {
        public string Id { get; protected set; }
        public string Tags { get; protected set; }
        public string Data { get; protected set; }
        public DateTime CreatedAt { get; protected set; } = DateTime.UtcNow;

        public Document()
        {
        }

        public Document(string id, string[] tags, string data)
        {
            Id = id;
            Tags = string.Join(",", tags);
            Data = data;
        }

        public void UpdateData(string[] tags, string data)
        {
            Tags = string.Join(",", tags);
            Data = data;
        }
    }
}
