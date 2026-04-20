namespace AutoOglasi.Web.ViewModels.Posts
{
    using Cars;

    public class PostInAdminAreaViewModel
    {
        public int Id { get; init; }

        public BaseCarViewModel Car { get; init; }

        public string PublishedOn { get; init; }

        public bool IsPublic { get; init; }
    }
}
