namespace Befer.Server.Features.GameScores
{
    using Befer.Server.Data;
    using Befer.Server.Data.Models;
    using Befer.Server.Features.GameScores.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class GameScoreService : IGameScoreService
    {
        private readonly BeferDbContext data;

        public GameScoreService(BeferDbContext data)
        {
            this.data = data;
        }

        public async Task<bool> Create(CreateGameScoreRequestModel model, string userId)
        {
            var gameScore = new GameScore
            {
                AliensKilled = model.AliensKilled,
                BoostRemaining = model.BoostRemaining,
                Points = model.Points,
                TimeRemaining = model.TimeRemaining,
                TotalPoints = model.TotalPoints,
                PlayerId = userId,
            };

            await this.data.GameScores.AddAsync(gameScore);

            await this.data.SaveChangesAsync();

            return true;
        }

        //public Task<IEnumerable<GameScoresServiceModel>> GetMine(string userId)
        //{
        //    var gameScores = this.data
        //        .GameScores
        //        .Where(gs => gs.PlayerId)
        //}
    }
}
