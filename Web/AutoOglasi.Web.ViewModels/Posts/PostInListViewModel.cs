namespace AutoOglasi.Web.ViewModels.Posts
{
    using Cars;

    public class PostInListViewModel
    {
        public int Id { get; init; }

        public CarInListViewModel Car { get; init; }

        public string PublishedOn { get; init; }
    }
}