namespace Befer.Server.Features.Comments.Models
{
    public class CommentListingServiceModel
    {
        public string ObjectId { get; set; }
        
        public string Content { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public DateTime? DeletedOn { get; set; }

        public bool IsDeleted { get; set; }

        public string PostId { get; set; }

        public CommentAuthorListingServiceModel Author { get; set; }
    }
}
