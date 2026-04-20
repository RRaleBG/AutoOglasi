namespace AutoOglasi.Services.Tests;

using AutoMapper;
using AutoOglasi.Data;
using AutoOglasi.Data.Models;
using AutoOglasi.Services.Cars;
using AutoOglasi.Services.Cars.Models;
using AutoOglasi.Services.Images;
using Microsoft.Extensions.Logging.Abstractions;

public class CarsServiceLookupTests
{
    [Fact]
    public async Task GetAllCategoriesAsync_ReturnsMappedCategories()
    {
        await using var data = TestDbContextFactory.Create("cars-service-tests");
        data.Categories.AddRange(
            new Category { Name = "SUV" },
            new Category { Name = "Sedan" });
        await data.SaveChangesAsync();

        var service = CreateService(data);

        var result = (await service.GetAllCategoriesAsync()).ToList();

        Assert.Equal(2, result.Count);
    }

    [Fact]
    public async Task GetAllFuelTypesAsync_ReturnsMappedFuelTypes()
    {
        await using var data = TestDbContextFactory.Create("cars-service-tests");
        data.FuelTypes.AddRange(
            new FuelType { Name = "Diesel" },
            new FuelType { Name = "Petrol" });
        await data.SaveChangesAsync();

        var service = CreateService(data);

        var result = (await service.GetAllFuelTypesAsync()).ToList();

        Assert.Equal(2, result.Count);
    }

    [Fact]
    public async Task GetAllTransmissionTypesAsync_ReturnsMappedTransmissionTypes()
    {
        await using var data = TestDbContextFactory.Create("cars-service-tests");
        data.TransmissionTypes.AddRange(
            new TransmissionType { Name = "Manual" },
            new TransmissionType { Name = "Automatic" });
        await data.SaveChangesAsync();

        var service = CreateService(data);

        var result = (await service.GetAllTransmissionTypesAsync()).ToList();

        Assert.Equal(2, result.Count);
    }

    [Fact]
    public async Task GetAllCarExtrasAsync_ReturnsExtrasOrderedByType()
    {
        await using var data = TestDbContextFactory.Create("cars-service-tests");
        var comfort = new ExtraType { Name = "Comfort" };
        var safety = new ExtraType { Name = "Safety" };

        data.ExtraTypes.AddRange(comfort, safety);
        data.Extras.AddRange(
            new Extra { Name = "Airbag", Type = safety, TypeId = 2 },
            new Extra { Name = "Climatronic", Type = comfort, TypeId = 1 });
        await data.SaveChangesAsync();

        var service = CreateService(data);

        var result = (await service.GetAllCarExtrasAsync()).ToList();

        Assert.Equal("Climatronic", result[0].Name);
    }

    private static CarsService CreateService(AutoOglasiDbContext data)
    {
        return new CarsService(data, new StubImagesService(), CreateMapper());
    }

    private static IMapper CreateMapper()
    {
        var configuration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Category, BaseCarSpecificationServiceModel>();
                cfg.CreateMap<FuelType, BaseCarSpecificationServiceModel>();
                cfg.CreateMap<TransmissionType, BaseCarSpecificationServiceModel>();
                cfg.CreateMap<Extra, CarExtrasServiceModel>();
            },
            NullLoggerFactory.Instance);

        return configuration.CreateMapper();
    }
    private sealed class StubImagesService : IImagesService
    {
        public string GetCoverImagePath(ICollection<Image> carImages)
        {
            throw new NotSupportedException();
        }

        public string GetDefaultCarImagesPath(string imageId, string imageExtension)
        {
            throw new NotSupportedException();
        }

        public Task RemoveCoverImagePropertyAsync(string imageId)
        {
            throw new NotSupportedException();
        }

        public Task SetCoverImagePropertyAsync(string imageId)
        {
            throw new NotSupportedException();
        }

        public Task<Image> UploadImageAsync(Microsoft.AspNetCore.Http.IFormFile image, string userId, string imagePath)
        {
            throw new NotSupportedException();
        }
    }
}
