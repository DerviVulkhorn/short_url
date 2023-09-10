using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ShortLink.Models
{
    public class Users
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public List<OriginalLink> OriginalLink { get; set; }
        public DateTime createdAt { get; set; } = DateTime.Today;
    }
}
