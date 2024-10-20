using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MotorbikeStore.Models;

namespace MotorbikeStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MotorbikesApiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public MotorbikesApiController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/MotorbikesApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Motorbike>>> GetMotorbikes()
        {
          if (_context.Motorbikes == null)
          {
              return NotFound();
          }
            return await _context.Motorbikes.ToListAsync();
        }

        // GET: api/MotorbikesApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Motorbike>> GetMotorbike(int id)
        {
          if (_context.Motorbikes == null)
          {
              return NotFound();
          }
            var motorbike = await _context.Motorbikes.FindAsync(id);

            if (motorbike == null)
            {
                return NotFound();
            }

            return motorbike;
        }

        // PUT: api/MotorbikesApi/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMotorbike(int id, Motorbike motorbike)
        {
            if (id != motorbike.MotorbikeId)
            {
                return BadRequest();
            }

            _context.Entry(motorbike).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MotorbikeExists(id))
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

        // POST: api/MotorbikesApi
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Motorbike>> PostMotorbike(Motorbike motorbike)
        {
          if (_context.Motorbikes == null)
          {
              return Problem("Entity set 'ApplicationDbContext.Motorbikes'  is null.");
          }
            _context.Motorbikes.Add(motorbike);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMotorbike", new { id = motorbike.MotorbikeId }, motorbike);
        }

        // DELETE: api/MotorbikesApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMotorbike(int id)
        {
            if (_context.Motorbikes == null)
            {
                return NotFound();
            }
            var motorbike = await _context.Motorbikes.FindAsync(id);
            if (motorbike == null)
            {
                return NotFound();
            }

            _context.Motorbikes.Remove(motorbike);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MotorbikeExists(int id)
        {
            return (_context.Motorbikes?.Any(e => e.MotorbikeId == id)).GetValueOrDefault();
        }
    }
}
