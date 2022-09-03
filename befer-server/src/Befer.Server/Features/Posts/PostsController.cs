namespace Befer.Server.Features.Posts
{
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
