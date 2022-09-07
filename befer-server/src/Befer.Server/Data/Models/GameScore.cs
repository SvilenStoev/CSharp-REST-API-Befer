namespace Befer.Server.Data.Models
{
    using Befer.Server.Data.Models.Base;
    using System.ComponentModel.DataAnnotations;

    public class GameScore : BaseModel<string>
    {
        public GameScore()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        [Required]
        [Range(0, 1000)]
        public int AliensKilled { get; set; }

        [Required]
        [Range(0, 1000)]
        public int AliensMissed { get; set; }

        [Required]
        public int HealthRemaining { get; set; }

        [Required]
        [Range(-1, 500)]
        public int TimeRemaining { get; set; }

        [Required]
        [Range(0, 101)]
        public int BoostRemaining { get; set; }

        [Required]
        public int Points { get; set; }

        [Required]
        public int TotalPoints { get; set; }

        [Required]
        public string PlayerId { get; set; }

        public User Player { get; set; }
    }
}
