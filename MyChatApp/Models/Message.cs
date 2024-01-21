using System.ComponentModel.DataAnnotations;

namespace MyChatApp.Models
{
    public class Message
    {
        [Key]
        public Guid Id { get; set; }
        public string Content { get; set; } = string.Empty;
        public User Sender { get; set; }
        public Group Group { get; set; }
    }
}
