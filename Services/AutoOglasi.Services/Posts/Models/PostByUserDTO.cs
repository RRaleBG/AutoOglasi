namespace AutoOglasi.Services.Posts.Models
{
    using Cars.Models;

    public class PostByUserDTO
    {
        public int Id { get; init; }

        public CarByUserDTO Car { get; init; }

        public string PublishedOn { get; init; }
    }
}