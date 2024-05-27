namespace AutoOglasi.MapperConfigurations.Profiles
{
    using AutoMapper;
    using Services.Posts.Models;
    using Web.ViewModels.Posts;

    public class PostsProfile : Profile
    {
        public PostsProfile()
        {
            CreateMap<PostFormInputModelDTO, PostFormInputModel>().ReverseMap();

            CreateMap<PostInListDTO, PostInListViewModel>().ReverseMap();

            CreateMap<SearchPostDTO, SearchPostInputModel>().ReverseMap();

            CreateMap<SinglePostDTO, SinglePostViewModel>().ReverseMap();

            CreateMap<PostByUserDTO, PostByUserViewModel>().ReverseMap();

            CreateMap<PostsByUserDTO, PostsByUserViewModel>().ReverseMap();

            CreateMap<PostInLatestListDTO, PostInLatestListViewModel>().ReverseMap();

            CreateMap<EditPostDTO, EditPostViewModel>().ReverseMap();

            CreateMap<BasePostInListDTO, PostInAdminAreaViewModel>().ReverseMap();

            CreateMap<BasePostsListDTO, PostsListAdminAreaViewModel>().ReverseMap();
        }
    }
}