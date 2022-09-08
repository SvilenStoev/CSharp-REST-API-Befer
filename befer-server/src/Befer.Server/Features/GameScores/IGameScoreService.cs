namespace Befer.Server.Features.GameScores
{
    using Befer.Server.Features.GameScores.Models;

    public interface IGameScoreService
    {
        public Task<bool> Create(CreateGameScoreRequestModel model, string userId);

        public Task<bool> Update(UpdateGameScoreRequestModel model, string userId, string scoresId);

        public Task<IEnumerable<GameScoresServiceModel>> GetMine(string userId);

        public Task<IEnumerable<GameScoresAllServiceModel>> GetAll();
    }
}
