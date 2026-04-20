namespace AutoOglasi.Services.Posts
{
    using System;
    using System.Linq;
    using System.Globalization;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using Cars;
    using Cars.Models;
    using Data;
    using Data.Models;
    using Images;
    using Images.Models;
    using Microsoft.EntityFrameworkCore;
    using Models;

    public class PostsService : IPostsService
    {
        private readonly AutoOglasiDbContext data;
        private readonly ICarsService carsService;
        private readonly IImagesService imagesService;

        public PostsService(AutoOglasiDbContext data, ICarsService carsService, IImagesService imagesService)
        {
            this.data = data;
            this.carsService = carsService;
            this.imagesService = imagesService;
        }

        public async Task<int> CreateAsync(PostFormInputModelDTO inputPost, Car car, string userId, bool isPublic)
        {
            var post = new Post
            {
                Car = car,
                CreatorId = userId,
                PublishedOn = DateTime.UtcNow,
                SellerName = inputPost.SellerName,
                SellerPhoneNumber = inputPost.SellerPhoneNumber,
                IsPublic = isPublic,
            };

            await this.data.Posts.AddAsync(post);
            await this.data.SaveChangesAsync();

            return post.Id;
        }

        public IEnumerable<T> GetPostsByPage<T>(IEnumerable<T> posts, int page, int postsPerPage)
        {
            return posts.Skip((page - 1) * postsPerPage).Take(postsPerPage).ToList();
        }

        public async Task<IEnumerable<PostByUserDTO>> GetPostsByUserAsync(string userId, int sortingNumber)
        {
            var postsQuery = this.data.Posts.Where(p => p.CreatorId == userId && !p.IsDeleted).AsQueryable();

            postsQuery = GetSortedPosts(postsQuery, sortingNumber);

            return await postsQuery
                .Select(p => new PostByUserDTO()
                {
                    Id = p.Id,
                    Car = new CarByUserDTO()
                    {
                        Id = p.Car.Id,
                        Make = p.Car.Make,
                        Model = p.Car.Model,
                        Price = p.Car.Price,
                        Year = p.Car.Year,
                        CoverImage = this.imagesService.GetCoverImagePath(p.Car.Images.ToList()),
                    },
                    PublishedOn = GetFormattedDate(p.PublishedOn),
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<PostInListDTO>> GetMatchingPostsAsync(SearchPostDTO searchInputModel, int sortingNumber)
        {
            var postsQuery = this.data.Posts.Where(p => !p.IsDeleted && p.IsPublic).AsQueryable();

            if (searchInputModel.Car != null)
            {
                var searchedCarDetails = searchInputModel.Car;

                if (!string.IsNullOrWhiteSpace(searchedCarDetails.TextSearchTerm))
                {
                    postsQuery = postsQuery.Where(p =>
                        (p.Car.Make + " " + p.Car.Model + " " + p.Car.Description).ToLower()
                        .Contains(searchedCarDetails.TextSearchTerm.ToLower()));
                }

                if (searchedCarDetails.FromYear is > 0)
                {
                    postsQuery = postsQuery.Where(p => p.Car.Year >= searchedCarDetails.FromYear);
                }

                if (searchedCarDetails.ToYear is > 0)
                {
                    postsQuery = postsQuery.Where(p => p.Car.Year <= searchedCarDetails.ToYear);
                }

                if (searchedCarDetails.MinHorsepower is > 0)
                {
                    postsQuery = postsQuery.Where(p => p.Car.Horsepower >= searchedCarDetails.MinHorsepower);
                }

                if (searchedCarDetails.MaxHorsepower is > 0)
                {
                    postsQuery = postsQuery.Where(p => p.Car.Horsepower <= searchedCarDetails.MaxHorsepower);
                }

                if (searchedCarDetails.MinPrice is > 0)
                {
                    postsQuery = postsQuery.Where(p => p.Car.Price >= searchedCarDetails.MinPrice);
                }

                if (searchedCarDetails.MaxPrice is > 0)
                {
                    postsQuery = postsQuery.Where(p => p.Car.Price <= searchedCarDetails.MaxPrice);
                }
            }

            if (searchInputModel.SelectedCategoriesIds.Any())
            {
                postsQuery = postsQuery.Where(p => searchInputModel.SelectedCategoriesIds.Contains(p.Car.CategoryId));
            }

            if (searchInputModel.SelectedFuelTypesIds.Any())
            {
                postsQuery = postsQuery.Where(p => searchInputModel.SelectedFuelTypesIds.Contains(p.Car.FuelTypeId));
            }

            if (searchInputModel.SelectedTransmissionTypesIds.Any())
            {
                postsQuery = postsQuery.Where(p => searchInputModel.SelectedTransmissionTypesIds.Contains(p.Car.TransmissionTypeId));
            }

            if (searchInputModel.SelectedExtrasIds.Any())
            {
                var searchedExtrasIds = searchInputModel.SelectedExtrasIds;
                var currentQueuedCars = await postsQuery.Select(p => p.Car).ToListAsync();
                var allMatchedCarIds = new List<int>();

                foreach (var car in currentQueuedCars)
                {
                    var currentCarExtrasIds = await this.data.CarExtras
                        .Where(ce => ce.Car.Id == car.Id)
                        .Select(ce => ce.ExtraId)
                        .ToListAsync();

                    if (searchedExtrasIds.Intersect(currentCarExtrasIds).Count() == searchedExtrasIds.Count())
                    {
                        allMatchedCarIds.Add(car.Id);
                    }
                }

                postsQuery = postsQuery.Where(p => allMatchedCarIds.Contains(p.Car.Id));
            }

            if (!await postsQuery.AnyAsync())
            {
                throw new Exception("Unfortunately, there are no cars in our system that match this search criteria.");
            }

            postsQuery = GetSortedPosts(postsQuery, sortingNumber);

            return await postsQuery
                .Select(p => new PostInListDTO()
                {
                    Id = p.Id,
                    Car = new CarInListDTO()
                    {
                        Id = p.Car.Id,
                        Make = p.Car.Make,
                        Model = p.Car.Model,
                        Description = p.Car.Description.Length <= 100 ?
                            p.Car.Description
                            :
                            p.Car.Description.Substring(0, 100) + "...",
                        Price = p.Car.Price,
                        Year = p.Car.Year,
                        Kilometers = p.Car.Kilometers,
                        FuelType = p.Car.FuelType.Name,
                        TransmissionType = p.Car.TransmissionType.Name,
                        Category = p.Car.Category.Name,
                        LocationCity = p.Car.LocationCity,
                        LocationCountry = p.Car.LocationCountry,
                        CoverImage = this.imagesService.GetCoverImagePath(p.Car.Images.ToList()),
                    },
                    PublishedOn = GetFormattedDate(p.PublishedOn),
                })
                .ToListAsync();
        }

        public Task<int> GetAllPostsCountAsync()
        {
            return this.data.Posts.CountAsync();
        }

        public async Task<IEnumerable<BasePostInListDTO>> GetAllPostsBaseInfoAsync(int page, int postsPerPage)
        {
            return await this.data.Posts
                .Where(p => !p.IsDeleted)
                .OrderBy(p => p.IsPublic)
                .ThenByDescending(p => p.PublishedOn)
                .Skip((page - 1) * postsPerPage)
                .Take(postsPerPage)
                .Select(p => new BasePostInListDTO()
                {
                    Id = p.Id,
                    Car = new BaseCarDTO()
                    {
                        Id = p.Car.Id,
                        Make = p.Car.Make,
                        Model = p.Car.Model,
                        Year = p.Car.Year,
                        Price = p.Car.Price,
                    },
                    PublishedOn = GetFormattedDate(p.PublishedOn),
                    IsPublic = p.IsPublic,
                })
                .ToListAsync();
        }

        public async Task<SinglePostDTO> GetSinglePostViewModelByIdAsync(int postId, bool publicOnly = true)
        {
            return await this.data.Posts
                .Where(p => p.Id == postId && !p.IsDeleted && (!publicOnly || p.IsPublic))
                .Select(p => new SinglePostDTO()
                {
                    Car = new SingleCarDTO()
                    {
                        Id = p.Car.Id,
                        Make = p.Car.Make,
                        Model = p.Car.Model,
                        Description = p.Car.Description,
                        Price = p.Car.Price,
                        Year = p.Car.Year,
                        Kilometers = p.Car.Kilometers,
                        Horsepower = p.Car.Horsepower,
                        FuelType = p.Car.FuelType.Name,
                        TransmissionType = p.Car.TransmissionType.Name,
                        Category = p.Car.Category.Name,
                        LocationCity = p.Car.LocationCity,
                        LocationCountry = p.Car.LocationCountry,
                        ComfortExtras = p.Car.CarExtras.Where(ce => ce.Extra.TypeId == 1).Select(ce => ce.Extra.Name).ToList(),
                        SafetyExtras = p.Car.CarExtras.Where(ce => ce.Extra.TypeId == 2).Select(ce => ce.Extra.Name).ToList(),
                        OtherExtras = p.Car.CarExtras.Where(ce => ce.Extra.TypeId == 3).Select(ce => ce.Extra.Name).ToList(),
                        Images = p.Car.Images.OrderByDescending(img => img.IsCoverImage)
                                             .Select(img => this.imagesService.GetDefaultCarImagesPath(img.Id, img.Extension))
                                             .ToList(),
                    },
                    PublishedOn = GetFormattedDate(p.PublishedOn),
                    SellerName = p.SellerName,
                    SellerPhoneNumber = p.SellerPhoneNumber,
                })
                .FirstOrDefaultAsync();
        }

        public async Task<EditPostDTO> GetPostFormInputModelByIdAsync(int postId)
        {
            return await this.data.Posts
                .Where(p => p.Id == postId && !p.IsDeleted)
                .Select(p => new EditPostDTO()
                {
                    Car = new CarFormInputModelDTO()
                    {
                        Make = p.Car.Make,
                        Model = p.Car.Model,
                        Description = p.Car.Description,
                        Price = p.Car.Price,
                        Year = p.Car.Year,
                        Kilometers = p.Car.Kilometers,
                        Horsepower = p.Car.Horsepower,
                        CategoryId = p.Car.CategoryId,
                        FuelTypeId = p.Car.FuelTypeId,
                        TransmissionTypeId = p.Car.TransmissionTypeId,
                        LocationCity = p.Car.LocationCity,
                        LocationCountry = p.Car.LocationCountry,
                    },
                    SelectedExtrasIds = p.Car.CarExtras.Select(ce => ce.ExtraId).ToList(),
                    SellerName = p.SellerName,
                    SellerPhoneNumber = p.SellerPhoneNumber,
                    CreatorId = p.CreatorId,
                    CurrentImages = p.Car.Images.OrderByDescending(img => img.IsCoverImage)
                        .Select(img => new ImageInfoDTO()
                        {
                            Id = img.Id,
                            Path = this.imagesService.GetDefaultCarImagesPath(img.Id, img.Extension),
                        }).ToList(),
                    SelectedCoverImageId = p.Car.Images
                        .Where(img => img.IsCoverImage)
                        .Select(img => img.Id)
                        .FirstOrDefault(),
                    CarId = p.CarId,
                })
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<ImageInfoDTO>> GetCurrentDbImagesForAPostAsync(int postId)
        {
            var post = await this.data.Posts.FirstOrDefaultAsync(p => p.Id == postId && !p.IsDeleted);

            if (post == null)
            {
                return Enumerable.Empty<ImageInfoDTO>();
            }

            var car = await this.data.Cars.FirstOrDefaultAsync(c => c.Id == post.CarId && !c.IsDeleted);

            if (car == null)
            {
                return Enumerable.Empty<ImageInfoDTO>();
            }

            return await this.data.Images
                .Where(img => img.CarId == car.Id)
                .OrderByDescending(img => img.IsCoverImage)
                .Select(img => new ImageInfoDTO()
                {
                    Id = img.Id,
                    Path = this.imagesService.GetDefaultCarImagesPath(img.Id, img.Extension),
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<PostInLatestListDTO>> GetLatestAsync(int count)
        {
            return await this.data.Posts
                .Where(p => !p.IsDeleted && p.IsPublic)
                .OrderByDescending(p => p.PublishedOn)
                .Take(count)
                .Select(p => new PostInLatestListDTO()
                {
                    Id = p.Id,
                    Car = new LatestPostsCarDTO()
                    {
                        Id = p.Car.Id,
                        Make = p.Car.Make,
                        Model = p.Car.Model,
                        Price = p.Car.Price,
                        Year = p.Car.Year,
                        Horsepower = p.Car.Horsepower,
                        FuelType = p.Car.FuelType.Name,
                        TransmissionType = p.Car.TransmissionType.Name,
                        CoverImage = this.imagesService.GetCoverImagePath(p.Car.Images.ToList()),
                    },
                    PublishedOn = GetFormattedDate(p.PublishedOn),
                })
                .ToListAsync();
        }

        public async Task UpdateAsync(int postId, EditPostDTO editedPost, bool isPublic)
        {
            var post = await this.GetDbPostByIdAsync(postId);

            if (post == null)
            {
                throw new Exception("Unfortunately, we cannot find such post in our system!");
            }

            post.ModifiedOn = DateTime.UtcNow;
            post.SellerName = editedPost.SellerName;
            post.SellerPhoneNumber = editedPost.SellerPhoneNumber;
            post.IsPublic = isPublic;

            await this.data.SaveChangesAsync();
        }

        public async Task ChangeVisibilityAsync(int postId)
        {
            var post = await this.GetDbPostByIdAsync(postId);

            if (post == null)
            {
                throw new Exception("Unfortunately, we cannot find such post in our system!");
            }

            post.IsPublic = !post.IsPublic;

            await this.data.SaveChangesAsync();
        }

        public async Task<PostByUserDTO> GetBasicPostInformationByIdAsync(int postId)
        {
            return await this.data.Posts
                .Where(p => p.Id == postId && !p.IsDeleted)
                .Select(p => new PostByUserDTO()
                {
                    Id = p.Id,
                    Car = new CarByUserDTO()
                    {
                        Id = p.Car.Id,
                        Make = p.Car.Make,
                        Model = p.Car.Model,
                        Price = p.Car.Price,
                        Year = p.Car.Year,
                        CoverImage = this.imagesService.GetCoverImagePath(p.Car.Images.ToList()),
                    },
                    PublishedOn = GetFormattedDate(p.PublishedOn),
                })
                .FirstOrDefaultAsync();
        }

        public async Task<string> GetPostCreatorIdAsync(int postId)
        {
            var post = await this.GetDbPostByIdAsync(postId);

            return post?.CreatorId;
        }

        public async Task DeletePostByIdAsync(int postId)
        {
            var post = await this.GetDbPostByIdAsync(postId);

            if (post == null)
            {
                throw new Exception("Unfortunately, we cannot find such post in our system!");
            }

            await this.carsService.DeleteCarByIdAsync(post.CarId);

            post.IsDeleted = true;
            post.DeletedOn = DateTime.UtcNow;
            post.IsPublic = false;

            await this.data.SaveChangesAsync();
        }

        private Task<Post> GetDbPostByIdAsync(int postId)
        {
            return this.data.Posts.FirstOrDefaultAsync(p => p.Id == postId && !p.IsDeleted);
        }

        private static string GetFormattedDate(DateTime inputDateTime)
        {
            if (inputDateTime.Date == DateTime.Now)
            {
                return inputDateTime.ToString("dddd dd.MMM", CultureInfo.CreateSpecificCulture("sr-Latn-RS"));
            }

            return inputDateTime.ToString("dddd dd.MMM", CultureInfo.CreateSpecificCulture("sr-Latn-RS"));
        }

        private static IQueryable<Post> GetSortedPosts(IQueryable<Post> postsQuery, int sortingNumber)
        {
            postsQuery = sortingNumber switch
            {
                1 => postsQuery.OrderBy(p => p.Id),
                2 => postsQuery.OrderByDescending(p => p.Car.Price),
                3 => postsQuery.OrderBy(p => p.Car.Price),
                4 => postsQuery.OrderByDescending(p => p.Car.Horsepower),
                5 => postsQuery.OrderBy(p => p.Car.Horsepower),
                6 => postsQuery.OrderByDescending(p => p.Car.Year),
                7 => postsQuery.OrderBy(p => p.Car.Year),
                _ => postsQuery.OrderByDescending(p => p.Id),
            };

            return postsQuery;
        }
    }
}