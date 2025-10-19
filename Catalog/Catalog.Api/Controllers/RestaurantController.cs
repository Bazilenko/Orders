using AutoMapper;
using Catalog.Bll.DTOs.Dish;
using Catalog.Bll.DTOs.Restaurant;
using Catalog.Bll.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RestaurantController : ControllerBase
    {
        private IRestaurantService _service { get; set; }
        public RestaurantController(IRestaurantService service) {
            _service = service;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RestaurantDto>>> GetAllDishes()
        {
            var entities = await _service.GetAllRestaurantsAsync();
            return Ok(entities);
        }

        [HttpPost]
        public async Task<ActionResult<RestaurantDto>> Create([FromBody] RestaurantCreateDto dto)
        {
            var entity = await _service.AddRestaurant(dto);
            return Ok(entity);
        }

        [HttpPut]
        public async Task<ActionResult> Update([FromBody] RestaurantUpdateDto dto)
        {
            await _service.UpdateRestaurant(dto);
            return NoContent();
        }

        [HttpGet("by-id")]
        public async Task<ActionResult<DishDto>> GetById(int id)
        {
            var entity = await _service.GetRestaurantByIdAsync(id);
            return Ok(entity);
        }
    }
}
