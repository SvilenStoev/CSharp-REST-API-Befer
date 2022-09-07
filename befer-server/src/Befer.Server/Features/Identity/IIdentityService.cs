namespace Befer.Server.Features.Identity
{
    using Befer.Server.Features.Identity.Models;

    public interface IIdentityService
    {
        public string GenerateJwtToken(string userId, string username, string secret);

        public Task<ProfileServiceModel> Profile(string userId);

        public Task<bool> Update(string userId, UpdateProfileRequestModel model);
    }
}
