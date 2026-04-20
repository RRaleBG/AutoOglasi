namespace AutoOglasi.Services.Posts.Models
{
    using Cars.Models;

    public class PostInListDTO
    {
        public int Id { get; init; }

        public CarInListDTO Car { get; init; }

        public string PublishedOn { get; init; }
    }
}