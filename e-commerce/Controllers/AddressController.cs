using e_commerce.Core.Services.Interfaces;
using e_commerce.Services.DTO;
using Microsoft.AspNetCore.Mvc;

namespace e_commerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly IAddressService _service;

        public AddressController(IAddressService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<AddressGetDto>>> Get()
        {
            var addresses = await _service.GetAll();
            return Ok(addresses);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<AddressGetDto>> GetById(int id)
        {
            var result = await _service.GetById(id);
            if (result == null)
                return NotFound(new { message = "Address not found" });

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<AddressGetDto>> Post([FromBody] AddressCreateDto dto)
        {
            var created = await _service.Add(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, [FromBody] AddressUpdateDto dto)
        {
            var updated = await _service.Update(id, dto);
            if (!updated)
                return NotFound(new { message = "Address not found" });

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var deleted = await _service.Delete(id);
            if (!deleted)
                return NotFound(new { message = "Address not found" });

            return Ok(new { message = "Address Deleted" });
        }
    }
}