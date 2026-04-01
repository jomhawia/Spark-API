using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using e_commerce.Entites;
using e_commerce.Infrastructure.Persistence;
using e_commerce.Services.DTO;

namespace e_commerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhoneEvaluationsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PhoneEvaluationsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PhoneEvaluation>>> GetPhoneEvaluations()
        {
            return await _context.PhoneEvaluations.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PhoneEvaluation>> GetPhoneEvaluation(int id)
        {
            var phoneEvaluation = await _context.PhoneEvaluations.FindAsync(id);

            if (phoneEvaluation == null)
            {
                return NotFound();
            }

            return phoneEvaluation;
        }

        
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPhoneEvaluation(int id, PhoneEvaluation phoneEvaluation)
        {
            if (id != phoneEvaluation.Id)
            {
                return BadRequest();
            }

            _context.Entry(phoneEvaluation).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PhoneEvaluationExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

       
        [HttpPost]
        public async Task<ActionResult<PhoneEvaluation>> PostPhoneEvaluation(PhoneEvaluation phoneEvaluation)
        {
            _context.PhoneEvaluations.Add(phoneEvaluation);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPhoneEvaluation", new { id = phoneEvaluation.Id }, phoneEvaluation);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePhoneEvaluation(int id)
        {
            var phoneEvaluation = await _context.PhoneEvaluations.FindAsync(id);
            if (phoneEvaluation == null)
            {
                return NotFound();
            }

            _context.PhoneEvaluations.Remove(phoneEvaluation);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet("brand/{brandName}")]
        public async Task<ActionResult<IEnumerable<PhoneEvaluation>>> GetPhoneEvaluationsByBrand(string brandName)
        {
            brandName = brandName.Trim();

            var phoneEvaluations = await _context.PhoneEvaluations
                                                  .Where(p => p.PhoneBrand.Equals(brandName))
                                                  .ToListAsync();

            if (phoneEvaluations == null || phoneEvaluations.Count == 0)
            {
                return NotFound(new { message = "No phones found for this brand." });
            }

            return Ok(phoneEvaluations);
        }


        private bool PhoneEvaluationExists(int id)
        {
            return _context.PhoneEvaluations.Any(e => e.Id == id);
        }

        [HttpPost("evaluation")]
        public async Task<ActionResult<decimal>> GetEvaluation(EvaluationDto dto)
        {
            var phone = _context.PhoneEvaluations.FirstOrDefault(x => x.Name == dto.name);

            if (phone == null)
            {
                return BadRequest("the phone not exist");
            }

            var basePrice = phone.BasePrice;
            decimal modifyPrice = 0; 


            if (dto.PercentageOfUsed)
            {
                
                modifyPrice += basePrice * ((decimal)phone.PercentageOfUsed / 100);  
            }
            else
            {
                return Ok(basePrice);
            }

            if (dto.PercentageOfCamera)
            {
                modifyPrice += basePrice * ((decimal)phone.PercentageOfCamera / 100);  
            }
            if (dto.PercentageOfScreen)
            {
                modifyPrice += basePrice * ((decimal)phone.PercentageOfScreen / 100);  
            }
            if (dto.PercentageOfBackScreen)
            {
                modifyPrice += basePrice * ((decimal)phone.PercentageOfBackScreen / 100);  
            }
            if (dto.PercentageOfBattery)
            {
                modifyPrice += basePrice * ((decimal)phone.PercentageOfBattery / 100); 
            }
            if (dto.PercentageOfOpen)
            {
                modifyPrice += basePrice * ((decimal)phone.PercentageOfOpen / 100); 
            }
            if (dto.PercentageOfOutScrren)
            {
                modifyPrice += basePrice * ((decimal)phone.PercentageOfOutScrren / 1000); 
            }
            if (dto.PercentageOfBody)
            {
                modifyPrice += basePrice * ((decimal)phone.PercentageOfBody / 100);  
            }
            if (dto.PercentageOfBiometrics)
            {
                modifyPrice += basePrice * ((decimal)phone.PercentageOfBiometrics / 100); 
            }


            modifyPrice = basePrice - modifyPrice;

            return Ok(modifyPrice);
        }
    }
}
