using e_commerce.Core.Services.Interfaces;
using e_commerce.Services.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace e_commerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _service;

        public CategoryController(ICategoryService service)
        {
            _service = service;
        }
        [HttpGet]
        public async Task<ActionResult<List<CategoryGetDto>>> Get()
            => Ok(await _service.GetAll());

        [HttpGet("{id:int}")]
        public async Task<ActionResult<CategoryGetDto>> GetById(int id)
        {
            var result = await _service.GetById(id);
            if (result == null)
                return NotFound(new { message = "Category not found" });

            return Ok(result);
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<ActionResult<CategoryGetDto>> Post([FromForm] CategoryCreateDto dto)
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
        [Consumes("multipart/form-data")]
        public async Task<ActionResult> Put(int id, [FromForm] CategoryUpdateDto dto)
        {
            try
            {
                var updated = await _service.Update(id, dto);
                if (!updated)
                    return NotFound(new { message = "Category not found" });

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
                return NotFound(new { message = "Category not found" });

            return Ok(new { message = "Category Deleted" });
        }
    }
}