namespace Befer.Server.Features.Identity
{
    using Befer.Server.Data.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Options;
    using Befer.Server.Features.Identity.Models;
    using Befer.Server.Features;
    using Befer.Server.Infrastructure.Extensions;

    using static Infrastructure.WebConstants;

    public class IdentityController : ApiController
    {
        private readonly UserManager<User> userManager;
        private readonly IIdentityService identityService;
        private readonly AppSettings appSettings;

        public IdentityController(
            UserManager<User> userManager,
            IIdentityService identityService,
            IOptions<AppSettings> appSettings)
        {
            this.userManager = userManager;
            this.identityService = identityService;
            this.appSettings = appSettings.Value;
        }

        [HttpPost]
        [Route(nameof(Register))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<RegisterResponseModel>> Register(RegisterRequestModel model)
        {
            var userData = new User
            {
                UserName = model.Username,
                FullName = model.FullName,
                Email = model.Email,
            };

            var result = await userManager.CreateAsync(userData, model.Password);

            if (result.Succeeded)
            {
                var user = userManager.FindByNameAsync(model.Username).Result;

                var token = this.identityService.GenerateJwtToken(user.Id, user.UserName, this.appSettings.Secret);

                var response = new RegisterResponseModel
                {
                    ObjectId = user.Id,
                    Username = user.UserName,
                    SessionToken = token
                };

                return Created(nameof(this.Register), response);
            }

            return BadRequest(result.Errors);
        }

        [HttpPost]
        [Route(nameof(Login))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<LoginResponseModel>> Login(LoginRequestModel model)
        {
            var user = userManager.FindByNameAsync(model.Username).Result;

            if (user == null)
            {
                return Unauthorized();
            }

            var passwordValid = await userManager.CheckPasswordAsync(user, model.Password);

            if (!passwordValid)
            {
                return Unauthorized();
            }

            var token = this.identityService.GenerateJwtToken(user.Id, user.UserName, this.appSettings.Secret);

            return new LoginResponseModel
            {
                ObjectId = user.Id,
                Username = user.UserName,
                SessionToken = token
            };
        }

        [HttpGet]
        [Route(nameof(Profile))]
        public async Task<ActionResult<ProfileServiceModel>> Profile()
        {
            var userId = User.GetId();

            var userData = await this.identityService.Profile(userId);

            if (userData == null)
            {
                return Unauthorized();
            }

            return userData;
        }

        [HttpPut]
        public async Task<ActionResult> Update(UpdateProfileRequestModel model)
        {
            var userId = User.GetId();

            var updated = await this.identityService.Update(userId, model);

            if (!updated)
            {
                return Unauthorized();
            }

            return Ok();
        }

        [HttpPost]
        [Route(nameof(Logout))]
        public async Task<ActionResult> Logout()
        {
            //TODO delete token
            return Ok();
        }
    }
}
