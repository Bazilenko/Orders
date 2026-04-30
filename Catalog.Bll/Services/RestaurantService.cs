using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Catalog.Bll.DTOs.Dish;
using Catalog.Bll.DTOs.Pagination;
using Catalog.Bll.DTOs.Restaurant;
using Catalog.Bll.Services.Interfaces;
using Catalog.Dal.Entities;
using Catalog.Dal.Specifications;
using Catalog.Dal.UOW.Interfaces;

namespace Catalog.Bll.Services
{
    public class RestaurantService : IRestaurantService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public RestaurantService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<RestaurantDto> AddRestaurant(RestaurantCreateDto dto)
    {
        var city = dto.Addresses.FirstOrDefault()?.City;
        var spec = new RestaurantUniqueCheckSpecification(dto.Name, city);
        var existing = await _unitOfWork.Restaurants.ListAsync(spec);

        if (existing.Any())
        {
            throw new InvalidOperationException($"Ресторан '{dto.Name}' у місті {city} вже зареєстровано.");
        }

        var restaurant = _mapper.Map<Restaurant>(dto);

        restaurant.IsActive = false; 
        restaurant.Rating = 0;       

        await _unitOfWork.Restaurants.AddAsync(restaurant);
        await _unitOfWork.SaveChangesAsync();

        return _mapper.Map<RestaurantDto>(restaurant);
    }

        public async Task<IEnumerable<RestaurantDto?>> GetAllRestaurantsAsync()
        {
            var restaurants = await _unitOfWork.Restaurants.GetAllAsync();
            return _mapper.Map<IEnumerable<RestaurantDto>>(restaurants);
        }

        public async Task<RestaurantDto?> GetRestaurantByIdAsync(int id)
        {
            var restaurant = await _unitOfWork.Restaurants.GetByIdWithFullInfo(id);
            return _mapper.Map<RestaurantDto>(restaurant);
        }

        public async Task<IEnumerable<RestaurantDto>> GetRestaurantByRating(int rating)
        {
            var specification = new RestaurantByRatingSpecification(rating);
            var restaurants =  await _unitOfWork.Restaurants.ListAsync(specification);
            return _mapper.Map<IEnumerable<RestaurantDto>>(restaurants);

        }

        public async Task<PagedResult<RestaurantDto>> GetPaginatedAsync(PagedRequest request)
        {
            
            var (entities, totalCount) = await _unitOfWork.Restaurants.GetPagedDataAsync(
                request.PageNumber,
                request.PageSize,
                request.SortColumn,
                request.SortOrder
            );

            
            var dtos = _mapper.Map<IEnumerable<RestaurantDto>>(entities);

            return new PagedResult<RestaurantDto>(
                dtos.ToList(),
                totalCount,
                request.PageNumber,
                request.PageSize
            );
        }

        public async Task UpdateRestaurant(RestaurantUpdateDto dto)
    {
        var restaurant = await _unitOfWork.Restaurants.GetByIdAsync(dto.Id);
        if (restaurant == null) throw new KeyNotFoundException("Ресторан не знайдено.");

        var currentRating = restaurant.Rating;

        // Мапимо дані з DTO в існуючу сутність
        _mapper.Map(dto, restaurant);

        // 3. ЗАХИСТ РЕЙТИНГУ ПРИ ОНОВЛЕННІ
        // Навіть якщо в DTO прийшло нове значення рейтингу, ми його ігноруємо
        restaurant.Rating = currentRating;

        await _unitOfWork.Restaurants.UpdateAsync(restaurant);
        await _unitOfWork.SaveChangesAsync();
    }

        public async Task DeactivateRestaurant(int id)
    {
        var restaurant = await _unitOfWork.Restaurants.GetByIdAsync(id);
        if (restaurant == null) throw new KeyNotFoundException("Ресторан не знайдено.");

        // 2. SOFT DELETE
        restaurant.IsActive = false;

        await _unitOfWork.Restaurants.UpdateAsync(restaurant);
        await _unitOfWork.SaveChangesAsync();
    }
    }
}
