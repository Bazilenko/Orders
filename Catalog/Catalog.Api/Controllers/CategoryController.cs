using System.Threading.Tasks;
using Catalog.Bll.DTOs.Category;
using Catalog.Bll.Services;
using Catalog.Bll.Services.Interfaces;
using Catalog.Dal.Context;
using Catalog.Dal.Entities;
using Catalog.Dal.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _service;
        public CategoryController(ICategoryService service) {
            _service = service;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryDto>>> GetAll() {
            var entities = await _service.GetAll();
            return Ok(entities);
        }

        [HttpPost]
        public async Task<ActionResult<CategoryDto>> Create([FromBody] CategoryCreateDto dto)
        {
            var entity = await _service.Create(dto);
            return Ok(entity);
        }

        [HttpPut]
        public async Task<ActionResult> Update([FromBody] CategoryUpdateDto dto)
        {
            await _service.Update(dto);
            return NoContent();
        }

        [HttpGet("by-id")]
        public async Task<ActionResult<CategoryDto>> GetById(int id)
        {
            var entity = await _service.GetById(id);
            return Ok(entity);
        }

        [HttpDelete]
        public async Task<ActionResult> Delete(int id)
        {
            await _service.Delete(id);
            return Ok();
        }
    }
}
