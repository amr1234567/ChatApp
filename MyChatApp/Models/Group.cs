using System.ComponentModel.DataAnnotations;

namespace MyChatApp.Models
{
    public class Group
    {
        [Key]
        public Guid Id { get; set; }
        [Required(ErrorMessage = "{0} is Required")]
        public string Name { get; set; }

        public List<User>? Users { get; set; }
    }
}
