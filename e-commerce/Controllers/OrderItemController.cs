using e_commerce.Core.Services.Interfaces;
using e_commerce.Services.DTO;
using Microsoft.AspNetCore.Mvc;

namespace e_commerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderItemController : ControllerBase
    {
        private readonly IOrderItemService _service;

        public OrderItemController(IOrderItemService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<OrderItemGetDto>>> Get()
            => Ok(await _service.GetAll());

        [HttpGet("{id:int}")]
        public async Task<ActionResult<OrderItemGetDto>> GetById(int id)
        {
            var result = await _service.GetById(id);
            if (result == null)
                return NotFound(new { message = "OrderItem not found" });

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<OrderItemGetDto>> Post([FromBody] OrderItemCreateDto dto)
        {
            try
            {
                var created = await _service.Add(dto);
                return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, [FromBody] OrderItemUpdateDto dto)
        {
            try
            {
                var updated = await _service.Update(id, dto);
                if (!updated)
                    return NotFound(new { message = "OrderItem not found" });

                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
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
                return NotFound(new { message = "OrderItem not found" });

            return Ok(new { message = "OrderItem Deleted" });
        }
    }
}