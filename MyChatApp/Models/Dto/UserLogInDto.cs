using System.ComponentModel.DataAnnotations;

namespace MyChatApp.Models.Dto
{
    public class UserLogInDto
    {
        [Required(ErrorMessage ="{0} is Required")]
        [Display(Name = "User Name")]
        public string UserName { get; set; }

        [Required(ErrorMessage ="{0} is Required")]
        [DataType(DataType.Password)]
        [MinLength(8)]
        public string Password { get; set; }
    }
}
