namespace AutoOglasi.MapperConfigurations.Profiles
{
    using AutoMapper;
    using Data.Models;
    using Services.Cars.Models;
    using Web.ViewModels.Cars;

    public class CarsProfile : Profile
    {
        public CarsProfile()
        {
            CreateMap<Category, BaseCarSpecificationServiceModel>();

            CreateMap<FuelType, BaseCarSpecificationServiceModel>();

            CreateMap<TransmissionType, BaseCarSpecificationServiceModel>();

            CreateMap<Extra, CarExtrasServiceModel>();

            CreateMap<BaseCarSpecificationServiceModel, BaseCarSpecificationViewModel>().ReverseMap();

            CreateMap<CarExtrasServiceModel, CarExtrasViewModel>().ReverseMap();

            CreateMap<BaseCarDTO, BaseCarViewModel>().ReverseMap();

            CreateMap<CarFormInputModelDTO, CarFormInputModel>().ReverseMap();

            CreateMap<SearchCarInputModelDTO, SearchCarInputModel>().ReverseMap();

            CreateMap<CarInListDTO, CarInListViewModel>().ReverseMap();

            CreateMap<SingleCarDTO, SingleCarViewModel>().ReverseMap();

            CreateMap<CarByUserDTO, CarByUserViewModel>().ReverseMap();

            CreateMap<LatestPostsCarDTO, LatestPostsCarViewModel>().ReverseMap();
        }
    }
}