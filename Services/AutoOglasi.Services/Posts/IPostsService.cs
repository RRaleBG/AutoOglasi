namespace AutoOglasi.Services.Posts
{
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using Models;
    using Data.Models;
    using Images.Models;

    public interface IPostsService
    {
        Task<int> CreateAsync(PostFormInputModelDTO inputPost, Car car, string userId, bool isPublic);

        Task<IEnumerable<PostInListDTO>> GetMatchingPostsAsync(SearchPostDTO searchInputModel, int sortingNumber);

        Task<IEnumerable<BasePostInListDTO>> GetAllPostsBaseInfoAsync(int page, int postsPerPage);

        Task<int> GetAllPostsCountAsync();

        IEnumerable<T> GetPostsByPage<T>(IEnumerable<T> posts, int page, int postsPerPage);

        Task<IEnumerable<PostByUserDTO>> GetPostsByUserAsync(string userId, int sortingNumber);

        Task<SinglePostDTO> GetSinglePostViewModelByIdAsync(int postId, bool publicOnly = true);

        Task<EditPostDTO> GetPostFormInputModelByIdAsync(int postId);

        Task<IEnumerable<PostInLatestListDTO>> GetLatestAsync(int count);

        Task UpdateAsync(int postId, EditPostDTO input, bool isPublic);

        Task ChangeVisibilityAsync(int postId);

        Task<IEnumerable<ImageInfoDTO>> GetCurrentDbImagesForAPostAsync(int postId);

        Task<PostByUserDTO> GetBasicPostInformationByIdAsync(int postId);

        Task<string> GetPostCreatorIdAsync(int postId);

        Task DeletePostByIdAsync(int postId);
    }
}
