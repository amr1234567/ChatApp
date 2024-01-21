using System.ComponentModel.DataAnnotations;

namespace MyChatApp.Models
{
    public class User
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "{0} is Required")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "{0} is Required")]
        [MinLength(8,ErrorMessage ="{0} min length is {1}")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        public string ConnectionId { get; set; } = string.Empty;

        public List<Group>? Groups { get; set; }
    }
}
