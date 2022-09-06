namespace Befer.Server.Features.Likes.Models
{
    using Befer.Server.Data.Models;

    public class CreateLikeResponseModel
    {
        public IEnumerable<Like> Likes { get; } = new HashSet<Like>();
    }
}
