namespace AutoOglasi.Services.Tests;

using AutoMapper;
using AutoOglasi.Services.Cars.Models;
using AutoOglasi.Services.Posts.Models;
using AutoOglasi.Web.ViewModels.Cars;
using AutoOglasi.Web.ViewModels.Posts;
using Microsoft.Extensions.Logging.Abstractions;

internal static class TestMapperFactory
{
    public static IMapper CreateControllerMapper()
    {
        var configuration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<BaseCarDTO, BaseCarViewModel>();
                cfg.CreateMap<BaseCarSpecificationServiceModel, BaseCarSpecificationViewModel>();
                cfg.CreateMap<CarExtrasServiceModel, CarExtrasViewModel>();
                cfg.CreateMap<CarByUserDTO, CarByUserViewModel>();
                cfg.CreateMap<CarFormInputModelDTO, CarFormInputModel>().ReverseMap();
                cfg.CreateMap<CarInListDTO, CarInListViewModel>();
                cfg.CreateMap<SearchCarInputModelDTO, SearchCarInputModel>().ReverseMap();
                cfg.CreateMap<SingleCarDTO, SingleCarViewModel>();
                cfg.CreateMap<PostByUserDTO, PostByUserViewModel>();
                cfg.CreateMap<PostInListDTO, PostInListViewModel>();
                cfg.CreateMap<SearchPostDTO, SearchPostInputModel>().ReverseMap();
                cfg.CreateMap<SinglePostDTO, SinglePostViewModel>();
                cfg.CreateMap<BasePostInListDTO, PostInAdminAreaViewModel>();
            },
            NullLoggerFactory.Instance);

        return configuration.CreateMapper();
    }
}
