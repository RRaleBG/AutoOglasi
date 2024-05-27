namespace AutoOglasi.Web.ViewModels.Posts
{
    using System.Collections.Generic;
    using System.Globalization;

    public class PostsListAdminAreaViewModel : PagingViewModel
    {
        public IEnumerable<PostInAdminAreaViewModel> Posts { get; init; }
    }
}