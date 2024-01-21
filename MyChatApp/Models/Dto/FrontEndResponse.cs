using System.ComponentModel.DataAnnotations;

namespace MyChatApp.Models.Dto
{
    public class FrontEndResponse
    {
        public bool isSuccess { get; set; }
        public string Id { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;
        public List<string> ErrorMessages { get; set; } = new List<string>();
    }
}
