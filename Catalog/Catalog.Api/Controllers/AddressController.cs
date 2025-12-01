using Catalog.Bll.DTOs.Address;
using Catalog.Bll.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AddressController : ControllerBase
    {
        private IAddressService _service;

        public AddressController(IAddressService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AddressDto>>> GetAllDishes()
        {
            var entities = await _service.GetAll();
            return Ok(entities);
        }

        [HttpPost]
        public async Task<ActionResult<AddressDto>> Create([FromBody] AddressCreateDto dto)
        {
            var entity = await _service.Create(dto);
            return Ok(entity);
        }

        [HttpPut]
        public async Task<ActionResult> Update([FromBody] AddressUpdateDto dto)
        {
            await _service.Update(dto);
            return NoContent();
        }

        [HttpGet("by-id")]
        public async Task<ActionResult<AddressDto>> GetById(int id)
        {
            var entity = await _service.GetById(id);
            return Ok(entity);
        }

    }
}
