namespace Befer.Server.Features.Comments
{
    using Befer.Server.Data.Models;
    using Befer.Server.Features.Comments.Models;
    using Befer.Server.Features.Posts.Models;
    using Befer.Server.Infrastructure.Extensions;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize]
    public class CommentsController : ApiController
    {
        private readonly ICommentService commentService;

        public CommentsController(ICommentService commentService)
        {
            this.commentService = commentService;
        }

        [HttpPost]
        public async Task<ActionResult> Create(CreateCommentRequestModel model)
        {
            var userId = User.GetId();

            var created = await this.commentService.Create(model.Content, model.PostId, userId);

            if (!created)
            {
                return BadRequest();
            }

            return Created(nameof(Create), new { Created = true });
        }
    }
}
