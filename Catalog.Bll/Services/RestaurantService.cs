using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Catalog.Bll.DTOs.Dish;
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
            var restaurant = _mapper.Map<Restaurant>(dto);
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

        public Task UpdateRestaurant(RestaurantUpdateDto dto)
        {
            throw new NotImplementedException();
        }
    }
}
