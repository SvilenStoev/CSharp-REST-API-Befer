namespace Befer.Server.Features.Identity
{
    public interface IIdentityService
    {
        public string GenerateJwtToken(string userId, string username, string secret);
    }
}
