using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Catalog.Bll.DTOs.Dish;
using Catalog.Bll.DTOs.Restaurant;
using Catalog.Dal.UOW.Interfaces;

namespace Catalog.Bll.Services.Interfaces
{
    public interface IRestaurantService
    {
        Task<IEnumerable<RestaurantDto?>> GetAllRestaurantsAsync();
        Task<RestaurantDto?> GetRestaurantByIdAsync(int id);
        Task<RestaurantDto> AddRestaurant(RestaurantCreateDto dto);
        Task UpdateRestaurant(RestaurantUpdateDto dto);
        Task<IEnumerable<RestaurantDto>> GetRestaurantByRating(int rating);

    }
}
