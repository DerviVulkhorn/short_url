namespace ShortLink.Models
{
    public class OriginalLink
    {
        public int id { get; set; }
        public string link { get; set; }
        public List<Code> code { get; set; }
        public DateTime createdAt { get; set; }
    }
}
