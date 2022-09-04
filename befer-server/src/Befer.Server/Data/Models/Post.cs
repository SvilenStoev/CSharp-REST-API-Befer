namespace Befer.Server.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using static DataConstants.Post;

    public class Post
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime UpdatedAt { get; set; } = DateTime.Now;

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

        [Required]
        public string OwnerId { get; set; } 

        public User Owner { get; set; }
    }
}
