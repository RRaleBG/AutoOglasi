namespace AutoOglasi.MapperConfigurations.Profiles
{
    using AutoMapper;
    using Services.Images.Models;
    using Web.ViewModels.Images;

    public class ImagesProfile : Profile
    {
        public ImagesProfile()
        {
            CreateMap<ImageInfoDTO, ImageInfoViewModel>().ReverseMap();
        }
    }
}