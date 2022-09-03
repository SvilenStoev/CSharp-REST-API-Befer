namespace Befer.Server.Features.Posts
{
    using Befer.Server.Features.Posts.Models;
    using Befer.Server.Infrastructure;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class PostsController : ApiController
    {
        private readonly IPostService postService;

        public PostsController(IPostService postService)
        {
            this.postService = postService;
        }

        [Authorize]
        [HttpPost]
        [Route(nameof(Create))]
        public async Task<ActionResult> Create(CreatePostRequestModel model)
        {
            var userId = User.GetId();

            string postId = await this.postService.Create(
                model.Title,
                model.AfterImgUrl,
                model.BeforeImgUrl,
                model.Description,
                model.IsPublic,
                userId);

            var response = new CreatePostResponseModel
            {
                ObjectId = postId
            };

            return Created(nameof(this.Create), response);
        }

        [Authorize]
        [HttpGet]
        [Route("details/{id}")]
        public async Task<ActionResult<GetPostResponseModel>> Details(string id)
        {
            return await this.postService.Get(id);
        }

        [Authorize]
        [HttpGet]
        [Route(nameof(GetAll))]
        public async Task<IEnumerable<PostListResponseModel>> GetAll(string order, int skip)
        {
            return await this.postService.GetAll(order, skip);
        }

        [Authorize]
        [HttpGet]
        [Route(nameof(GetMine))]
        public async Task<IEnumerable<PostListResponseModel>> GetMine(string order, int skip)
        {
            var userId = User.GetId();

            return await this.postService.GetMine(order, skip, userId);
        }

        [Authorize]
        [HttpGet]
        [Route(nameof(AllPostsCount))]
        public async Task<ActionResult<int>> AllPostsCount()
        {
            return await this.postService.AllPostsCount();
        }

        [Authorize]
        [HttpGet]
        [Route(nameof(UserPostsCount))]
        public async Task<ActionResult<int>> UserPostsCount()
        {
            var userId = User.GetId();

            return await this.postService.UserPostsCount(userId);
        }
    }
}
