using System.Collections.Generic;

namespace AutoOglasi.Web.ViewModels.Posts
{
    public class PostsListAdminAreaViewModel : PagingViewModel
    {
        public IEnumerable<PostInAdminAreaViewModel> Posts { get; init; }
    }
}