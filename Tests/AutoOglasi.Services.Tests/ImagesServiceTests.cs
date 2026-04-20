namespace AutoOglasi.Services.Tests;

using AutoOglasi.Data.Models;
using AutoOglasi.Services.Images;

public class ImagesServiceTests
{
    [Fact]
    public void GetDefaultCarImagesPath_ReturnsExpectedPath()
    {
        using var data = TestDbContextFactory.Create("images-service-tests");
        var service = new ImagesService(data);

        var result = service.GetDefaultCarImagesPath("image-1", "png");

        Assert.Equal("/images/cars/image-1.png", result);
    }

    [Fact]
    public void GetCoverImagePath_ReturnsCoverImagePath()
    {
        using var data = TestDbContextFactory.Create("images-service-tests");
        var service = new ImagesService(data);
        var images = new List<Image>
        {
            new() { Id = "image-1", Extension = "jpg", CreatorId = "user-1", IsCoverImage = false },
            new() { Id = "image-2", Extension = "png", CreatorId = "user-1", IsCoverImage = true },
        };

        var result = service.GetCoverImagePath(images);

        Assert.Equal("/images/cars/image-2.png", result);
    }

    [Fact]
    public void GetCoverImagePath_ReturnsFirstImagePathWhenCoverImageIsMissing()
    {
        using var data = TestDbContextFactory.Create("images-service-tests");
        var service = new ImagesService(data);
        var images = new List<Image>
        {
            new() { Id = "image-1", Extension = "jpg", CreatorId = "user-1", IsCoverImage = false },
            new() { Id = "image-2", Extension = "png", CreatorId = "user-1", IsCoverImage = false },
        };

        var result = service.GetCoverImagePath(images);

        Assert.Equal("/images/cars/image-1.jpg", result);
    }

    [Fact]
    public async Task SetCoverImagePropertyAsync_SetsIsCoverImageToTrue()
    {
        await using var data = TestDbContextFactory.Create("images-service-tests");
        data.Images.Add(new Image { Id = "image-1", Extension = "jpg", CreatorId = "user-1", IsCoverImage = false });
        await data.SaveChangesAsync();
        var service = new ImagesService(data);

        await service.SetCoverImagePropertyAsync("image-1");

        Assert.True(data.Images.Where(image => image.Id == "image-1").Select(image => image.IsCoverImage).Single());
    }

    [Fact]
    public async Task RemoveCoverImagePropertyAsync_SetsIsCoverImageToFalse()
    {
        await using var data = TestDbContextFactory.Create("images-service-tests");
        data.Images.Add(new Image { Id = "image-1", Extension = "jpg", CreatorId = "user-1", IsCoverImage = true });
        await data.SaveChangesAsync();
        var service = new ImagesService(data);

        await service.RemoveCoverImagePropertyAsync("image-1");

        Assert.False(data.Images.Where(image => image.Id == "image-1").Select(image => image.IsCoverImage).Single());
    }
}
