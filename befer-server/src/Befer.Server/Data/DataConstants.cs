namespace Befer.Server.Data
{
    public class DataConstants
    {
        public class Post 
        {
            public const int TitleMinLength = 4; 
            public const int TitleMaxLength = 50;
            public const int DescriptionMaxLength = 450;
        }

        public class Comment
        {
            public const int ContentMaxLength = 200;
        }
    }
}
