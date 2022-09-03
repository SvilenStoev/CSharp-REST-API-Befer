﻿namespace Befer.Server.Features.Posts.Models
{
    using System.ComponentModel.DataAnnotations;

    public class PostListResponseModel
    {
        public string ObjectId { get; set; }

        public DateTime CreatedAt { get; set; }

        public string Title { get; set; }

        public string BeforeImgUrl { get; set; }

        public bool IsPublic { get; set; }
    }
}
