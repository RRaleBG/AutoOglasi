namespace AutoOglasi.Web.ViewModels.Posts
{
    using Cars;

    public class PostInLatestListViewModel
    {
        public int Id { get; init; }

        public LatestPostsCarViewModel Car { get; init; }

        public string PublishedOn { get; init; }
    }
}