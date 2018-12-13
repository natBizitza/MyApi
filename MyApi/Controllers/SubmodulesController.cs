using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyApi.Models;

namespace MyApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubmodulesController : ControllerBase
    {
        private readonly oesiaContext _context;

        public SubmodulesController(oesiaContext context)
        {
            _context = context;
        }

        // GET: api/Submodules
        [HttpGet]
        public IEnumerable<Submodule> GetSubmodule()
        {
            return _context.Submodule;
        }

        // GET: api/Submodules/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSubmodule([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var submodule = await _context.Submodule.FindAsync(id);

            if (submodule == null)
            {
                return NotFound();
            }

            return Ok(submodule);
        }

        // PUT: api/Submodules/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSubmodule([FromRoute] long id, [FromBody] Submodule submodule)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != submodule.Id)
            {
                return BadRequest();
            }

            _context.Entry(submodule).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SubmoduleExists(id))
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

        // POST: api/Submodules
        [HttpPost]
        public async Task<IActionResult> PostSubmodule([FromBody] Submodule submodule)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Submodule.Add(submodule);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSubmodule", new { id = submodule.Id }, submodule);
        }

        // DELETE: api/Submodules/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSubmodule([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var submodule = await _context.Submodule.FindAsync(id);
            if (submodule == null)
            {
                return NotFound();
            }

            _context.Submodule.Remove(submodule);
            await _context.SaveChangesAsync();

            return Ok(submodule);
        }

        private bool SubmoduleExists(long id)
        {
            return _context.Submodule.Any(e => e.Id == id);
        }
    }
}