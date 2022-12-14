namespace Befer.Server.Features.Comments.Models
{
    using System.ComponentModel.DataAnnotations;
    using static Data.DataConstants.Comment;

    public class CreateCommentRequestModel
    {
        [Required(ErrorMessage = "{0} is required.")]
        [StringLength(ContentMaxLength, ErrorMessage = "{0} must be with maximum length of {1}.")]
        public string Content { get; set; }

        [Required]
        public string PostId { get; set; }
    }
}
