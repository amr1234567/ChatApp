namespace MyChatApp.Models.Dto
{
    public class SignDetails
    {
        public bool Succesed { get; set; }
        public Guid UserId { get; set; }
        public List<string> messageDetails { get; set; } = new List<string>();
    }
}
