namespace Befer.Server.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Like
    {
        public Post Post { get; set; }

        [Required]
        public string ToPostId { get; set; }

        public User User { get; set; }

        [Required]
        public string FromUserId { get; set; }
    }
}
