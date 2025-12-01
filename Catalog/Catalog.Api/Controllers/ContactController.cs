using Catalog.Bll.DTOs.Contact;
using Catalog.Bll.DTOs.Dish;
using Catalog.Bll.DTOs.Restaurant;
using Catalog.Bll.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContactController : ControllerBase
    {
        private IContactService _service;

        public ContactController(IContactService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ContactDto>>> GetAllDishes()
        {
            var entities = await _service.GetAll();
            return Ok(entities);
        }

        [HttpPost]
        public async Task<ActionResult<ContactDto>> Create([FromBody] ContactCreateDto dto)
        {
            var entity = await _service.Create(dto);
            return Ok(entity);
        }

        [HttpPut]
        public async Task<ActionResult> Update([FromBody] ContactUpdateDto dto)
        {
            await _service.Update(dto);
            return NoContent();
        }

        [HttpGet("by-id")]
        public async Task<ActionResult<ContactDto>> GetById(int id)
        {
            var entity = await _service.GetById(id);
            return Ok(entity);
        }

    }
}
