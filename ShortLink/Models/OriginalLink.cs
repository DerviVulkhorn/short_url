using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ShortLink.Models
{
    public class OriginalLink
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public string link { get; set; }
        public List<Code> code { get; set; }
        public DateTime createdAt { get; set; } = DateTime.UtcNow;
    }
}
