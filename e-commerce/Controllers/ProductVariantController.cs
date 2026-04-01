using e_commerce.Core.Services.Interfaces;
using e_commerce.Services.DTO;
using Microsoft.AspNetCore.Mvc;

namespace e_commerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductVariantController : ControllerBase
    {
        private readonly IProductVariantService _service;

        public ProductVariantController(IProductVariantService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<ProductVariantGetDto>>> Get()
            => Ok(await _service.GetAll());

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ProductVariantGetDto>> GetById(int id)
        {
            var result = await _service.GetById(id);
            if (result == null)
                return NotFound(new { message = "ProductVariant not found" });

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<ProductVariantGetDto>> Post([FromBody] ProductVariantCreateDto dto)
        {
            try
            {
                var created = await _service.Add(dto);
                return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, [FromBody] ProductVariantUpdateDto dto)
        {
            try
            {
                var updated = await _service.Update(id, dto);
                if (!updated)
                    return NotFound(new { message = "ProductVariant not found" });

                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var deleted = await _service.Delete(id);
            if (!deleted)
                return NotFound(new { message = "ProductVariant not found" });

            return Ok(new { message = "ProductVariant Deleted" });
        }
    }
}