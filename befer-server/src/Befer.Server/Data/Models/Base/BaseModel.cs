namespace Befer.Server.Data.Models.Base
{
    public class BaseModel<IdType> : IBaseModel
    {
        public IdType Id { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime? UpdatedAt { get; set; }

        public DateTime? DeletedOn { get; set; }

        public bool IsDeleted { get; set; }
    }
}
