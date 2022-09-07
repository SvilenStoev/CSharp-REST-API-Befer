namespace Befer.Server.Features.Identity
{
    using Befer.Server.Data;
    using Befer.Server.Data.Models;
    using Befer.Server.Features.Identity.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.IdentityModel.Tokens;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Text;
    using System.Threading.Tasks;

    public class IdentityService : IIdentityService
    {
        private readonly UserManager<User> userManager;
        private readonly BeferDbContext data;

        public IdentityService(
            UserManager<User> userManager,
            BeferDbContext data)
        {
            this.userManager = userManager;
            this.data = data;
        }

        public string GenerateJwtToken(string userId, string username, string secret)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, userId),
                    new Claim(ClaimTypes.Name, username)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var encryptedToken = tokenHandler.WriteToken(token);

            return encryptedToken;
        }

        public async Task<ProfileServiceModel> Profile(string userId)
        {
            var user = await this.userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return null;
            }

            return new ProfileServiceModel
            {
                ObjectId = user.Id,
                Username = user.UserName,
                FullName = user.FullName,
                Email = user.Email,
            };
        }

        public async Task<bool> Update(string userId, UpdateProfileRequestModel model)
        {
            var user = await this.userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return false;
            }

            user.UserName = model.Username;
            user.FullName = model.FullName;
            user.Email = model.Email;

            await this.data.SaveChangesAsync();

            return true;
        }
    }
}
