namespace Befer.Server.Data.Models
{
    using Befer.Server.Data.Models.Base;
    using System.ComponentModel.DataAnnotations;

    using static DataConstants.Comment;

    public class Comment : BaseModel<string>
    {
        public Comment()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        [Required]
        [MaxLength(ContentMaxLength)]
        public string Content { get; set; }

        [Required]
        public string AuthorId { get; set; }

        public User Author { get; set; }

        [Required]
        public string PostId { get; set; }

        public Post Post { get; set; }
    }
}
