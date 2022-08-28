namespace Befer.Server.Features.Posts
{
    using System.ComponentModel.DataAnnotations;
    using static Data.DataConstants.Post;

    public class CreatePostRequestModel
    {
        [Required(ErrorMessage = "{0} is required.")]
        [StringLength(TitleMaxLength, MinimumLength = TitleMinLength, ErrorMessage = "{0} must be with a minimum length of {2} and a maximum length of {1}.")]
        public string Title { get; set; }

        [Required]
        public string AfterImgUrl { get; set; }

        [Required]
        public string BeforeImgUrl { get; set; }

        [Required]
        public bool IsPublic { get; set; }

        [MaxLength(DescriptionMaxLength)]
        public string Description { get; set; }
    }
}
