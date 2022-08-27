namespace Befer.Server.Controllers
{
    using Befer.Server.Models.Posts;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class PostsController : ApiController
    {
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<int>> Create (CreatePostRequestModel model)
        {

        }
         

    }
}
