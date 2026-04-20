namespace AutoOglasi.Services.Posts.Models
{
    using Cars.Models;

    public class PostInLatestListDTO
    {
        public int Id { get; init; }

        public LatestPostsCarDTO Car { get; init; }

        public string PublishedOn { get; init; }
    }
}