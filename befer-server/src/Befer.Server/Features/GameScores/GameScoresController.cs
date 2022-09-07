namespace Befer.Server.Features.GameScores
{
    using Befer.Server.Features.GameScores.Models;
    using Befer.Server.Features.Posts.Models;
    using Befer.Server.Infrastructure.Extensions;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize]
    public class GameScoresController : ApiController
    {
        private readonly IGameScoreService gameScoreService;

        public GameScoresController(IGameScoreService gameScoreService)
        {
            this.gameScoreService = gameScoreService;
        }

        [HttpPost]
        public async Task<ActionResult> Create(CreateGameScoreRequestModel model)
        {
            var userId = User.GetId();

            var created = await this.gameScoreService.Create(model, userId);

            if (!created)
            {
                return BadRequest();
            }

            return Created(nameof(Create), new { Created = true });
        }

        //[HttpGet]
        //[Route(nameof(GetMine))]
        //public async Task<IEnumerable<GameScoresServiceModel>> GetMine()
        //{
        //    var userId = User.GetId();

        //    return this.gameScoreService.GetMine(userId);
        //}
    }
}
