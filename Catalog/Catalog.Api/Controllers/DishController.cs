using Catalog.Bll.DTOs.Category;
using Catalog.Bll.DTOs.Dish;
using Catalog.Bll.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DishController : ControllerBase
    {
        private IDishService _service {  get; set; }
        public DishController(IDishService service) {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DishDto>>> GetAllDishes()
        {
            var entities = await _service.GetAllDishesAsync();
            return Ok(entities);
        }

        [HttpPost]
        public async Task<ActionResult<DishDto>> Create([FromBody] DishCreateDto dto)
        {
            var entity = await _service.AddDish(dto);
            return Ok(entity);
        }

        [HttpPut]
        public async Task<ActionResult> Update([FromBody] DishUpdateDto dto)
        {
            await _service.UpdateDish(dto);
            return NoContent();
        }

        [HttpGet("by-id")]
        public async Task<ActionResult<DishDto>> GetById(int id)
        {
            var entity = await _service.GetDishByIdAsync(id);
            return Ok(entity);
        }
    }
}
