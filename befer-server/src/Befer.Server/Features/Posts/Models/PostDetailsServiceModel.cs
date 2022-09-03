namespace Befer.Server.Features.Posts.Models
{
    public class PostDetailsServiceModel
    {
        public string ObjectId { get; set; }

        public string Title { get; set; }

        public string AfterImgUrl { get; set; }

        public string BeforeImgUrl { get; set; }

        public bool IsPublic { get; set; }

        public string Description { get; set; }

        public string OwnerId { get; set; }

        public PostOwnerDetailsServiceModel Owner { get; set; }
    }
}
