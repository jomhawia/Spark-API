using e_commerce.Core.Services.Interfaces;
using e_commerce.Entites;
using e_commerce.Services.DTO;
using Microsoft.AspNetCore.Mvc;

namespace e_commerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;

        public UserController(IUserService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<UserGetDto>>> Get()
            => Ok(await _service.GetAll());

        [HttpGet("{id:int}")]
        public async Task<ActionResult<UserGetDto>> GetById(int id)
        {
            var result = await _service.GetById(id);
            if (result == null)
                return NotFound(new { message = "User not found" });

            return Ok(result);
        }

        [HttpGet("email-exists")]
        public async Task<ActionResult> EmailExists([FromQuery] string email)
        {
            try
            {
                var exists = await _service.EmailExists(email);
                return Ok(new { email, exists });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("by-email")]
        public async Task<ActionResult<UserGetDto>> GetByEmail([FromQuery] string email)
        {
            try
            {
                var user = await _service.GetByEmail(email);
                if (user == null)
                    return NotFound(new { message = "User not found" });

                // رجّع DTO (بدون Password)
                var dto = new UserGetDto
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,
                    CartId = user.CartId,
                    CreatedAt = user.CreatedAt,
                    UpdatedAt = user.UpdatedAt
                };

                return Ok(dto);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<UserGetDto>> Post([FromBody] UserCreateDto dto)
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
        public async Task<ActionResult> Put(int id, [FromBody] UserUpdateDto dto)
        {
            try
            {
                var updated = await _service.Update(id, dto);
                if (!updated)
                    return NotFound(new { message = "User not found" });

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
                return NotFound(new { message = "User not found" });

            return Ok(new { message = "User Deleted" });
        }
    }
}