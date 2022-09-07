namespace Befer.Server.Features.Identity.Models
{
    using System.ComponentModel.DataAnnotations;

    public class UpdateProfileRequestModel
    {

        [Required]
        public string FullName { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Email { get; set; }
    }
}
