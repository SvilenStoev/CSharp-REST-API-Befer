namespace Befer.Server.Features.Identity.Models
{
    public class LoginResponseModel
    {

        public string ObjectId { get; set; }

        public string Username { get; set; }

        public string SessionToken { get; set; }
    }
}
