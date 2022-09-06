namespace Befer.Server.Data.Models
{
    using Befer.Server.Data.Models.Base;
    using System.ComponentModel.DataAnnotations;
    using static DataConstants.Post;

    public class Post : BaseModel<string>
    {
        public Post()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        [Required]
        [MaxLength(TitleMaxLength)]
        public string Title { get; set; }

        [Required]
        public string AfterImgUrl { get; set; }

        [Required]
        public string BeforeImgUrl { get; set; }

        [Required]
        public bool IsPublic { get; set; }

        [MaxLength(DescriptionMaxLength)]
        public string Description { get; set; }

        public IEnumerable<Like> Likes { get; } = new HashSet<Like>();

        public IEnumerable<Comment> Comments { get; } = new HashSet<Comment>();

        [Required]
        public string OwnerId { get; set; }

        public User Owner { get; set; }
    }
}
