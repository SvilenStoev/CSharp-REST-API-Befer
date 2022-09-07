namespace Befer.Server.Data.Models
{
    using Microsoft.AspNetCore.Identity;
    using System.ComponentModel.DataAnnotations;

    public class User : IdentityUser
    {
        [Required]
        public string FullName { get; set; }

        public IEnumerable<Post> Posts { get; } = new HashSet<Post>();

        public IEnumerable<Like> GivenLikes { get; } = new HashSet<Like>();

        public IEnumerable<Comment> Comments { get; } = new HashSet<Comment>();

        public IEnumerable<GameScore> GameScores { get; } = new HashSet<GameScore>();
    }
}
