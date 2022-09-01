namespace Befer.Server.Features.Posts
{
    using Befer.Server.Data.Models;
    using System.ComponentModel.DataAnnotations;

    public class GetPostResponseModel
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public string AfterImgUrl { get; set; }

        public string BeforeImgUrl { get; set; }

        public bool IsPublic { get; set; }

        public string Description { get; set; }

        public string OwnerId { get; set; }

        public PostOwnerResponseModel Owner { get; set; }
    }
}
