using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyChatApp.Models
{
    public class Message
    {
        [Key]
        public Guid Id { get; set; }

        public string Content { get; set; } = string.Empty;

        public User Sender { get; set; }

        [ForeignKey(nameof(Sender))]
        public Guid SenderId { get; set; }

        [ForeignKey(nameof(Group))]
        public Guid GroupId { get; set; }

        public Group Group { get; set; }
    }
}
