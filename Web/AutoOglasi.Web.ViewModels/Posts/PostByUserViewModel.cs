namespace AutoOglasi.Web.ViewModels.Posts
{
    using Cars;

    public class PostByUserViewModel
    {
        public int Id { get; init; }

        public CarByUserViewModel Car { get; init; }

        public string PublishedOn { get; init; }
    }
}