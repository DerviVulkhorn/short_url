using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ShortLink.Models
{
    public class Code
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public string code { get; set; }
        public int countMove { get; set; }
        public DateTime createdAt { get; set; } = DateTime.UtcNow;
    }
}
